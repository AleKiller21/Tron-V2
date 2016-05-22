using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Result
    {
        public Cell[,] Matrix { get; set; }

        public string Description { get; set; }

        public Result(Cell[,] matrix, string description)
        {
            Matrix = matrix;
            Description = description;
        }
    }
}
