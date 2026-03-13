
using System.Runtime.CompilerServices;
using ConwaysGoL.Application.Models;
using ConwaysGoL.Application.Services;


var service = new CGoLBackgroundService();
service.AddCellsFromText(new Cell(0, 0), """
 xx
xx 
 x 
""");

service.Speed = 512;
service.Start();
Console.WriteLine(service.Status);
for (int i = 0; i < 200; i++)
{
    Thread.Sleep(100);
    Console.WriteLine(service.Status);
}
service.Stop();