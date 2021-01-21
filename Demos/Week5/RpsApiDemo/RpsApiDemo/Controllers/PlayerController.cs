using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.ViewModels;

namespace RpsApiDemo.Controllers
{
	[ApiController]
	[Route("Player")]
	public class PlayerController : Controller
	{
		private BusinessLogicClass _businessLogicClass;
		private readonly ILogger<PlayerController> _logger;

		public PlayerController(BusinessLogicClass businessLogicClass, ILogger<PlayerController> logger)
		{
			_businessLogicClass = businessLogicClass;
			_logger = logger;
		}


		[HttpGet("EditPlayer/{playerGuid}")]
		public IActionResult EditPlayer(Guid playerGuid)
		{
			// call a method on BusinessLogic Layer that will take a playerId and return a PlayerView Model
			PlayerViewModel playerViewModel = _businessLogicClass.EditPlayer(playerGuid);
			return Ok(playerViewModel);
		}

		[HttpPost("EditedPlayer")]
		public ActionResult EditPlayer(PlayerViewModel playerViewModel)
		{
			if (!ModelState.IsValid) { return NotFound(); }

			// call a method on BusinessLogic Layer that will take a playerId and return a PlayerView Model
			PlayerViewModel playerViewModel1 = _businessLogicClass.EditedPlayer(playerViewModel);
			return Ok(playerViewModel1);
		}

		[HttpGet("PlayersList")]
		public IActionResult PlayersList()
		{
			//call BusinessLayer method that returns a List<PLayerViewModel>
			List<PlayerViewModel> playerViewModelList = _businessLogicClass.PlayersList();
			//render the List View
			return Ok(playerViewModelList);
		}

		[HttpGet("PlayerDetails/{playerGuid}")]
		public IActionResult PlayerDetails(Guid playerGuid)
		{
			PlayerViewModel playerViewModel = _businessLogicClass.EditPlayer(playerGuid);
			return Ok(playerViewModel);
		}

		[HttpGet]
		[Route("DeletePlayer")]
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


		[HttpGet("PlayGame/{playerGuid}")]
		public IActionResult PlayGame(Guid playerGuid)
		{
			MatchViewModel matchViewModel = _businessLogicClass.PlayGame(playerGuid);
			return View(matchViewModel);
		}

		[HttpPost("playnextround/{matchViewModel}")]
		public IActionResult playnextround(MatchViewModel matchViewModel)
		{
			// after each round, send the game to playinggame() for it to evaluate round winner. 
			// it will send back either the game with a new round ready or a completed game object
			matchViewModel = _businessLogicClass.PlayingGame(matchViewModel);

			// if there is a winner, render the winner View
			if (matchViewModel.p1RoundWins == 2)
			{
				ViewData["winner"] = $"The winner is {matchViewModel.Player1Fname} {matchViewModel.Player1Lname}!";
				return View("GameOver", matchViewModel);
			}
			else if (matchViewModel.p2RoundWins == 2)
			{
				ViewData["winner"] = $"The winner is {matchViewModel.Player2Fname} {matchViewModel.Player2Lname}!";
				return View("GameOver", matchViewModel);
			}
			else
			{
				return View(matchViewModel);
			}
		}
	}//end of class
}//end of namespace
