using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Cell
    {
        public Player Player { get; set; }

        public bool CellActive { get; set; }

        public Cell()
        {
            Player = null;
            CellActive = false;
        }

        public override string ToString()
        {
            if (Player == null)
                return "";

            return Player.Tag;
        }
    }
}
