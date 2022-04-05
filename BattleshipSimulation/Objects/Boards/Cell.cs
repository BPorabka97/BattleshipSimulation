using BattleshipSimulation.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipSimulation.Objects.Boards
{
    public class Cell
    {
        public CellStatus CellStatus { get; set; }
        public Coordinates Coordinates { get; set; }

        public Cell(int row, int column)
        {
            Coordinates = new Coordinates(row, column);
            CellStatus = CellStatus.Empty;
        }

        public string Status
        {
            get
            {
                return CellStatus.GetAttributeOfType<DescriptionAttribute>().Description;
            }
        }

        public bool IsOccupied
        {
            get
            {
                return CellStatus == CellStatus.Battleship
                    || CellStatus == CellStatus.Destroyer
                    || CellStatus == CellStatus.Cruiser
                    || CellStatus == CellStatus.Submarine
                    || CellStatus == CellStatus.Carrier;
            }
        }

        public bool IsRandomAvailable
        {
            get
            {
                return (Coordinates.Row % 2 == 0 && Coordinates.Column % 2 == 0)
                    || (Coordinates.Row % 2 == 1 && Coordinates.Column % 2 == 1);
            }
        }
    }
}
