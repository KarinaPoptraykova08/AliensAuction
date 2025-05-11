using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using AliensStore.Data;
using AliensStore.Controllers;
using AliensStore.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.InMemory;


namespace AliensStore.Tests.Controllers
{
	public class GalaxiesControllerTests
	{
		private ApplicationDbContext _context;
		private GalaxiesController _controller;

		// Изпълнява се преди всеки тест – настройка на in-memory база и контролер
		[SetUp]
		public void Setup()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase("GalaxiesTestDb") // Използваме InMemory база
				.Options;

			_context = new ApplicationDbContext(options);

			// Добавяме тестова галактика в базата
			_context.Galaxy.Add(new Galaxy { Id = 1, Name = "Milky Way" });
			_context.SaveChanges();

			_controller = new GalaxiesController(_context); // Създаваме контролера
		}

		// Изпълнява се след всеки тест – изчистване на ресурсите
		[TearDown]
		public void Teardown()
		{
			_controller?.Dispose();
			_context.Database.EnsureDeleted(); // Изтриваме базата
			_context.Dispose(); // Освобождаваме ресурсите
		}

		// Тест: Проверка дали Index() връща списък с галактики
		[Test]
		public async Task Index_ReturnsViewWithGalaxies()
		{
			var result = await _controller.Index() as ViewResult;

			Assert.IsNotNull(result); // Проверяваме дали резултатът не е null
			var model = result.Model as List<Galaxy>;
			Assert.IsNotNull(model);
			Assert.AreEqual(1, model.Count); // Очакваме една галактика
			Assert.AreEqual("Milky Way", model[0].Name);
		}

		// Тест: Details с валидно ID трябва да върне галактика
		[Test]
		public async Task Details_ValidId_ReturnsGalaxy()
		{
			var result = await _controller.Details(1) as ViewResult;

			Assert.IsNotNull(result);
			var galaxy = result.Model as Galaxy;
			Assert.IsNotNull(galaxy);
			Assert.AreEqual("Milky Way", galaxy.Name);
		}

		// Тест: Details с невалидно ID трябва да върне NotFound
		[Test]
		public async Task Details_InvalidId_ReturnsNotFound()
		{
			var result = await _controller.Details(99);

			Assert.IsInstanceOf<NotFoundResult>(result);
		}

		// Тест: Create (GET) трябва да върне View
		[Test]
		public void Create_Get_ReturnsView()
		{
			var result = _controller.Create();
			Assert.IsInstanceOf<ViewResult>(result);
		}

		// Тест: Create (POST) с валиден модел трябва да пренасочи към Index
		[Test]
		public async Task Create_Post_ValidModel_RedirectsToIndex()
		{
			var newGalaxy = new Galaxy { Name = "Andromeda" };

			var result = await _controller.Create(newGalaxy) as RedirectToActionResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName); // Очакваме пренасочване

			Assert.AreEqual(2, _context.Galaxy.Count()); // Общо 2 галактики
		}

		// Тест: Edit (GET) трябва да върне съществуваща галактика
		[Test]
		public async Task Edit_Get_ValidId_ReturnsViewWithGalaxy()
		{
			var result = await _controller.Edit(1) as ViewResult;

			Assert.IsNotNull(result);
			var galaxy = result.Model as Galaxy;
			Assert.AreEqual("Milky Way", galaxy.Name);
		}

		// Тест: Edit (POST) с валиден модел трябва да актуализира обекта
		[Test]
		public async Task Edit_Post_ValidModel_UpdatesGalaxy()
		{
			var updatedGalaxy = new Galaxy { Id = 1, Name = "Milky Way Updated" };

			var result = await _controller.Edit(1, updatedGalaxy) as RedirectToActionResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName);

			var galaxy = await _context.Galaxy.FindAsync(1);
			Assert.AreEqual("Milky Way Updated", galaxy.Name);
		}

		// Тест: Delete (GET) трябва да зареди потвърждение за изтриване
		[Test]
		public async Task Delete_Get_ValidId_ReturnsViewWithGalaxy()
		{
			var result = await _controller.Delete(1) as ViewResult;

			Assert.IsNotNull(result);
			var galaxy = result.Model as Galaxy;
			Assert.AreEqual("Milky Way", galaxy.Name);
		}

		// Тест: DeleteConfirmed трябва да изтрие галактиката и да пренасочи
		[Test]
		public async Task DeleteConfirmed_RemovesGalaxyAndRedirects()
		{
			var result = await _controller.DeleteConfirmed(1) as RedirectToActionResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName);
			Assert.AreEqual(0, _context.Galaxy.Count()); // Очакваме 0 галактики
		}
	}
}
