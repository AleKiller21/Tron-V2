using System.Collections.Generic;

namespace CommandParser
{
    public class CommandParser
    {
        private const string CorrectFileExtension = "tb";

        public List<Command> Parse(string path)
        {
            var incorrectFileExtension = ParserUtils.GetPathExtension(path) != CorrectFileExtension;

            return incorrectFileExtension ? null : new List<Command>();
        }
    }
}
