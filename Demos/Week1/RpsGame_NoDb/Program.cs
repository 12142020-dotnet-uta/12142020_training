using System;

namespace RpsGame_NoDb
{
    class Program
    {
        // public enum Choice
        // {
        //     Rock,
        //     Paper,
        //     Scissors
        // }

        static void Main(string[] args)
        {
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
            int userChoice; // declare these two variables to be used int he do/while loop
            bool userResponseParsed;
            do
            {
                Console.WriteLine("Please choose Rock, Paper, or Scissors by typing 1, 2, or 3 and hitting enter.");
                Console.WriteLine("\t1. Rock \n\t2. Paper \n\t3. Scissors");
                // Console.WriteLine("2. Paper");
                // Console.WriteLine("3. Scissors");

                string userResponse = Console.ReadLine();   // read the users unput

                userResponseParsed = int.TryParse(userResponse, out userChoice);    // parse the users input to am int

                if (userResponseParsed == false || (userChoice > 3 || userChoice < 1))  // give a message if the users unput was invalid
                {
                    Console.WriteLine("Your response is invalid.");
                }

            } while (userResponseParsed == false || userChoice > 3 || userChoice < 1);  // state conditions for if we will repeat the loop

            // Console.WriteLine($"Congrats you entered a correct number. It is {userChoice}.");
            Console.WriteLine("Starting the game...");

            Random randonNumber = new Random(10); // create a randon number object
            int computerChoice = randonNumber.Next(1, 4);   // get a randon number 1, 2, or 3.

            Console.WriteLine($"The computer choice is => {computerChoice}");

            // compare the numebrs to see who won.
            if (userChoice == computerChoice)   // is the playes tied
            {
                Console.WriteLine("This game was a tie");
            }
            else if ((userChoice == 2 && computerChoice == 1) || // if the user won
                (userChoice == 3 && computerChoice == 2) ||
                (userChoice == 1 && computerChoice == 3))
            {
                Console.WriteLine("Congrats. You (the user) WON!."); // if the computer won.
            }
            else
            {
                Console.WriteLine("We're sorry. The computer won.");
            }


        }
    }


}
