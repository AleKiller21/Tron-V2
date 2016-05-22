using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameOptions
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string Path { get; set; }

        public GameOptions(int rows, int columns, string path)
        {
            Rows = rows;
            Columns = columns;
            Path = path;
        }
    }
}
