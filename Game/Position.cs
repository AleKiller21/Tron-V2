using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Position
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public static Position CalculatePosition(Position pos, string direction)
        {
            int row = pos.Row;
            int col = pos.Column;

            switch (direction)
            {
                case "arriba":
                    return new Position(row - 1, col);

                case "derecha":
                    return new Position(row, col + 1);

                case "abajo":
                    return new Position(row + 1, col);

                case "izquierda":
                    return new Position(row, col - 1);

                default:
                    return null;
            }
        }
    }
}
