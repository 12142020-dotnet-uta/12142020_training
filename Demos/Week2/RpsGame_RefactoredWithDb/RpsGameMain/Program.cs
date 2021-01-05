using System;
using System.Collections.Generic;
using System.Linq;

namespace RpsGame_NoDb
{
    class Program
    {
        static RpsGameRepositoryLayer gameContext = new RpsGameRepositoryLayer(); // create the context here to acceess it in all methods of this class
        static int numberOfChoices = Enum.GetNames(typeof(Choice)).Length;
        static void Main(string[] args)
        {
            // program loop starts here.
            Player p1 = gameContext.CreatePlayer("Max", "HeadRoom"); // create the computer

            Console.WriteLine("This is The Official Batch Rock-Paper-Scissors Game");
            // program loop starts here.
            int logInOrQuitInt;
            do
            {
                //Menu to log in or quit. start loop for logged in player. exits when he logs out
                logInOrQuitInt = MainMenu();
                if (logInOrQuitInt == 2) { break; }

                //log in or create a new player. unique fName and lName means create a new player, other wise, grab the existing player
                string[] userNamesArray = GetUserNames();

                Player p2;
                if (userNamesArray.Length == 1)                                         //if the user unputted just one name
                { p2 = gameContext.CreatePlayer(fName: userNamesArray[0]); }
                else if (userNamesArray.Length > 1)                                     //if the user unputted 2 names
                { p2 = gameContext.CreatePlayer(userNamesArray[0], userNamesArray[1]); }
                else
                { throw new ArgumentNullException("User input for name was invalid."); } // if the userNamesArray is empty.

                int response1Parsed;
                do //game loop starts here.
                {
                    Match match = gameContext.CreateMatch(p1, p2);

                    Console.WriteLine("\n\tStarting the Match...\n");
                    do                                              // start loop to last till one player wins 2 games.
                    {
                        Choice userChoice = UserChoiceMenu(match);
                        Choice computerChoice = gameContext.GetRandomChoice();   // get a randon number 1, 2, or 3.
                        Round round = gameContext.PlayRound(match, computerChoice, userChoice);
                        Console.WriteLine($"The computer choice is => {round.Player1Choice}.");
                        TellUserWhoWonTheRound(round);
                    } while (match.MatchWinner().Fname == "null");// end the game when once a player wins 2 rounds

                    gameContext.UpdateWinLossRecords(match);
                    gameContext.AddCompletedMatch(match);

                    do
                    {
                        Console.WriteLine("Do you want to play again? Enter 1 to play again, Enter 2 to Log out.");
                        string response1 = Console.ReadLine();
                        bool r1pBool = int.TryParse(response1, out response1Parsed);
                        if (r1pBool == false)
                        {
                            Console.WriteLine("I SAID... Enter 1 to play again, Enter 2 to Log out. Get it right!");
                        }
                    } while (response1Parsed != 1 && response1Parsed != 2); // play again or quit.
                } while (response1Parsed == 1);// end of the game loop.
            } while (logInOrQuitInt != 2); // log out


            // get all inputted info and pront it to the console.
            List<Player> players = gameContext.GetPlayers();
            List<Round> rounds = gameContext.GetRounds();
            List<Match> matches = gameContext.GetMatches();

            PrintPlayersData(players);
            PrintRoundsData(rounds);
            PrintGamesData(matches);

        }// END Main end

        /// <summary>
        /// Gives the user the choice to log in or quit the program
        /// </summary>
        /// <returns></returns>
        public static int MainMenu()
        {
            int logInOrQuitInt;
            do
            {
                Console.WriteLine("Enter 1 to log in and 2 to quit the program.");
                //call a method to validate user input.
                logInOrQuitInt = gameContext.ConvertStringToInt(Console.ReadLine());
                if (logInOrQuitInt == -1)
                { Console.WriteLine("I SAID... Enter 1 to play again, Enter 2 to Log out. Get it right!"); }
            } while (logInOrQuitInt != 1 && logInOrQuitInt != 2);// loop runs till the user selects 1 or 2
            return logInOrQuitInt;
        }

