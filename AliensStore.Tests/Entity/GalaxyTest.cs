using NUnit.Framework;
using AliensStore.Data.Entity;

namespace AliensStore.Tests.Entity
{
	internal class GalaxyControllerTests
	{
		// Тест за създаване на Galaxy обект и проверка на свойствата
		[Test]
		public void Galaxy_ShouldBeCreatedWithCorrectProperties()
		{
			// Създаваме Galaxy обект
			var galaxy = new Galaxy
			{
				Name = "Milky Way"
			};

			// Проверяваме дали свойството Name е правилно зададено
			Assert.AreEqual("Milky Way", galaxy.Name);
		}

		// Тест за проверка на Planets колекцията
		[Test]
		public void Galaxy_ShouldInitializePlanetsCollection()
		{
			// Създаваме Galaxy обект
			var galaxy = new Galaxy();

			// Проверяваме дали колекцията Planets не е null и дали е празна
			Assert.IsNotNull(galaxy.Planets);
			Assert.AreEqual(0, galaxy.Planets.Count);
		}

		// Тест за добавяне на Planet в колекцията
		[Test]
		public void Galaxy_ShouldAddPlanetToCollection()
		{
			// Създаваме Galaxy обект
			var galaxy = new Galaxy();

			// Създаваме Planet обект
			var planet = new Planet { Name = "Earth" };

			// Добавяме Planet в Planets колекцията
			galaxy.Planets.Add(planet);

			// Проверяваме дали планетата е добавена към колекцията чрез First()
			var firstPlanet = galaxy.Planets.First();
			Assert.AreEqual("Earth", firstPlanet.Name);
		}
	}
}
