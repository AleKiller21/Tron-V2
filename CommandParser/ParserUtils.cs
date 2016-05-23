namespace CommandParser
{
    public class ParserUtils
    {
        internal static string GetPathExtension(string path)
        {
            var lastIndexOfPeriod = path.LastIndexOfAny(new[] {'.'});
            var noExtension = lastIndexOfPeriod < 0;

            if (noExtension) return "";

            var extensionBeginIndex = lastIndexOfPeriod + 1;
            var extensionLength = path.Length - extensionBeginIndex;

            var extension = path.Substring(extensionBeginIndex, extensionLength);

            return extension;
        }
    }
}
