using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGoL.Application.Models
{
    public readonly record struct Cell(int X, int Y)
    {
        public Cell[] GetNeighbors()
        {
            return [
                this with { X = X - 1, Y = Y + 1 },
                this with { Y = Y + 1 },
                this with { X = X + 1, Y = Y + 1 },
                this with { X = X - 1 },
                this with { X = X + 1 },
                this with { X = X - 1, Y = Y - 1 },
                this with { Y = Y - 1 },
                this with { X = X + 1, Y = Y - 1 }
                ];
        }
    }
}
