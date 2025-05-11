using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using AliensStore.Controllers;
using AliensStore.Data;
using AliensStore.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AliensStore.Tests.Controllers
{
	public class PlanetsControllerTests
	{
		private ApplicationDbContext _context;
		private PlanetsController _controller;

		// Метод, който се изпълнява преди всеки тест
		[SetUp]
		public void Setup()
		{
			// Конфигурация на InMemory база данни
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "TestDatabase")
				.Options;

			// Инициализация на контекста
			_context = new ApplicationDbContext(options);

			// Добавяне на тестови данни за галактики и планети
			var galaxy = new Galaxy { Id = 1, Name = "Milky Way" };
			var planet = new Planet { Id = 1, Name = "Earth", GalaxyId = 1, Galaxy = galaxy };

			_context.Galaxy.Add(galaxy);
			_context.Planet.Add(planet);
			_context.SaveChanges();

			// Инициализация на контролера с InMemory контекста
			_controller = new PlanetsController(_context);
		}

		[Test]
		public async Task Index_ReturnsViewWithPlanets()
		{
			// Акт: Извикваме метода Index
			var result = await _controller.Index();

			// Проверка: Резултатът е ViewResult
			Assert.IsInstanceOf<ViewResult>(result);

			// Извличане на модела от ViewResult
			var viewResult = result as ViewResult;
			var model = viewResult.Model as List<Planet>;

			// Проверка: Очакваме в модела да има 1 планета
			Assert.AreEqual(1, model.Count);
			Assert.AreEqual("Earth", model[0].Name);
		}

		[Test]
		public async Task Details_ValidId_ReturnsPlanet()
		{
			// Акт: Извикваме Details с валидно ID
			var result = await _controller.Details(1);

			// Проверка: Резултатът е ViewResult
			Assert.IsInstanceOf<ViewResult>(result);

			// Извличане на модела от ViewResult
			var viewResult = result as ViewResult;
			var planet = viewResult.Model as Planet;

			// Проверка: Името на планетата е "Earth"
			Assert.AreEqual("Earth", planet.Name);
		}

		[Test]
		public async Task Details_InvalidId_ReturnsNotFound()
		{
			// Акт: Извикваме Details с невалидно ID
			var result = await _controller.Details(999);

			// Проверка: Резултатът е NotFoundResult
			Assert.IsInstanceOf<NotFoundResult>(result);
		}

		[TearDown]
		public void TearDown()
		{
			_context.Dispose();
			_controller.Dispose();
		}
	}
}
