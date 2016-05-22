using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameLogic
    {
        public Cell[,] Matrix { get; set; }

        public List<Command> Commands { get; set; }

        public List<Player> Players { get; set; }

        public string Result { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public GameLogic()
        {
            //Commands = new List<Command>();
            Players = new List<Player>();
        }

        public void Setup(GameOptions gameData)
        {
            SetMatrix(gameData.Rows, gameData.Columns);
            Commands = LoadCommands(gameData.Path);
            ExecuteGame();
        }

        internal void SetMatrix(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            Matrix = new Cell[Rows, Columns];

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    Matrix[row, col] = new Cell();
                }
            }
        }

        private List<Command> LoadCommands(string path)
        {
            //TODO: Devolver la lista de comandos del por el Parser
            return null;
        }

        private void ExecuteGame()
        {
            RunCommands();
        }

        private void RunCommands()
        {
            foreach (var command in Commands)
            {
                ExecuteSingleCommand(command);
            }
        }

        private void ExecuteSingleCommand(Command command)
        {
            if (!ValidatePlayerExists(command.Tag))
                AddPlayer(command.Tag);
            //TODO: Mover el personaje en la matriz
            //TODO: Correr todas las validaciones
        }

        internal void AddPlayer(string playerTag)
        {
            Players.Add(new Player(playerTag));
        }

        private bool ValidatePlayerExists(string playerTag)
        {
            return Players.Any(player => player.Tag == playerTag);
        }
    }
}
