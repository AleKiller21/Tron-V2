using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class GameLogic
    {
        internal Cell[,] Matrix { get; set; }

        internal List<CommandParser.Command> Commands { get; set; }

        internal List<Player> Players { get; set; }

        internal string Result { get; set; }

        internal int Rows { get; set; }

        internal int Columns { get; set; }

        internal int CountPlayersAlive { get; set; }

        internal Random randomGenerator { get; set; }

        internal CommandParser.CommandParser Parser { get; set; }

        public GameLogic()
        {
            Players = new List<Player>();
            CountPlayersAlive = 0;
            randomGenerator = new Random(1);
            Parser = new CommandParser.CommandParser();
            Commands = new List<CommandParser.Command>();
        }

        public Result Setup(GameOptions gameData)
        {
            SetMatrix(gameData.Rows, gameData.Columns);
            Commands = Parser.Parse(gameData.Path);
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
            if ((CountPlayersAlive == 2) && Players.Count > 2)
            {
                return SetTie();
            }

            if (CountPlayersAlive == 1 && Players.Count > 1)
            {
                return GetLastManStanding();
            }

            if (CountPlayersAlive == 0)
            {
                return new Result(Matrix, "No hay ningun ganador");
            }

            return null;
        }

        private Result GetLastManStanding()
        {
            Player winner = null;

            foreach (var player in Players.Where(player => player.IsAlive))
            {
                winner = player;
            }

            string message = String.Format("El ganador es {0}", winner.Tag);
            Result result = new Result(Matrix, message);

            return result;
        }

        private Result SetTie()
        {
            List<Player> playersAlive = new List<Player>();

            foreach (var player in Players.Where(player => player.IsAlive))
            {
                playersAlive.Add(player);
            }

            return new Result(Matrix, String.Format("Ha habido un empate entre {0}," +
                                                    "{1}", playersAlive[0].Tag, playersAlive[1].Tag));
        }

        internal void ExecuteSingleCommand(CommandParser.Command command)
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

        private Player AddPlayer(string playerTag)
        {
            Position position = GeneratePosition();
            Player player = new Player(playerTag, position);
            Players.Add(player);
            UpdateTheMatrix(player);
            CountPlayersAlive++;

            return player;
        }

        internal Player AddFixedPlayer(string playerTag, int row, int col)
        {
            Position position = new Position(row, col);
            Player player = new Player(playerTag, position);
            Players.Add(player);
            UpdateTheMatrix(player);
            CountPlayersAlive++;

            return player;
        }

        private Position GeneratePosition()
        {
            int row = RandomGenerator.Between(0, Rows-1);
            int col = RandomGenerator.Between(0, Columns-1);

            while(Matrix[row, col].Player != null)
            {
                row = RandomGenerator.Between(0, Rows-1);
                col = RandomGenerator.Between(0, Columns-1);
            }

            return new Position(row, col);
        }

        private void AnalyzeAndMovePlayer(Player currentPlayer, CommandParser.PlayerMoves direction)
        {
            if(!currentPlayer.IsAlive) return;

            ValidationReport report = RunMovementValidations(currentPlayer, direction);
            if (report.Status != ValidationStatus.Ok)
                ChooseAction(report, currentPlayer);

            else
            {
                MovePlayer(currentPlayer, direction);
            }
        }

        private ValidationReport RunMovementValidations(Player currentPlayer, CommandParser.PlayerMoves direction)
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

        private void MovePlayer(Player currentPlayer, CommandParser.PlayerMoves direction)
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
