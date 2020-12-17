using System;

namespace RpsGame_NoDb
{
    class Round
    {
        private Guid roundId = Guid.NewGuid();
        public Guid RoundId { get { return roundId; } }

        public Choice Player1Choice { get; set; } // always the computer
        public Choice Player2Choice { get; set; } // always the user
        public Player WinningPlayer { get; set; } = null; // default value ==null


    }
}