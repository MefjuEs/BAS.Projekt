using BAS.Database;
using BAS.Repository.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MovieDbContext db;

        public GenericRepository(MovieDbContext db)
        {
            this.db = db;
        }

        public async Task Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public async Task Delete(long id)
        {
            T entity = await GetById(id);
            _ = Delete(entity);
        }

        public async Task<T> GetById(long id)
        {
            return await db.Set<T>().FindAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task Insert(T entity)
        {
            await db.Set<T>().AddAsync(entity);
        }

        public async Task Update(T entity)
        {
            db.Set<T>().Update(entity);
        }
    }
}
