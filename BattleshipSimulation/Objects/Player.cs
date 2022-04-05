using BattleshipSimulation.Extensions;
using BattleshipSimulation.Objects.Boards;
using BattleshipSimulation.Objects.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipSimulation.Objects
{
    public class Player
    {
        public string Name { get; set; }
        public GameBoard GameBoard { get; set; }
        public FiringBoard FiringBoard { get; set; }
        public List<Ship> Ships { get; set; }
        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }

        public Player(string name)
        {
            Name = name;
            Ships = new List<Ship>()
            {
                new Destroyer(),
                new Submarine(),
                new Cruiser(),
                new Battleship(),
                new Carrier()
            };
            GameBoard = new GameBoard();
            FiringBoard = new FiringBoard();
        }

        public void OutputBoards()
        {
            Console.WriteLine("");
            Console.WriteLine(Name);
            Console.WriteLine("Own Board:                          Firing Board:");
            for(int row = 0; row <= 9; row++)
            {
                for(int ownColumn = 0; ownColumn <= 9; ownColumn++)
                {
                    var currCell = GameBoard.Cells.At(row, ownColumn);
                    ChangeConsoleColour(currCell);
                    Console.Write(currCell.Status + " ");
                }
                Console.Write("                ");
                for (int firingColumn = 0; firingColumn <= 9; firingColumn++)
                {
                    var currCell = FiringBoard.Cells.At(row, firingColumn);
                    ChangeConsoleColour(currCell);
                    Console.Write(currCell.Status + " ");
                }
                Console.WriteLine("");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

        private void ChangeConsoleColour(Cell currCell)
        {
            switch (currCell.Status)
            {
                case "O":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                case "X":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        public void ShipPlacement()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {
                bool isOpen = true;
                while (isOpen)
                {
                    var startcolumn = rand.Next(0,9);
                    var startrow = rand.Next(0, 9);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2; 

                    List<int> panelNumbers = new List<int>();

                    if (CheckBoardBoundaries(orientation, ref endcolumn, ref endrow, ship))
                    {
                        isOpen = true;
                        continue;
                    }

                    var affectedCells = GameBoard.Cells.Range(startrow, startcolumn, endrow, endcolumn);
                    if(affectedCells.Any(x=>x.IsOccupied))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach(var cell in affectedCells)
                    {
                        cell.CellStatus = ship.CellStatus;
                    }
                    isOpen = false;
                }
            }
        }

        public bool CheckBoardBoundaries(int orientation, ref int endcolumn, ref int endrow, Ship ship)
        {
            if (orientation == 0)
            {
                for (int i = 1; i < ship.Width; i++)
                {
                    endrow++;
                }
            }
            else
            {
                for (int i = 1; i < ship.Width; i++)
                {
                    endcolumn++;
                }
            }

            if (endrow > 9 || endcolumn > 9)
            {
                return true;
            }

            return false;
        }
        public Coordinates Shoot()
        {
            var hitNeighbors = FiringBoard.GetHitNeighbors();
            Coordinates target;
            if (hitNeighbors.Any())
            {
                target = SearchingShot();
            }
            else
            {
                target = RandomShot();
            }
            Console.WriteLine(Name + " says: \"Firing shot at " + target.Row.ToString() + ", " + target.Column.ToString() + "\"");
            return target;
        }

        public ShotResult ProcessShot(Coordinates target)
        {
            var cell = GameBoard.Cells.At(target.Row, target.Column);
            if(!cell.IsOccupied)
            {
                Console.WriteLine(Name + " says: \"Miss!\"");
                return ShotResult.Miss;
            }
            var ship = Ships.First(x => x.CellStatus == cell.CellStatus);
            ship.Hits++;
            Console.WriteLine(Name + " says: \"Hit!\"");
            if (ship.IsSunk)
            {
                Console.WriteLine(Name + " says: \"You sunk my " + ship.Name + "!\"");
            }
            return ShotResult.Hit;
        }

        public void ProcessShotResult(Coordinates target, ShotResult result)
        {
            var cell = FiringBoard.Cells.At(target.Row, target.Column);
            switch(result)
            {
                case ShotResult.Hit:
                    cell.CellStatus = CellStatus.Hit;
                    break;

                default:
                    cell.CellStatus = CellStatus.Miss;
                    break;
            }
        }
        private Coordinates RandomShot()
        {
            var availablePanels = FiringBoard.GetOpenRandomPanels();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var panelID = rand.Next(availablePanels.Count);
            return availablePanels[panelID];
        }

        private Coordinates SearchingShot()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            var hitNeighbors = FiringBoard.GetHitNeighbors();
            var neighborID = rand.Next(hitNeighbors.Count);
            return hitNeighbors[neighborID];
        }
    }
}
