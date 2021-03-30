using BAS.AppCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAS.Repository.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(long id);
        IEnumerable<T> GetByPredicate(Func<T, bool> predicate = null, Func<T, object> orderBy = null, bool isDescending = false, int page = 1, QuantityOfItemsOnPage pageSize = QuantityOfItemsOnPage.Ten);
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(long id);
        Task Delete(T entity);
        Task<int> Count(Func<T, bool> predicate);
    }
}
