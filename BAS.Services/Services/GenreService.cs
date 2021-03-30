using BAS.AppCommon.Enums;
using BAS.AppServices.DTOs;
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
    public class GenreService
    {
        private readonly IUnitOfWork unitOfWork;

        public GenreService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> InsertGenre(Genre genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name) ||
                genre.Description.Length > 500 ||
                unitOfWork.GenreRepository.GetByPredicate(g => g.Name == genre.Name).Any())
                return false;

            await unitOfWork.GenreRepository.Insert(genre);

            return true;
        }

        public async Task<bool> UpdateGenre(Genre genre)
        {
            if(string.IsNullOrWhiteSpace(genre.Name) ||
                genre.Description.Length > 500 ||
                unitOfWork.GenreRepository.GetByPredicate(g => g.Name == genre.Name).Any())
                return false;

            await unitOfWork.GenreRepository.Update(genre);

            return true;
        }

        public async Task<bool> DeleteGenre(long id)
        {
            await unitOfWork.GenreRepository.Delete(id);

            return true;
        }

        public async Task<GenreListWithFilters> GetGenresByName(string containStringInName, QuantityOfItemsOnPage pageSize, int page)
        {
            Func<Genre, bool> predicate = g => string.IsNullOrWhiteSpace(containStringInName) ||
                    g.Name.Contains(containStringInName);

            var result = new GenreListWithFilters()
            {
                CurrentPage = page,
                PageSize = pageSize,
                AllPages = await unitOfWork.GenreRepository.Count(predicate)
            };

            result.GenreList = unitOfWork.GenreRepository.GetByPredicate(predicate)
                .Select(g => new GenreInListDTO() 
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList();

            return null;
        }

        public async Task<Genre> GetGenre(long id)
        {
            return await unitOfWork.GenreRepository.GetById(id);
        }
    }
}
