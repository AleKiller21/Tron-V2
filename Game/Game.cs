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
            for (int row = 0; row < gameData.Rows; row++)
            {
                for (int col = 0; col < gameData.Columns; col++)
                {
                    Matrix[row][col] = null;
                }
            }
        }
    }
}
