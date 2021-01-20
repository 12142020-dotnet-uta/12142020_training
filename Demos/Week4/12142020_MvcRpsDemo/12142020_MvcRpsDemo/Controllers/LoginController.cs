using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;

namespace _12142020_MvcRpsDemo.Controllers
{
	public class LoginController : Controller
	{
		private BusinessLogicClass _businessLogicClass;
		private readonly ILogger<LoginController> _logger;
		public LoginController(BusinessLogicClass businessLogicClass, ILogger<LoginController> logger)
		{
			_businessLogicClass = businessLogicClass;
			_logger = logger;
		}

		// GET: LoginController
		//[ActionName("Login")]
		public ActionResult Login()
		{
			return View();
		}

		[ActionName("LoginPlayer")]
		public ActionResult Login(LoginPlayerViewModel loginPlayerViewModel)
		{
			// instead of doing logic here, call a method in the business logic 
			// layer to create teh player, persist to the Db, and return a player to display.
			// use DI (Dependency Injection) to get an instance to the business class and access to itds functionality.
			PlayerViewModel playerViewModel = _businessLogicClass.LoginPlayer(loginPlayerViewModel);

			//_logger.LogInformation($"The LogininPlayer() returned NUll");

			///do things to log in....
			return View("DisplayPlayerDetails", playerViewModel);
		}


		// GET: LoginController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: LoginController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: LoginController/Create
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

		// GET: LoginController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: LoginController/Edit/5
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

		// GET: LoginController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: LoginController/Delete/5
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
