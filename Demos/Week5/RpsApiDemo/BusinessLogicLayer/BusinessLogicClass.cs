using System;
using System.Collections.Generic;
using ModelLayer;
using ModelLayer.ViewModels;
using RepositoryLayer;

namespace BusinessLogicLayer
{
	public class BusinessLogicClass
	{
		int numberOfChoices = Enum.GetNames(typeof(Choice)).Length; // get a always-current number of options of Enum Choice
		Random randomNumber = new Random((int)DateTime.Now.Millisecond); // create a random number object
		private readonly Repository _repository;
		private readonly MapperClass _mapperClass;
		public BusinessLogicClass(Repository repository, MapperClass mapperClass)
		{
			_repository = repository;
			_mapperClass = mapperClass;
		}

		/// <summary>
		/// takes a LoginPlayerViewModel instance and returns a PlayerViewModel Instance
		/// </summary>
		/// <returns></returns>
		public PlayerViewModel LoginPlayer(LoginPlayerViewModel loginPlayerViewModel)
		{
			// have all logic confined to this Business layer.
			Player player = new Player()
			{
				Fname = loginPlayerViewModel.Fname,
				Lname = loginPlayerViewModel.Lname
			};

			Player player1 = _repository.LoginPlayer(player);

			//convert the Player to a PlayerViewModel
			PlayerViewModel playerViewModel = _mapperClass.ConvertPlayerToPlayerViewModel(player1);
			return playerViewModel;
		}

		public PlayerViewModel EditPlayer(Guid playerId)
		{
			// call a method in Repository that will return a player based on his Id.
			Player player = _repository.GetPlayerById(playerId);

			// map the player to a PlayerViewModel
			PlayerViewModel playerViewModel = _mapperClass.ConvertPlayerToPlayerViewModel(player);

			return playerViewModel;

		}

		public PlayerViewModel EditedPlayer(PlayerViewModel playerViewModel)
		{
			// get an instance of the player being edited.
			Player player = _repository.GetPlayerById(playerViewModel.playerId);

			player.Fname = playerViewModel.Fname;
			player.Lname = playerViewModel.Lname;
			player.numLosses = playerViewModel.numLosses;
			player.numWins = playerViewModel.numWins;

			if (playerViewModel.IformFileImage != null)
			{
				player.ByteArrayImage = _mapperClass.ConvertIformFileToByteArray(playerViewModel.IformFileImage);  //call the mapper class method ot convert the iformfile to byte[]
			}


			Player player1 = _repository.EditPlayer(player);
			PlayerViewModel playerViewModel1 = _mapperClass.ConvertPlayerToPlayerViewModel(player1);
			return playerViewModel1;
		}

		public List<PlayerViewModel> PlayersList()
		{
			//call Repo method to get a List<Player>
			List<Player> playerList = _repository.PlayerList();


			//convert that List<Player> to List<PlayerViewModel>
			List<PlayerViewModel> playerViewModelList = new List<PlayerViewModel>();
			foreach (Player p in playerList)
			{
				playerViewModelList.Add(_mapperClass.ConvertPlayerToPlayerViewModel(p));
			}

			return playerViewModelList;
		}

		public bool CheckPlayerExists(Guid playerGuid)
		{
			bool exists = _repository.CheckPlayerExists(playerGuid);
			return exists;
		}

