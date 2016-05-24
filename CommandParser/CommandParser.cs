using System.Collections.Generic;
using System.IO;
using CommandParser.Parser_States;

namespace CommandParser
{
    public class CommandParser
    {
        internal List<Command> Commands;
        internal FileStream MatchFile;
        internal ParserState ParserState;
        internal int CurrentLine, CurrentColumn;
 
        public List<Command> Parse(string path)
        {
            ValidateFile(path);
            BeginParse(path);
            
            return Commands;
        }

        private void ValidateFile(string path)
        {
            if (!ParserUtils.IsAValidFile(path)) 
                throw new InvalidFileExtensionException("Invalid file extension: " + Path.GetExtension(path) + ". Expected: .tb");

            if (!File.Exists(path)) 
                throw new FileNotFoundException("Unable to find file at: " + path);
        }

        private void BeginParse(string path)
        {
            Commands = new List<Command>();
            MatchFile = File.Open(path, FileMode.Open);
            ParserState =  new WaitForTagState(this);
            CurrentLine = 1;
            CurrentColumn = 1;
            
            while (MatchFile.Position < MatchFile.Length)
            {
                var character = ParserUtils.ReadChar(MatchFile);

                ParserState.ParseNext(character);

                CurrentColumn++;

                if (character != '\n') continue;
                CurrentLine++;
                CurrentColumn = 1;
            }

            MatchFile.Close();
        }
    }
}
