using System;
using System.Collections.Generic;

namespace MovieProduct
{
    public class User
    {
        public User(int id, string name, List<int> viewed, List<int> purchased)
        {
            Id = id;
            Name = name;
            ViewedMovies = viewed;
            PurchasedMovies = purchased;
        }

        public int Id { get; }
        public string Name { get; }
        public List<int> ViewedMovies { get; set; }
        public List<int> PurchasedMovies { get; set; }
    }
}