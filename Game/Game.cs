using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Game
    {
        public Player[][] Matrix { get; set; }
        public string Result { get; set; }
        public void Setup(GameOptions gameData)
        {
            SetMatrix(gameData.Rows, gameData.Columns);
            //TODO: Mandarle el path al Parser
            //TODO: Obtener la lista de comandos devuelta por el Parser
        }

        private void SetMatrix(int rows, int cols)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Matrix[row][col] = null;
                }
            }
        }
    }
}
