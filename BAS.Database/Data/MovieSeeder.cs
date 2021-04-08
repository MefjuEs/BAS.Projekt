using BAS.AppCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BAS.Database
{
    public static class MovieSeeder
    {
        public static void Seed(MovieDbContext db)
        {
            List<Movie> movies = new List<Movie>
            {
                new Movie()
                {
                    Title = "Pulp Fiction",
                    AverageRating = 0.0,
                    Description = "Przemoc i odkupienie w opowieści o dwóch płatnych mordercach pracujących na zlecenie mafii, żonie gangstera, bokserze i parze okradającej ludzi w restauracji.",
                    MovieLengthInMinutes = 154,
                    Poster = "",
                    ReleaseYear = 1994
                },
                new Movie()
                {
                    Title = "Formuła",
                    AverageRating = 0.0,
                    Description = "Mafijny chemik próbuje sprzedać konkurencji wzór nowego, niezwykle silnego narkotyku.",
                    MovieLengthInMinutes = 92,
                    Poster = "",
                    ReleaseYear = 2001
                },
                new Movie()
                {
                    Title = "Kac Vegas",
                    AverageRating = 0.0,
                    Description = "Czterech przyjaciół spędza wieczór kawalerski w Las Vegas. Następnego dnia okazuje się, że zgubili pana młodego i nic nie pamiętają.",
                    MovieLengthInMinutes = 100,
                    Poster = "",
                    ReleaseYear = 2009
                },
                new Movie()
                {
                    Title = "Joker",
                    AverageRating = 0.0,
                    Description = "Strudzony życiem komik popada w obłęd i staje się psychopatycznym mordercą.",
                    MovieLengthInMinutes = 122,
                    Poster = "",
                    ReleaseYear = 2019
                }
            };
            List<Personnel> actors = new List<Personnel>
            {
                new Personnel()
                {
                    DateOfBirth = new DateTime(1948, 12, 21),
                    Name = "Samuel L.",
                    Surname = "Jackson",
                    Nationality = "USA"
                },
                new Personnel()
                {
                    DateOfBirth = new DateTime(1954, 2, 18),
                    Name = "John",
                    Surname = "Travolta",
                    Nationality = "USA"
                },
                new Personnel()
                {
                    DateOfBirth = new DateTime(1970, 4, 29),
                    Name = "Uma",
                    Surname = "Thurman",
                    Nationality = "USA"
                },
                new Personnel()
                {
                    DateOfBirth = new DateTime(1971, 12, 1),
                    Name = "Emily",
                    Surname = "Mortimer",
                    Nationality = "Anglia"
                },
                new Personnel()
                {
                    DateOfBirth = new DateTime(1975, 1, 5),
                    Name = "Bradley",
                    Surname = "Cooper",
                    Nationality = "USA"
                },
                new Personnel()
                {
                    DateOfBirth = new DateTime(1969, 10, 1),
                    Name = "Zach",
                    Surname = "Galifianakis",
                    Nationality = "USA"
                },
                new Personnel()
                {
                    DateOfBirth = new DateTime(1974, 10, 28),
                    Name = "Joaquin",
                    Surname = "Phoenix",
                    Nationality = "Portoryko"
                },
                new Personnel()
                {
                    DateOfBirth = new DateTime(1943, 8, 17),
                    Name = "Robert",
                    Surname = "De Niro",
                    Nationality = "USA"
                }
            };
            List<Genre> genres = new List<Genre>
            {
                new Genre()
                {
                    Description = "",
                    Name = "Gangsterski"
                },
                new Genre()
                {
                    Description = "",
                    Name = "Dramat"
                },
                new Genre()
                {
                    Description = "",
                    Name = "Sensacyjny"
                },
                new Genre()
                {
                    Description = "",
                    Name = "Komedia"
                },
                new Genre()
                {
                    Description = "",
                    Name = "Kryminał"
                },
                new Genre()
                {
                    Description = "",
                    Name = "Thriller"
                },
                new Genre()
                {
                    Description = "",
                    Name = "Science Fiction"
                }
            };
            List<MoviePersonnel> moviePersonnels = new List<MoviePersonnel>
            {
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[0],
                    Personnel = actors[0]
                },
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[0],
                    Personnel = actors[1]
                },
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[0],
                    Personnel = actors[2]
                },
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[1],
                    Personnel = actors[0]
                },
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[1],
                    Personnel = actors[3]
                },
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[2],
                    Personnel = actors[4]
                },
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[2],
                    Personnel = actors[5]
                },
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[3],
                    Personnel = actors[6]
                },
                new MoviePersonnel()
                {
                    MemberPosition = FilmCrew.Actor,
                    Movie = movies[3],
                    Personnel = actors[7]
                }
            };
            List<MovieGenre> movieGenres = new List<MovieGenre>
            {
                new MovieGenre()
                {
                    Movie = movies[0],
                    Genre = genres[0]
                },
                new MovieGenre()
                {
                    Movie = movies[0],
                    Genre = genres[1]
                },
                new MovieGenre()
                {
                    Movie = movies[1],
                    Genre = genres[0]
                },
                new MovieGenre()
                {
                    Movie = movies[1],
                    Genre = genres[2]
                },
                new MovieGenre()
                {
                    Movie = movies[1],
                    Genre = genres[3]
                },
                new MovieGenre()
                {
                    Movie = movies[2],
                    Genre = genres[3]
                },
                new MovieGenre()
                {
                    Movie = movies[3],
                    Genre = genres[0]
                },
                new MovieGenre()
                {
                    Movie = movies[3],
                    Genre = genres[1]
                },
                new MovieGenre()
                {
                    Movie = movies[3],
                    Genre = genres[5]
                }
            };
            //List<Review> reviews = new List<Review>();

            if (!db.Movies.Any())
            {
                db.AddRange(movies);
                db.SaveChanges();
            }

            if (!db.Actors.Any())
            {
                db.AddRange(actors);
                db.SaveChanges();
            }

            if (!db.Genres.Any())
            {
                db.AddRange(genres);
                db.SaveChanges();
            }

            if (!db.MovieGenres.Any())
            {
                db.AddRange(movieGenres);
                db.SaveChanges();
            }

            if (!db.MoviePersonnel.Any())
            {
                db.AddRange(moviePersonnels);
                db.SaveChanges();
            }
        }
    }
}
