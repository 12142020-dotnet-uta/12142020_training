using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
	public class MatchViewModel
	{
		public Guid matchId { get; set; } = Guid.NewGuid();

		//to add
		public Guid Player1 { get; set; }
		public Guid Player2 { get; set; }
		public string Player1Fname { get; set; }
		public string Player1Lname { get; set; }

		public string Player2Fname { get; set; }
		public string Player2Lname { get; set; }

		//public PlayerViewModel Player1 { get; set; } // always the computer
		//public PlayerViewModel Player2 { get; set; } // always the user.

		public List<Round> Rounds = new List<Round>();
		public Choice Player1Choice { get; set; }
		public Choice Player2Choice { get; set; }

		[Range(0, 2)]
		public int p1RoundWins { get; set; } // Who many rounds has the player won?

		[Range(0, 2)]
		public int p2RoundWins { get; set; }

		[Range(0, int.MaxValue)]
		public int ties { get; set; }


		/// <summary>
		/// This method takes an optional Player object and increments the number of round wins for that player.
		/// no arguments means a tie.
		/// </summary>
		/// <param name="p"></param>
		public void RoundWinner(Guid? p)
		{
			if (p == null)
			{
				ties++;
			}
			else if (p == Player1)
			{
				p1RoundWins++;
			}
			else if (p == Player2)
			{
				p2RoundWins++;
			}
		}

		/// <summary>
		/// compares the number of wins or Player1 and Player2 and returns that player. 
		/// If no player has 2 wins yet, returns null.
		/// </summary>
		/// <returns></returns>
		public Guid? MatchWinner()
		{
			if (p1RoundWins == 2)
			{
				return Player1;
			}
			else if (p2RoundWins == 2)
			{
				return Player2;
			}
			else
			{
				return null;
			}
		}
	}
}
