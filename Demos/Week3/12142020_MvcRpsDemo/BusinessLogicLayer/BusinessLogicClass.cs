using System;
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

	}// end of class
}// end of namespace
