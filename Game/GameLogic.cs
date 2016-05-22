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

            AnalyzeAndMovePlayer(currentPlayer, command.Direction);
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

        private void AnalyzeAndMovePlayer(Player currentPlayer, string direction)
        {
            ValidationReport report = RunMovementValidations(currentPlayer, direction);
            if (report.Status != ValidationStatus.Ok)
                ChooseAction(report, currentPlayer);

            else
            {
                MovePlayer(currentPlayer, direction);
            }
        }

        private ValidationReport RunMovementValidations(Player currentPlayer, string direction)
        {
            ValidationProperties properties =
                new ValidationProperties(Players, Matrix, currentPlayer, direction, Rows, Columns);

            Validations validations = new Validations(properties);
            return validations.RunValidations();
        }

        private void ChooseAction(ValidationReport report, Player currentPlayer)
        {
            ValidationStatus status = report.Status;

            switch (status)
            {
                case ValidationStatus.CollisionWithBorder:
                case ValidationStatus.CollisionWithOneSelfTrail:
                case ValidationStatus.CollisionWithOtherPlayerTrail:
                    KillPlayer(currentPlayer);
                    break;
                case ValidationStatus.CollisionWithOtherPlayer:
                    currentPlayer.IsAlive = report.CrashTarget.IsAlive = false;
                    break;
            }
        }

        private static void KillPlayer(Player currentPlayer)
        {
            currentPlayer.IsAlive = false;
        }

        private void MovePlayer(Player currentPlayer, string direction)
        {
            DisableCurrentPlayerCell(currentPlayer.Position);
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

        private void DisableCurrentPlayerCell(Position position)
        {
            Matrix[position.Row, position.Column].CellActive = false;
        }
    }
}
