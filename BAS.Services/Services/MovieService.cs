using BAS.AppCommon.StaticValues;
using BAS.AppServices.DTOs;
using BAS.AppServices.Services.Interfaces;
using BAS.Database;
using BAS.Database.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BAS.AppServices.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext db;
        private readonly IGenreService genreService;
        private readonly IPersonnelService personnelService;
        private IHostingEnvironment appEnvironment;

        public MovieService(MovieDbContext db, IGenreService genreService, IPersonnelService personnelService, IHostingEnvironment appEnvironment)
        {
            this.db = db;
            this.genreService = genreService;
            this.personnelService = personnelService;
            this.appEnvironment = appEnvironment;
        }


        public async Task<bool> DeleteMovie(long id)
        {
            var movie = db.Movies.Find(id);

            if (movie != null)
            {
                await DeleteMoviePoster(id);

                db.Movies.Remove(movie);
                db.SaveChanges();
            }

            return true;
        }

        public Task<bool> DeleteReview()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovie(long id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieListWithFilters> GetMoviesWtihFilter(MovieFilters personnelFilter)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertMovie(InsertUpdateMovieDTO movieDTO)
        {
            if (string.IsNullOrWhiteSpace(movieDTO.Title) ||
                movieDTO.Title.Length > StaticValues.MovieTitleMaxLength)
                return false;

            if (db.Movies.Any(m => m.Title.ToLower().Equals(movieDTO.Title.ToLower())))
                return false;

            if (movieDTO.Description.Length > StaticValues.MovieDescriptionMaxLength)
                return false;

            if (movieDTO.MovieLengthInMinutes <= 0)
                return false;

            var movie = new Movie()
            {
                MovieLengthInMinutes = movieDTO.MovieLengthInMinutes,
                AverageRating = 0.0,
                Description = movieDTO.Description,
                Poster = "",//"Movie_"
                ReleaseYear = movieDTO.ReleaseYear,
                Title = movieDTO.Title
            };

            db.Movies.Add(movie);
            db.SaveChanges();

            var movieId = db.Movies.FirstOrDefault(m => m.Title.Equals(movieDTO.Title)).Id;

            await InsertMovieGenres(movieId, movieDTO.Genres);
            await InsertMovieCrew(movieId, movieDTO.Crew);
            movie.Poster = await AddMoviePoster(movieId, movieDTO.File);

            db.Movies.Update(movie);
            db.SaveChanges();

            return true;
        }

        private async Task InsertMovieCrew(long movieId, List<InsertMovieCrewDTO> crew)
        {
            if (crew == null)
                return;

            var personnelList = new List<MoviePersonnel>();

            foreach (var personnel in crew)
            {
                if (await personnelService.IsPersonnelInDB(personnel.PersonnelId))
                {
                    personnelList.Add(new MoviePersonnel()
                    {
                        MemberPosition = personnel.FilmCrew,
                        MovieId = movieId,
                        PersonId = personnel.PersonnelId
                    });
                }
            }

            db.MoviePersonnel.AddRange(personnelList);
            db.SaveChanges();
        }

        private async Task InsertMovieGenres(long movieId, List<long> genres)
        {
            if (genres == null)
                return;

            var genreList = new List<MovieGenre>();
            genres = genres.Distinct().ToList();

            foreach (var genreId in genres)
            {
                if (await genreService.IsGenreInDB(genreId))
                {
                    genreList.Add(new MovieGenre()
                    {
                        GenreId = genreId,
                        MovieId = movieId
                    });
                }
            }

            db.MovieGenres.AddRange(genreList);
            db.SaveChanges();
        }

        private async Task<string> AddMoviePoster(long movieId, IFormFile file)
        {
            var enableExtensions = new string[] { ".jpeg", ".jpg", ".png" };

            var extension = "." + file.FileName.Split(".").Last();

            if (file.Length <= 0 ||
                file.Length > StaticValues.MoviePosterMaxFileSize ||
                 !enableExtensions.Any(e => e == extension))
                return "";

            string fileName = "Movie_" + movieId;
            string path = this.appEnvironment.WebRootPath + "\\MovieImages";

            if(File.Exists(path + "\\" + fileName))
            {
                long i = 1;
                var fileFound = true;

                do
                {
                    var tempFileName = fileName + "_" + i;

                    if(!File.Exists(path + "\\" + tempFileName))
                    {
                        fileFound = false;
                        fileName = tempFileName;
                    }

                    i++;
                } while (fileFound);
            }

            using (var stream = File.Create(path + "\\" + fileName + extension))
            {
                await file.CopyToAsync(stream);
            }

            return fileName + extension;
        }

        public async Task<bool> UpdateMovie(InsertUpdateMovieDTO movieDTO)
        {
            if (string.IsNullOrWhiteSpace(movieDTO.Title) ||
                movieDTO.Title.Length > StaticValues.MovieTitleMaxLength)
                return false;

            if (db.Movies.Any(m => m.Title.ToLower().Equals(movieDTO.Title.ToLower()) &&
                    m.Id != movieDTO.Id))
                return false;

            if (movieDTO.Description.Length > StaticValues.MovieDescriptionMaxLength)
                return false;

            if (movieDTO.MovieLengthInMinutes <= 0)
                return false;

            var movie = db.Movies.Find(movieDTO.Id);

            if (movie == null)
                return false;

            movie.Description = movieDTO.Description;
            movie.MovieLengthInMinutes = movieDTO.MovieLengthInMinutes;
            movie.ReleaseYear = movieDTO.ReleaseYear;
            movie.Title = movieDTO.Title;

            await UpdateMovieGenres(movie.Id, movieDTO.Genres);
            await UpdateMovieCrew(movie.Id, movieDTO.Crew);
            if(movieDTO.UpdatePhoto)
                movie.Poster = await UpdateMoviePoster(movie.Id, movieDTO.File);

            db.Movies.Update(movie);
            db.SaveChanges();

            return true;
        }

        private async Task<string> UpdateMoviePoster(long movieId, IFormFile file)
        {
            await DeleteMoviePoster(movieId);
            return await AddMoviePoster(movieId, file);
        }

        private async Task DeleteMoviePoster(long movieId)
        {
            var moviePosterName = db.Movies.Find(movieId)?.Poster;

            if (moviePosterName == "")
                return;

            string path = this.appEnvironment.WebRootPath + "\\MovieImages";
            if (File.Exists(path + "\\" + moviePosterName))
            {
                File.Delete(path + "\\" + moviePosterName);
            }
        }

        private async Task UpdateMovieGenres(long movieId, List<long> genres)
        {
            if (genres == null)
                genres = new List<long>();

            genres = genres.Distinct().ToList();

            var existingMovieGenres = db.MovieGenres.Where(mg => mg.MovieId == movieId).ToList();

            var genreList = new List<MovieGenre>();

            foreach (var genreId in genres)
            {
                if (await genreService.IsGenreInDB(genreId))
                {
                    var existingMG = existingMovieGenres.Find(mg => mg.GenreId == genreId);
                    if (existingMG != null)
                    {
                        existingMovieGenres.Remove(existingMG);
                    }
                    else
                    {
                        genreList.Add(new MovieGenre()
                        {
                            GenreId = genreId,
                            MovieId = movieId
                        });
                    }
                }
            }

            db.MovieGenres.AddRange(genreList);
            db.MovieGenres.RemoveRange(existingMovieGenres);
            db.SaveChanges();
        }

        private async Task UpdateMovieCrew(long movieId, List<InsertMovieCrewDTO> crew)
        {
            if (crew == null)
                crew = new List<InsertMovieCrewDTO>();

            crew = crew.GroupBy(c => new { c.FilmCrew, c.PersonnelId })
                .Select(c => c.First())
                .ToList();

            var existingMoviePersonnel = db.MoviePersonnel.Where(mp => mp.MovieId == movieId).ToList();

            var personnelList = new List<MoviePersonnel>();

            foreach (var personnel in crew)
            {
                if (await personnelService.IsPersonnelInDB(personnel.PersonnelId))
                {
                    var existingMP = existingMoviePersonnel.Find(mp => mp.PersonId == personnel.PersonnelId);
                    
                    if (existingMP != null)
                    {
                        existingMoviePersonnel.Remove(existingMP);
                    }
                    else
                    {
                        personnelList.Add(new MoviePersonnel()
                        {
                            MemberPosition = personnel.FilmCrew,
                            MovieId = movieId,
                            PersonId = personnel.PersonnelId
                        });
                    }
                }
            }

            db.MoviePersonnel.AddRange(personnelList);
            db.MoviePersonnel.RemoveRange(existingMoviePersonnel);
            db.SaveChanges();
        }

        public Task<bool> InsertReview()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReview()
        {
            throw new NotImplementedException();
        }
    }
}
