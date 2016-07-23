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

        [TestMethod]
        public void ShouldReturnCola()
        {
            var vendingMachine=new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.AreEqual(Choice.Cola, can.Type);
        }
    }

    public enum Choice
    {
        Cola
    }

    public class VendingMachine
    {
        private Choice? _choice;

        public Can Deliver(Choice cola)
        {
            if (_choice==null)
            {
                return null;
            }
            return new Can {Type = _choice.Value};
        }

        public void AddChoice(Choice cola)
        {
            _choice = cola;
        }
    }

    public class Can
    {
        public Choice Type { get; set; }
    }
}
