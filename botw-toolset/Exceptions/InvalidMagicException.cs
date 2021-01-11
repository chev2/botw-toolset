using System;

namespace BOTWToolset.Exceptions
{
    /// <summary>
    /// An exception that's thrown when the magic header in a file is not what was expected.
    /// </summary>
    class InvalidMagicException : Exception
    {
        public InvalidMagicException() : base() { }
        public InvalidMagicException(string message) : base(message) { }
        public InvalidMagicException(string message, Exception inner) : base(message, inner) { }
    }
}
