using BattleshipSimulation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipSimulation.Objects.Boards
{
    public class GameBoard
    {
        public List<Cell> Cells { get; set; }

        public GameBoard()
        {
            Cells = new List<Cell>();
            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    Cells.Add(new Cell(i, j));
                }
            }
        }
    }
}
