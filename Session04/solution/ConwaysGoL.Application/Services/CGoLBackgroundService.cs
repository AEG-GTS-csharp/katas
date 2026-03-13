using System.Diagnostics;
using ConwaysGoL.Application.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ConwaysGoL.Application.Services
{
    public class CGoLBackgroundService
    {
        private const double MaxSpeedForTaskDelay = 64;

        private ConwaysGoLSimulator _simulator;
        private double _speed;

        private Task _serviceTask;
        private CancellationTokenSource? _stoppingCts;
        private readonly Lock _serviceLock;

        public CGoLBackgroundService()
        {
            _simulator = new ConwaysGoLSimulator();
            _speed = 1;
            _serviceTask = Task.CompletedTask;
            _serviceLock = new Lock();
        }

        public Cell[] AliveCells
        {
            get
            {
                lock (_serviceLock)
                {
                    return _simulator.AliveCells;
                }
            }
        }

        public Cell[]? TryGetAliveCellsNonBlocking()
        {
            if (_serviceLock.TryEnter())
            {
                try
                {
                    return _simulator.AliveCells;
                }
                finally
                {
                    _serviceLock.Exit();
                }
            }
            return null;
        }

        public SimulationStatus Status
        {
            get
            {
                lock (_serviceLock)
                {
                    return new(_simulator.Iteration, _simulator.AliveCellCount);
                }
            }
        }

        public double Speed 
        { 
            get 
            {
                lock (_serviceLock)
                {
                    return _speed;
                }
            } 
            set
            {
                lock (_serviceLock)
                {
                    _speed = value;
                }
            }
        }

        public Action? UpdateLoopCallback { get; set; }

        public bool IsRunning => !_serviceTask.IsCompleted;

        public void AddCellsFromText(Cell topLeftCorner, string text)
        {
            lock (_serviceLock)
            {
                _simulator.AddCellsFromText(topLeftCorner, text);
            }
        }

        public void ToggleCell(Cell cell)
        {
            lock (_serviceLock)
            {
                _simulator.ToggleCell(cell);
            }
        }

        public void Next()
        {
            lock (_serviceLock)
            {
                _simulator.Next();
            }
        }

        private async Task Service(CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();
            int delaysSkipped = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                var speed = Speed;
                int skipDelayCount = (int)(speed / MaxSpeedForTaskDelay);
                speed /= skipDelayCount + 1;

                Next();

                if (delaysSkipped >= skipDelayCount)
                {
                    stopwatch.Stop();
                    UpdateLoopCallback?.Invoke();
                    var delay = TimeSpan.FromSeconds(1d / speed) - stopwatch.Elapsed;
                    if (delay.Ticks > 0)
                    {
                        try
                        {
                            await Task.Delay(delay, cancellationToken);
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                    }
                    delaysSkipped = 0;
                    stopwatch.Restart();
                }
                else
                {
                    delaysSkipped++;
                }
            }
        }

        public void Start()
        {
            Stop();
            _stoppingCts = new CancellationTokenSource();

            _serviceTask = Task.Run(() => Service(_stoppingCts.Token));
        }

        public void Stop()
        {
            _stoppingCts?.Cancel();
        }

        public void Reset()
        {
            lock (_serviceLock)
            {
                _simulator = new ConwaysGoLSimulator();
            }
        }
    }
}
