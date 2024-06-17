using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoJongWebService.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoJongWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReviewItemsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/ReviewItems
        [HttpPost()]
        public async Task<ActionResult<ReviewItem>> PostReviewItem(ReviewItem reviewItem)
        {
            _context.ReviewItems.Add(reviewItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostReviewItem), new { id = reviewItem.Id }, reviewItem);
        }

        // GET: api/ReviewItems
        [HttpGet()]
        public async Task<ActionResult> GetReviewItems(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 5;

            var totalRecords = await _context.ReviewItems.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var reviewItems = await _context.ReviewItems
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();

            var result = new
            {
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = reviewItems
            };

            return Ok(result);
        }

        // GET: api/ReviewItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewItem>> GetReviewItem(Guid id)
        {
            var reviewItem = await _context.ReviewItems.FindAsync(id);

            if (reviewItem == null)
            {
                return NotFound();
            }

            return reviewItem;
        }

        // DELETE: api/ReviewItems/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteReviewItem(Guid id)
        {
            var reviewItem = await _context.ReviewItems.FindAsync(id);
            if (reviewItem == null)
            {
                return NotFound();
            }

            _context.ReviewItems.Remove(reviewItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewItemExists(Guid id)
        {
            return _context.ReviewItems.Any(e => e.Id == id);
        }
    }
}
