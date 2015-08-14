using System;
using Disty.Common.Log.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Disty.Common.Log.Test.Unit
{
    [TestClass]
    public class LoggedExceptionTest
    {
        [TestMethod]
        public void CanCreateLoggedException()
        {
            const string msg1 = "Message 1";
            const string msg2 = "Message 2";
            var ex = new LoggedException(msg1, new Exception(msg2));

            Assert.AreEqual(ex.Message, msg1);
            Assert.AreEqual(ex.InnerException.Message, msg2);
        }
    }
}
