using BattleshipSimulation.Objects;
using BattleshipSimulation.Objects.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start simulation");
            Console.ReadKey();

            Game game = new Game();
            game.PlayToEnd();

            Console.ReadLine();
           
        }
    }
}
