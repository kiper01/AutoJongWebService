using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoJongWebService.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoJongWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RequestItemsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление новой заявки.
        /// </summary>
        [HttpPost()]
        public async Task<ActionResult<RequestItem>> PostRequestItem(RequestItem requestItem)
        {
            _context.RequestItems.Add(requestItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRequestItem), new { id = requestItem.Id }, requestItem);
        }

        /// <summary>
        /// Получение всех заявок (постранично). #Admin
        /// </summary>
        [HttpGet()]
        [Authorize(Policy = "AdminOnly")]
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

        /// <summary>
        /// Получение заявки по id. #Admin
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<RequestItem>> GetRequestItem(Guid id)
        {
            var requestItem = await _context.RequestItems.FindAsync(id);

            if (requestItem == null)
            {
                return NotFound();
            }

            return requestItem;
        }

        /// <summary>
        /// Обновление заявки. #Admin
        /// </summary>
        [HttpPut()]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutRequestItem(RequestItem requestItem)
        {
            _context.Entry(requestItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestItemExists(requestItem.Id))
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

        /// <summary>
        /// Удаление заявки по id. #Admin
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
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
