using System.Diagnostics;
using ConwaysGoL.Application.Services;
using ConwaysGoL.MauiAppProject.Services;
using Cell = ConwaysGoL.Application.Models.Cell;

namespace ConwaysGoL.MauiAppProject
{
    public partial class MainPage : ContentPage
    {
        private readonly CGoLBackgroundService _cGoLBackgroundService;
        private readonly ThemeChangedService _themeChangedService;
        private readonly SimulationCanvas _simulationCanvas;
        private bool _waitUntilDispatch;

        private PointF _lastSimCanvasTouch;
        private PointF _lastSimCanvasOrigion;
        private bool _touchGotDragged;

        public MainPage(
            CGoLBackgroundService cGoLBackgroundService, 
            ThemeChangedService themeChangedService)
        {
            _cGoLBackgroundService = cGoLBackgroundService;
            SetInitialState();

            _themeChangedService = themeChangedService;
            _simulationCanvas = new SimulationCanvas(cGoLBackgroundService, themeChangedService);

            InitializeComponent();

            SimulationCanvas.Drawable = _simulationCanvas;
            _themeChangedService.SetThemeChangedListener(SimulationCanvas, _ => SimulationCanvas.Invalidate());

            var simulationStatus = _cGoLBackgroundService.Status;
            IterationLabel.Text = string.Format(StringTemplates.IterationLabelText, simulationStatus.Iteration);
            AliveCellsLabel.Text = string.Format(StringTemplates.AliveCellsLabelText, simulationStatus.AliveCellCount);
            SpeedLabel.Text = string.Format(StringTemplates.NumericSpeedLabelText, _cGoLBackgroundService.Speed);

            SpeedSlider.Value = Math.Log2(_cGoLBackgroundService.Speed);
            ZoomSlider.Value = (_simulationCanvas.GridZoom + 10) / 6;

            _cGoLBackgroundService.UpdateLoopCallback = TryDispatch;
        }

        private void SetInitialState()
        {
            _cGoLBackgroundService.AddCellsFromText(new Cell(0, 0), File.ReadAllText("InitialState.txt"));
        }

        private void TryDispatch()
        {
            if (!_waitUntilDispatch)
            {
                _waitUntilDispatch = true;
                Dispatcher.Dispatch(() =>
                {
                    var simulationStatus = _cGoLBackgroundService.Status;
                    IterationLabel.Text = string.Format(StringTemplates.IterationLabelText, simulationStatus.Iteration);
                    AliveCellsLabel.Text = string.Format(StringTemplates.AliveCellsLabelText, simulationStatus.AliveCellCount);

                    _waitUntilDispatch = false;
                });
                SimulationCanvas.Invalidate();
            }
        }

        private void OnToggleSimulation(object sender, EventArgs args)
        {
            if (_cGoLBackgroundService.IsRunning)
            {
                StopSimulation();
                TryDispatch();
            }
            else
            {
                StartSimulation();
            }
        }

        private void StartSimulation()
        {
            _cGoLBackgroundService.Start();
            StartBtn.Text = "Stop";
            _themeChangedService.SetThemeChangedListener(StartBtn, appTheme =>
            {
                StartBtn.BackgroundColor = appTheme == AppTheme.Dark ? Colors.IndianRed : Colors.Red;
            });
            _themeChangedService.TryInvokeListener(StartBtn);
        }

        private void StopSimulation()
        {
            _cGoLBackgroundService.Stop();
            StartBtn.Text = "Start";
            _themeChangedService.SetThemeChangedListener(StartBtn, appTheme =>
            {
                StartBtn.BackgroundColor = appTheme == AppTheme.Dark ? Colors.LightGreen : Colors.Green;
            });
            _themeChangedService.TryInvokeListener(StartBtn);
        }

        private void OnNextIteration(object sender, EventArgs args)
        {
            _cGoLBackgroundService.Next();
            TryDispatch();
        }

        private void OnResetSimulation(object sender, EventArgs args)
        {
            StopSimulation();
            _cGoLBackgroundService.Reset();
            SetInitialState();
            _simulationCanvas.GridOrigin = new PointF(0, 0);
            TryDispatch();
        }

        private void OnSpeedChanged(object sender, ValueChangedEventArgs args)
        {
            const double step = 1;

            double roundedSliderValue = Math.Round(args.NewValue / step) * step;
            double speed = Math.Pow(2, roundedSliderValue);
            _cGoLBackgroundService.Speed = speed;
            SpeedLabel.Text = string.Format(StringTemplates.NumericSpeedLabelText, speed);
        }

        private void OnDragSimulationCanvas(object sender, TouchEventArgs args)
        {
            if (args.Touches.Length == 1)
            {
                _simulationCanvas.GridOrigin = new PointF(
                    _lastSimCanvasOrigion.X + (args.Touches[0].X - _lastSimCanvasTouch.X) / _simulationCanvas.GridScale,
                    _lastSimCanvasOrigion.Y + (args.Touches[0].Y - _lastSimCanvasTouch.Y) / _simulationCanvas.GridScale);

                SimulationCanvas.Invalidate();
                _touchGotDragged = true;
            }
        }

        private void OnClickSimulationCanvas(object sender, TouchEventArgs args)
        {
            if (args.Touches.Length == 1)
            {
                _lastSimCanvasTouch = args.Touches[0];
                _lastSimCanvasOrigion = _simulationCanvas.GridOrigin;
                _touchGotDragged = false;
            }
        }

        private void OnClickReleaseSimulationCanvas(object sender, TouchEventArgs args)
        {
            if (!_touchGotDragged && args.Touches.Length == 1)
            {
                _cGoLBackgroundService.ToggleCell(_simulationCanvas.GetCellFromCoords(args.Touches[0], SimulationCanvas.Bounds));
                SimulationCanvas.Invalidate();
            }
        }

        private void OnZoomSimulationCanvas(object sender, ValueChangedEventArgs args)
        {
            _simulationCanvas.GridZoom = (float)args.NewValue * 6 - 10;
            SimulationCanvas?.Invalidate();
        }
    }
}
