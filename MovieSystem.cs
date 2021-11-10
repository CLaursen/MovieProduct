using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieProduct
{
    public class MovieSystem
    {
        public MovieSystem()
        {
            Movies = new List<Movie>();
            Users = new List<User>();
        }
        
        public MovieSystem(List<Movie> movies, List<User> users)
        {
            Movies = movies;
            Users = users;
        }

        public List<Movie> Movies { get; set; }

        public List<User> Users { get; private set; }

        public string AddUser(User user)
        {
            // Adds user to the list and updates movies with views and purchases
            Users.Add(user);

            foreach (int view in user.ViewedMovies)
            {
                foreach (Movie movie in Movies)
                {
                    if (movie.Id == view)
                    {
                        movie.Views++;
                        break;
                    }
                }
            }

            foreach (int purchase in user.PurchasedMovies)
            {
                foreach (Movie movie in Movies)
                {
                    if (movie.Id == purchase)
                    {
                        movie.Purchases++;
                        break;
                    }
                }
            }

            return $"User: {user.Name} added to the system.";
        }

        public List<string> GetPopularMovies()
        {
            // get the highest rated movie and the highest purchaceRate movie
            List<Movie> PopularMovies = new List<Movie>();

            var sortedList = Movies.OrderByDescending(x=>x.Rating).ToList();
            PopularMovies.Add(sortedList[0]);

            //Remove all movies with zero views to avoid dividing by zero
            var viewedMovies = Movies.Select(movie => new Movie(movie)).ToList();
            viewedMovies.RemoveAll(x=>x.Views == 0);
            var viewedMoviesSorted = viewedMovies.OrderByDescending(x=>(float)x.Purchases/(float)x.Views).ToList();
            if (viewedMoviesSorted[0] != sortedList[0])
            {
                PopularMovies.Add(viewedMoviesSorted[0]);

            }
            else
            {
                PopularMovies.Add(viewedMoviesSorted[1]);
            }

            return PopularMovies.ConvertAll(x => x.Name);
        }

        public string RecommendMovie(User user, int currentMovie)
        {
            Dictionary<string, int> userCategories = new Dictionary<string, int>();
            foreach (int purchase in user.PurchasedMovies)
            {
                var categories = Movies.Find(movie => movie.Id == purchase).Categories;
                
                foreach (string category in categories)
                {
                    if (userCategories.ContainsKey(category))
                    {
                        userCategories[category]++;
                    }
                    else
                    {
                        userCategories.Add(category, 1);
                    }
                }
            }

            var mostPurchasedCategorySortedList = userCategories.ToList();
            mostPurchasedCategorySortedList.Sort((x, y) => y.Value.CompareTo(x.Value)); // y compared to x to get descending order sort
            var mostPurchasedCategorySortedListKeys = mostPurchasedCategorySortedList.Select(x => x.Key).ToList();

            var currentMovieCategories = Movies.Find(movie => movie.Id == currentMovie).Categories;

            var moviesUserHaveNotPurchased = Movies.Select(movie => new Movie(movie)).ToList();;
            moviesUserHaveNotPurchased.RemoveAll(movie => user.PurchasedMovies.Contains(movie.Id));
            
            foreach (string category in mostPurchasedCategorySortedListKeys)
            {
                if (currentMovieCategories.Contains(category))
                {
                    foreach (var movie in moviesUserHaveNotPurchased)
                    {
                        if (movie.Categories.Contains(category))
                        {
                            return movie.Name;
                        }
                    }
                }
            }

            //Incase user haven't bought any movies in any of the categories of the current viewed movie
            var CategoriesNotBoughtAnyMoviesIn = currentMovieCategories.Except(mostPurchasedCategorySortedListKeys).ToList();

            foreach (var movie in moviesUserHaveNotPurchased)
            {
                if (movie.Categories.Contains(CategoriesNotBoughtAnyMoviesIn[0]))
                {
                    return movie.Name;
                }
            }

            //should never reach here
            return null;
        }
    }
}