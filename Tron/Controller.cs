using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Game;
using CommandParser;

namespace Tron
{
    public class Controller
    {
        private string _path;
        private int _rowSize = 0;
        private int _columnSize = 0;
        public void StartGame()
        {
            GameLoop();
        }

        private string AskTheUserFor(string message)
        {
            Console.WriteLine(message);
            var readLine = Console.ReadLine();
            return readLine;
        }

        private void Menu()
        {
            Console.WriteLine("\n\n\t\t\t----------Tron V2 ----------");
            Console.WriteLine("\t\t\t\t 1) Jugar");
            Console.WriteLine("\t\t\t\t 2) Salir");
            Console.WriteLine("\t\t\t-----------------------------");
        }

        private void GameLoop()
        {
            int option = 0;
            while (option !=2)
            {
                Menu();
                option = int.Parse(AskTheUserFor("Seleecione una opcion"));
                switch (option)
                {
                    case 1:
                        Play();
                        break;
                    case 2:
                        option = 2;
                        break;
                    default:
                        Console.WriteLine("Opcion invalida");
                        ClearScrean();
                        break;
                }
                    
            }
        }

        private void Play()
        {
            _path = AskTheUserFor("Ingresa la direccion del archivo: ");
            _rowSize = int.Parse(AskTheUserFor("Ingrese el numero de filas en el tablero: "));
            _columnSize = int.Parse(AskTheUserFor("Ingrese el numero de columnas en el tablero: "));
            var gameOptions = new GameOptions(_rowSize, _columnSize, _path);
            RunLogic(gameOptions);
            ClearScrean();
        }

        private void RunLogic(GameOptions gameOptions)
        {
            var gameLogic = new GameLogic();
            try
            {
                var result = gameLogic.Setup(gameOptions);
                PrintResultMatrix(result);
            }
            catch (InvalidFileExtensionException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            catch (UnexpectedTokenException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            catch (InvalidMoveException ex)
            {
               Console.WriteLine(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void PrintResultMatrix(Result result)
        {
            for (var i = 0; i < _rowSize; i++)
            {
                for (var j = 0; j < _columnSize; j++)
                {
                    if(result.Matrix[i, j].Player == null)
                        Console.Write("\t0");
                    else
                        Console.Write("\t"+result.Matrix[i,j].Player.Tag.Substring(0,3));
                }
                Console.WriteLine();
            }
            Console.WriteLine(result.Description);
        }

        private void ClearScrean()
        {
            Console.WriteLine("\n\nPresione una tecla para continuar");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
