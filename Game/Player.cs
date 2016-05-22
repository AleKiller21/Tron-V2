using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player
    {
        public string Tag { get; set; }

        public string Color { get; set; }

        public bool IsAlive { get; set; }

        public Player(string name, string color)
        {
            Tag = name;
            Color = color;
            IsAlive = true;
        }
    }
}
