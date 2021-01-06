using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
	public class Repository
	{
		private readonly DbContextClass _dbContext;
		public Repository(DbContextClass dbContextClass)
		{
			_dbContext = dbContextClass;
		}


	}
}
