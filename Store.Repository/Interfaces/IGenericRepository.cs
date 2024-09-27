using Store.Data.Entities;
using Store.Repository.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity,TKey>where TEntity:BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey? id);
        Task<TEntity> GetByIdSpecificationsAsync(ISpecifcation<TEntity> specs);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<IReadOnlyList<TEntity>> GetAllwithSpecficicationAsync(ISpecifcation<TEntity> specs);
        Task<int> GetCountSpecficicationAsync(ISpecifcation<TEntity> specs);
        Task AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
