using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipSimulation.Objects.Ships
{
    public class Carrier : Ship
    {
        public Carrier()
        {
            Name = "Aircraft Carrier";
            Width = 5;
            CellStatus = CellStatus.Carrier;
        }
    }
}
