using System;

namespace RpsGame_NoDb
{
    public class Player
    {
        public Player(string fname = "null", string lname = "null")
        {
            this.Fname = fname;
            this.Lname = lname;
        }

        private Guid playerId = Guid.NewGuid();
        public Guid PlayerId
        {
            get
            {
                return playerId;
            }
        }

        private int numWins;
        private int numLosses;
        private string fName;
        public string Fname
        {
            get { return fName; }
            set
            {
                if (value is string && value.Length < 20 && value.Length > 0)
                {
                    fName = value;
                }
                else
                {
                    throw new Exception("The player name you sent is not valid");
                }
            }
        }

        private string lName;
        public string Lname
        {
            get { return lName; }
            set
            {
                if (value is string && value.Length < 20 && value.Length > 0)
                {
                    lName = value;
                }
                else
                {
                    throw new Exception("The player name you sent is no valid");
                }
            }
        }

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