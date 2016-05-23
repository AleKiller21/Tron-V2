using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ValidationProperties
    {
        public List<Player> Players { get; set; }

        public Cell[,] Matrix { get; set; }

        public Player CurrentPlayer { get; set; }

        public PlayerMoves Direction { get; set; }

        public int MatrixRows { get; set; }

        public int MatrixColumns { get; set; }

        public ValidationProperties(List<Player> players, Cell[,] matrix, Player currentPlayer, PlayerMoves direction, int matrixRows, int matrixColumns)
        {
            Players = players;
            Matrix = matrix;
            CurrentPlayer = currentPlayer;
            Direction = direction;
            MatrixRows = matrixRows;
            MatrixColumns = matrixColumns;
        }
    }
}
