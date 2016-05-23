namespace CommandParser.Parser_States
{
    class WaitForTagState : ParserState
    {
        public WaitForTagState(CommandParser parser)
            : base(parser)
        {
        }

        internal override void ParseNext(char c)
        {
            if (c == ':')
                throw new UnexpectedTokenException("Unexpected token: '" + c + "' at line " +
                    Parser.CurrentLine + ", column " +
                    Parser.CurrentColumn + ". Expected player tag.");

            if (!ParserUtils.IsWhiteSpaceOrLineBreak(c))
                Parser.ParserState = new ParsingTagState(Parser, c);
        }
    }
}
