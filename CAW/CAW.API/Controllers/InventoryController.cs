using CAW.Application.Interfaces;
using CAW.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAW.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
        {
            _service = service;
        }

        [HttpGet("GetAllInventory")]
        public async Task<IActionResult> GetAllInventory()
        {
            var result = await _service.GetAllInventory();
            return Ok(result);
        }

        [HttpGet("GetInventoryById/{id}")]
        public async Task<IActionResult> GetInventoryById(int id)
        {
            var result = await _service.GetInventoryById(id);

            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("GetInventoryByIdUsingPredicate/{id}")]
        public async Task<IActionResult> GetInventoryByIdUsingPredicate(int id)
        {
            var result = await _service.GetInventoryById(x => x.InventoryId == id);
            return Ok(result);
        }

        [HttpPost("AddInventoryAsync")]
        public async Task<IActionResult> AddInventoryAsync(InventoryModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest();
            }

            var result = await _service.AddInventoryAsync(inventory);
            return Ok(result);
        }

        [HttpPut("UpdateInventory")]
        public async Task<IActionResult> UpdateInventory(InventoryModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest();
            }

            var result = await _service.UpdateInventory(inventory);
            return Ok(result);
        }

        [HttpDelete("RemoveInventory")]
        public async Task<IActionResult> RemoveInventory(InventoryModel inventory)
        {
            if (inventory == null)
            {
                return BadRequest();
            }
            int result = await _service.RemoveInventory(inventory);
            return Ok(result);
        }

        [HttpDelete("RemoveInventoryById")]
        public async Task<IActionResult> RemoveInventoryById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            int result = await _service.RemoveInventoryById(id);
            return Ok(result);
        }

        


        [HttpGet("GetInventorySingleOrDefaultAsync/{id}")]
        public async Task<IActionResult> GetInventorySingleOrDefaultAsync(int id)
        {
            var result = await _service.SingleOrDefaultAsync(x => x.InventoryId == id);
            return Ok(result);
        }

        [HttpGet("GetInventoryFirstOrDefaultAsync/{id}")]
        public async Task<IActionResult> GetInventoryFirstOrDefaultAsync(int id)
        {
            var result = await _service.FirstOrDefaultAsync(x => x.InventoryId == id);
            return Ok(result);
        }



        [HttpPost("AddInventoryRange")]
        public async Task<IActionResult> AddInventoryRange(IEnumerable<InventoryModel> inventories)
        {
            if (inventories == null)
            {
                return BadRequest();
            }

            int result = await _service.AddRange(inventories);
            return Ok(result);
        }

        [HttpPost("AddInventoryRangeAsync")]
        public async Task<IActionResult> AddInventoryRangeAsync(IEnumerable<InventoryModel> inventories)
        {
            if (inventories == null)
            {
                return BadRequest();
            }

            int result = await _service.AddRangeAsync(inventories);
            return Ok(result);
        }


        [HttpPut("UpdateInventoryRange")]
        public async Task<IActionResult> UpdateInventoryRange(IEnumerable<InventoryModel> inventories)
        {
            if (inventories == null)
            {
                return BadRequest();
            }

            int result = await _service.UpdateRange(inventories);
            return Ok(result);
        }


        [HttpPut("RemoveInventoryRange")]
        public async Task<IActionResult> RemoveInventoryRange(IEnumerable<InventoryModel> inventories)
        {
            if (inventories == null)
            {
                return BadRequest();
            }

            int result = await _service.RemoveRange(inventories);
            return Ok(result);
        }
    }
}
