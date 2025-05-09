using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        Task <IEnumerable<TEntity>> GetAllAsync();

        Task <TEntity> GetbyIdAsync(TKey id);

        Task<TEntity> AddAsync(TEntity entity);

        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
