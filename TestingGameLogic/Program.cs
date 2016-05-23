using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game;

namespace TestingGameLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            GameLogic logic = new GameLogic();
            GameOptions options = new GameOptions(8, 8, "");

            Result result = logic.Setup(options);
            Cell[,] matrix = result.Matrix;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if(matrix[row, col].Player == null)
                        Console.Write("-  ");

                    else
                    {
                        Console.Write(matrix[row, col].Player.Tag + "  ");
                    }
                }

                Console.WriteLine("\n");
            }

            Console.WriteLine(result.Description);
        }
    }
}
