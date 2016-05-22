using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public Player(string name, string color)
        {
            Name = name;
            Color = color;
        }
    }
}
