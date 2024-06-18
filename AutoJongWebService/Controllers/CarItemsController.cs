using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoJongWebService.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoJongWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarItemsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление новой машины. #Admin
        /// </summary>
        [HttpPost()]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CarItem>> PostCarItem(CarItem carItem)
        {
            _context.CarItems.Add(carItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCarItem), new { id = carItem.Id }, carItem);
        }

        /// <summary>
        /// Получение всех машин (постарнично). #Admin
        /// </summary>
        [HttpGet()]
        [Authorize(Policy = "AdminOnly")]
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

        /// <summary>
        /// Получение машины по id. #Admin
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<CarItem>> GetCarItem(Guid id)
        {
            var carItem = await _context.CarItems.FindAsync(id);

            if (carItem == null)
            {
                return NotFound();
            }

            return carItem;
        }

        /// <summary>
        /// Обновление данных машины. #Admin
        /// </summary>
        [HttpPut()]
        [Authorize(Policy = "AdminOnly")]
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

        /// <summary>
        /// Удаление машины по id. #Admin
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
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

        /// <summary>
        /// Получение списка машин, удолетворяющих условию теста.
        /// </summary>
        [HttpPost("Quiz")]
        public async Task<ActionResult> GetCarItemsByQuiz(QuizModel quiz)
        {
            var carItems = await _context.CarItems
                                         .ApplyQuizFilter(quiz)
                                         .ToListAsync();

            return Ok(carItems);
        }

        private bool CarItemExists(Guid id)
        {
            return _context.CarItems.Any(e => e.Id == id);
        }
    }
}

public class QuizModel
{
    public uint YearFrom { get; set; }
    public uint YearTo { get; set; }
    public uint PriceFrom { get; set; }
    public uint PriceTo { get; set; }
    public CarItem.FuelType FuelType { get; set; }
    public CarItem.CountryType CountryType { get; set; }
    public CarItem.BodyType BodyType { get; set; }
    public CarItem.GearboxType GearboxType { get; set; }
}