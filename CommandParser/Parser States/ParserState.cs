namespace CommandParser.Parser_States
{
    abstract class ParserState
    {
        protected CommandParser Parser;

        protected ParserState(CommandParser parser)
        {
            Parser = parser;
        }

        internal abstract void ParseNext(char c);
    }

}
