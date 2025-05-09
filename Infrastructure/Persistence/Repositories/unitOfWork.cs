using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class unitOfWork : IunitOfWork
    {
    private readonly StoreDbContext _dbContext;
    private readonly Dictionary<string, object> _repositories;

    // Constructor
    public unitOfWork(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<string, object>();
    }
    //    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>(TEntity entity) where TEntity : BaseEntity<TKey>
    //{
    //    var typeName = typeof(TEntity).Name;

    //    //if (_repositories.ContainsKey(typeName))
    //    //    return (IGenericRepository<TEntity,TKey>) _repositories[typeName];

    //       if (_repositories.TryGetValue(typeName,out object? value))
    //            return (IGenericRepository<TEntity,TKey>) value;

    //    else
    //            {
    //        var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
    //        _repositories[typeName]= Repo;
    //        return Repo;
    //    }
    //}

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;

            //if (_repositories.ContainsKey(typeName))
            //    return (IGenericRepository<TEntity,TKey>) _repositories[typeName];

            if (_repositories.TryGetValue(typeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;

            else
            {
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories[typeName] = Repo;
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}
}
    

