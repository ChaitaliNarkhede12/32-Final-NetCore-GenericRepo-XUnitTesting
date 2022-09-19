using CAW.API.Controllers;
using CAW.Application.Interfaces;
using CAW.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CAW.API.UnitTest.Controller
{
    public class InventoryControllerTest
    {
        [Fact]
        public async Task GetAllInventory_ReturnOkResult()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();

            mockRepo.Setup(repo => repo.GetAllInventory())
                .ReturnsAsync(GetInventoryList());

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.GetAllInventory();

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetInventoryById_ReturnOkResult()
        {
            // Arrange
            int id = 1;

            var mockRepo = new Mock<IInventoryService>();

            mockRepo.Setup(repo => repo.GetInventoryById(id))
                .ReturnsAsync(GetInventoryById(id));

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.GetInventoryById(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetInventoryByIdUsingPredicate_ReturnOkResult()
        {
            // Arrange
            int id = 1;

            var mockRepo = new Mock<IInventoryService>();

            mockRepo.Setup(repo => repo.GetInventoryById(x=>x.InventoryId == id))
                .ReturnsAsync(GetInventoryList());

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.GetInventoryByIdUsingPredicate(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async Task AddInventoryAsync_ReturnOkResult()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var addInventory = AddInventoryModel();

            mockRepo.Setup(repo => repo.AddInventoryAsync(addInventory))
                .ReturnsAsync(1);

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.AddInventoryAsync(addInventory);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddEmployeeAsync_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.AddInventoryAsync(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }



        [Fact]
        public async Task UpdateInventory_ReturnOkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IInventoryService>();
            var updateInventory = GetInventoryById(id);

            mockRepo.Setup(repo => repo.UpdateInventory(updateInventory))
                .ReturnsAsync(1);

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.UpdateInventory(updateInventory);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateInventory_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.UpdateInventory(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }



        [Fact]
        public async Task RemoveInventory_ReturnOkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IInventoryService>();
            var removeInventory = GetInventoryById(id);

            mockRepo.Setup(repo => repo.RemoveInventory(removeInventory))
                .ReturnsAsync(id);

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveInventory(removeInventory);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RemoveInventory_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveInventory(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }



        [Fact]
        public async Task RemoveInventoryById_ReturnOkResult()
        {
            // Arrange
            int id = 1;
            var mockRepo = new Mock<IInventoryService>();

            mockRepo.Setup(repo => repo.RemoveInventoryById(id))
                .ReturnsAsync(id);

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveInventoryById(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RemoveInventoryById_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveInventoryById(0);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }



        [Fact]
        public async Task GetInventorySingleOrDefaultAsync_ReturnOkResult()
        {
            // Arrange
            int id = 1;

            var mockRepo = new Mock<IInventoryService>();

            mockRepo.Setup(repo => repo.SingleOrDefaultAsync(x => x.InventoryId == id))
                .ReturnsAsync(GetInventoryById(id));

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.GetInventorySingleOrDefaultAsync(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetInventoryFirstOrDefaultAsync_ReturnOkResult()
        {
            // Arrange
            int id = 1;

            var mockRepo = new Mock<IInventoryService>();

            mockRepo.Setup(repo => repo.FirstOrDefaultAsync(x => x.InventoryId == id))
                .ReturnsAsync(GetInventoryById(id));

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.GetInventoryFirstOrDefaultAsync(id);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async Task AddInventoryRange_ReturnOkResult()
        {
            // Arrange
            int res = 1;
            var mockRepo = new Mock<IInventoryService>();
            var inventoryList = GetInventoryList();

            mockRepo.Setup(repo => repo.AddRange(inventoryList))
                .ReturnsAsync(res);

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.AddInventoryRange(inventoryList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddInventoryRange_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.AddInventoryRange(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }



        [Fact]
        public async Task AddInventoryRangeAsync_ReturnOkResult()
        {
            // Arrange
            int res = 1;
            var mockRepo = new Mock<IInventoryService>();
            var inventoryList = GetInventoryList();

            mockRepo.Setup(repo => repo.AddRangeAsync(inventoryList))
                .ReturnsAsync(res);

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.AddInventoryRangeAsync(inventoryList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task AddInventoryRangeAsync_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.AddInventoryRangeAsync(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async Task UpdateInventoryRange_ReturnOkResult()
        {
            // Arrange
            int res = 1;
            var mockRepo = new Mock<IInventoryService>();
            var inventoryList = GetInventoryList();

            mockRepo.Setup(repo => repo.UpdateRange(inventoryList))
                .ReturnsAsync(res);

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.UpdateInventoryRange(inventoryList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateInventoryRange_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.UpdateInventoryRange(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async Task RemoveInventoryRange_ReturnOkResult()
        {
            // Arrange
            int res = 1;
            var mockRepo = new Mock<IInventoryService>();
            var inventoryList = GetInventoryList();

            mockRepo.Setup(repo => repo.RemoveRange(inventoryList))
                .ReturnsAsync(res);

            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveInventoryRange(inventoryList);

            //// Assert
            Assert.Equal(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task RemoveInventoryRange_ReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IInventoryService>();
            var controller = new InventoryController(mockRepo.Object);

            //// Act
            var result = await controller.RemoveInventoryRange(null);

            //// Assert
            Assert.Equal(400, ((Microsoft.AspNetCore.Mvc.StatusCodeResult)result).StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }



        private IEnumerable<InventoryModel> GetInventoryList()
        {
            List<InventoryModel> list = new List<InventoryModel>()
            {
                new InventoryModel {
                    InventoryId=1,
                    Name="Test1",
                    Price=10,
                    Quantity=10,
                    CreatedDate=DateTime.Now
                },
                new InventoryModel {
                    InventoryId=2,
                    Name="Test2",
                    Price=20,
                    Quantity=20,
                    CreatedDate=DateTime.Now
                },
            };

            return list;
        }

        private InventoryModel GetInventoryById(int id)
        {
            var inventory = GetInventoryList().FirstOrDefault(x=>x.InventoryId == id);

            return inventory;
        }

        private InventoryModel AddInventoryModel()
        {
            InventoryModel obj = new InventoryModel()
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
