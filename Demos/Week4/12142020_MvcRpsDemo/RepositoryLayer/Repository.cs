using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer;

namespace RepositoryLayer
{
	public class Repository
	{
		private readonly ILogger<Repository> _logger;

		private readonly DbContextClass _dbContext;
		DbSet<Player> players;
		DbSet<Match> matches;
		DbSet<Round> rounds;

		public Repository(DbContextClass dbContextClass, ILogger<Repository> logger)
		{
			_dbContext = dbContextClass;
			this.players = _dbContext.players;
			this.matches = _dbContext.matches;
			this.rounds = _dbContext.rounds;
			_logger = logger;
		}

		public Player LoginPlayer(Player player)
		{
			Player player1 = players.FirstOrDefault(x => x.Fname == player.Fname && x.Lname == player.Lname);// check if the player is in the Db

			if (player1 == null)// create new player if none exists
			{
				player1 = new Player()
				{
					Fname = player.Fname,
					Lname = player.Lname
				};
				players.Add(player1);// ass new player
				_dbContext.SaveChanges();// save new player to Db

				try
				{
					Player player2 = players.FirstOrDefault(x => x.playerId == player1.playerId);// check if the player is in the Db
					return player2;
				}
				catch (ArgumentNullException ex)
				{
					_logger.LogInformation($"Saving a player to the Db threw an error, {ex}");
				}
			}
			return player1;
		}

		public Player GetPlayerById(Guid playerId)
		{
			Player player = players.FirstOrDefault(x => x.playerId == playerId);// check if the player is in the Db
			return player;
		}

		/// <summary>
		/// Takes a Player and returns the edited version of the Player after saving it to the Db.
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		public Player EditPlayer(Player player)
		{
			// search Db for the player
			Player player1 = GetPlayerById(player.playerId);

			// transfer over all the new values
			player1.Fname = player.Fname;
			player1.Lname = player.Lname;
			player1.numLosses = player.numLosses;
			player1.numWins = player.numWins;
			player1.ByteArrayImage = player.ByteArrayImage;
			_dbContext.SaveChanges();

			// search the player again to verify that the new player is in the Db
			Player player2 = GetPlayerById(player.playerId);
			// return the edited Player
			return player2;
		}


		/// <summary>
		/// returns all match objects in List<Player>
		/// </summary>
		/// <returns></returns>
		public List<Player> PlayerList()
		{
			return players.ToList();
		}

		public bool CheckPlayerExists(Guid playerGuid)
		{
			bool exists = players.Any(x => x.playerId == playerGuid);
			return exists;
		}

		/// <summary>
		/// Takes a PlayerGuid and deletes teh player matching that Guid. Returns TRUE on success, otherwise FALSE
		/// </summary>
		/// <param name="playerGuid"></param>
		/// <returns></returns>
		public bool DeletePlayerById(Guid playerGuid)
		{
			Player player = players.FirstOrDefault(x => x.playerId == playerGuid);
			var success = players.Remove(player);
			_dbContext.SaveChanges();
			if (success != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public Player GetPlayer1_TheComputer()
		{
			//check if the computer (Max HeadRoom) exists
			Player player1 = players.FirstOrDefault(x => x.Fname == "Max" && x.Lname == "HeadRoom");

			// if the computer doesn't exist in the Db already, create Max HeadRoom
			if (player1 != null)
			{
				return player1;
			}
			else
			{
				player1 = new Player()
				{
					Fname = "Max",
					Lname = "HeadRoom"
				};
				return player1;
			}
		}

		/// <summary>
		/// takes a new match instance and saves it to the List<Match> (or context).
		/// If the match already exists, returns false.
		/// </summary>
		/// <returns></returns>
		public bool SaveMatch(Match match)
		{
			//check if the match is already there
			if (!matches.Any(x => x.matchId == match.matchId))
			{
				matches.Add(match);
				_dbContext.SaveChanges();
				return true;
			}
			else return false;
		}

		/// <summary>
		/// adds the completed match to the List<Match> if it ins't already in the List.
		/// returns true if the match was successfully added, false if the matchId already exists
		/// </summary>
		/// <param name="match"></param>
		public bool AddCompletedMatch(Match match)
		{
			if (!matches.Any(x => x.matchId == match.matchId))
			{
				matches.Add(match);
				_dbContext.SaveChanges();
				return true;
			}
			return false;
		}
	}// end of class
}// end of namespace
