using System;
using System.Collections.Generic;
using ModelLayer;
using ModelLayer.ViewModels;
using RepositoryLayer;

namespace BusinessLogicLayer
{
	public class BusinessLogicClass
	{
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
			if (success != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}// end of class
}// end of namespace
