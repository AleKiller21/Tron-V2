using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Command
    {
        public string Tag { get; set; }

        public PlayerMoves Direction { get; set; }

        public Command(string name, PlayerMoves direction)
        {
            Tag = name;
            Direction = direction;
        }
    }
}
