using System;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer
{
	public class Round
	{
		[Key]
		public Guid RoundId { get; set; } = Guid.NewGuid();

		public Guid? MatchId { get; set; } = null;

		public Choice Player1Choice { get; set; }           // always the computer

		public Choice Player2Choice { get; set; }           // always the user

		public Player WinningPlayer { get; set; } = null;   // will be set when there is a winning player.
	}
}