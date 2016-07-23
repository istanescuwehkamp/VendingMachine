using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachine
{
    [TestClass]
    public class ChoiceTests
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

    public enum Choice
    {
        Cola,
        Fanta
    }

    public class VendingMachine
    {
        private List<Choice> _choices=new List<Choice>();
        private int amount;

        public Can Deliver(Choice choice)
        {
            if (!_choices.Contains(choice)||amount==0)
            {
                return null;
            }
            
            return new Can {Type = choice};
        }

        public void AddChoice(Choice choice, int i=int.MaxValue)
        {
            _choices.Add(choice);
        }
    }

    public class Can
    {
        public Choice Type { get; set; }
    }

    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void ShouldReturnNoCanIfRackIsEmpty()
        {
            var vendingMachine=new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola,0);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNull(can);
        }

        [TestMethod]
        public void ShouldReturnACanIfRackIsNotEmpty()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola,1);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNotNull(can);
        }
    }
}
