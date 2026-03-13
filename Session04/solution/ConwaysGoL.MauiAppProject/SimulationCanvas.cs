using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ConwaysGoL.Application.Services;
using ConwaysGoL.MauiAppProject.Services;
using Cell = ConwaysGoL.Application.Models.Cell;

namespace ConwaysGoL.MauiAppProject
{
    public class SimulationCanvas : IDrawable
    {
        private readonly CGoLBackgroundService _cGoLBackgroundService;
        private readonly ThemeChangedService _themeChangedService;

        private Cell[] _aliveCellsCache;

        public SimulationCanvas(
            CGoLBackgroundService cGoLBackgroundService, 
            ThemeChangedService themeChangedService)
        {
            _cGoLBackgroundService = cGoLBackgroundService;
            _themeChangedService = themeChangedService;
            _aliveCellsCache = [];
            GridZoom = -6;
        }

        public PointF GridOrigin { get; set; }

        public float GridZoom { get; set; }

        public float GridScale => float.Pow(2, GridZoom);

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            _aliveCellsCache = _cGoLBackgroundService.TryGetAliveCellsNonBlocking() ?? _aliveCellsCache;

            float gridScale = GridScale;
            float aspectRatio = dirtyRect.Width / dirtyRect.Height;

            float translateX = dirtyRect.Center.X + GridOrigin.X * gridScale;
            float translateY = dirtyRect.Center.Y + GridOrigin.Y * gridScale;

            int gridCellSizeFactor = 1 << (-(int)GridZoom / 4 * 4 - 4);

            int fromX = (int)(-1 / gridScale / 2 - GridOrigin.X / dirtyRect.Width) / gridCellSizeFactor - 1;
            int toX = (int)(1 / gridScale / 2 - GridOrigin.X / dirtyRect.Width) / gridCellSizeFactor + 1;

            int fromY = (int)(-1 / gridScale / 2 / aspectRatio - GridOrigin.Y / dirtyRect.Width) / gridCellSizeFactor - 1;
            int toY = (int)(1 / gridScale / 2 / aspectRatio - GridOrigin.Y / dirtyRect.Width) / gridCellSizeFactor + 1;

            float cellSize = dirtyRect.Width * gridScale;
            float gridCellSize = cellSize * gridCellSizeFactor;

            canvas.StrokeColor = new Color(_themeChangedService.CurrentTheme == AppTheme.Dark ? 0.2f : 0.85f);
            for (int x = fromX; x < toX; x++)
            {
                for (int y = fromY; y < toY; y++)
                {
                    canvas.DrawRectangle(new Rect(
                        translateX + x * gridCellSize,
                        translateY + y * gridCellSize,
                        gridCellSize, gridCellSize));
                }
            }

            canvas.ResetState();

            canvas.FillColor = new Color(_themeChangedService.CurrentTheme == AppTheme.Dark ? 0.7f : 0.4f);
            foreach (var (x, y) in _aliveCellsCache)
            {
                canvas.FillRectangle(new Rect(
                    translateX + x * cellSize,
                    translateY + y * cellSize,
                    cellSize, cellSize));
            }
        }
    }
}
