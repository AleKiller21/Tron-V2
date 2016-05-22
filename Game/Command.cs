using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Command
    {
        public string Name { get; set; }
        public string Direction { get; set; }

        public Command(string name, string direction)
        {
            Name = name;
            Direction = direction;
        }
    }
}
