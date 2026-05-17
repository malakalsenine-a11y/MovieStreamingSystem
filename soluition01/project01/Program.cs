using Microsoft.EntityFrameworkCore;
using project01.Data;
using project01.Models;

ApplicationDbContext db = new ApplicationDbContext();

while (true)
{
    Console.WriteLine("\n===== Movie Streaming System =====");
    Console.WriteLine("1. Add Category");
    Console.WriteLine("2. Add User");
    Console.WriteLine("3. Add Movie");
    Console.WriteLine("4. Show All Movies");
    Console.WriteLine("5. Update Movie");
    Console.WriteLine("6. Delete Movie");
    Console.WriteLine("7. Add Movie to Watchlist");
    Console.WriteLine("8. Show User Watchlist");
    Console.WriteLine("9. Add Review");
    Console.WriteLine("10. Show Reviews for Movie");
    Console.WriteLine("11. Top Rated Movies");
    Console.WriteLine("12. Filter Movies by Category");
    Console.WriteLine("0. Exit");
    Console.Write("Choose: ");

    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.Write("Enter Category Name: ");
        string name = Console.ReadLine();

        Category category = new Category
        {
            Name = name
        };

        db.Categories.Add(category);
        db.SaveChanges();

        Console.WriteLine("Category added successfully.");
    }

    else if (choice == "2")
    {
        Console.Write("Enter User Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        User user = new User
        {
            Name = name,
            Email = email
        };

        db.Users.Add(user);
        db.SaveChanges();

        Console.WriteLine("User added successfully.");
    }

    else if (choice == "3")
    {
        Console.Write("Enter Movie Title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Description: ");
        string description = Console.ReadLine();

        Console.Write("Enter Release Year: ");
        int year = int.Parse(Console.ReadLine());

        Console.WriteLine("\nAvailable Categories:");
        foreach (var c in db.Categories.ToList())
        {
            Console.WriteLine($"{c.Id}. {c.Name}");
        }

        Console.Write("Enter Category Id: ");
        int categoryId = int.Parse(Console.ReadLine());

        Movie movie = new Movie
        {
            Title = title,
            Description = description,
            ReleaseYear = year,
            CategoryId = categoryId
        };

        db.Movies.Add(movie);
        db.SaveChanges();

        Console.WriteLine("Movie added successfully.");
    }

    else if (choice == "4")
    {
        var movies = db.Movies
            .Include(m => m.Category)
            .ToList();

        Console.WriteLine("\nAll Movies:");

        foreach (var movie in movies)
        {
            Console.WriteLine($"{movie.Id}. {movie.Title} - {movie.ReleaseYear} - Category: {movie.Category.Name}");
        }
    }

    else if (choice == "5")
    {
        Console.Write("Enter Movie Id to Update: ");
        int id = int.Parse(Console.ReadLine());

        Movie movie = db.Movies.Find(id);

        if (movie == null)
        {
            Console.WriteLine("Movie not found.");
        }
        else
        {
            Console.Write("Enter New Title: ");
            movie.Title = Console.ReadLine();

            Console.Write("Enter New Description: ");
            movie.Description = Console.ReadLine();

            Console.Write("Enter New Release Year: ");
            movie.ReleaseYear = int.Parse(Console.ReadLine());

            Console.WriteLine("\nAvailable Categories:");
            foreach (var c in db.Categories.ToList())
            {
                Console.WriteLine($"{c.Id}. {c.Name}");
            }

            Console.Write("Enter New Category Id: ");
            movie.CategoryId = int.Parse(Console.ReadLine());

            db.SaveChanges();

            Console.WriteLine("Movie updated successfully.");
        }
    }

    else if (choice == "6")
    {
        Console.Write("Enter Movie Id to Delete: ");
        int id = int.Parse(Console.ReadLine());

        Movie movie = db.Movies.Find(id);

        if (movie == null)
        {
            Console.WriteLine("Movie not found.");
        }
        else
        {
            db.Movies.Remove(movie);
            db.SaveChanges();

            Console.WriteLine("Movie deleted successfully.");
        }
    }

    else if (choice == "7")
    {
        Console.Write("Enter User Id: ");
        int userId = int.Parse(Console.ReadLine());

        Console.Write("Enter Movie Id: ");
        int movieId = int.Parse(Console.ReadLine());

        bool userExists = db.Users.Any(u => u.Id == userId);
        bool movieExists = db.Movies.Any(m => m.Id == movieId);

        if (!userExists)
        {
            Console.WriteLine("User not found.");
        }
        else if (!movieExists)
        {
            Console.WriteLine("Movie not found.");
        }
        else
        {
            bool exists = db.Watchlists.Any(w => w.UserId == userId && w.MovieId == movieId);

            if (exists)
            {
                Console.WriteLine("This movie is already in the watchlist.");
            }
            else
            {
                Watchlist watchlist = new Watchlist
                {
                    UserId = userId,
                    MovieId = movieId,
                    AddedDate = DateTime.Now
                };

                db.Watchlists.Add(watchlist);
                db.SaveChanges();

                Console.WriteLine("Movie added to watchlist successfully.");
            }
        }
    }

    else if (choice == "8")
    {
        Console.Write("Enter User Id: ");
        int userId = int.Parse(Console.ReadLine());

        var user = db.Users
            .Include(u => u.Watchlists)
            .ThenInclude(w => w.Movie)
            .FirstOrDefault(u => u.Id == userId);

        if (user == null)
        {
            Console.WriteLine("User not found.");
        }
        else
        {
            Console.WriteLine($"\nWatchlist for {user.Name}:");

            if (user.Watchlists.Count == 0)
            {
                Console.WriteLine("No movies in watchlist.");
            }
            else
            {
                foreach (var item in user.Watchlists)
                {
                    Console.WriteLine($"{item.Movie.Id}. {item.Movie.Title} - Added Date: {item.AddedDate}");
                }
            }
        }
    }

    else if (choice == "9")
    {
        Console.Write("Enter User Id: ");
        int userId = int.Parse(Console.ReadLine());

        Console.Write("Enter Movie Id: ");
        int movieId = int.Parse(Console.ReadLine());

        bool userExists = db.Users.Any(u => u.Id == userId);
        bool movieExists = db.Movies.Any(m => m.Id == movieId);

        if (!userExists)
        {
            Console.WriteLine("User not found.");
        }
        else if (!movieExists)
        {
            Console.WriteLine("Movie not found.");
        }
        else
        {
            Console.Write("Enter Comment: ");
            string comment = Console.ReadLine();

            Console.Write("Enter Rating from 1 to 5: ");
            int rating = int.Parse(Console.ReadLine());

            if (rating < 1 || rating > 5)
            {
                Console.WriteLine("Rating must be between 1 and 5.");
            }
            else
            {
                Review review = new Review
                {
                    UserId = userId,
                    MovieId = movieId,
                    Comment = comment,
                    Rating = rating
                };

                db.Reviews.Add(review);
                db.SaveChanges();

                Console.WriteLine("Review added successfully.");
            }
        }
    }

    else if (choice == "10")
    {
        Console.Write("Enter Movie Id: ");
        int movieId = int.Parse(Console.ReadLine());

        var movie = db.Movies.FirstOrDefault(m => m.Id == movieId);

        if (movie == null)
        {
            Console.WriteLine("Movie not found.");
        }
        else
        {
            var reviews = db.Reviews
                .Include(r => r.User)
                .Where(r => r.MovieId == movieId)
                .ToList();

            Console.WriteLine($"\nReviews for {movie.Title}:");

            if (reviews.Count == 0)
            {
                Console.WriteLine("No reviews for this movie.");
            }
            else
            {
                foreach (var review in reviews)
                {
                    Console.WriteLine($"User: {review.User.Name}");
                    Console.WriteLine($"Rating: {review.Rating}");
                    Console.WriteLine($"Comment: {review.Comment}");
                    Console.WriteLine("-----------------------");
                }
            }
        }
    }

    else if (choice == "11")
    {
        var topMovies = db.Reviews
            .Include(r => r.Movie)
            .GroupBy(r => r.Movie)
            .Select(g => new
            {
                MovieId = g.Key.Id,
                MovieTitle = g.Key.Title,
                AverageRating = g.Average(r => r.Rating),
                ReviewCount = g.Count()
            })
            .OrderByDescending(x => x.AverageRating)
            .ToList();

        Console.WriteLine("\nTop Rated Movies:");

        if (topMovies.Count == 0)
        {
            Console.WriteLine("No reviews yet.");
        }
        else
        {
            foreach (var item in topMovies)
            {
                Console.WriteLine($"{item.MovieId}. {item.MovieTitle} - Average Rating: {item.AverageRating:F1} - Reviews: {item.ReviewCount}");
            }
        }
    }

    else if (choice == "12")
    {
        Console.WriteLine("\nAvailable Categories:");
        foreach (var c in db.Categories.ToList())
        {
            Console.WriteLine($"{c.Id}. {c.Name}");
        }

        Console.Write("Enter Category Id: ");
        int categoryId = int.Parse(Console.ReadLine());

        var movies = db.Movies
            .Include(m => m.Category)
            .Where(m => m.CategoryId == categoryId)
            .ToList();

        Console.WriteLine("\nMovies in this Category:");

        if (movies.Count == 0)
        {
            Console.WriteLine("No movies found in this category.");
        }
        else
        {
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.Id}. {movie.Title} - {movie.ReleaseYear} - Category: {movie.Category.Name}");
            }
        }
    }

    else if (choice == "0")
    {
        Console.WriteLine("Exiting program...");
        break;
    }

    else
    {
        Console.WriteLine("Invalid choice. Try again.");
    }
}