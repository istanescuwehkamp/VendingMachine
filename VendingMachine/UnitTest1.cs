using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void ShouldReturnNothingIfEmpty()
        {
            var vendingMachine = new VendingMachine();
            var result = vendingMachine.Deliver(Choice.Cola);
            Assert.AreEqual(null,result);
        }
    }

    public enum Choice
    {
        Cola
    }

    public class VendingMachine
    {
        public Can<Choice> Deliver(Choice cola)
        {
            return null;
        }
    }

    public class Can<T>
    {
    }
}
