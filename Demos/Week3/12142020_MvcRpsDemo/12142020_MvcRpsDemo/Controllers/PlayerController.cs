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
				ModelState.AddModelError("Failure", "That Player was not found.");
				ModelState.AddModelError("Try Again", "Please select another player.");
				ModelState.AddModelError("But really", "I repeat, Please select another player!");

				//return RedirectToAction("PlayersList");
				//call BusinessLayer method that returns a List<PLayerViewModel>
				List<PlayerViewModel> playerViewModelList = _businessLogicClass.PlayersList();
				//render the List View
				return View("PlayersList", playerViewModelList);
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


		[HttpGet]
		[ActionName("PlayGame")]
		public IActionResult PlayGame(Guid playerGuid)
		{
			MatchViewModel matchViewModel = _businessLogicClass.PlayGame(playerGuid);
			return View(matchViewModel);
		}

		[ActionName("playnextround")]
		public IActionResult playnextround(MatchViewModel matchViewModel)
		{
			// after each round, send the game to playinggame() for it to evaluate round winner. it will send back either the game with a new round ready or a completed game object
			MatchViewModel matchViewModel1 = _businessLogicClass.PlayingGame(matchViewModel);

			// check if either player has 2 wins . if so render the view to play the next round.

			// if it returns a winner render the completedgameview

			// if there is a winner, render the winner View
			if (matchViewModel.p1RoundWins == 2)
			{
				ViewBag["winner"] = $"The winner is {matchViewModel.Player1Fname} {matchViewModel.Player1Lname}!";
				return View("GameOver", matchViewModel);
			}
			else if (matchViewModel.p2RoundWins == 2)
			{
				ViewBag["winner"] = $"The winner is {matchViewModel.Player2Fname} {matchViewModel.Player2Lname}!";
				return View("GameOver", matchViewModel);
			}
			else
			{
				return View(matchViewModel);
			}

		}


	}// end of class
}// end of namespace
