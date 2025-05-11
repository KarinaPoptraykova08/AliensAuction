using NUnit.Framework;
using AliensStore.Data.Entity;

namespace AliensStore.Tests.Entity
{
	internal class AlienTest
	{
		// Тест за създаване на Alien обект и проверка на свойствата
		[Test]
		public void Alien_ShouldBeCreatedWithCorrectProperties()
		{
			// Подготвяме данни
			var alien = new Alien
			{
				Name = "Zorg",
				Price = 100.5m,
				CoinsPerDay = 10,
				Age = 150,
				Color = "Green",
				Legs = 4,
				Arms = 2,
				Eyes = 3,
				IsForSale = true,
				PlanetId = 1,
				ImageUrl = "http://example.com/alien.jpg"
			};

			// Проверка на свойствата
			Assert.AreEqual("Zorg", alien.Name);
			Assert.AreEqual(100.5m, alien.Price);
			Assert.AreEqual(10, alien.CoinsPerDay);
			Assert.AreEqual(150, alien.Age);
			Assert.AreEqual("Green", alien.Color);
			Assert.AreEqual(4, alien.Legs);
			Assert.AreEqual(2, alien.Arms);
			Assert.AreEqual(3, alien.Eyes);
			Assert.IsTrue(alien.IsForSale);
			Assert.AreEqual(1, alien.PlanetId);
			Assert.AreEqual("http://example.com/alien.jpg", alien.ImageUrl);
		}

		// Тест за проверка дали Planet е правилно асоцииран с Alien
		[Test]
		public void Alien_ShouldHaveCorrectPlanet()
		{
			// Създаваме Planet обект и го свързваме с Alien
			var alien = new Alien
			{
				Name = "Zorg",
				Planet = new Planet { Name = "Earth" }
			};

			// Проверка дали Planet е правилно асоцииран с Alien
			Assert.AreEqual("Earth", alien.Planet.Name);
		}
	}
}
