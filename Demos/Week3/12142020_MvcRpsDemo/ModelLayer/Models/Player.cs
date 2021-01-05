using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
	public class Player
	{
		//constructor
		public Player(string fname = "null", string lname = "null")
		{
			this.Fname = fname;
			this.Lname = lname;
		}

		[Key]
		public Guid playerId { get; set; } = Guid.NewGuid();

		[StringLength(20, ErrorMessage = "The first name must be from 3 to 20 characters.", MinimumLength = 3)]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
		[Required]
		[Display(Name = "First Name")]
		public string Fname { get; set; }


		[StringLength(20, ErrorMessage = "The last name must be from 3 to 20 characters.", MinimumLength = 3)]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
		[Required]
		[Display(Name = "First Name")]
		public string Lname { get; set; }

		[Range(0, int.MaxValue)]
		[Display(Name = "Number of wins")]
		public int numWins { get; set; }

		[Range(0, int.MaxValue)]
		[Display(Name = "Number of losses")]
		public int numLosses { get; set; }

		public byte[] ByteArrayImage { get; set; }

		//public string StringImage { get; set; }



		//below is methods
		/// <summary>
		/// This method inrements the Wins or the player
		/// </summary>
		public void AddWin()
		{
			numWins++;
		}

		/// <summary>
		/// This methods increments the wins of the player by the passed integer amount.
		/// </summary>
		/// <param name="x"></param>
		public void AddWin(int x)
		{
			numWins += x;
		}

		public void AddLoss()
		{
			numLosses++;
		}

		public int[] GetWinLossRecord()
		{
			int[] winsAndLosses = new int[2]; // create an array to hole the num of wins and losses

			winsAndLosses[0] = numWins; // put in the wins and losses
			winsAndLosses[1] = numLosses;

			return winsAndLosses; // return the array.
		}





	}
}