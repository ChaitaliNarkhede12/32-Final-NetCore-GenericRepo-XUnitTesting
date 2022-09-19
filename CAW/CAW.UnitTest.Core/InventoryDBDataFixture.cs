using CAW.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAW.UnitTest.Core
{
    public class InventoryDBDataFixture : IDisposable
    {
        public InventoryDBContext inventoryDBContext { get; private set; }
        public DbContextOptions<InventoryDBContext> inventoryDBContextOptions { get; private set; }
        private const string Database = "InventoryDBInMemoryDatabase";

        public InventoryDBDataFixture()
        {

            inventoryDBContextOptions = new DbContextOptionsBuilder<InventoryDBContext>()
                .UseInMemoryDatabase(Database + DateTime.Now.ToFileTimeUtc())

             .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
             .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(true)
                .Options;


            inventoryDBContext = new InventoryDBContext(inventoryDBContextOptions);
            inventoryDBContext.Database.EnsureDeleted();
            inventoryDBContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            inventoryDBContext.Database.EnsureDeleted();
            inventoryDBContext.Dispose();
        }

    }
}
