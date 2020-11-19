using System;
using System.Runtime.Serialization;

namespace Solid.DataTypes.Exceptions
{
    [Serializable]
    public class InvalidFilePathException : Exception
    {
        public InvalidFilePathException() { }

        public InvalidFilePathException(string message) : base(message) { }

        public InvalidFilePathException(string message, Exception inner) : base(message, inner) { }

        protected InvalidFilePathException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
