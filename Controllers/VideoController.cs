using Microsoft.AspNetCore.Mvc;
using Parliament_API.Data;
using Parliament_API.Models;

namespace Parliament_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public VideosController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ✅ Upload Video
        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo(IFormFile video)
        {
            if (video == null || video.Length == 0)
                return BadRequest("No video uploaded");

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "videos");

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(video.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await video.CopyToAsync(stream);
            }

            var videoEntity = new Video
            {
                FileName = fileName,
                FilePath = $"videos/{fileName}",
                FileSize = video.Length
            };

            _context.Videos.Add(videoEntity);
            await _context.SaveChangesAsync();

            return Ok(videoEntity);
        }

        // ✅ Get All Videos
        [HttpGet]
        public IActionResult GetVideos()
        {
            return Ok(_context.Videos.ToList());
        }
        // ✅ Delete Video
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
                return NotFound();

            // Delete file from server
            var filePath = Path.Combine(_env.WebRootPath ?? "wwwroot", video.FilePath);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            // Remove from database
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Video deleted successfully" });
        }

    }
}
