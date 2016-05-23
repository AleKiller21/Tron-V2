namespace CommandParser.Parser_States
{
    internal class WaitForPlayerMoveState : ParserState
    {
        public WaitForPlayerMoveState(CommandParser parser)
            : base(parser)
        {
        }

        internal override void ParseNext(char c)
        {
            if (c == ':')
                throw new UnexpectedTokenException("Unexpected token '" + c + "' at line " +
                    Parser.CurrentLine + ", column " +
                    Parser.CurrentColumn + ". Expected player move.");

            if (!ParserUtils.IsWhiteSpaceOrLineBreak(c))
                Parser.ParserState = new ParsingPlayerMoveState(Parser, c);
        }
    }

}
