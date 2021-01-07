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

		[HttpGet]
		[ActionName("EditPlayer")]
		//[Route("{playerGuid}")]
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

		public IActionResult PlayersList()
		{
			//call BusinessLayer method that returns a List<PLayerViewModel>
			List<PlayerViewModel> playerViewModelList = _businessLogicClass.PlayersList();
			//render the List View
			return View(playerViewModelList);
		}

		[HttpGet]
		[ActionName("PlayerDetails")]
		//[Route("{playerGuid}")]
		public IActionResult PlayerDetails(Guid playerGuid)
		{
			PlayerViewModel playerViewModel = _businessLogicClass.EditPlayer(playerGuid);
			return View("DisplayPlayerDetails", playerViewModel);
		}

		[HttpGet]
		[ActionName("DeletePlayer")]
		public IActionResult DeletePlayer(Guid playerGuid)
		{
			// verify that the player exists
			bool exists = _businessLogicClass.CheckPlayerExists(playerGuid);

			if (!exists)
			{
				//ModelState. ("Failure", "This player doesnt exist.");
				//TempData["PlayerError"] = "This Player does not exist";
				ModelState.AddModelError("Failure", "Wrong Username and password combination!");
				return RedirectToAction("PlayersList");
			}
			// send the player to be deleted
			else
			{
				bool success = _businessLogicClass.DeletePlayerById(playerGuid);
				if (success)
				{
					return RedirectToAction("PlayersList");
				}
				else
				{
					ModelState.AddModelError("Failure", "Unable to delete that player");
					return View("Error");
				}
			}
		}
	}// end of class
}// end of namespace
