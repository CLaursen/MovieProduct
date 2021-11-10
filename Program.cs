using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieProduct
{
    class Program
    {
        static void Main(string[] args)
        {
            MovieSystem movieDB = new MovieSystem();
            
            string currentDirectory = Environment.CurrentDirectory;

            string[] movies = System.IO.File.ReadAllLines(currentDirectory + "/Movie product data/Products.txt");
            string[] users = System.IO.File.ReadAllLines(currentDirectory + "/Movie product data/Users.txt");
            string[] userSessions = System.IO.File.ReadAllLines(currentDirectory + "/Movie product data/CurrentUserSession.txt");

            foreach (var line in movies)
            {
                string[] columns = line.Split(",");
                List<string> categories = new List<string>();

                for (int i = 3; i < 8; i++)
                {
                    if (columns[i].Length > 1)
                    {
                        categories.Add(columns[i].Trim());
                    }
                }
                Movie movie = new Movie(Int32.Parse(columns[0]), columns[1].Trim(), Int32.Parse(columns[2]), categories, double.Parse(columns[8]), Int32.Parse(columns[9]));
                movieDB.Movies.Add(movie);
            }

            foreach (var line in users)
            {
                string[] columns = line.Split(", ");
                List<int> viewed = columns[2].Split(";").ToList().Select(x => Int32.Parse(x)).ToList();
                List<int> purchased = columns[3].Split(";").ToList().Select(x => Int32.Parse(x)).ToList();

                movieDB.AddUser(new User(Int32.Parse(columns[0]), columns[1], viewed, purchased));
            }

            //Write out popularmovies to console
            List<string> popularMovies = movieDB.GetPopularMovies();
            foreach (var movie in popularMovies)
            {
                System.Console.WriteLine(movie);
            }

            foreach (var line in userSessions)
            {
                string[] columns = line.Split(", ");

                User user = movieDB.Users.Find(user => user.Id == (Int32.Parse(columns[0])));

                System.Console.Write($"User {user.Id} is recommended: ");
                System.Console.WriteLine(movieDB.RecommendMovie(user, Int32.Parse(columns[1])));
            }
        }
    }
}