using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheAzureArchiveAPI.Helpers;
using TheAzureArchiveAPI.Services;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.Models;
using TheAzureArchiveAPI.DataTransferObjects.PatchObjects;

namespace TheAzureArchiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoriesService _storiesService;
        private readonly ILogger<StoriesController> _logger;

        public StoriesController(IStoriesService storiesService, ILogger<StoriesController> logger)
        {
            _storiesService = storiesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStoriesAsync()
        {
            try
            {
                _logger.LogInformation("GetStoriesAsync started");
                var stories = await _storiesService.GetStoriesAsync();
                if (stories == null || !stories.Any())
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(stories));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetStories error: {ex.Message}");
                return NotFound(ErrorMessagesEnum.NoElementFound);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoryByIdAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("GetStoryByIdAsync started");
                var story = await _storiesService.GetStoryByIdAsync(id);
                if (story == null)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(story));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetStoryById error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStoryAsync([FromBody] Story story)
        {
            try
            {
                _logger.LogInformation("CreateStoryAsync started");
                if (story == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _storiesService.CreateStoryAsync(story);
                return new JsonResult(Ok(SuccessMessageEnum.ElementSuccessfullyCreated));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStory([FromRoute] Guid id, [FromBody] UpdateStory story)
        {
            try
            {
                _logger.LogInformation($"UpdateStory started");
                if (story == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                UpdateStory updatedStory = await _storiesService.UpdateStoryAsync(id, story);
                if (updatedStory == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return Ok(SuccessMessageEnum.ElementSuccessfullyUpdated);
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateStory([FromRoute] Guid id, [FromBody] PatchStory story)
        {
            try
            {
                _logger.LogInformation($"PartiallyUpdateStory started");
                if (story == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }

                PatchStory updatedStory = await _storiesService.PartiallyUpdateStoryAsync(id, story);
                if (updatedStory == null)
                {
                    return StatusCode((int)HttpStatusCode.NoContent, ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(SuccessMessageEnum.ElementSuccessfullyUpdated));
            }
            catch (ModelValidationException ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)(HttpStatusCode.InternalServerError), ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoryAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("DeleteStoryAsync started");
                bool result = await _storiesService.DeleteStoryAsync(id);
                if (result)
                {
                    return new JsonResult(Ok(SuccessMessageEnum.ElementSuccessfullyDeleted));
                }
                return BadRequest(ErrorMessagesEnum.NoElementFound);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
