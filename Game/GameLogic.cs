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

        internal void ExecuteGame()
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

        internal void ExecuteSingleCommand(Command command)
        {
            Player currentPlayer = GetPlayer(command.Tag);

            if(currentPlayer == null)
                currentPlayer = AddPlayer(command.Tag);

            MovePlayer(currentPlayer, command.Direction);
            RunValidations(command);
        }

        internal Player GetPlayer(string tag)
        {
            return Players.FirstOrDefault(player => player.Tag == tag);
        }

        internal Player AddPlayer(string playerTag)
        {
            Player player = new Player(playerTag, 0, 0);
            Players.Add(player);

            return player;

            //TODO: Hacer la funcion mas inteligente para que busque otras posiciones
        }

        private void MovePlayer(Player currentPlayer, string direction)
        {
            currentPlayer.Position = Position.CalculatePosition(currentPlayer.Position, direction);
            UpdateTheMatrix(currentPlayer);
        }

        private void UpdateTheMatrix(Player currentPlayer)
        {
            int row = currentPlayer.Position.Row;
            int col = currentPlayer.Position.Column;

            Matrix[row, col].Player = currentPlayer;
            Matrix[row, col].CellActive = true;
        }

        private void RunValidations(Command command)
        {
            return;
        }
    }
}
