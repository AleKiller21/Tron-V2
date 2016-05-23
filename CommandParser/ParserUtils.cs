using System.IO;

namespace CommandParser
{
    internal class ParserUtils
    {
        public static bool IsWhiteSpaceOrLineBreak(char c)
        {
            return char.IsWhiteSpace(c) || c == '\n';
        }

        public static char ReadChar(FileStream file)
        {
            var buffer = new byte[1];
            file.Read(buffer, 0, 1);

            return (char)buffer[0];
        }

        public static bool IsAValidFile(string path)
        {
            return Path.GetExtension(path) == ".tb";
        }
    }
}
