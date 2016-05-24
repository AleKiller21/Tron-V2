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

        public static Position CalculatePosition(Position pos, CommandParser.PlayerMoves direction)
        {
            int row = pos.Row;
            int col = pos.Column;

            switch (direction)
            {
                case CommandParser.PlayerMoves.Up:
                    return new Position(row - 1, col);

                case CommandParser.PlayerMoves.Right:
                    return new Position(row, col + 1);

                case CommandParser.PlayerMoves.Down:
                    return new Position(row + 1, col);

                case CommandParser.PlayerMoves.Left:
                    return new Position(row, col - 1);

                default:
                    return null;
            }
        }
    }
}
