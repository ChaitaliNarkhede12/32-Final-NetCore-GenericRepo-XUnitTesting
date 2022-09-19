using CAW.Data.Interfaces;
using CAW.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CAW.Data.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        IRepository<Inventory, int> _repo;
        public InventoryRepository(IRepository<Inventory, int> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Inventory>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<Inventory> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<IEnumerable<Inventory>> GetById(Expression<Func<Inventory, bool>> predicate)
        {
            return await _repo.GetById(predicate);
        }

        public async Task<Inventory> AddAsync(Inventory entity)
        {
            return await _repo.AddAsync(entity);
        }

        public Inventory Update(Inventory entity)
        {
            return _repo.Update(entity);
        }

        public void Remove(Inventory entity)
        {
            _repo.Remove(entity);
        }

        public async Task RemoveById(int id)
        {
            await _repo.RemoveById(id);
        }



        public int SaveChanges()
        {
            return _repo.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repo.SaveChangesAsync();
        }




        public async Task<Inventory> FirstOrDefaultAsync(Expression<Func<Inventory, bool>> predicate)
        {
            return await _repo.FirstOrDefaultAsync(predicate);
        }

        public async Task<Inventory> SingleOrDefaultAsync(Expression<Func<Inventory, bool>> predicate)
        {
            return await _repo.SingleOrDefaultAsync(predicate);
        }



        public void AddRange(IEnumerable<Inventory> entities)
        {
            _repo.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<Inventory> entities)
        {
            await _repo.AddRangeAsync(entities);
        }

        public void RemoveRange(IEnumerable<Inventory> entities)
        {
            _repo.RemoveRange(entities);
        }

        public void UpdateRange(IEnumerable<Inventory> entities)
        {
            _repo.UpdateRange(entities);
        }
    }
}
