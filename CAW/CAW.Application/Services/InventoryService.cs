using AutoMapper;
using CAW.Application.Interfaces;
using CAW.Application.Models;
using CAW.Data.Interfaces;
using CAW.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CAW.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository inventoryRepository,
            IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryModel>> GetAllInventory()
        {
            try
            {
                var inventory = await _inventoryRepository.GetAll();
                var result = _mapper.Map<IEnumerable<InventoryModel>>(inventory);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InventoryModel> GetInventoryById(int id)
        {
            try
            {
                var inventory = await _inventoryRepository.GetById(id);
                return _mapper.Map<InventoryModel>(inventory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<InventoryModel>> GetInventoryById(Expression<Func<Inventory, bool>> predicate)
        {
            try
            {
                var inventory = await _inventoryRepository.GetById(predicate);
                return _mapper.Map<IEnumerable<InventoryModel>>(inventory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddInventoryAsync(InventoryModel entity)
        {
            try
            {
                var inventory = _mapper.Map<Inventory>(entity);
                var res  = await _inventoryRepository.AddAsync(inventory);

                int result = await _inventoryRepository.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateInventory(InventoryModel entity)
        {
            try
            {
                var inventory = _mapper.Map<Inventory>(entity);
                var res = _inventoryRepository.Update(inventory);

                int result = await _inventoryRepository.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> RemoveInventory(InventoryModel entity)
        {
            try
            {
                var inventory = _mapper.Map<Inventory>(entity);
                _inventoryRepository.Remove(inventory);

                int result = await _inventoryRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> RemoveInventoryById(int id)
        {
            try
            {
                await _inventoryRepository.RemoveById(id);

                int result = await _inventoryRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<InventoryModel> SingleOrDefaultAsync(Expression<Func<Inventory, bool>> predicate)
        {
            try
            {
                var inventory = await _inventoryRepository.SingleOrDefaultAsync(predicate);
                return _mapper.Map<InventoryModel>(inventory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<InventoryModel> FirstOrDefaultAsync(Expression<Func<Inventory, bool>> predicate)
        {
            try
            {
                var inventory = await _inventoryRepository.FirstOrDefaultAsync(predicate);
                return _mapper.Map<InventoryModel>(inventory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<int> AddRange(IEnumerable<InventoryModel> entities)
        {
            try
            {
                var inventories = _mapper.Map<IEnumerable<Inventory>>(entities);
                _inventoryRepository.AddRange(inventories);

                int result = await _inventoryRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddRangeAsync(IEnumerable<InventoryModel> entities)
        {
            try
            {
                var inventories = _mapper.Map<IEnumerable<Inventory>>(entities);
                await _inventoryRepository.AddRangeAsync(inventories);

                int result = await _inventoryRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateRange(IEnumerable<InventoryModel> entities)
        {
            try
            {
                var inventories = _mapper.Map<IEnumerable<Inventory>>(entities);
                _inventoryRepository.UpdateRange(inventories);

                int result = await _inventoryRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> RemoveRange(IEnumerable<InventoryModel> entities)
        {
            try
            {
                var inventories = this._mapper.Map<IEnumerable<Inventory>>(entities);
                this._inventoryRepository.RemoveRange(inventories);

                int result = await _inventoryRepository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
