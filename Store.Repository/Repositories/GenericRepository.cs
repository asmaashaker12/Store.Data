using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _storeDbContext;
        public GenericRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task AddAsync(TEntity entity)
        =>await _storeDbContext.Set<TEntity>().AddAsync(entity);
        

        public void DeleteAsync(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()

         => await _storeDbContext.Set<TEntity>().ToListAsync();


        public async Task<TEntity> GetByIdAsync(TKey? id)
        => await _storeDbContext.Set<TEntity>().FindAsync();



        public  void UpdateAsync(TEntity entity)

        =>  _storeDbContext.Set<TEntity>().Update(entity);
    }
}
