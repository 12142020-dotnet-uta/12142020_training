
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
	public class Match
	{
		[Key]
		public Guid matchId { get; set; } = Guid.NewGuid();

		public Player Player1 { get; set; } // always the computer

		public Player Player2 { get; set; } // always the user.

		public List<Round> Rounds { get; set; }

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
		public void RoundWinner(Guid? p = null)
		{
			if (p == null)
			{
				ties++;
			}
			else if (p == Player1.playerId)
			{
				p1RoundWins++;
			}
			else if (p == Player2.playerId)
			{
				p2RoundWins++;
			}
		}

		/// <summary>
		/// Takes the Player instance who won the round and updates the correct RoundWins property.
		/// </summary>
		/// <returns></returns>
		public Player MatchWinner()
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
