using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;


namespace Service.WalletApi.PingApi.Tests
{
    public class TestPingApi
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine("Debug output");
            ClassicAssert.Pass();
        }
    }
}
