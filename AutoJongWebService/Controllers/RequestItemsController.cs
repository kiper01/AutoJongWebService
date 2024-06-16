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
    public class RequestItemsController : ControllerBase
    {
        private readonly RequestContext _context;

        public RequestItemsController(RequestContext context)
        {
            _context = context;
        }

        // POST: api/RequestItems/Add
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<RequestItem>> PostRequestItem(RequestItem requestItem)
        {
            _context.RequestItems.Add(requestItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRequestItem), new { id = requestItem.Id }, requestItem);
        }

        // GET: api/RequestItems/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetRequestItems(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 5;

            var totalRecords = await _context.RequestItems.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var requestItems = await _context.RequestItems
                                             .Skip((pageNumber - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToListAsync();

            var result = new
            {
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = requestItems
            };

            return Ok(result);
        }

        // GET: api/RequestItems/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<RequestItem>> GetRequestItem(Guid id)
        {
            var requestItem = await _context.RequestItems.FindAsync(id);

            if (requestItem == null)
            {
                return NotFound();
            }

            return requestItem;
        }

        // PUT: api/RequestItems/Update/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PutRequestItem(Guid id, RequestItem requestItem)
        {
            if (id != requestItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestItemExists(id))
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

        // DELETE: api/RequestItems/DeleteById/5
        [HttpDelete("DeleteById/{id}")]
        public async Task<IActionResult> DeleteRequestItem(Guid id)
        {
            var requestItem = await _context.RequestItems.FindAsync(id);
            if (requestItem == null)
            {
                return NotFound();
            }

            _context.RequestItems.Remove(requestItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestItemExists(Guid id)
        {
            return _context.RequestItems.Any(e => e.Id == id);
        }
    }
}
