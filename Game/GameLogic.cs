using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameLogic
    {
        internal Cell[,] Matrix { get; set; }

        internal List<Command> Commands { get; set; }

        internal List<Player> Players { get; set; }

        internal List<Player> PlayersAlive { get; set; }

        internal string Result { get; set; }

        internal int Rows { get; set; }

        internal int Columns { get; set; }

        internal int CountPlayersAlive { get; set; }

        internal bool FlagCheckPlayersAlive { get; set; }

        public GameLogic()
        {
            Players = new List<Player>();
            PlayersAlive = new List<Player>();
            FlagCheckPlayersAlive = false;
            CountPlayersAlive = 0;
        }

        public Result Setup(GameOptions gameData)
        {
            SetMatrix(gameData.Rows, gameData.Columns);
            Commands = LoadCommands(gameData.Path);
            return ExecuteGame();
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

        internal Result ExecuteGame()
        {
            return RunCommands();
        }

        private Result RunCommands()
        {
            foreach (var command in Commands)
            {
                ExecuteSingleCommand(command);
                Result result = CheckPlayersAlive();

                if (result != null)
                    return result;
            }

            string message = "Ningun jugador ha ganado";
            return new Result(Matrix, message);
        }

        private Result CheckPlayersAlive()
        {
            if (!FlagCheckPlayersAlive)
                return null;

            if (CountPlayersAlive > 2)
                return null;

            if (CountPlayersAlive == 2)
            {
                SetLastTwoPlayersAlive();
                return null;
            }

            if (CountPlayersAlive == 1)
            {
                return LastManStanding();
            }

            if (CountPlayersAlive == 0)
            {
                return SetTie();
            }

            return null;
        }

        private void SetLastTwoPlayersAlive()
        {
            PlayersAlive = Players.Where(player => player.IsAlive).ToList();
        }

        private Result LastManStanding()
        {
            Player winner = null;

            if (PlayersAlive.Count > 0)
            {
                foreach (var player in PlayersAlive.Where(player => player.IsAlive))
                {
                    winner = player;
                }
            }

            else
            {
                foreach (var player in Players.Where(player => player.IsAlive))
                {
                    winner = player;
                }
            }

            string message = String.Format("El ganador es {0}", winner.Tag);
            Result result = new Result(Matrix, message);

            return result;
        }

        private Result SetTie()
        {
            if (PlayersAlive.Count > 0)
            {
                string message = String.Format("Ha sido un empate entre {0} y {1}", PlayersAlive[0], PlayersAlive[1]);
                return new Result(Matrix, message);
            }

            else
            {
                return new Result(Matrix, "Ha sido un empate");
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
            CountPlayersAlive++;

            if (Players.Count > 1 && !FlagCheckPlayersAlive)
                FlagCheckPlayersAlive = true;

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
                    currentPlayer.IsAlive = false;
                    CountPlayersAlive--;
                    DisableCurrentPlayerCell(currentPlayer.Position);
                    break;
                case ValidationStatus.CollisionWithOtherPlayerTrail:
                    KillTwoPlayers(report, currentPlayer);
                    break;
                case ValidationStatus.CollisionWithOtherPlayer:
                    KillTwoPlayers(report, currentPlayer);
                    break;
            }
        }

        private void KillTwoPlayers(ValidationReport report, Player currentPlayer)
        {
            Matrix[report.Destination.Row, report.Destination.Column].Player = currentPlayer;

            if (report.Status == ValidationStatus.CollisionWithOtherPlayerTrail)
            {
                DisableCurrentPlayerCell(currentPlayer.Position);
                currentPlayer.IsAlive = false;
                CountPlayersAlive--;
            }

            else
            {
                DisableCurrentPlayerCell(currentPlayer.Position);
                DisableCurrentPlayerCell(report.CrashTarget.Position);
                currentPlayer.IsAlive = report.CrashTarget.IsAlive = false;
                CountPlayersAlive -= 2;
            }
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
