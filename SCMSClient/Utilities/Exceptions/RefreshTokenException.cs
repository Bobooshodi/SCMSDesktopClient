using System;

namespace SCMSClient.Utilities
{
    public class RefreshTokenException : Exception
    {
        public RefreshTokenException()
        {
        }

        public RefreshTokenException(string message) : base(message)
        {
        }

        public RefreshTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RefreshTokenException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}
