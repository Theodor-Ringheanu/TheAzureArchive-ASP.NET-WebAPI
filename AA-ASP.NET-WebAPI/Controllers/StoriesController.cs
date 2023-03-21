using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.Models;

namespace TheAzureArchiveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly TheAzureArchiveDataContext _context;
        public StoriesController(TheAzureArchiveDataContext context)
        {
            _context = context;
        }

        [Route("GetAllStories")]
        [HttpGet]
        public async Task<IActionResult> GetAllStories()
        {
            try
            {
                var stories = await _context.Stories.ToListAsync();
                if (stories == null || !stories.Any())
                    return StatusCode((int)HttpStatusCode.NoContent, "No story");
                return new JsonResult(Ok(stories));
            }
            catch (Exception ex)
            {
                return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
            }
        }

        [Route("GetStoryById")]
        [HttpGet]
        public async Task<IActionResult> GetStoryById(Guid id)
        {
            var storyById =  await _context.Stories.FindAsync(id);
            if (storyById == null)
                return new JsonResult(new { error = $"Story with ID {id} not found" }) { StatusCode = 404 };

            return new JsonResult(Ok(storyById));
        }

        [Route("AddStory")]
        [HttpPost]
        public async Task<IActionResult> AddStory([FromBody] Story story)
        {
            var storyInDb = _context.Stories.FirstOrDefault(s => s.Id == story.Id);
            if (storyInDb == null)
                _context.Stories.Add(story);
            else return new JsonResult(new { error = $"A story with ID {story.Id} already exists" }) { StatusCode = 2147024809 };

            await _context.SaveChangesAsync();
            return new JsonResult(Ok(story));
        }

        [Route("EditStory")]
        [HttpPut]
        public async Task<IActionResult> EditStory([FromBody] Story storyToEdit)
        {
            _context.Stories.Update(storyToEdit);

            await _context.SaveChangesAsync();
            return new JsonResult(Ok(storyToEdit));
        }

        [Route("DeleteStory")]
        [HttpDelete]
        public async Task<IActionResult> DeleteStory(Guid id)
        {
            var storyToDelete = await _context.Stories.FindAsync(id);
            if (storyToDelete == null)
                return new JsonResult(new { error = $"Story with ID {id} not found" }) { StatusCode = 404 };

            _context.Stories.Remove(storyToDelete);
            await _context.SaveChangesAsync();
            return new JsonResult(Ok(storyToDelete));
        }
    }
}
