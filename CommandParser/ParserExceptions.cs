using System;

namespace CommandParser
{
    class InvalidFileExtensionException : Exception
    {
        public InvalidFileExtensionException(string message) : base(message)
        {
            
        }
    }

    class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException(string message) : base(message)
        {

        }
    }

    class InvalidMoveException : Exception
    {
        public InvalidMoveException(string message)
            : base(message)
        {

        }
    }
}
