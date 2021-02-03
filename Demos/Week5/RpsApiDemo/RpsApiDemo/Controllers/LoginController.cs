using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http.Cors;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer;
using ModelLayer.ViewModels;

namespace RpsApiDemo.Controllers
{
	[ApiController]
	[Route("Login")]
	public class LoginController : Controller
	{
		private BusinessLogicClass _businessLogicClass;
		private readonly ILogger<LoginController> _logger;
		public LoginController(BusinessLogicClass businessLogicClass, ILogger<LoginController> logger)
		{
			_businessLogicClass = businessLogicClass;
			_logger = logger;
		}

		//[EnableCors("AllowOrigin")]
		[HttpGet("Login")]
		public IActionResult Login()
		{
			Player p = new Player()
			{
				Fname = "Pudge",
				Lname = "Rodriguez"
			};

			//p = null;
			if (p == null)
			{
				return NotFound(p);
			}
			else
			{
				return Ok(p);
			}
		}

		//[EnableCors("policy1")]
		[HttpPost("LoginPlayer")]
		public ActionResult Login(LoginPlayerViewModel loginPlayerViewModel)
		{
			// instead of doing logic here, call a method in the business logic 
			// layer to create the player, persist to the Db, and return a player to display.
			// use DI (Dependency Injection) to get an instance to the business class and access to itds functionality.
			PlayerViewModel playerViewModel = _businessLogicClass.LoginPlayer(loginPlayerViewModel);

			//_logger.LogInformation($"The LogininPlayer() returned NUll");

			///do things to log in....
			return Ok(playerViewModel);
		}
	}
}
