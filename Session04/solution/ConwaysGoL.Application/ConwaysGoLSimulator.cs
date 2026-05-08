using ConwaysGoL.Application.Models;
using ConwaysGoL.Application.Services;

namespace ConwaysGoL.Application
{
    public class ConwaysGoLSimulator
    {
        private HashSet<Cell> _aliveCells;
        private int _iteration;

        public ConwaysGoLSimulator()
        {
            _aliveCells = [];
            _iteration = 0;
        }

        public Cell[] AliveCells => _aliveCells.ToArray();

        public int AliveCellCount => _aliveCells.Count;

        public int Iteration => _iteration;

        public void AddCellsFromText(Cell topLeftCorner, string text)
        {
            int x = topLeftCorner.X;
            int y = topLeftCorner.Y;
            foreach (char c in text)
            {
                if (c == 'x')
                {
                    _aliveCells.Add(new Cell(x, y));
                }
                if (c == '\n')
                {
                    y++;
                    x = topLeftCorner.X;
                }
                else
                {
                    x++;
                }
            }
        }

        public void ToggleCell(Cell cell)
        {
            if (!_aliveCells.Add(cell))
                _aliveCells.Remove(cell);
        }

        public void Next()
        {
            HashSet<Cell> newAliveCells = new(_aliveCells.Capacity);
            foreach (Cell aliveCell in _aliveCells)
            {
                int aliveNeighbors = 0;
                List<Cell> deadNeighbors = [];
                foreach (var neighbor in aliveCell.GetNeighbors())
                {
                    if (_aliveCells.Contains(neighbor))
                        aliveNeighbors++;
                    else
                        deadNeighbors.Add(neighbor);
                }

                if (aliveNeighbors == 2 || aliveNeighbors == 3)
                    newAliveCells.Add(aliveCell);

                foreach (var deadNeighbor in deadNeighbors)
                    if (deadNeighbor.GetNeighbors().Where(_aliveCells.Contains).Count() == 3)
                        newAliveCells.Add(deadNeighbor);
            }

            _aliveCells = newAliveCells;
            _iteration++;
        }
    }
}
