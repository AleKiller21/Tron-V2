using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Validations
    {
        public ValidationProperties Properties { get; set; }

        public Validations(ValidationProperties properties)
        {
            Properties = properties;
        }

        public ValidationReport RunValidations()
        {
            Player currentPlayer = Properties.CurrentPlayer;
            CommandParser.PlayerMoves direction = Properties.Direction;
            Position testPosition = Position.CalculatePosition(currentPlayer.Position, direction);
            int row = testPosition.Row;
            int col = testPosition.Column;

            if (CollisionWithBorder(testPosition))
                return new ValidationReport(ValidationStatus.CollisionWithBorder, null, testPosition);

            if (CollisionWithOneSelfTrail(currentPlayer, row, col))
                return new ValidationReport(ValidationStatus.CollisionWithOneSelfTrail, null, testPosition);

            if (CollisionWithOtherPlayerTrail(row, col))
                return new ValidationReport(ValidationStatus.CollisionWithOtherPlayerTrail, null, testPosition);

            if (CollisionWithOtherPlayer(row, col))
            {
                Player crashPlayer = Properties.Matrix[testPosition.Row, testPosition.Column].Player;
                return new ValidationReport(ValidationStatus.CollisionWithOtherPlayer, crashPlayer, testPosition);
            }

            return new ValidationReport(ValidationStatus.Ok, null, testPosition);
        }

        private bool CollisionWithBorder(Position testPosition)
        {
            return testPosition.Row == Properties.MatrixRows || testPosition.Row < 0 ||
                   testPosition.Column == Properties.MatrixColumns || testPosition.Column < 0;
        }

        private bool CollisionWithOneSelfTrail(Player currentPlayer, int row, int col)
        {
            return Properties.Matrix[row, col].Player != null && 
                Properties.Matrix[row, col].Player.Tag.Equals(currentPlayer.Tag);
        }

        private bool CollisionWithOtherPlayerTrail(int row, int col)
        {
            return Properties.Matrix[row, col].Player != null && !Properties.Matrix[row, col].CellActive;
        }

        private bool CollisionWithOtherPlayer(int row, int col)
        {
            return Properties.Matrix[row, col].Player != null && 
                   Properties.Matrix[row, col].CellActive   &&
                   Properties.Matrix[row, col].Player.IsAlive;
        }
    }
}
