using System;
using System.Collections.Generic;
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
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void ShouldReturnACan()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNotNull(can);
        }

        [TestMethod]
        public void ShouldReturnACanOfTheSelectedChoice()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Fanta);
            vendingMachine.AddChoice(Choice.Cola);
            var can = vendingMachine.Deliver(Choice.Fanta);
            Assert.AreEqual(Choice.Fanta, can.Type);
        }

        [TestMethod]
        public void ShouldSetTheInventoryCorrectly()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 1);
            vendingMachine.Deliver(Choice.Cola);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNull(can);
        }
    }

    public enum Choice
    {
        Cola,
        Fanta
    }

    public class VendingMachine
    {
        private List<Choice> _choices = new List<Choice>();
        private Dictionary<Choice, int> _choiceQuantities = new Dictionary<Choice, int>();

        public Can Deliver(Choice choice)
        {
            if (!_choices.Contains(choice)||_choiceQuantities[choice]<1)
            {
                return null;
            }
            _choiceQuantities[choice] -= 1;
            return new Can { Type = choice };
        }

        public void AddChoice(Choice choice, int count = int.MaxValue)
        {
            _choiceQuantities.Add(choice, count);
            _choices.Add(choice);
        }
    }

    public class Can
    {
        public Choice Type { get; set; }
    }
}
