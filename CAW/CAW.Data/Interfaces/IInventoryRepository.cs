using CAW.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CAW.Data.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAll();
        Task<Inventory> GetById(int id);
        Task<IEnumerable<Inventory>> GetById(Expression<Func<Inventory, bool>> predicate);
        Task<Inventory> AddAsync(Inventory entity);
        Inventory Update(Inventory entity);
        void Remove(Inventory entity);
        Task RemoveById(int id);


        int SaveChanges();
        Task<int> SaveChangesAsync();


        Task<Inventory> SingleOrDefaultAsync(Expression<Func<Inventory, bool>> predicate);
        Task<Inventory> FirstOrDefaultAsync(Expression<Func<Inventory, bool>> predicate);


        void AddRange(IEnumerable<Inventory> entities);
        Task AddRangeAsync(IEnumerable<Inventory> entities);

        void UpdateRange(IEnumerable<Inventory> entities);

        void RemoveRange(IEnumerable<Inventory> entities);
    }
}