		public bool DeletePlayerById(Guid playerGuid)
		{
			bool success = _repository.DeletePlayerById(playerGuid);
			if (success)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// takes a playerId and creates a Match object.
		/// It then saves the match object with both players to the Db for retrieval later.
		/// </summary>
		/// <param name="playerId"></param>
		/// <returns></returns>
		public MatchViewModel PlayGame(Guid playerId)
		{
			// create a  Match and populate it with the players
			Match match = new Match()
			{
				Player1 = _repository.GetPlayer1_TheComputer(),
				Player2 = _repository.GetPlayerById(playerId)
			};
			// save the empty Match to the Db
			bool uniqueMatchId = _repository.SaveNewMatch(match);
			if (uniqueMatchId == false)
			{
				throw new Exception("This Match Guid already exists.");
			}
			// create an empty matchview model
			MatchViewModel matchViewModel = new MatchViewModel()
			{
				matchId = match.matchId, // set the matchViewModel.matchId the same as the match in the Db
				Player1 = _repository.GetPlayer1_TheComputer().playerId,
				Player2 = _repository.GetPlayerById(playerId).playerId
			};
			//get the player and computer to assign to the Match
			//Player player2 = _repository.GetPlayerById(playerId);
			//PlayerViewModel Player2ViewModel = _mapperClass.ConvertPlayerToPlayerViewModel(player2);
			//matchViewModel.Player2 = Player2ViewModel.playerId;

			// get the player1(computer) and player1ViewModel (computer)
			//Player player1 = _repository.GetPlayer1_TheComputer();
			//PlayerViewModel Player1ViewModel = _mapperClass.ConvertPlayerToPlayerViewModel(player1);
			//matchViewModel.Player1 = Player1ViewModel.playerId;

			// get the first round to allow the player to choose a choice to be put in to their slot.
			//matchViewModel.Rounds.Add(GetNextRound());

			// we'll also get the computers choice before sending it to the user to choose their Choice.
			//matchViewModel.Rounds[matchViewModel.Rounds.Count - 1].Player1Choice = GetComputerChoice();

			return matchViewModel;
		}

		/// <summary>
		/// This method is for an ongoing game. It takes a gameViewModel object containing the MatchID, and both playerIDs, and the users Choice.
		/// Then creates a....
		/// If so, the game is updated to reflect the winner. 
		/// It not, the game is updated to reflect the latest round winner, and a new round is prepared with a computer Choice and returned.
		/// </summary>
		/// <param name="matchViewModel"></param>
		/// <returns></returns>
		public MatchViewModel PlayingGame(MatchViewModel matchViewModel)
		{
			//first decide on a round winner by calling EvaluateRound(match) which will evaluate the round winner AND update the match
			matchViewModel = EvaluateRound(matchViewModel);

			//if there is a match winner, return the commpleted match.
			if (matchViewModel.MatchWinner() != null)
			{
				// this is where you would fully populate the matchViewModel to display the complete game to the user.
				Player p1 = _repository.GetPlayerById(matchViewModel.Player1);
				Player p2 = _repository.GetPlayerById(matchViewModel.Player2);
				matchViewModel.Player1Fname = p1.Fname;
				matchViewModel.Player1Lname = p1.Lname;
				matchViewModel.Player2Fname = p2.Fname;
				matchViewModel.Player2Lname = p2.Lname;

				return matchViewModel;
			}
			else // if there is no winner yet, get another round and return.
			{
				//matchViewModel.Rounds.Add(GetNextRound());
				// we'll also get the computers choice before sending it to the user to choose their Choice.
				//matchViewModel.Rounds[matchViewModel.Rounds.Count - 1].Player1Choice = GetComputerChoice();

				return matchViewModel;
			}
		}

		/// <summary>
		/// returns a random Choice Enum based on the numebr of Choices available
		/// </summary>
		/// <returns></returns>
		private Choice GetComputerChoice()
		{
			return (Choice)randomNumber.Next(numberOfChoices);
		}

		/// <summary>
		/// returns an empty Round
		/// </summary>
		/// <returns></returns>
		private Round GetNextRound()
		{
			// create a Round, complete with a computer choice already made
			Round round = new Round();
			return round;
		}

		private MatchViewModel EvaluateRound(MatchViewModel matchViewModel)
		{
			// create a Round and set the Choices for both players
			Round round = GetNextRound();
			round.MatchId = matchViewModel.matchId;
			round.Player1Choice = GetComputerChoice();
			round.Player2Choice = matchViewModel.Player2Choice;

			// get the Match fro the Db to update along with the matchViewModel
			Match match = _repository.GetMatchById(matchViewModel.matchId);

			//manually populating the match players bc the context isn't returning them with the match object.
			//match.Player1 = _repository.GetPlayer1_TheComputer();
			//match.Player2 = _repository.GetPlayerById(matchViewModel.Player2);

			if (round.Player1Choice == round.Player2Choice)   // did the players tie?
			{
				// update the Match stats
				matchViewModel.RoundWinner(); // send in the player who won. empty args means a tie round
				match.RoundWinner();
			}
			else if (((int)round.Player2Choice == 1 && (int)round.Player1Choice == 0) || // if the user (Player2) won
				((int)round.Player2Choice == 2 && (int)round.Player1Choice == 1) ||
				((int)round.Player2Choice == 0 && (int)round.Player1Choice == 2))
			{
				round.WinningPlayer = _repository.GetPlayerById(matchViewModel.Player2); // set the winning player of the round
				matchViewModel.RoundWinner(matchViewModel.Player2);
				match.RoundWinner(matchViewModel.Player2);
			}
			else // Computer won
			{
				round.WinningPlayer = _repository.GetPlayerById(matchViewModel.Player1);
				matchViewModel.RoundWinner(matchViewModel.Player1);
				match.RoundWinner(matchViewModel.Player1);
			}


			matchViewModel.Rounds.Add(round);   // add the round to the matchViewModel
			_repository.AddRound(round);        // save the round to the Db
			match.Rounds.Add(round);            // add the round to the match in the Db
			_repository.SaveChanges();

			//transfer over the status of the game bc the matchViewModel doesn't save that stat between rounds.
			matchViewModel.p1RoundWins = match.p1RoundWins;
			matchViewModel.p2RoundWins = match.p2RoundWins;
			matchViewModel.ties = match.ties;

			return matchViewModel;
		}

		/// <summary>
		/// Takes a completed Game and finalizes it. 
		/// Then the Game is saved to the DataBase.
		/// </summary>
		public void SaveGame()
		{
			//call UpdateWinLossRecords(match)

			// Convert the MatchViewModel into a Match

			//save teh Match to the Db.

		}

		/// <summary>
		/// Updates the win/Loss records of both players in the match  after the user chooses to save the game.
		/// </summary>
		/// <param name="match"></param>
		public void UpdateWinLossRecords(Match match)
		{
			if (match.MatchWinner().playerId == match.Player1.playerId)
			{
				match.Player1.AddWin();
				match.Player2.AddLoss();
			}
			else if (match.MatchWinner().playerId == match.Player2.playerId)
			{
				match.Player2.AddWin();
				match.Player1.AddLoss();
			}

			// make sure to call Repo layer to manually update the player and save it to the Db.
		}

	}// end of class
}// end of namespace
