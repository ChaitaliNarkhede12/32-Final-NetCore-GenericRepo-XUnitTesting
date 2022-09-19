using CAW.Application.Interfaces;
using CAW.Application.Models;
using CAW.Application.Services;
using CAW.Data.Interfaces;
using CAW.Data.Models;
using CAW.Data.Repository;
using CAW.UnitTest.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CAW.Application.UnitTest.Services
{
    public class InventoryServiceTest
    {
        [Fact]
        public async Task GetAllInventory_ShouldReturnList()
        {
            //Arrange
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var mockInventoryRepo = new Mock<IInventoryRepository>();
            mockInventoryRepo.Setup(repo => repo.GetAll())
                .ReturnsAsync(GetInventoryList());

            var service = new InventoryService(mockInventoryRepo.Object, mapper);

            //Act
            var result = await service.GetAllInventory();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<InventoryModel>>(result);
        }

        [Fact]
        public async Task GetInventoryById_ShouldReturnList()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var mockInventoryRepo = new Mock<IInventoryRepository>();
            mockInventoryRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync(GetInventoryById(id));

            var service = new InventoryService(mockInventoryRepo.Object, mapper);

            //Act
            var result = await service.GetInventoryById(id);

            //Assert
            Assert.IsAssignableFrom<InventoryModel>(result);
        }

        [Fact]
        public async Task GetInventoryByIdUsingPredicate_ShouldReturnList()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var mockInventoryRepo = new Mock<IInventoryRepository>();
            mockInventoryRepo.Setup(repo => repo.GetById(id))
                .ReturnsAsync(GetInventoryById(id));

            var service = new InventoryService(mockInventoryRepo.Object, mapper);

            //Act
            var result = await service.GetInventoryById(x=>x.InventoryId ==id);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<InventoryModel>>(result);
        }


        [Fact]
        public async Task AddInventoryAsync_ShouldSaveRecord()
        {
            //Arrange
            var addInventory = AddInventory();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();
            inventoryRepository.Setup(x=>x.AddAsync(addInventory)).ReturnsAsync(addInventory);
            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var addInventoryModel = mapper.Map<InventoryModel>(addInventory);

            var result = service.AddInventoryAsync(addInventoryModel);

            Assert.Equal(result.Result, 1);

            //IUnitOfWork unitOfWork = new UnitOfWork(fixture.inventoryDBContext);

            //IRepository<Inventory, int> repository = new Repository<Inventory, int>(unitOfWork);

            //IInventoryRepository inventoryRepository = new InventoryRepository(repository);

            //IInventoryService service = new InventoryService(inventoryRepository, mapper);

            //var addInventoryModel = mapper.Map<InventoryModel>(addInventory);
            //var result = service.AddInventoryAsync(addInventoryModel);

            //Assert.IsAssignableFrom<InventoryModel>(result.Result);


            //var unitOfWork = new Mock<UnitOfWork>(fixture.inventoryDBContext);

            //var repository = new Mock<Repository<Inventory, int>>(unitOfWork.Object);

            //var inventoryRepository = new Mock<InventoryRepository>(repository.Object);
            //    //.Setup(x=>x.AddAsync(addInventory)).ReturnsAsync(AddInventory());

            //var service = new InventoryService(inventoryRepository.Object,mapper);


            //var addInventoryModel = mapper.Map<InventoryModel>(addInventory);
            //var result = service.AddInventoryAsync(addInventoryModel);

            //Assert.IsAssignableFrom<InventoryModel>(result.Result);
        }

        [Fact]
        public async Task AddInventoryAsync_ShouldNotSaveRecord()
        {
            //Arrange
            var addInventory = AddInventory();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();
            inventoryRepository.Setup(x => x.AddAsync(addInventory)).ReturnsAsync(addInventory);
            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var addInventoryModel = mapper.Map<InventoryModel>(addInventory);

            var result = service.AddInventoryAsync(addInventoryModel);

            Assert.Equal(result.Result, 0);
       }


        [Fact]
        public async Task UpdateInventory_ShouldUpdateRecord()
        {
            //Arrange
            var updateInventory = GetInventoryById(1);
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.Update(updateInventory)).Returns(updateInventory);

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var updateInventoryModel = mapper.Map<InventoryModel>(updateInventory);

            var result = service.AddInventoryAsync(updateInventoryModel);

            Assert.Equal(result.Result, 1);
        }


        [Fact]
        public async Task UpdateInventory_ShouldNotUpdateRecord()
        {
            //Arrange
            var updateInventory = GetInventoryById(1);
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.Update(updateInventory)).Returns(updateInventory);

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var updateInventoryModel = mapper.Map<InventoryModel>(updateInventory);

            var result = service.AddInventoryAsync(updateInventoryModel);

            Assert.Equal(result.Result, 0);
        }


        [Fact]
        public async Task RemoveInventory_ShouldRemoveRecord()
        {
            //Arrange
            var deleteInventory = GetInventoryById(1);
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.Remove(deleteInventory));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var deleteInventoryModel = mapper.Map<InventoryModel>(deleteInventory);

            var result = service.RemoveInventory(deleteInventoryModel);

            Assert.Equal(result.Result, 1);
        }

        [Fact]
        public async Task RemoveInventory_ShouldNotRemoveRecord()
        {
            //Arrange
            var deleteInventory = GetInventoryById(1);
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.Remove(deleteInventory));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var deleteInventoryModel = mapper.Map<InventoryModel>(deleteInventory);

            var result = service.RemoveInventory(deleteInventoryModel);

            Assert.Equal(result.Result, 0);
        }


        [Fact]
        public async Task RemoveInventoryById_ShouldRemoveRecord()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.RemoveById(1));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var result = service.RemoveInventoryById(id);

            Assert.Equal(result.Result, 1);
        }

        [Fact]
        public async Task RemoveInventoryById_ShouldNotRemoveRecord()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.RemoveById(1));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var result = service.RemoveInventoryById(id);

            Assert.Equal(result.Result, 0);
        }


        [Fact]
        public async Task SingleOrDefaultAsync_ShouldReturnList()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var mockInventoryRepo = new Mock<IInventoryRepository>();
            mockInventoryRepo.Setup(repo => repo.SingleOrDefaultAsync(x=>x.InventoryId == id))
                .ReturnsAsync(GetInventoryById(id));

            var service = new InventoryService(mockInventoryRepo.Object, mapper);

            //Act
            var result = await service.SingleOrDefaultAsync(x => x.InventoryId == id);

            //Assert
            Assert.IsAssignableFrom<InventoryModel>(result);
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnList()
        {
            //Arrange
            int id = 1;
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var mockInventoryRepo = new Mock<IInventoryRepository>();
            mockInventoryRepo.Setup(repo => repo.FirstOrDefaultAsync(x => x.InventoryId == id))
                .ReturnsAsync(GetInventoryById(id));

            var service = new InventoryService(mockInventoryRepo.Object, mapper);

            //Act
            var result = await service.FirstOrDefaultAsync(x => x.InventoryId == id);

            //Assert
            Assert.IsAssignableFrom<InventoryModel>(result);
        }


        [Fact]
        public async Task AddRange_ShouldSaveRecord()
        {
            //Arrange
            var addInventoryList = GetInventoryList();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.AddRange(addInventoryList));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var addInventoryModelList = mapper.Map<IEnumerable<InventoryModel>>(addInventoryList);

            var result = service.AddRange(addInventoryModelList);

            Assert.Equal(result.Result, 1);
        }

        [Fact]
        public async Task AddRange_ShouldNotSaveRecord()
        {
            //Arrange
            var addInventoryList = GetInventoryList();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.AddRange(addInventoryList));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var addInventoryModelList = mapper.Map<IEnumerable<InventoryModel>>(addInventoryList);

            var result = service.AddRange(addInventoryModelList);

            Assert.Equal(result.Result, 0);
        }



        [Fact]
        public async Task AddRangeAsync_ShouldSaveRecord()
        {
            //Arrange
            var addInventoryList = GetInventoryList();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.AddRangeAsync(addInventoryList));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var addInventoryModelList = mapper.Map<IEnumerable<InventoryModel>>(addInventoryList);

            var result = service.AddRangeAsync(addInventoryModelList);

            Assert.Equal(result.Result, 1);
        }

        [Fact]
        public async Task AddRangeAsync_ShouldNotSaveRecord()
        {
            //Arrange
            var addInventoryList = GetInventoryList();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.AddRangeAsync(addInventoryList));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var addInventoryModel = mapper.Map<IEnumerable<InventoryModel>>(addInventoryList);

            var result = service.AddRangeAsync(addInventoryModel);

            Assert.Equal(result.Result, 0);
        }


        [Fact]
        public async Task UpdateRange_ShouldUpdateRecord()
        {
            //Arrange
            var updateInventoryList = GetInventoryList();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.UpdateRange(updateInventoryList));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var updateInventoryModelList = mapper.Map<IEnumerable<InventoryModel>>(updateInventoryList);

            var result = service.UpdateRange(updateInventoryModelList);

            Assert.Equal(result.Result, 1);
        }

        [Fact]
        public async Task UpdateRange_ShouldNotUpdateRecord()
        {
            //Arrange
            var updateInventoryList = GetInventoryList();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.UpdateRange(updateInventoryList));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var updateInventoryModelList = mapper.Map<IEnumerable<InventoryModel>>(updateInventoryList);

            var result = service.UpdateRange(updateInventoryModelList);

            Assert.Equal(result.Result, 0);
        }


        [Fact]
        public async Task RemoveRange_ShouldRemoveeRecord()
        {
            //Arrange
            var removeInventoryList = GetInventoryList();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.RemoveRange(removeInventoryList));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var removeInventoryModelList = mapper.Map<IEnumerable<InventoryModel>>(removeInventoryList);

            var result = service.RemoveRange(removeInventoryModelList);

            Assert.Equal(result.Result, 1);
        }

        [Fact]
        public async Task RemoveRange_ShouldNotRemoveRecord()
        {
            //Arrange
            var removeInventoryList = GetInventoryList();
            AutoMapping autoMapping = new AutoMapping();
            var mapper = autoMapping.GetMapper(new InventoryMapping());

            var inventoryRepository = new Mock<IInventoryRepository>();

            inventoryRepository.Setup(x => x.RemoveRange(removeInventoryList));

            inventoryRepository.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            var service = new InventoryService(inventoryRepository.Object, mapper);

            var removeInventoryModelList = mapper.Map<IEnumerable<InventoryModel>>(removeInventoryList);

            var result = service.RemoveRange(removeInventoryModelList);

            Assert.Equal(result.Result, 0);
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

        private Inventory AddInventory()
        {
            Inventory obj = new Inventory()
            {
                InventoryId = 1,
                Name = " Test 1",
                Price = 10,
                Quantity = 10,
                CreatedDate = DateTime.Now
            };

            return obj;
        }

    }
}
