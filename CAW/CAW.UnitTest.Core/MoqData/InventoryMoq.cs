using CAW.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CAW.UnitTest.Core.MoqData
{
    public class InventoryMoq : IClassFixture<InventoryDBDataFixture>
    {
        InventoryDBDataFixture fixture;

        public InventoryMoq(InventoryDBDataFixture fixture)
        {
            this.fixture = fixture;
        }
        public void MoqData(Inventory entity)
        {
            using (var qssContext = new InventoryDBContext(fixture.inventoryDBContextOptions))
            {
                qssContext.Inventory.Add(entity);
                qssContext.SaveChanges();
            }
        }

        public void MoqDataList(IEnumerable<Inventory> entityList)
        {
            using (var qssContext = new InventoryDBContext(fixture.inventoryDBContextOptions))
            {
                qssContext.Inventory.AddRangeAsync(entityList);
                qssContext.SaveChanges();
            }
        }

        public System.Linq.IQueryable<Inventory> GetDataList()
        {
            using (var qssContext = new InventoryDBContext(fixture.inventoryDBContextOptions))
            {
                return qssContext.Inventory.AsQueryable();
           }
        }

        public async Task<Inventory> GetDataById(int id)
        {
            using (var qssContext = new InventoryDBContext(fixture.inventoryDBContextOptions))
            {
                return await qssContext.Inventory.FindAsync(id);
            }
        }
    }
}
