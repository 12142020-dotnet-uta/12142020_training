using System.Collections.Generic;
using System.Linq;
using System;

namespace RpsGame_NoDb
{
    public class RpsGameRepositoryLayer
    {
        List<Player> players = new List<Player>();
        List<Match> matches = new List<Match>();
        List<Round> rounds = new List<Round>();

        //Random rand = new Random();
        Random randomNumber = new Random((int)DateTime.Now.Millisecond); // create a random number object

        /// <summary>
        /// Creates a player after verifying that the player does not already exist. returns the player opj
        /// </summary>
        /// <returns></returns>
        public Player CreatePlayer(string fName = "null", string lName = "null")
        {
            Player p1 = new Player();
            p1 = players.Where(x => x.Fname == fName && x.Lname == lName).FirstOrDefault();

            if (p1 == null)
            {
                p1 = new Player()
                {
                    Fname = fName,
                    Lname = lName
                };
                players.Add(p1);
            }
            return p1;
        }




    }
}