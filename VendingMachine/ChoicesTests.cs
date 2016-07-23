using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine
{
    [TestClass]
    public class ChoicesTests
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
            vendingMachine.AddChoice(Choice.Cola);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNotNull(can);
        }

        [TestMethod]
        public void ShouldReturnACanOfTheSelectedChoice()
        {
            var vendingMachine=new VendingMachine();
            vendingMachine.AddChoice(Choice.Fanta);
            vendingMachine.AddChoice(Choice.Cola);
            var can = vendingMachine.Deliver(Choice.Fanta);
            Assert.AreEqual(Choice.Fanta,can.Type);
        }

        
    }

    [TestClass]
    public class RackTests
    {
        [TestMethod]
        public void ShouldReturnNothingIfRackIsEmpty()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 0);

            var result = vendingMachine.Deliver(Choice.Cola);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldReturnACanIfRackIsNotEmpty()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 1);

            var result = vendingMachine.Deliver(Choice.Cola);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldNotReturnMoreThanAddedCans()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 1);

            vendingMachine.Deliver(Choice.Cola);
            var result = vendingMachine.Deliver(Choice.Cola);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void MutipleChoicesReturnCorrectAmmount()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 2);
            vendingMachine.AddChoice(Choice.Fanta, 1);

            vendingMachine.Deliver(Choice.Cola);
            var result = vendingMachine.Deliver(Choice.Cola);

            Assert.IsNotNull(result);
        }
    }

    public enum Choice
    {
        Cola,
        Fanta
    }

    public class VendingMachine
    {
        private List<Choice> _choices=new List<Choice>();
        private int _rackAmount;

        public Can Deliver(Choice choice)
        {
            if (!_choices.Contains(choice))
            {
                return null;
            }

            if(_rackAmount == 0)
            {
                return null;
            }

            _rackAmount--;

            return new Can {Type = choice};
        }

        public void AddChoice(Choice choice, int amount = Int32.MaxValue)
        {
            _rackAmount = amount;
            _choices.Add(choice);
        }
    }

    public class Can
    {
        public Choice Type { get; set; }
    }
}
