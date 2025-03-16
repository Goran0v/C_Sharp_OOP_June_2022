using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        [Test]
        public void TheSmartphoneConstructorShouldSetTheValuesCorrectly()
        {
            Smartphone smartphone = new Smartphone("Nokia", 100);

            Assert.AreEqual(smartphone.ModelName, "Nokia");
            Assert.AreEqual(smartphone.MaximumBatteryCharge, 100);
            Assert.AreEqual(smartphone.CurrentBateryCharge, 100);
        }

        [Test]
        public void TheShopConstructorShouldWorkCorrectly()
        {
            Shop shop = new Shop(10);

            Assert.AreEqual(shop.Capacity, 10);
            Assert.AreEqual(shop.Count, 0);
        }

        [TestCase(-1)]
        [TestCase(-16)]
        public void TheCapacityShouldThrowAnExceptionIfItIsNegative(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Shop smartphone = new Shop(capacity);
            }, "Invalid capacity.");
        }

        [Test]
        public void TheMethodAddShouldThrowAnExceptionIfTheSmartphoneIsAlreadyInTheShop()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Shop shop = new Shop(10);
                shop.Add(new Smartphone("Nokia", 99));
                Smartphone smartphone = new Smartphone("Nokia", 100);
                shop.Add(smartphone);

            }, "The phone model Nokia already exist.");
        }

        [Test]
        public void TheMethodAddShouldThrowAnExceptionIfTheShopIsFull()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Shop shop = new Shop(1);
                shop.Add(new Smartphone("Nokia", 100));
                Smartphone smartphone = new Smartphone("Xiaomi", 100);
                shop.Add(smartphone);

            }, "The shop is full.");
        }

        [Test]
        public void TheMethodAddShouldWork()
        {
            Shop shop = new Shop(3);
            shop.Add(new Smartphone("Nokia", 100));
            shop.Add(new Smartphone("Xiaomi", 100));

            Assert.AreEqual(shop.Count, 2);
        }

        [Test]
        public void TheMethodRemoveShouldThrowAnExceptionIfTheSmartphoneDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Shop shop = new Shop(10);
                shop.Remove("Nokia");

            }, "The phone model Nokia doesn't exist.");
        }

        [Test]
        public void TheMethodRemoveShouldWork()
        {
            Shop shop = new Shop(3);
            shop.Add(new Smartphone("Nokia", 100));
            shop.Add(new Smartphone("Xiaomi", 100));
            shop.Remove("Nokia");

            Assert.AreEqual(shop.Count, 1);
        }

        [Test]
        public void TheMethodTestPhoneShouldThrowAnExceptionIfTheSmartphoneDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Shop shop = new Shop(10);
                shop.TestPhone("Nokia", 50);

            }, "The phone model Nokia doesn't exist.");
        }

        [Test]
        public void TheMethodTestPhoneShouldThrowAnExceptionIfTheSmartphoneDoesNotHaveEnoughBattery()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Shop shop = new Shop(10);
                shop.Add(new Smartphone("Nokia", 40));
                shop.TestPhone("Nokia", 50);

            }, "The phone model Nokia is low on batery.");
        }

        [Test]
        public void TheMethodTestPhoneShouldWork()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            shop.Add(smartphone);
            shop.TestPhone("Nokia", 40);

            Assert.AreEqual(smartphone.CurrentBateryCharge, 60);
        }

        [Test]
        public void TheMethodChargePhoneShouldThrowAnExceptionIfTheSmartphoneDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Shop shop = new Shop(10);
                shop.ChargePhone("Nokia");

            }, "The phone model Nokia doesn't exist.");
        }

        [Test]
        public void TheMethodChargePhoneShouldWork()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone = new Smartphone("Nokia", 100);
            shop.Add(smartphone);
            shop.TestPhone("Nokia", 60);
            shop.ChargePhone("Nokia");

            Assert.AreEqual(smartphone.CurrentBateryCharge, smartphone.MaximumBatteryCharge);
        }
    }
}