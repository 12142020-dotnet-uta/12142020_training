using System;
using System.Collections.Generic;

namespace RpsGame_NoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            List<Match> matches = new List<Match>();
            List<Round> rounds = new List<Round>();

            // create the Computer that everyone plays against.
            Player p1 = new Player()
            {
                Fname = "Max",
                Lname = "Headroom"
            };

            players.Add(p1);

            Console.WriteLine("This is The Official Batch Rock-Paper-Scissors Game");
            // Console.WriteLine(userResponse);
            // try
            // {
            //     int userResponseInt = int.Parse(userResponse);
            // }
            // catch (FormatException ex)
            // {
            //     // throw new FormatException("There was a problem with parsing the user input", ex);
            // }

            //log in or create a new player. unique fName and lName means create a new player, other wise, grab the existing player
            string[] userNamesArray;
            do
            {
                Console.WriteLine("\nPlease enter your first name.\n If you enter unique first and last name I will create a new player.\n");
                string userNames = Console.ReadLine();
                userNamesArray = userNames.Split(' ');
            } while (userNamesArray[0] == "");

            Player p2 = new Player();

            //is the user unputted jsut one name
            if (userNamesArray.Length == 1)
            {
                p2.Fname = userNamesArray[0];
            }

            // if the user inputted 2 or more names.
            if (userNamesArray.Length > 1)
            {
                p2.Fname = userNamesArray[0];
                p2.Lname = userNamesArray[1];
            }

            players.Add(p2);
            Match match = new Match();
            match.Player1 = p1;
            match.Player2 = p2;
            Round round = new Round();

            // user will choose Rock, Paper, or Scissors
            Choice userChoice; // declare these two variables to be used int he do/while loop
            bool userResponseParsed;
            do
            {
                Console.WriteLine($"Welcome, {p2.Fname}. Please choose Rock, Paper, or Scissors by typing 0, 1, or 2 and hitting enter.");
                Console.WriteLine("\t0. Rock \n\t1. Paper \n\t2. Scissors");
                // Console.WriteLine("2. Paper");
                // Console.WriteLine("3. Scissors");

                string userResponse = Console.ReadLine();   // read the users unput

                userResponseParsed = Choice.TryParse(userResponse, out userChoice);    // parse the users input to am int

                if (userResponseParsed == false || ((int)userChoice > 2 || (int)userChoice < 0))  // give a message if the users unput was invalid
                {
                    Console.WriteLine("Your response is invalid.");
                }

            } while (userResponseParsed == false || (int)userChoice > 2 || (int)userChoice < 0);  // state conditions for if we will repeat the loop

            // Console.WriteLine($"Congrats you entered a correct number. It is {userChoice}.");
            Console.WriteLine("Starting the game...");

            Random randomNumber = new Random(10); // create a randon number object
            Choice computerChoice = (Choice)randomNumber.Next(0, 3);   // get a randon number 1, 2, or 3.

            round.Player1Choice = computerChoice;
            round.Player2Choice = userChoice;

            Console.WriteLine($"The computer choice is => {computerChoice}.");

            // compare the numebrs to see who won.
            if (userChoice == computerChoice)   // is the playes tied
            {
                Console.WriteLine("This game was a tie");
                //rounds,WinningPlayer is default null
                match.Rounds.Add(round);
                rounds.Add(round);
                match.RoundWinner();
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

            foreach (Round round1 in rounds)
            {
                Console.WriteLine($"\nROUND - \nThe GUID is {round1.RoundId}.\n P1 Choice is {round1.Player1Choice}\n P2 Choice is {round1.Player2Choice}\nThe winning player is {round1.WinningPlayer.Fname}");
            }


        }
    }


}
