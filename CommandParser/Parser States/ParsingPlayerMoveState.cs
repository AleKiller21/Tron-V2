using System;
using System.Linq;

namespace CommandParser.Parser_States
{
    internal class ParsingPlayerMoveState : ParserState
    {
        private readonly Command _command;
        private string _move;

        public ParsingPlayerMoveState(CommandParser parser, char first)
            : base(parser)
        {
            _command = parser.Commands.Last();
            _move += first;
        }

        internal override void ParseNext(char c)
        {
            var eof = Parser.MatchFile.Position == Parser.MatchFile.Length;

            if (ParserUtils.IsWhiteSpaceOrLineBreak(c))
                NextState();

            else
            {
                _move += c;

                if (eof)
                    NextState();
            }
        }

        private void NextState()
        {
            if (Enum.IsDefined(typeof(PlayerMoves), _move))
                SetPlayerMove();
            else
                throw new InvalidMoveException("Invalid move '" + _move + "' at line " + Parser.CurrentLine + ".");

            Parser.ParserState = new WaitForTagState(Parser);
        }

        private void SetPlayerMove()
        {
            PlayerMoves direction;
            Enum.TryParse(_move, out direction);
            _command.Direction = direction;
        }
    }

}
