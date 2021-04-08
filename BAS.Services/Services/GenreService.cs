﻿using BAS.AppCommon.Enums;
using BAS.AppServices.DTOs;
using BAS.AppServices.Services.Interfaces;
using BAS.Database;
using BAS.Database.Models;
using BAS.Repository.Infrastructure;
using BAS.Repository.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.AppServices.Services
{
    public class GenreService : IGenreService
    {
        private readonly MovieDbContext db;

        public GenreService(MovieDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> InsertGenre(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name) ||
                genre.Description.Length > 500 ||
                db.Genres.Any(g => g.Name.ToLower().Equals(genre.Name.ToLower()))) //unitOfWork.GenreRepository.GetByPredicate(g => g.Name == genre.Name).Any()
                return false;

            db.Genres.Add(genre);
            db.SaveChanges();

            return true;
        }

        public async Task<bool> UpdateGenre(GenreDTO genreDTO)
        {
            if (string.IsNullOrWhiteSpace(genreDTO.Name) ||
                genreDTO.Description.Length > 500 ||
                db.Genres.Any(g => g.Name.ToLower().Equals(genreDTO.Name.ToLower()) && g.Id != genreDTO.Id)) //unitOfWork.GenreRepository.GetByPredicate(g => g.Name == genreDTO.Name && g.Id != genreDTO.Id).Any()
                return false;

            var genre = db.Genres.Find(genreDTO.Id);

            if (genre == null)
                return false;

            genre.Name = genreDTO.Name;
            genre.Description = genreDTO.Description;

            db.Genres.Update(genre);
            db.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteGenre(long id)
        {
            var genre = db.Genres.Find(id);

            if (genre != null)
            {
                db.Genres.Remove(genre);
                db.SaveChanges();
            }

            return true;
        }

        public async Task<GenreListWithFilters> GetGenresByName(GetGenresFiltersDTO genreFilter)
        {
            if (string.IsNullOrWhiteSpace(genreFilter.Name))
            {
                genreFilter.Name = "";
            }

            //Func<Genre, bool> predicate = g => string.IsNullOrWhiteSpace(genreFilter.Name) ||
            //        g.Name.Contains(genreFilter.Name);

            //Func<Genre, object> orderBy;

            //switch (genreFilter.OrderBy)
            //{
            //    case "description":
            //        orderBy = g => g.Description;
            //        break;
            //    case "name":
            //        orderBy = g => g.Name;
            //        break;
            //    default:
            //        orderBy = null;
            //        break;
            //}

            var pageSize = genreFilter.PageSize.HasValue ? genreFilter.PageSize.Value : int.MaxValue;

            var result = new GenreListWithFilters()
            {
                CurrentPage = genreFilter.Page,
                PageSize = pageSize,
                AllPages = (int)Math.Ceiling(db.Genres.Count(g => g.Name.ToLower().Contains(genreFilter.Name.ToLower())) * 1.0 / pageSize)
            };

            var genres = db.Genres.Where(g => g.Name.ToLower().Contains(genreFilter.Name.ToLower()));

            switch (genreFilter.OrderBy.ToLower())
            {
                case "description":
                    if(genreFilter.IsDescending)
                        genres = genres.OrderByDescending(g => g.Description);
                    else
                        genres = genres.OrderBy(g => g.Description);
                    break;
                case "name":
                    if (genreFilter.IsDescending)
                        genres = genres.OrderByDescending(g => g.Name);
                    else
                        genres = genres.OrderBy(g => g.Name);
                    break;
                default:
                    break;
            }


            genres = genres.Skip((genreFilter.Page - 1) * pageSize).Take(pageSize);

            //result.GenreList = 
            //    unitOfWork.GenreRepository.GetByPredicate(predicate, orderBy, genreFilter.IsDescending, genreFilter.Page, pageSize)
            //    .Select(g => new GenreInListDTO()
            //    {
            //        Id = g.Id,
            //        Name = g.Name
            //    }).ToList();

            result.GenreList = genres.Select(g => new GenreInListDTO()
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();

            return result;
        }

        public async Task<Genre> GetGenre(long id)
        {
            return db.Genres.Find(id);
        }

        public async Task<bool> IsGenreInDB(long id)
        {
            return await db.Genres.AnyAsync(g => g.Id == id);
        }
    }
}