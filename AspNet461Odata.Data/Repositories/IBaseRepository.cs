using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.OData;

namespace AspNet461Odata.Data.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T objectVm);

        void Delete(Guid key);

        IQueryable<T> Get();

        Task<T> GetByIDAsync(Guid key);

        T Patch(Guid key, Delta<T> patch);

        Task<T> Update(Guid key, T updatedObjectVm);
    }
}