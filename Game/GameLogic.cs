using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameLogic
    {
        public Player[,] Matrix { get; set; }

        public List<Command> Commands { get; set; }

        public string Result { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public GameLogic()
        {
            Commands = new List<Command>();
        }

        public void Setup(GameOptions gameData)
        {
            SetMatrix(gameData.Rows, gameData.Columns);
            //TODO: Mandarle el path al Parser
            //TODO: Obtener la lista de comandos devuelta por el Parser
            ExecuteGame();
        }

        internal void SetMatrix(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            Matrix = new Player[Rows, Columns];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Matrix[row, col] = null;
                }
            }
        }

        private void ExecuteGame()
        {
            
        }
    }
}
