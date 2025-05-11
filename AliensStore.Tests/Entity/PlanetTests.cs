using NUnit.Framework;
using AliensStore.Data.Entity;

namespace AliensStore.Tests.Entity
{
	internal class PlanetControllerTests
	{
		// Тест за създаване на Planet обект и проверка на свойствата
		[Test]
		public void Planet_ShouldBeCreatedWithCorrectProperties()
		{
			// Създаваме Planet обект
			var planet = new Planet
			{
				Name = "Earth",
				GalaxyId = 1
			};

			// Проверяваме дали свойствата са зададени правилно
			Assert.AreEqual("Earth", planet.Name);
			Assert.AreEqual(1, planet.GalaxyId);
		}

		// Тест за проверка на Galaxy асоциацията
		[Test]
		public void Planet_ShouldHaveCorrectGalaxy()
		{
			// Създаваме Galaxy и Planet обект
			var galaxy = new Galaxy { Name = "Milky Way" };
			var planet = new Planet
			{
				Name = "Earth",
				Galaxy = galaxy
			};

			// Проверяваме дали Planet е свързан с Galaxy
			Assert.AreEqual("Milky Way", planet.Galaxy.Name);
		}

		// Тест за инициализация на Aliens колекцията
		[Test]
		public void Planet_ShouldInitializeAliensCollection()
		{
			// Създаваме Planet обект
			var planet = new Planet();

			// Проверяваме дали Aliens колекцията не е null и дали е празна
			Assert.IsNotNull(planet.Aliens);
			Assert.AreEqual(0, planet.Aliens.Count);
		}

		// Тест за добавяне на Alien в Aliens колекцията
		[Test]
		public void Planet_ShouldAddAlienToCollection()
		{
			// Създаваме Planet и Alien обекти
			var planet = new Planet();
			var alien = new Alien { Name = "Zorg" };

			// Добавяме Alien в Aliens колекцията
			planet.Aliens.Add(alien);

			// Проверяваме дали Alien е добавен в колекцията
			var firstAlien = planet.Aliens.First();
			Assert.AreEqual("Zorg", firstAlien.Name);
		}
	}
}
