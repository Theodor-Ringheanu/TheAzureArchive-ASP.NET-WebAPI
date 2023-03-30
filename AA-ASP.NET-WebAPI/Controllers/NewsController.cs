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
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsService newsService, ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsAsync()
        {
            try
            {
                _logger.LogInformation("GetNewsAsync started");
                var news = await _newsService.GetNewsAsync();
                if (news == null || !news.Any())
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(news));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetNews error: {ex.Message}");
                return NotFound(ErrorMessagesEnum.NoElementFound);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsByIdAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("GetNewsByIdAsync started");
                var news = await _newsService.GetNewsByIdAsync(id);
                if (news == null)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(news));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetNewsById error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewsAsync([FromBody] News news)
        {
            try
            {
                _logger.LogInformation("CreateNewsAsync started");
                if (news == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _newsService.CreateNewsAsync(news);
                return new JsonResult(Ok(SuccessMessageEnum.ElementSuccessfullyCreated));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNews([FromRoute] Guid id, [FromBody] UpdateNews news)
        {
            try
            {
                _logger.LogInformation($"UpdateNews started");
                if (news == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                UpdateNews updatedNews = await _newsService.UpdateNewsAsync(id, news);
                if (updatedNews == null)
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
        public async Task<IActionResult> PartiallyUpdateNews([FromRoute] Guid id, [FromBody] PatchNews news)
        {
            try
            {
                _logger.LogInformation($"PartiallyUpdateNews started");
                if (news == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }

                PatchNews updatedNews = await _newsService.PartiallyUpdateNewsAsync(id, news);
                if (updatedNews == null)
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
        public async Task<IActionResult> DeleteNewsAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("DeleteNewsAsync started");
                bool result = await _newsService.DeleteNewsAsync(id);
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
