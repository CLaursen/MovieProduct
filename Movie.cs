using System;
using System.Collections.Generic;

namespace MovieProduct
{
    public class Movie
    {
        public Movie(int id, string name, int year, List<string> categories, double rating, int price)
        {
            Id = id;
            Name = name;
            Year = year;
            Categories = categories;
            Rating = rating;
            Price = price;
            Views = 0;
            Purchases = 0;
        }

        public Movie(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            Year = movie.Year;
            Categories = movie.Categories;
            Rating = movie.Rating;
            Price = movie.Price;
            Views = movie.Views;
            Purchases = movie.Purchases;
        }

        public int Id { get; }
        public string Name { get; }
        public int Year { get; }
        public List<string> Categories { get; set; }
        public double Rating { get; set; }
        public int Price { get; set; }
        public int Views { get; set; }
        public int Purchases { get; set; }
    }
}