using System;
using CreditCardModule;
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

        [TestMethod]
        public void ShouldHaveTotalZeroInitialy()
        {
            var vendingMachine = new VendingMachine();
            Assert.AreEqual(0, vendingMachine.Total);
        }

        [TestMethod]
        public void ShouldAcceptCoins()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddCoin(5);
            Assert.AreEqual(5, vendingMachine.Total);
        }

        [TestMethod]
        public void ShouldAddCoinsToTotal()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddCoin(5);
            vendingMachine.AddCoin(1);
            Assert.AreEqual(6, vendingMachine.Total);
        }

        [TestMethod]
        public void ShouldReturnChange()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddCoin(5);
            var change = vendingMachine.PayOutChange();
            Assert.AreEqual(5, change);
        }

        [TestMethod]
        public void ShouldSetBalanceToZero()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddCoin(5);
            vendingMachine.PayOutChange();
            Assert.AreEqual(0, vendingMachine.Total);
        }

        [TestMethod]
        public void ShouldAcceptCostForOption()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 5);
            vendingMachine.AddPrice(Choice.Cola, 5.5);
            var colaPrice = vendingMachine.GetPrice(Choice.Cola);
            Assert.AreEqual(5.5, colaPrice);
        }

        [TestMethod]
        public void ShouldAcceptDifferentCostsForOptions()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 5);
            vendingMachine.AddPrice(Choice.Cola, 5.5);
            vendingMachine.AddChoice(Choice.Fanta, 5);
            vendingMachine.AddPrice(Choice.Fanta, 2.5);
            var colaPrice = vendingMachine.GetPrice(Choice.Cola);
            var fantaPrice = vendingMachine.GetPrice(Choice.Fanta);
            Assert.AreEqual(5.5, colaPrice);
            Assert.AreEqual(2.5, fantaPrice);
        }

        [TestMethod]
        public void ShouldGiveProductIfPriceIsLessThanBalance()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 5);
            vendingMachine.AddPrice(Choice.Cola, 3);
            vendingMachine.AddCoin(5);
            var colaCan = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNotNull(colaCan);
        }

        [TestMethod]
        public void ShouldSubstractPriceFromBalance()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola, 5);
            vendingMachine.AddPrice(Choice.Cola, 3);
            vendingMachine.AddCoin(5);
            vendingMachine.Deliver(Choice.Cola);
            Assert.AreEqual(2, vendingMachine.Total);
        }

        [TestMethod]
        public void ShouldNotReturnCanIfBalanceLowerThanCost()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola);
            vendingMachine.AddPrice(Choice.Cola, 2.5);
            var can = vendingMachine.Deliver(Choice.Cola);
            Assert.IsNull(can);
        }

        [TestMethod]
        public void ShouldAcceptCardAsPaymentMethod()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola);
            vendingMachine.AddPrice(Choice.Cola, 0);
            vendingMachine.AcceptCard(new CreditCard("holder name", "serial number", "card company", 1234));
            vendingMachine.SelectChoiceForCard(Choice.Cola);
            vendingMachine.GetPinNumber(1234);
            Can colaCan = vendingMachine.DeliverChoiceForCard();
            Assert.IsNotNull(colaCan);
        }

        [TestMethod]
        public void ShouldFailForInvalidPin()
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola);
            vendingMachine.AddPrice(Choice.Cola, 2.3);
            vendingMachine.AcceptCard(new CreditCard("holder name", "serial number", "card company", 1234));
            vendingMachine.SelectChoiceForCard(Choice.Cola);
            vendingMachine.GetPinNumber(3456);
            Can colaCan = vendingMachine.DeliverChoiceForCard();
            Assert.IsNull(colaCan);
        }

        [TestMethod]
        public void ShouldReturnTheCorrectChoiceWhenPayingByCard()
        {
            var vendingMachine=new VendingMachine();
            vendingMachine.AddChoice(Choice.Cola);
            vendingMachine.AddChoice(Choice.Fanta);
            vendingMachine.AddPrice(Choice.Cola, 2.3);
            vendingMachine.AcceptCard(new CreditCard("holder name", "serial number", "card company", 1234));
            vendingMachine.SelectChoiceForCard(Choice.Fanta);
            vendingMachine.GetPinNumber(1234);
            Can colaCan = vendingMachine.DeliverChoiceForCard();
            Assert.AreEqual(Choice.Fanta,colaCan.Type);
        }
    }
}
