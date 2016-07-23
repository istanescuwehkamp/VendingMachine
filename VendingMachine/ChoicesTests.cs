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
            vendingMachine.AddChoice(Choice.Cola, 1);
            vendingMachine.AddChoice(Choice.Fanta, 0);

            //vendingMachine.Deliver(Choice.Cola);
            var result = vendingMachine.Deliver(Choice.Cola);

            Assert.IsNotNull(result);
        }


    }

    [TestClass]
    public class CreditTests
    {
        [TestMethod]
        public void ShouldReturnNullWhenRequestingAProductWithPrice()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 1, 10);

            var result = vendingMachine.Deliver(Choice.Cola);

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
        private Dictionary<Choice, int> _choices=new Dictionary<Choice, int>();
        private int _priceAmount;

        public Can Deliver(Choice choice)
        {
            if (!_choices.ContainsKey(choice))
            {
                return null;
            }

            if(_choices[choice] == 0)
            {
                return null;
            }

            _choices[choice]--;

            return new Can {Type = choice};
        }

        public void AddChoice(Choice choice, int amount = Int32.MaxValue, int price = 0)
        {
            if (price == 0)
            {
                _priceAmount = price;
                _choices.Add(choice, amount);
            }
        }
    }

    public class Can
    {
        public Choice Type { get; set; }
    }
}
