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

        // GET: api/RequestItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestItem>>> GetRequestItems()
        {
            return await _context.RequestItems.ToListAsync();
        }

        // GET: api/RequestItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestItem>> GetRequestItem(Guid id)
        {
            var requestItem = await _context.RequestItems.FindAsync(id);

            if (requestItem == null)
            {
                return NotFound();
            }

            return requestItem;
        }

        // PUT: api/RequestItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/RequestItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestItem>> PostRequestItem(RequestItem requestItem)
        {
            _context.RequestItems.Add(requestItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRequestItem), new { id = requestItem.Id }, requestItem);
        }

        // DELETE: api/RequestItems/5
        [HttpDelete("{id}")]
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
