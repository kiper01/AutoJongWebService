using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoJongWebService.Models;

namespace AutoJongWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarItemsController : ControllerBase
    {
        private readonly CarContext _context;

        public CarItemsController(CarContext context)
        {
            _context = context;
        }

        // POST: api/CarItems/Add
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<CarItem>> PostCarItem(CarItem carItem)
        {
            _context.CarItems.Add(carItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCarItem), new { id = carItem.Id }, carItem);
        }

        // GET: api/CarItems/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetCarItems(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 5;

            var totalRecords = await _context.CarItems.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var carItems = await _context.CarItems
                                         .Skip((pageNumber - 1) * pageSize)
                                         .Take(pageSize)
                                         .ToListAsync();

            var result = new
            {
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = carItems
            };

            return Ok(result);
        }


        // GET: api/CarItems/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CarItem>> GetCarItem(Guid id)
        {
            var carItem = await _context.CarItems.FindAsync(id);

            if (carItem == null)
            {
                return NotFound();
            }

            return carItem;
        }

        // PUT: api/CarItems/UpdateByID
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update")]
        public async Task<IActionResult> PutCarItem(CarItem carItem)
        {
            _context.Entry(carItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarItemExists(carItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/CarItems/DeleteById/5
        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteCarItem(Guid id)
        {
            var carItem = await _context.CarItems.FindAsync(id);
            if (carItem == null)
            {
                return NotFound();
            }

            _context.CarItems.Remove(carItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarItemExists(Guid id)
        {
            return _context.CarItems.Any(e => e.Id == id);
        }
    }
}
