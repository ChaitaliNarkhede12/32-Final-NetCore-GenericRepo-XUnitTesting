using CAW.Application.Models;
using CAW.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CAW.Application.Interfaces
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryModel>> GetAllInventory();
        Task<InventoryModel> GetInventoryById(int id);
        Task<IEnumerable<InventoryModel>> GetInventoryById(Expression<Func<Inventory, bool>> predicate);
        Task<int> AddInventoryAsync(InventoryModel entity);
        Task<int> UpdateInventory(InventoryModel entity);
        Task<int> RemoveInventory(InventoryModel entity);
        Task<int> RemoveInventoryById(int id);

        Task<InventoryModel> SingleOrDefaultAsync(Expression<Func<Inventory, bool>> predicate);
        Task<InventoryModel> FirstOrDefaultAsync(Expression<Func<Inventory, bool>> predicate);


        Task<int> AddRange(IEnumerable<InventoryModel> entities);
        Task<int> AddRangeAsync(IEnumerable<InventoryModel> entities);

        Task<int> UpdateRange(IEnumerable<InventoryModel> entities);

        Task<int> RemoveRange(IEnumerable<InventoryModel> entities);
    }
}
