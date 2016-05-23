namespace CommandParser.Parser_States
{
    internal class WaitForColonState : ParserState
    {
        public WaitForColonState(CommandParser parser)
            : base(parser)
        {
        }

        internal override void ParseNext(char c)
        {
            if (c == ':')
                Parser.ParserState = new WaitForPlayerMoveState(Parser);

            else if (!ParserUtils.IsWhiteSpaceOrLineBreak(c))
                throw new UnexpectedTokenException("Unexpected token: '" + c + "' at line " +
                    Parser.CurrentLine + ", column " +
                    Parser.CurrentColumn + ". Expected ':'.");
        }
    }
}
