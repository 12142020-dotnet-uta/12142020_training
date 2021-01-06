using System;
using RepositoryLayer;

namespace BusinessLogicLayer
{
	public class BusinessLogicClass
	{
		private readonly Repository _repository;
		public BusinessLogicClass(Repository repository)
		{
			_repository = repository;
		}

	}
}
