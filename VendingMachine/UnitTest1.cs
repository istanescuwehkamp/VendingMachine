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
            Assert.AreEqual(null,result);
        }

        [TestMethod]
        public void ShouldReturnACan()
        {
            var vendingMachine=new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 1);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNotNull(can);
        }

        [TestMethod]
        public void ShouldReturnACanOfTheSelectedChoice()
        {
            var vendingMachine=new VendingMachine();
            vendingMachine.AddChoice(Choice.Fanta, 1);
            vendingMachine.AddChoice(Choice.Cola, 1);
            var can = vendingMachine.Deliver(Choice.Fanta);
            Assert.AreEqual(Choice.Fanta,can.Type);
        }
    }

    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void ShouldReturnNullWhenInvetoryIsEmpty()
        {
            var vendingMachine = new VendingMachine();
            var choice = Choice.Cola;
            vendingMachine.AddChoice(choice, 0);
            var result = vendingMachine.Deliver(choice);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldReturnNullWhenInventoryIsEmptied()
        {
            var vendingMachine = new VendingMachine();
            var choice = Choice.Cola;
            vendingMachine.AddChoice(choice, 1);

            vendingMachine.Deliver(choice);
            var result = vendingMachine.Deliver(choice);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldReturnNullWhenChoiceNotAdded()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 1);
            
            var result = vendingMachine.Deliver(Choice.Fanta);

            Assert.IsNull(result);
        }


    }

    public enum Choice
    {
        Cola,
        Fanta
    }

    public class VendingMachine
    {
        private readonly Dictionary<Choice, int> _choices = new Dictionary<Choice, int>();

        public Can Deliver(Choice choice)
        {
            if (!_choices.ContainsKey(choice) || _choices[choice] == 0)
            {
                return null;
            }

            _choices[choice] --;
            return new Can {Type = choice};
        }

        public void AddChoice(Choice choice, int quantity)
        {
            _choices.Add(choice, quantity);
        }
    }

    public class Can
    {
        public Choice Type { get; set; }
    }
}
