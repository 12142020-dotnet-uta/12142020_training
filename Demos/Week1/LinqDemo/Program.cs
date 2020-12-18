using System;
using System.Collections.Generic;
using System.Linq; // Language Integrated Query

namespace LinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();

            for (int x = 0; x < 10; x++)
            {
                players.Add(new Player()
                {
                    Fname = "Player-" + x,
                    Lname = "" + (x - (x * 2))
                });
            }

            foreach (var p in players)
            {
                Console.WriteLine($"Fname = {p.Fname}, Lname = {p.Lname}");
            }

            var result = players.Where(x => x.Fname == "Player-4").FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine($"The Players Fname is {result.Fname} and their last name is {result.Lname}");
            }
            else
            {
                throw new ArgumentNullException("The player wasn't found.");
            }

            int count = players.Count;
            players.Remove(result);

            result = players.Where(x => x.Fname == "Player-4").FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine($"The Players Fname is {result.Fname} and their last name is {result.Lname}");
            }
            else
            {
                throw new ArgumentNullException("The player wasn't found.");
            }

            players.SingleOrDefault();


        }
    }
}
