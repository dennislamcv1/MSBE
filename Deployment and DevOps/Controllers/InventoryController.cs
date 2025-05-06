using Microsoft.AspNetCore.Mvc;
using LogiTrack.Data;
using LogiTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace LogiTrack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly LogiTrackContext _context;

        public InventoryController(LogiTrackContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllInventory()
        {
            return Ok(_context.InventoryItems.ToList());
        }

        [HttpPost]
        public IActionResult AddInventoryItem([FromBody] InventoryItem item)
        {
            _context.InventoryItems.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllInventory), new { id = item.ItemId }, item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInventoryItem(int id)
        {
            var item = _context.InventoryItems.Find(id);
            if (item == null) return NotFound();

            _context.InventoryItems.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
        
        private readonly IMemoryCache _cache;
        public InventoryController(LogiTrackContext context, IMemoryCache cache)
        {
        _context = context;
        _cache = cache;
        }


        [Authorize] // Require authentication
        [Authorize(Roles = "Manager")] // Role-specific

    }
}