        /// <summary>
        /// Gets the users name and returns a  string array of those names.
        /// If the user inputs more than 2 names only the first 2 are taken.
        /// </summary>
        /// <returns></returns>
        public static string[] GetUserNames()
        {
            string[] userNamesArray;
            do
            {
                Console.WriteLine("\n\tPlease enter your first and last name.\n\tIf you enter unique first and last name I will create a new player.\n");
                userNamesArray = Console.ReadLine().Trim().Split(' ');
                //userNames.Trim(); // take out whitespace;
                //userNamesArray = userNames.Split(' ');
            } while (userNamesArray[0] == "");

            return userNamesArray;
        }

        /// <summary>
        /// Gives the user a menu to choose one of the choices available. ROCK, PAPER, SCISSORS
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public static Choice UserChoiceMenu(Match match)
        {
            int userChoice;                          // declare these two variables to be used int he do/while loop
            // bool userResponseParsed;
            do
            {
                Console.WriteLine($"Welcome, {match.Player2.Fname}. Please choose Rock, Paper, or Scissors by typing 0, 1, or 2 and hitting enter.");
                Console.WriteLine("\t0. Rock \n\t1. Paper \n\t2. Scissors");
                userChoice = gameContext.ConvertStringToInt(Console.ReadLine().Trim());
                if (userChoice > 2 || userChoice < 0)  // give a message if the users unput was invalid
                {
                    Console.WriteLine("Your response is invalid.");
                }
            } while (userChoice > numberOfChoices || userChoice < 0);  // state conditions for if we will repeat the loop
            return (Choice)userChoice;
        }

        /// <summary>
        /// Takes a completed round and tells the User who ron the last round
        /// </summary>
        /// <param name="round"></param>
        public static void TellUserWhoWonTheRound(Round round)
        {
            if (round.WinningPlayer.Fname == "TieGame")   // is the players tied
            {
                Console.WriteLine("This game was a tie");
            }
            else if (round.WinningPlayer.Fname != "Max")
            {
                Console.WriteLine("Congrats. You (the user) WON!."); // if the computer won.
            }
            else
            {
                Console.WriteLine("We're sorry. The computer won.");
            }
        }

        public static void PrintPlayersData(List<Player> p)
        {
            int i = 1;
            foreach (Player player in p)
            {
                Console.WriteLine($"\n\t\tPlayer {i++} - \nThe GUID is {player.playerId}.");
                Console.WriteLine($"\t First name is {player.Fname}.");
                Console.WriteLine($"\tLast Name is {player.Lname}.");
                int[] winsAndLosses = player.GetWinLossRecord();
                Console.WriteLine($"\tThe players record is {winsAndLosses[0]} wins and {winsAndLosses[1]} losses");
            }

        }

        public static void PrintRoundsData(List<Round> rounds)
        {
            int i = 1;
            foreach (Round round1 in rounds)
            {
                Console.WriteLine($"\n\t\tROUND {i++}- \nThe GUID is {round1.roundId}.");
                Console.WriteLine($"\tP1 Choice is {round1.Player1Choice}.");
                Console.WriteLine($"\tP2 Choice is {round1.Player2Choice}.");
                Console.WriteLine($"\tThe winning player is {round1.WinningPlayer.Fname}");
            }
        }

        public static void PrintGamesData(List<Match> matches)
        {
            int i = 1;
            foreach (Match m in matches)
            {
                Console.WriteLine($"\n\t\tMatch {i++}- \nThe GUID is {m.matchId}.");
                Console.WriteLine($"\tP1s name is {m.Player1.Fname}.");
                Console.WriteLine($"\tP2s name is {m.Player2.Fname}.");
                Console.WriteLine($"\tThe winning player is {m.MatchWinner().Fname}");
            }
        }
    }// END Class
}// END namespace
