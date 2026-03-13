using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGoL.Application.Models
{
    public readonly record struct SimulationStatus(
        int Iteration, 
        int AliveCellCount)
    {
    }
}
