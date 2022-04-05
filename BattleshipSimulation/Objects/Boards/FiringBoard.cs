using BattleshipSimulation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipSimulation.Objects.Boards
{

    public class FiringBoard : GameBoard
    {
        public List<Coordinates> GetOpenRandomPanels()
        {
            return Cells.Where(x => x.CellStatus == CellStatus.Empty && x.IsRandomAvailable).Select(x=>x.Coordinates).ToList();
        }

        public List<Coordinates> GetHitNeighbors()
        {
            List<Cell> cells = new List<Cell>();
            var hits = Cells.Where(x => x.CellStatus == CellStatus.Hit);
            foreach(var hit in hits)
            {
                cells.AddRange(GetNeighbors(hit.Coordinates).ToList());
            }
            return cells.Distinct().Where(x => x.CellStatus == CellStatus.Empty).Select(x => x.Coordinates).ToList();
        }

        public List<Cell> GetNeighbors(Coordinates coordinates)
        {
            int row = coordinates.Row;
            int column = coordinates.Column;
            List<Cell> cells = new List<Cell>();
            if (column > 0)
            {
                cells.Add(Cells.At(row, column - 1));
            }
            if (row > 0)
            {
                cells.Add(Cells.At(row - 1, column));
            }
            if (row < 9)
            {
                cells.Add(Cells.At(row + 1, column));
            }
            if (column < 9)
            {
                cells.Add(Cells.At(row, column + 1));
            }
            return cells;
        }
    }
}
