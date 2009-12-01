using System;

namespace OpenNETCF.MsTest
{
    public class TestRunException : Exception
    {
        public TestRunException()
        {
        }

        public TestRunException(string message)
            : base(message)
        {
        }

        public TestRunException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
