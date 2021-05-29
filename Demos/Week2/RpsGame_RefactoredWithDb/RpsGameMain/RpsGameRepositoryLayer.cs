using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace RpsGame_NoDb
{
    public class RpsGameRepositoryLayer
    {
        int numberOfChoices = Enum.GetNames(typeof(Choice)).Length; // get a always-current number of options of Enum Choice
        Random randomNumber = new Random((int)DateTime.Now.Millisecond); // create a random number object
        RpsDbContext DbContext;
        DbSet<Player> players;
        DbSet<Match> matches;
        DbSet<Round> rounds;

        public RpsGameRepositoryLayer()
        {
            this.DbContext = new RpsDbContext();
            this.players = DbContext.players;
            this.matches = DbContext.matches;
            this.rounds = DbContext.rounds;
        }
        public RpsGameRepositoryLayer(RpsDbContext context)
        {
            this.DbContext = context;
            this.players = DbContext.players;
            this.matches = DbContext.matches;
            this.rounds = DbContext.rounds;
        }


        /// <summary>
        /// Creates a player after verifying that the player does not already exist. 
        /// IF the player already exists, return that player.
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
                DbContext.SaveChanges();
            }
            return p1;
        }

        /// <summary>
        /// Converts string input fornt he user to its int32 variant. If unsuccessful, returns -1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int ConvertStringToInt(string input)
        {
            int result;
            bool logInOrQuitBool = int.TryParse(input, out result);
            if (logInOrQuitBool == false)
            {
                return -1;
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// takes 2 players, creates  match with those players and returns the match.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public Match CreateMatch(Player p1, Player p2)
        {
            Match match = new Match();
            match.Player1 = p1;
            match.Player2 = p2;
            return match;
        }

        /// <summary>
        /// takes a new match instance and saves it to the List<Match> (or context).
        /// If the match already exists, returns false.
        /// </summary>
        /// <returns></returns>
        public bool SaveMatch(Match match)
        {
            //check if the match is already there
            if (!matches.Any(x => x.matchId == match.matchId))
            {
                matches.Add(match);
                DbContext.SaveChanges();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// returns a random Choice Enum based on the number of options.
        /// </summary>
        /// <returns></returns>
        public Choice GetRandomChoice()
        {
            return (Choice)randomNumber.Next(numberOfChoices);
        }

        /// <summary>
        /// plays an entire round and adds the completedd round to the List<Match>, roiund to the List<Round>
        /// </summary>
        /// <param name="match"></param>
        /// <param name="computerChoice"></param>
        /// <param name="userChoice"></param>
        /// <returns></returns>
        public Round PlayRound(Match match, Choice computerChoice, Choice userChoice)
        {
            Round round = new Round();
            round.Player1Choice = computerChoice;
            round.Player2Choice = userChoice;
            if (userChoice == computerChoice)   // is the playes tied
            {
                round.WinningPlayer = CreatePlayer("TieGame", "TieGame");
                rounds.Add(round);
                match.Rounds.Add(round);
                match.RoundWinner(); // send in the player who won. empty args means a tie round
            }
            else if (((int)userChoice == 1 && (int)computerChoice == 0) || // if the user won
                ((int)userChoice == 2 && (int)computerChoice == 1) ||
                ((int)userChoice == 0 && (int)computerChoice == 2))
            {
                round.WinningPlayer = match.Player2;
                rounds.Add(round);
                match.Rounds.Add(round);
                match.RoundWinner(match.Player2);
            }
            else
            {
                round.WinningPlayer = match.Player1;
                rounds.Add(round);
                match.Rounds.Add(round);
                match.RoundWinner(match.Player1);
            }
            DbContext.SaveChanges();
            return round;
        }

        /// <summary>
        /// adds the completed match to the List<Match> if it ins't already in the List.
        /// returns true if the match was successfully added, false if the matchId already exists
        /// </summary>
        /// <param name="match"></param>
        public bool AddCompletedMatch(Match match)
        {
            if (!matches.Any(x => x.matchId == match.matchId))
            {
                matches.Add(match);
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the win/Loss records of both players in the match sent as an argument
        /// </summary>
        /// <param name="match"></param>
        public void UpdateWinLossRecords(Match match)
        {
            if (match.MatchWinner().playerId == match.Player1.playerId)
            {
                match.Player1.AddWin();
                match.Player2.AddLoss();
            }
            else if (match.MatchWinner().playerId == match.Player2.playerId)
            {
                match.Player2.AddWin();
                match.Player1.AddLoss();
            }
        }

        /// <summary>
        /// returns all match objects in List<Match>
        /// </summary>
        /// <returns></returns>
        public List<Match> GetMatches()
        {
            return matches.ToList();
        }

        /// <summary>
        /// returns all match objects in List<Player>
        /// </summary>
        /// <returns></returns>
        public List<Player> GetPlayers()
        {
            return players.ToList();
        }

        /// <summary>
        /// returns all match objects in List<Round>
        /// </summary>
        /// <returns></returns>
        public List<Round> GetRounds()
        {
            return rounds.ToList();
        }
    }
}