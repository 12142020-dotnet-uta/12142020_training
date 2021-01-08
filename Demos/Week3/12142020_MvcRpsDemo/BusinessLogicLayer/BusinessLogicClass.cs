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
			player.ByteArrayImage = _mapperClass.ConvertIformFileToByteArray(playerViewModel.IformFileImage);  //call the mapper class method ot convert the iformfile to byte[]

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

		public MatchViewModel PlayGame(Guid playerId)
		{
			// create a match
			MatchViewModel matchViewModel = new MatchViewModel();
			//get the player and computer to assign to the Match
			Player player2 = _repository.GetPlayerById(playerId);
			PlayerViewModel Player2ViewModel = _mapperClass.ConvertPlayerToPlayerViewModel(player2);
			matchViewModel.Player2 = Player2ViewModel.playerId;

			// get the player1(computer) and player1ViewModel (computer)
			Player player1 = _repository.GetPlayer1_TheComputer();
			PlayerViewModel Player1ViewModel = _mapperClass.ConvertPlayerToPlayerViewModel(player1);
			matchViewModel.Player1 = Player1ViewModel.playerId;

			// get the first round to allow the player to choose a choice to be put in to their slot.
			matchViewModel.Rounds.Add(GetNextRound());

			// we'll also get the computers choice before sending it to the user to choose their Choice.
			matchViewModel.Rounds[matchViewModel.Rounds.Count - 1].Player1Choice = GetComputerChoice();

			return matchViewModel;
		}

		/// <summary>
		/// This method is for an ongoing game. It takes a game object, evaluates of the game has been won.
		/// If so, the game is updated to reflect the winner. 
		/// It not, the game is updated to reflect the latest round winner, and a new round is prepared with a computer Choice and returned.
		/// </summary>
		/// <param name="matchViewModel"></param>
		/// <returns></returns>
		public MatchViewModel PlayingGame(MatchViewModel matchViewModel)
		{
			//first decide on a round winner by calling EvaluateRound(match) which will evaluate the round winner AND update the match
			MatchViewModel match1 = EvaluateRound(matchViewModel);

			//if there is a match winner, return the commpleted match.
			if (matchViewModel.p1RoundWins == 2 || matchViewModel.p2RoundWins == 2)
			{
				return matchViewModel;
			}
			else // if there is no winner yet, get another round and return.
			{
				matchViewModel.Rounds.Add(GetNextRound());
				// we'll also get the computers choice before sending it to the user to choose their Choice.
				matchViewModel.Rounds[matchViewModel.Rounds.Count - 1].Player1Choice = GetComputerChoice();
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


		private MatchViewModel EvaluateRound(MatchViewModel match)
		{
			//Round round = new Round();
			//round.Player1Choice = computerChoice;
			//round.Player2Choice = userChoice;
			Choice player1Choice = match.Rounds[match.Rounds.Count - 1].Player1Choice;
			Choice player2Choice = match.Rounds[match.Rounds.Count - 1].Player2Choice;

			if (player1Choice == player2Choice)   // is the playes tied
			{
				match.Rounds[match.Rounds.Count - 1].WinningPlayer = new Player("TieGame", "TieGame");
				//rounds.Add(round);
				//match.Rounds.Add(round);
				match.RoundWinner(null); // send in the player who won. empty args means a tie round
			}
			else if (((int)player2Choice == 1 && (int)player1Choice == 0) || // if the user won
				((int)player2Choice == 2 && (int)player1Choice == 1) ||
				((int)player2Choice == 0 && (int)player1Choice == 2))
			{
				match.Rounds[match.Rounds.Count - 1].WinningPlayer = _repository.GetPlayerById(match.Player2);
				//rounds.Add(round);
				//match.Rounds.Add(round);
				match.RoundWinner(match.Player2);
			}
			else
			{
				match.Rounds[match.Rounds.Count - 1].WinningPlayer = _repository.GetPlayerById(match.Player2);
				//rounds.Add(round);
				//match.Rounds.Add(round);
				match.RoundWinner(match.Player1);
			}

			// wait to save the game till the user clicks to save the game after the game is over.
			// DbContext.SaveChanges();
			return match;
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
