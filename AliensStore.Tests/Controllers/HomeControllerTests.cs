using AliensStore.Controllers;
using AliensStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace AliensStore.Tests.Controllers
{
	[TestFixture]
	public class HomeControllerTests
	{
		private HomeController _controller;
		private Mock<ILogger<HomeController>> _loggerMock;

		[SetUp]
		public void Setup()
		{
			// Създаваме mock на ILogger, за да избегнем реална зависимост
			_loggerMock = new Mock<ILogger<HomeController>>();

			// Инициализираме контролера с mock логер
			_controller = new HomeController(_loggerMock.Object);
		}

		[Test]
		public void Index_ReturnsViewResult()
		{
			// Действа: Извиква се методът Index
			var result = _controller.Index();

			// Проверка: Очакваме резултатът да е ViewResult
			Assert.IsInstanceOf<ViewResult>(result);
		}

		[Test]
		public void Privacy_ReturnsViewResult()
		{
			// Действа: Извиква се методът Privacy
			var result = _controller.Privacy();

			// Проверка: Очакваме резултатът да е ViewResult
			Assert.IsInstanceOf<ViewResult>(result);
		}

		[Test]
		public void Error_ReturnsViewResult_WithErrorViewModel()
		{
			// Действа: Извиква се методът Error
			var result = _controller.Error() as ViewResult;

			// Проверка: Резултатът трябва да е ViewResult
			Assert.IsNotNull(result);

			// Проверка: Моделът в ViewResult трябва да е от тип ErrorViewModel
			Assert.IsInstanceOf<ErrorViewModel>(result.Model);

			// Допълнителна проверка: RequestId не трябва да е null
			var model = result.Model as ErrorViewModel;
			Assert.IsNotNull(model.RequestId);
		}

		[TearDown]
		public void TearDown()
		{
			_controller.Dispose();
		}
	}
}
