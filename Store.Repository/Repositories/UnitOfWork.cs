﻿using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositories; //collection of key value pair
        public UnitOfWork(StoreDbContext context)
        {
            _context=context;
        }
        public async Task<int> CompleteAsync()
        =>await _context.SaveChangesAsync();
        
        

        public  IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
          if(_repositories is null||_repositories.Count==0)
                _repositories = new Hashtable();
          var entityKey=typeof(TEntity).Name;
            if (!_repositories.ContainsKey(entityKey))
            {
                var repositoyType=typeof(GenericRepository<,>);
                var reposirotyInstance=Activator.CreateInstance(repositoyType.MakeGenericType(typeof(TEntity),typeof(TKey)),_context);
                _repositories.Add(entityKey, reposirotyInstance);
            }
            return (IGenericRepository<TEntity, TKey>)_repositories[entityKey];
        }
    }
}
