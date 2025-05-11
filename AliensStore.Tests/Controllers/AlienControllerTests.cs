using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AliensStore.Data;
using AliensStore.Data.Entity;

namespace AliensStore.Controllers
{
	public class AliensController : Controller
	{
		private readonly ApplicationDbContext _context;

		// Конструкторът на контролера инициализира контекста на базата данни
		public AliensController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Aliens
		// Метод за извеждане на всички извънземни
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Alien.Include(a => a.Dealer).Include(a => a.Planet);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Aliens/Details/5
		// Метод за извеждане на детайли за конкретно извънземно
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound(); // Връща NotFound ако ID-то е null
			}

			var alien = await _context.Alien
				.Include(a => a.Dealer)
				.Include(a => a.Planet)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (alien == null)
			{
				return NotFound(); // Връща NotFound ако извънземното не е намерено
			}

			return View(alien); // Връща изглед с данните за извънземното
		}

		// GET: Aliens/Create
		// Метод за показване на формата за създаване на ново извънземно
		public IActionResult Create()
		{
			// Попълва dropdown листите за Изпълнители и Планети
			ViewData["DealerId"] = new SelectList(_context.Set<Dealer>(), "Id", "Id");
			ViewData["PlanetId"] = new SelectList(_context.Planet, "Id", "Id");
			return View();
		}

		// POST: Aliens/Create
		// Метод за обработка на данните от формата за създаване на ново извънземно
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Price,CoinsPerDay,Age,Color,Legs,Arms,Eyes,IsForSale,PlanetId,DealerId,ImageUrl,Id")] Alien alien)
		{
			if (ModelState.IsValid)
			{
				_context.Add(alien); // Добавя новото извънземно в контекста
				await _context.SaveChangesAsync(); // Записва промените в базата данни
				return RedirectToAction(nameof(Index)); // Пренасочва към списъка с извънземни
			}
			// Ако има грешки в моделния статус, връща изгледа с формата
			ViewData["DealerId"] = new SelectList(_context.Set<Dealer>(), "Id", "Id", alien.DealerId);
			ViewData["PlanetId"] = new SelectList(_context.Planet, "Id", "Id", alien.PlanetId);
			return View(alien);
		}

		// GET: Aliens/Edit/5
		// Метод за показване на формата за редактиране на извънземно
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound(); // Връща NotFound ако ID-то е null
			}

			var alien = await _context.Alien.FindAsync(id);
			if (alien == null)
			{
				return NotFound(); // Връща NotFound ако извънземното не е намерено
			}
			// Попълва dropdown листите за Изпълнители и Планети
			ViewData["DealerId"] = new SelectList(_context.Set<Dealer>(), "Id", "Id", alien.DealerId);
			ViewData["PlanetId"] = new SelectList(_context.Planet, "Id", "Id", alien.PlanetId);
			return View(alien); // Връща изгледа с данни за извънземното
		}

		// POST: Aliens/Edit/5
		// Метод за обработка на данните от формата за редактиране на извънземно
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Name,Price,CoinsPerDay,Age,Color,Legs,Arms,Eyes,IsForSale,PlanetId,DealerId,ImageUrl,Id")] Alien alien)
		{
			if (id != alien.Id)
			{
				return NotFound(); // Връща NotFound ако ID-то не съвпада
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(alien); // Актуализира извънземното в контекста
					await _context.SaveChangesAsync(); // Записва промените в базата данни
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AlienExists(alien.Id))
					{
						return NotFound(); // Връща NotFound ако извънземното не съществува
					}
					else
					{
						throw; // Изхвърля изключение ако има друга грешка
					}
				}
				return RedirectToAction(nameof(Index)); // Пренасочва към списъка с извънземни
			}
			// Ако има грешки в моделния статус, връща изгледа с формата
			ViewData["DealerId"] = new SelectList(_context.Set<Dealer>(), "Id", "Id", alien.DealerId);
			ViewData["PlanetId"] = new SelectList(_context.Planet, "Id", "Id", alien.PlanetId);
			return View(alien);
		}

		// GET: Aliens/Delete/5
		// Метод за показване на потвърждаващото съобщение за изтриване на извънземно
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound(); // Връща NotFound ако ID-то е null
			}

			var alien = await _context.Alien
				.Include(a => a.Dealer)
				.Include(a => a.Planet)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (alien == null)
			{
				return NotFound(); // Връща NotFound ако извънземното не е намерено
			}

			return View(alien); // Връща изглед с данните за извънземното
		}

		// POST: Aliens/Delete/5
		// Метод за потвърждаване на изтриването на извънземно
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var alien = await _context.Alien.FindAsync(id);
			if (alien != null)
			{
				_context.Alien.Remove(alien); // Премахва извънземното от контекста
			}

			await _context.SaveChangesAsync(); // Записва промените в базата данни
			return RedirectToAction(nameof(Index)); // Пренасочва към списъка с извънземни
		}

		// Проверява дали извънземното с даденото ID съществува в базата данни
		private bool AlienExists(int id)
		{
			return _context.Alien.Any(e => e.Id == id);
		}
	}
}
