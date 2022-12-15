using System;

namespace SpendingSummary.Common
{
    public sealed class EnvFileFormatException : Exception
    {
        public EnvFileFormatException() : base(".env should contain key value separated by '='")
        {
        }
    }
}
