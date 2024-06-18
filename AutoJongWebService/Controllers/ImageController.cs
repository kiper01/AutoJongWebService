using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace AutoJongWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// Загрузка изображения. #Admin
        /// </summary>
        [HttpPost()]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "Images");

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/images/{file.FileName}";

            return Ok(new { Url = fileUrl });
        }

        /// <summary>
        /// Получение изображения по имени файла.
        /// </summary>
        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "Images", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var mimeType = GetMimeType(filePath);
            return PhysicalFile(filePath, mimeType);
        }

        /// <summary>
        /// Удаление изображения по имени файла. #Admin
        /// </summary>
        [HttpDelete("{fileName}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteImage(string fileName)
        {
            var filePath = Path.Combine(_environment.WebRootPath, "Images", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            System.IO.File.Delete(filePath);

            return NoContent();
        }

        private string GetMimeType(string filePath)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}