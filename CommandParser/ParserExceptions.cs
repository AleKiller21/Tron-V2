using System;

namespace CommandParser
{
    public class InvalidFileExtensionException : Exception
    {
        public InvalidFileExtensionException(string message) : base(message)
        {
            
        }
    }

    public class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException(string message) : base(message)
        {

        }
    }

    public class InvalidMoveException : Exception
    {
        public InvalidMoveException(string message)
            : base(message)
        {

        }
    }
}
