using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleshipSimulation.Objects.Games
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Game()
        {
            Player1 = new Player("PC1");
            Player2 = new Player("PC2");

            Player1.ShipPlacement();
            Player2.ShipPlacement();

            Player1.OutputBoards();
            Player2.OutputBoards();
        }

        public void PlayRound()
        {
            var coordinates = Player1.Shoot();
            var result = Player2.ProcessShot(coordinates);
            Player1.ProcessShotResult(coordinates, result);

            if (!Player2.HasLost) 
            {
                coordinates = Player2.Shoot();
                result = Player1.ProcessShot(coordinates);
                Player2.ProcessShotResult(coordinates, result);
            }
        }

        public void PlayToEnd()
        {
            int numOfRounds = 1;
            while (!Player1.HasLost && !Player2.HasLost)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"======================== TURN {numOfRounds++} ========================");
                Console.WriteLine("");
                PlayRound();
                Player1.OutputBoards();
                Player2.OutputBoards();
            }


            if (Player1.HasLost)
            {
                Console.WriteLine(Player2.Name + " has won the game!");
            }
            else if (Player2.HasLost)
            {
                Console.WriteLine(Player1.Name + " has won the game!");
            }
        }
    }
}
