using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    [TestFixture]
    public class GymsTests
    {
        [Test]
        public void TheAthleteConstructorShouldSetValuesCorrectly()
        {
            Athlete athlete = new Athlete("Gosho");

            Assert.AreEqual(athlete.FullName, "Gosho");
            Assert.IsFalse(athlete.IsInjured);
        }

        [TestCase("")]
        [TestCase(null)]
        public void AnExceptionShouldBeThrownIfTheNameOfTheGymIsInvalid(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Gym gym = new Gym(name, 10);
            }, "Invalid gym name.");
        }

        [TestCase(-1)]
        [TestCase(-34)]
        public void AnExceptionShouldBeThrownIfTheCapacityIsNegative(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Gym gym = new Gym("Titan", capacity);
            }, "Invalid gym capacity.");
        }

        [Test]
        public void TheGymConstructorShouldSetValuesCorrectly()
        {
            Gym gym = new Gym("Titan", 10);

            Assert.AreEqual(gym.Name, "Titan");
            Assert.AreEqual(gym.Capacity, 10);
            Assert.AreEqual(gym.Count, 0);
        }

        [Test]
        public void TheMethodAddAthleteShouldThrowAnExceptionIfTheGymIsFull()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Gym gym = new Gym("Titan", 0);
                gym.AddAthlete(new Athlete("Gogo"));
            }, "The gym is full.");
        }

        [Test]
        public void TheMethodAddAthleteShouldWork()
        {
            Gym gym = new Gym("Titan", 5);
            gym.AddAthlete(new Athlete("Gogo"));
            gym.AddAthlete(new Athlete("Koki"));

            Assert.AreEqual(gym.Count, 2);
        }

        [Test]
        public void TheMethodRemoveAthleteShouldThrowAnExceptionIfTheAthleteDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Gym gym = new Gym("Titan", 5);
                gym.RemoveAthlete("Gogo");
            }, "The athlete Gogo doesn't exist.");
        }

        [Test]
        public void TheMethodRemoveAthleteShouldWork()
        {
            Gym gym = new Gym("Titan", 5);
            gym.AddAthlete(new Athlete("Gogo"));
            gym.AddAthlete(new Athlete("Koki"));
            gym.RemoveAthlete("Gogo");

            Assert.AreEqual(gym.Count, 1);
        }

        [Test]
        public void TheMethodInjureAthleteShouldThrowAnExceptionIfTheAthleteDoesNotExist()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Gym gym = new Gym("Titan", 5);
                gym.InjureAthlete("Gogo");
            }, "The athlete Gogo doesn't exist.");
        }

        [Test]
        public void TheMethodInjureAthleteShouldWork()
        {
            Gym gym = new Gym("Titan", 5);
            Athlete athlete = new Athlete("Gogo");
            gym.AddAthlete(athlete);
            gym.InjureAthlete("Gogo");

            Assert.IsTrue(athlete.IsInjured);
        }

        [Test]
        public void TheMethodInjureAthleteShouldReturnTheExactAthlete()
        {
            Gym gym = new Gym("Titan", 5);
            Athlete athlete = new Athlete("Gogo");
            gym.AddAthlete(athlete);
            Athlete athlete2 = gym.InjureAthlete("Gogo");

            Assert.AreEqual(athlete, athlete2);
        }

        [Test]
        public void TheMethodReportShouldWork()
        {
            Gym gym = new Gym("Titan", 5);
            Athlete athlete = new Athlete("Gogo");
            gym.AddAthlete(athlete);
            gym.InjureAthlete("Gogo");
            gym.AddAthlete(new Athlete("Pesho"));
            gym.AddAthlete(new Athlete("Roko"));

            Assert.AreEqual(gym.Report(), $"Active athletes at {gym.Name}: Pesho, Roko");
        }
    }
}
