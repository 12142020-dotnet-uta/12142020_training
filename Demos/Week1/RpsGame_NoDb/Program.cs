using System;
using System.Collections.Generic;
using System.Linq;

namespace RpsGame_NoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            List<Match> matches = new List<Match>();
            List<Round> rounds = new List<Round>();
            Random randomNumber = new Random(53); // create a random number object
            // create the Computer that everyone plays against.
            Player p1 = new Player()
            {
                Fname = "Max",
                Lname = "Headroom"
            };
            players.Add(p1);

            Console.WriteLine("This is The Official Batch Rock-Paper-Scissors Game");
            // program loop starts here.
            int logInOrQuitInt;
            do
            {//Menu to log in or quit. start loop for logged in player. exits when he logs out
                do
                {
                    Console.WriteLine("Enter 1 to log in and 2 to quit the program.");
                    string logInOrQuit = Console.ReadLine();
                    bool logInOrQuitBool = int.TryParse(logInOrQuit, out logInOrQuitInt);
                    if (logInOrQuitBool == false)
                    { Console.WriteLine("I SAID... Enter 1 to play again, Enter 2 to Log out. Get it right!"); }
                } while (logInOrQuitInt != 1 && logInOrQuitInt != 2);

                if (logInOrQuitInt == 2) { break; }

                //log in or create a new player. unique fName and lName means create a new player, other wise, grab the existing player
                string[] userNamesArray;
                do
                {
                    Console.WriteLine("\n\tPlease enter your first and last name.\n\tIf you enter unique first and last name I will create a new player.\n");
                    string userNames = Console.ReadLine();
                    userNamesArray = userNames.Split(' ');
                } while (userNamesArray[0] == "");

                Player p2 = players.Where(x => x.Fname == userNamesArray[0] && x.Lname == userNamesArray[1]).FirstOrDefault();
                if (p2 == null)// if this is a new player
                {
                    if (userNamesArray.Length == 1)                 //if the user unputted just one name
                    {
                        p2 = new Player();
                        p2.Fname = userNamesArray[0];
                    }

                    if (userNamesArray.Length > 1)                  //if the user unputted 2 names
                    {
                        p2 = new Player();
                        p2.Fname = userNamesArray[0];
                        p2.Lname = userNamesArray[1];
                    }
                    players.Add(p2);
                }

                int response1Parsed;
                do //game loop starts here.
                {
                    Match match = new Match();
                    match.Player1 = p1;
                    match.Player2 = p2;
                    Console.WriteLine("\n\tStarting the game...\n");
                    do                                              // start loop to last till one player wins 2 games.
                    {
                        Round round = new Round();
                        Choice userChoice;                          // declare these two variables to be used int he do/while loop
                        bool userResponseParsed;
                        do
                        {
                            Console.WriteLine($"Welcome, {p2.Fname}. Please choose Rock, Paper, or Scissors by typing 0, 1, or 2 and hitting enter.");
                            Console.WriteLine("\t0. Rock \n\t1. Paper \n\t2. Scissors");
                            string userResponse = Console.ReadLine();// read the users unput
                            userResponseParsed = Choice.TryParse(userResponse, out userChoice);    // parse the users input to am int
                            if (userResponseParsed == false || ((int)userChoice > 2 || (int)userChoice < 0))  // give a message if the users unput was invalid
                            {
                                Console.WriteLine("Your response is invalid.");
                            }
                        } while (userResponseParsed == false || (int)userChoice > 2 || (int)userChoice < 0);  // state conditions for if we will repeat the loop

                        Choice computerChoice = (Choice)randomNumber.Next(2);   // get a randon number 1, 2, or 3.
                        round.Player1Choice = computerChoice;
                        round.Player2Choice = userChoice;
                        Console.WriteLine($"The computer choice is => {round.Player1Choice}.");

                        // compare the numebrs to see who won.
                        if (userChoice == computerChoice)   // is the playes tied
                        {
                            Console.WriteLine("This game was a tie");
                            //rounds.WinningPlayer is default null
                            rounds.Add(round);
                            match.Rounds.Add(round);
                            match.RoundWinner(); // send in the player who won. empty args means a tie round
                        }
                        else if (((int)userChoice == 1 && (int)computerChoice == 0) || // if the user won
                            ((int)userChoice == 2 && (int)computerChoice == 1) ||
                            ((int)userChoice == 0 && (int)computerChoice == 2))
                        {
                            Console.WriteLine("Congrats. You (the user) WON!."); // if the computer won.
                            round.WinningPlayer = p2;
                            rounds.Add(round);
                            match.Rounds.Add(round);
                            match.RoundWinner(p2);
                        }
                        else
                        {
                            Console.WriteLine("We're sorry. The computer won.");
                            round.WinningPlayer = p1;
                            rounds.Add(round);
                            match.Rounds.Add(round);
                            match.RoundWinner(p1);
                        }
                    } while (match.MatchWinner() == null);// end the game when once a player wins 2 rounds

                    Console.WriteLine("\n\tPrinting out the Game information");
                    foreach (Round round1 in match.Rounds)
                    { Console.WriteLine($"\nROUND - \nThe GUID is {round1.RoundId}.\n P1 Choice is {round1.Player1Choice}\n P2 Choice is {round1.Player2Choice}\nThe winning player is {round1.WinningPlayer.Fname}"); }

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
        }
    }
}
