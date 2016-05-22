﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Player
    {
        public string Tag { get; set; }

        public bool IsAlive { get; set; }

        public Position Position { get; set; }

        public Player(string name, int row, int col)
        {
            Tag = name;
            IsAlive = true;
            Position = new Position(row, col);
        }
    }
}
