﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoJongWebService.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoJongWebService.Controllers
{
    [Authorize(Policy = "SuperAdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminItemsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminItemsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавление нового аккаунта администратора. #SuperAdmin
        /// </summary>
        [HttpPost()]
        public async Task<ActionResult<AdminItem>> PostAdminItem(AdminItem adminItem)
        {
            _context.AdminItems.Add(adminItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostAdminItem), new { id = adminItem.Id }, adminItem);
        }

        /// <summary>
        /// Получение всех администраторов (постранично). #SuperAdmin
        /// </summary>
        [HttpGet()]
        public async Task<ActionResult> GetAdminItems(int pageNumber = 1, int pageSize = 5)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 5;

            var totalRecords = await _context.AdminItems.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            var adminItems = await _context.AdminItems
                                           .Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToListAsync();

            var result = new
            {
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = adminItems
            };

            return Ok(result);
        }

        /// <summary>
        /// Получение администратора по id. #SuperAdmin
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminItem>> GetAdminItem(Guid id)
        {
            var adminItem = await _context.AdminItems.FindAsync(id);

            if (adminItem == null)
            {
                return NotFound();
            }

            return adminItem;
        }

        /// <summary>
        /// Обновление данных администратора. #SuperAdmin
        /// </summary>
        [HttpPut()]
        public async Task<IActionResult> PutAdminItem(AdminItem adminItem)
        {
            _context.Entry(adminItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminItemExists(adminItem.Id))
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
        /// Удаление администратора по id. #SuperAdmin
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminItem(Guid id)
        {
            var adminItem = await _context.AdminItems.FindAsync(id);
            if (adminItem == null)
            {
                return NotFound();
            }

            _context.AdminItems.Remove(adminItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminItemExists(Guid id)
        {
            return _context.AdminItems.Any(e => e.Id == id);
        }
    }
}
