using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.ViewModels;

namespace _12142020_MvcRpsDemo.Controllers
{
	public class PlayerController : Controller
	{
		private BusinessLogicClass _businessLogicClass;
		private readonly ILogger<PlayerController> _logger;

		public PlayerController(BusinessLogicClass businessLogicClass, ILogger<PlayerController> logger)
		{
			_businessLogicClass = businessLogicClass;
			_logger = logger;
		}
		// GET: PlayerController
		public ActionResult Index()
		{
			return View();
		}

		// GET: PlayerController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: PlayerController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: PlayerController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: PlayerController/Edit/5
		[Route("{playerGuid}")]
		public ActionResult EditPlayer(Guid playerGuid)
		{
			// call a method on BusinessLogic Layer that will take a playerId and return a PlayerView Model
			PlayerViewModel playerViewModel = _businessLogicClass.EditPlayer(playerGuid);
			return View(playerViewModel);
		}

		[HttpPost]
		[ActionName("EditedPlayer")]
		public ActionResult EditPlayer(PlayerViewModel playerViewModel)
		{
			if (!ModelState.IsValid) { return View(playerViewModel); }

			// call a method on BusinessLogic Layer that will take a playerId and return a PlayerView Model
			PlayerViewModel playerViewModel1 = _businessLogicClass.EditedPlayer(playerViewModel);
			return View("DisplayPlayerDetails", playerViewModel1);
		}

		// POST: PlayerController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: PlayerController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: PlayerController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
