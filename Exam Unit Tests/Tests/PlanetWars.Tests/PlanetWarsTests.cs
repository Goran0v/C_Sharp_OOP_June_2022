using NUnit.Framework;
using System;
using System.Xml.Linq;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [TestCase(-0.0002)]
            [TestCase(-1)]
            [TestCase(-16)]
            public void ThePriceShouldThrowAnExceptionIfItIsNegative(double price)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Weapon weapon = new Weapon("Nuclear", price, 5);
                }, "Price cannot be negative.");
            }

            [Test]
            public void TheConstructorOfWeaponShouldSetTheValuesCorrectly()
            {
                Weapon weapon = new Weapon("Nuclear", 46.89, 8);

                Assert.AreEqual(weapon.Name, "Nuclear");
                Assert.AreEqual(weapon.Price, 46.89);
                Assert.AreEqual(weapon.DestructionLevel, 8);
            }

            [Test]
            public void TheIncreaseDestructionLevelShouldWork()
            {
                Weapon weapon = new Weapon("Nuclear", 46.89, 8);
                weapon.IncreaseDestructionLevel();

                Assert.AreEqual(weapon.DestructionLevel, 9);
            }

            [TestCase(10)]
            [TestCase(15)]
            public void TheIsNuclearPropertyShouldWork(int destructionLevel)
            {
                Weapon weapon = new Weapon("Nuclear", 46.89, destructionLevel);

                Assert.IsTrue(weapon.IsNuclear);
            }

            [TestCase(null)]
            [TestCase("")]
            public void TheNameShouldThrowAnExceptionIfItIsNullOrEmpty(string name)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Planet planet = new Planet(name, 45.6);
                }, "Invalid planet Name");
            }

            [TestCase(-0.0002)]
            [TestCase(-1)]
            [TestCase(-16)]
            public void TheBudgetShouldThrowAnExceptionIfItIsNegative(double budget)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Planet planet = new Planet("Tatouin", budget);
                }, "Budget cannot drop below Zero!");
            }

            [Test]
            public void TheConstructorOfPlanetShouldSetTheValuesCorrectly()
            {
                Planet planet = new Planet("Tatouin", 3000.02);

                Assert.AreEqual(planet.Name, "Tatouin");
                Assert.AreEqual(planet.Budget, 3000.02);
                Assert.AreEqual(planet.Weapons.Count, 0);
            }

            [Test]
            public void TheMethodProfitShouldWork()
            {
                Planet planet = new Planet("Tatouin", 3000.02);
                planet.Profit(20);

                Assert.AreEqual(planet.Budget, 3020.02);
            }

            [Test]
            public void TheMethodSpendFundsShouldThrowAnExceptionIfTheBudgetIsNotEnough()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    Planet planet = new Planet("Tatouin", 3000.02);
                    planet.SpendFunds(3000.03);
                }, "Not enough funds to finalize the deal.");
            }

            [Test]
            public void TheMethodSpendFundsShouldWork()
            {
                Planet planet = new Planet("Tatouin", 3000.02);
                planet.SpendFunds(20);

                Assert.AreEqual(planet.Budget, 2980.02);
            }

            [Test]
            public void TheMethodAddWeaponShouldThrowAnExceptionIfTheWeaponIsAlreadyBought()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    Planet planet = new Planet("Tatouin", 3000.02);
                    planet.AddWeapon(new Weapon("Nuclear", 50, 5));
                    planet.AddWeapon(new Weapon("Nuclear", 55, 8));
                }, "There is already a Nuclear weapon.");
            }

            [Test]
            public void TheMethodAddWeaponShouldWork()
            {
                Planet planet = new Planet("Tatouin", 3000.02);
                Weapon weapon = new Weapon("Nuclear", 55, 5);
                Weapon weapon2 = new Weapon("Atomic", 66, 6);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);

                Assert.AreEqual(planet.Weapons.Count, 2);
            }

            [Test]
            public void TheMethodRemoveWeaponShouldWork()
            {
                Planet planet = new Planet("Tatouin", 3000.02);
                Weapon weapon = new Weapon("Nuclear", 55, 5);
                Weapon weapon2 = new Weapon("Atomic", 66, 6);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
                planet.RemoveWeapon("Nuclear");
                planet.RemoveWeapon("Bruh");

                Assert.AreEqual(planet.Weapons.Count, 1);
            }

            [Test]
            public void ThePropertyMilitaryPowerRatioShouldWork()
            {
                Planet planet = new Planet("Tatouin", 3000.02);
                Weapon weapon = new Weapon("Nuclear", 55, 5);
                Weapon weapon2 = new Weapon("Atomic", 66, 6);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);

                Assert.AreEqual(planet.MilitaryPowerRatio, 11);
            }

            [Test]
            public void TheMethodUpgradeWeaponShouldThrowAnExceptionIfTheWeaponDoesNotExist()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    Planet planet = new Planet("Tatouin", 3000.02);
                    planet.AddWeapon(new Weapon("Nuclear", 50, 5));
                    planet.AddWeapon(new Weapon("Atomic", 55, 8));
                    planet.UpgradeWeapon("Lazer");

                }, "Lazer does not exist in the weapon repository of Tatouin");
            }

            [Test]
            public void TheMethodUpgradeWeaponShouldWork()
            {
                Planet planet = new Planet("Tatouin", 3000.02);
                planet.AddWeapon(new Weapon("Nuclear", 50, 5));
                Weapon weapon = new Weapon("Atomic", 55, 8);
                planet.AddWeapon(weapon);
                planet.UpgradeWeapon("Atomic");

                Assert.AreEqual(weapon.DestructionLevel, 9);
            }

            [TestCase(5)]
            [TestCase(9)]
            public void TheMethodDestructOpponentShouldThrowAnExceptionIfThePlanetIsTooPowerful(int destructLevel)
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    Planet planet = new Planet("Tatouin", 3000.02);
                    planet.AddWeapon(new Weapon("Nuclear", 50, 5));
                    Planet planet2 = new Planet("Boba", 4000);
                    planet2.AddWeapon(new Weapon("Atomic", 55, destructLevel));
                    planet.DestructOpponent(planet2);

                }, "Boba is too strong to declare war to!");
            }

            [Test]
            public void TheMethodDestructOpponentShouldWork()
            {
                Planet planet = new Planet("Tatouin", 3000.02);
                planet.AddWeapon(new Weapon("Nuclear", 50, 5));
                Planet planet2 = new Planet("Boba", 4000);
                planet2.AddWeapon(new Weapon("Atomic", 55, 4));
                
                Assert.AreEqual(planet.DestructOpponent(planet2), "Boba is destructed!");
            }
        }
    }
}
