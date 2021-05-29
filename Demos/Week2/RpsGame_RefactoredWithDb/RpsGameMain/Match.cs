
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace RpsGame_NoDb
{
    public class Match
    {
        [Key]
        public Guid matchId { get; set; } = Guid.NewGuid();
        //public Guid MatchId { get { return matchId; } }

        public Player Player1 { get; set; } // always the computer
        public Player Player2 { get; set; } // always the user.

        public List<Round> Rounds = new List<Round>();

        public int p1RoundWins { get; set; } // Who many rounds has the player won?
        public int p2RoundWins { get; set; }
        public int ties { get; set; }


        /// <summary>
        /// This methodvtakes an optional Player object and increments the number of round wins for that player.
        /// no arguments means a tie.
        /// </summary>
        /// <param name="p"></param>
        public void RoundWinner(Player p = null)
        {
            if (p == null)
            {
                ties++;
            }
            else if (p.playerId == Player1.playerId)
            {
                p1RoundWins++;
            }
            else if (p.playerId == Player2.playerId)
            {
                p2RoundWins++;
            }
        }

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
                return new Player();
            }
        }
    }
}
