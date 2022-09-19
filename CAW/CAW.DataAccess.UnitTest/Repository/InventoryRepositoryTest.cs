using CAW.Data.Interfaces;
using CAW.Data.Models;
using CAW.Data.Repository;
using CAW.UnitTest.Core;
using CAW.UnitTest.Core.MoqData;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CAW.DataAccess.UnitTest.Repository
{
    public class InventoryRepositoryTest : IClassFixture<InventoryDBDataFixture>
    {
        InventoryDBDataFixture fixture;

        public InventoryRepositoryTest(InventoryDBDataFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task GetAll_ShouldReturnList()
        {
            //Arrange
            var repository = new Mock<IRepository<Inventory, int>>();
            repository.Setup(x => x.GetAll()).ReturnsAsync(GetInventoryList());

            var inventoryRepo = new InventoryRepository(repository.Object);

            //act
            var result = inventoryRepo.GetAll();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Inventory>>(result.Result);
        }

        [Fact]
        public async Task GetById_ShouldReturnInventory()
        {
            //Arrange
            int id = 1;
            var repository = new Mock<IRepository<Inventory, int>>();
            repository.Setup(x => x.GetById(id)).ReturnsAsync(GetInventoryById(id));

            var inventoryRepo = new InventoryRepository(repository.Object);

            //act
            var result = inventoryRepo.GetById(id);

            //Assert
            Assert.IsAssignableFrom<Inventory>(result.Result);
        }

        [Fact]
        public async Task GetByIdUsingPredicate_ShouldReturnInventory()
        {
            //Arrange
            int id = 1;
            var repository = new Mock<IRepository<Inventory, int>>();
            repository.Setup(x => x.GetById(id)).ReturnsAsync(GetInventoryById(id));

            var inventoryRepo = new InventoryRepository(repository.Object);

            //act
            var result = inventoryRepo.GetById(x => x.InventoryId == id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<Inventory>>(result.Result);
        }

        [Fact]
        public async Task AddAsync_ShouldSaveRecord()
        {
            //Arrange
            var addInventory = GetInventoryById(1);
            addInventory.InventoryId = 0;

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);
            IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);
            IInventoryRepository inventoryRepo = new InventoryRepository(repository);

            //act
            var result = await inventoryRepo.AddAsync(addInventory);

            //Assert
            Assert.IsAssignableFrom<Inventory>(result);
            Assert.Equal(addInventory.Name, result.Name);
        }

        [Fact]
        public async Task Update_ShouldUpdateRecord()
        {
            var updateInventory = GetInventoryById(1);
            updateInventory.Name = "Test11";
            InventoryMoq moq = new InventoryMoq(fixture);
            moq.MoqData(updateInventory);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);
            IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);
            IInventoryRepository inventoryRepo = new InventoryRepository(repository);

            //Act
            var result = inventoryRepo.Update(updateInventory);

            //Assert
            Assert.IsAssignableFrom<Inventory>(result);
            Assert.Equal(updateInventory.Name, result.Name);
        }

        [Fact]
        public async Task Remove_ShouldRemoveRecord()
        {
            //Arrange
            var removeInventory = GetInventoryById(1);
            removeInventory.InventoryId = 2;
            removeInventory.Name = "Test22";
            InventoryMoq moq = new InventoryMoq(fixture);
            moq.MoqData(removeInventory);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);
            IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);
            IInventoryRepository inventoryRepository = new InventoryRepository(repository);

            //Act
            inventoryRepository.Remove(removeInventory);
            var res = await inventoryRepository.SaveChangesAsync();
            var list = inventoryRepository.GetAll();

            Assert.Equal(list.Result.Count(), 5);
            Assert.Equal(res, 1);
        }

        [Fact]
        public async Task RemoveById_ShouldRemoveRecord()
        {
            //Arrange
            int id = 3;
            var removeInventory = GetInventoryById(1);
            removeInventory.Name = "Test33";
            removeInventory.InventoryId = 3;
            InventoryMoq moq = new InventoryMoq(fixture);
            moq.MoqData(removeInventory);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);
            IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);
            IInventoryRepository inventoryRepository = new InventoryRepository(repository);

            //Act
            await inventoryRepository.RemoveById(id);
            var res = await inventoryRepository.SaveChangesAsync();
            var list = inventoryRepository.GetAll();

            Assert.Equal(list.Result.Count(), 3);
            Assert.Equal(res, 2);
        }

        [Fact]
        public async Task SaveChanges_ShouldSaveRecord()
        {
            //Arrange
            var repository = new Mock<IRepository<Inventory, int>>();
            repository.Setup(x => x.SaveChanges()).Returns(1);

            var inventoryRepo = new InventoryRepository(repository.Object);

            //act
            int result = inventoryRepo.SaveChanges();

            //Assert
            Assert.Equal(result, 1);
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldSaveRecord()
        {
            //Arrange
            var repository = new Mock<IRepository<Inventory, int>>();
            repository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var inventoryRepo = new InventoryRepository(repository.Object);

            //act
            var result = inventoryRepo.SaveChangesAsync();

            //Assert
            Assert.Equal(result.Result, 1);
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnInventory()
        {
            //Arrange
            int id = 1;
            var repository = new Mock<IRepository<Inventory, int>>();
            repository.Setup(x => x.FirstOrDefaultAsync(x => x.InventoryId == id)).ReturnsAsync(GetInventoryById(id));

            var inventoryRepo = new InventoryRepository(repository.Object);

            //act
            var result = inventoryRepo.FirstOrDefaultAsync(x => x.InventoryId == id);

            //Assert
            Assert.IsAssignableFrom<Inventory>(result.Result);
        }

        [Fact]
        public async Task SingleOrDefaultAsync_ShouldReturnInventory()
        {
            //Arrange
            int id = 1;
            var repository = new Mock<IRepository<Inventory, int>>();
            repository.Setup(x => x.SingleOrDefaultAsync(x => x.InventoryId == id)).ReturnsAsync(GetInventoryById(id));

            var inventoryRepo = new InventoryRepository(repository.Object);

            //act
            var result = inventoryRepo.SingleOrDefaultAsync(x => x.InventoryId == id);

            //Assert
            Assert.IsAssignableFrom<Inventory>(result.Result);
        }


        [Fact]
        public async Task AddRange_ShouldSaveRecord()
        {
            //Arrange
            var addInventoryRangeList = AddRangeInventoryList();

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);
            IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);
            IInventoryRepository inventoryRepository = new InventoryRepository(repository);

            //Act
            inventoryRepository.AddRange(addInventoryRangeList);
            var res = await inventoryRepository.SaveChangesAsync();
            var list = inventoryRepository.GetAll();

            //Assert
            Assert.Equal(list.Result.Count(), 7);
            Assert.Equal(res, 2);
        }

        [Fact]
        public async Task AddRangeAsync_ShouldSaveRecord()
        {
            //Arrange
            var addInventoryRangeAsynList = AddRangeAsyncInventoryList();

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);
            IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);
            IInventoryRepository inventoryRepository = new InventoryRepository(repository);

            //Act
            await inventoryRepository.AddRangeAsync(addInventoryRangeAsynList);
            var res = await inventoryRepository.SaveChangesAsync();
            var list = inventoryRepository.GetAll();

            //Assert
            Assert.Equal(list.Result.Count(), 5);
            Assert.Equal(res, 2);
        }

        [Fact]
        public async Task RemoveRange_ShouldSaveRecord()
        {
            //Arrange
            var removeRangeList = GetRemoveRangeInventoryList();
            InventoryMoq moq = new InventoryMoq(fixture);
            moq.MoqDataList(removeRangeList);

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);
            IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);
            IInventoryRepository inventoryRepository = new InventoryRepository(repository);

            //Act
            inventoryRepository.RemoveRange(removeRangeList);
            var res = await inventoryRepository.SaveChangesAsync();
            var list = inventoryRepository.GetAll();

            //Assert
            Assert.Equal(list.Result.Count(), 2);
            Assert.Equal(res,2);
        }

        [Fact]
        public async Task UpdateRange_ShouldSaveRecord()
        {
            //Arrange
            var updateInventoryList = GetUpdateRangeInventoryList().ToList();
            InventoryMoq moq = new InventoryMoq(fixture);
            moq.MoqDataList(updateInventoryList);

            updateInventoryList[0].Name = updateInventoryList[0].Name + " Test";
            updateInventoryList[1].Name = updateInventoryList[1].Name + " Test";

            IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);
            IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);
            IInventoryRepository inventoryRepository = new InventoryRepository(repository);

            //Act
            inventoryRepository.UpdateRange(updateInventoryList);
            var res = await inventoryRepository.SaveChangesAsync();
            var list = inventoryRepository.GetAll();

            //Assert
            Assert.Equal(list.Result.Count(), 2);
            Assert.Equal(res, 2);
        }




        private IEnumerable<Inventory> GetInventoryList()
        {
            List<Inventory> list = new List<Inventory>()
            {
                new Inventory {
                    InventoryId=1,
                    Name="Test1",
                    Price=10,
                    Quantity=10,
                    CreatedDate=DateTime.Now
                },
                new Inventory {
                    InventoryId=2,
                    Name="Test2",
                    Price=20,
                    Quantity=20,
                    CreatedDate=DateTime.Now
                },
            };

            return list;
        }

        private Inventory GetInventoryById(int id)
        {
            var inventory = GetInventoryList().FirstOrDefault(x => x.InventoryId == id);

            return inventory;
        }
        

        private IEnumerable<Inventory> AddRangeInventoryList()
        {
            List<Inventory> list = new List<Inventory>()
            {
                new Inventory {
                    InventoryId=4,
                    Name="Test44",
                    Price=40,
                    Quantity=40,
                    CreatedDate=DateTime.Now
                },
                new Inventory {
                    InventoryId=5,
                    Name="Test55",
                    Price=50,
                    Quantity=50,
                    CreatedDate=DateTime.Now
                },
            };

            return list;
        }

        private IEnumerable<Inventory> AddRangeAsyncInventoryList()
        {
            List<Inventory> list = new List<Inventory>()
            {
                new Inventory {
                    InventoryId=6,
                    Name="Test66",
                    Price=40,
                    Quantity=40,
                    CreatedDate=DateTime.Now
                },
                new Inventory {
                    InventoryId=7,
                    Name="Test77",
                    Price=50,
                    Quantity=50,
                    CreatedDate=DateTime.Now
                },
            };

            return list;
        }

        private IEnumerable<Inventory> GetRemoveRangeInventoryList()
        {
            List<Inventory> list = new List<Inventory>()
            {
                new Inventory {
                    InventoryId=8,
                    Name="Test88",
                    Price=80,
                    Quantity=80,
                    CreatedDate=DateTime.Now
                },
                new Inventory {
                    InventoryId=9,
                    Name="Test99",
                    Price=90,
                    Quantity=90,
                    CreatedDate=DateTime.Now
                }
            };

            return list;
        }

        private IEnumerable<Inventory> GetUpdateRangeInventoryList()
        {
            List<Inventory> list = new List<Inventory>()
            {
                new Inventory {
                    InventoryId=10,
                    Name="Test10",
                    Price=100,
                    Quantity=100,
                    CreatedDate=DateTime.Now
                },
                new Inventory {
                    InventoryId=11,
                    Name="Test11",
                    Price=110,
                    Quantity=110,
                    CreatedDate=DateTime.Now
                }
            };

            return list;
        }
    }
}
