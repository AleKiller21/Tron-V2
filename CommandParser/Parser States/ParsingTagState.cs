namespace CommandParser.Parser_States
{
    class ParsingTagState : ParserState
    {
        private readonly Command _command;

        public ParsingTagState(CommandParser parser, char first)
            : base(parser)
        {
            _command = new Command();
            _command.Tag += first;
            parser.Commands.Add(_command);
        }

        internal override void ParseNext(char c)
        {
            if (ParserUtils.IsWhiteSpaceOrLineBreak(c))
                Parser.ParserState = new WaitForColonState(Parser);

            else if (c == ':')
                Parser.ParserState = new WaitForPlayerMoveState(Parser);

            else
                _command.Tag += c;
        }
    }
}
