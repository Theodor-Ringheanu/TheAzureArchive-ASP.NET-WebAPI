using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheAzureArchiveAPI.Helpers;
using TheAzureArchiveAPI.Models;
using TheAzureArchiveAPI.Services;

namespace TheAzureArchiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesService _articlesService;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IArticlesService articlesService, ILogger<ArticlesController> logger)
        {
            _articlesService = articlesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticlesAsync()
        {
            try
            {
                _logger.LogInformation("GetArticlesAsync started");
                var articles = await _articlesService.GetArticlesAsync();
                if (articles == null || !articles.Any())
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(articles));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetArticles error: {ex.Message}");
                return NotFound(ErrorMessagesEnum.NoElementFound);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleByIdAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("GetArticleByIdAsync started");
                var article = await _articlesService.GetArticleByIdAsync(id);
                if (article == null)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(article));
            }
            catch(Exception ex)
            {
                _logger.LogError($"GetArticleById error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddArticleAsync([FromBody] Article article)
        {
            try
            {
                _logger.LogInformation("AddArticleAsync started");
                if (article == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _articlesService.AddArticleAsync(article);
                return new JsonResult(Ok(SuccessMessageEnum.ElementSuccessfullyAdded));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleAsync([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("DeleteArticleAsync started");
                bool result = await _articlesService.DeleteArticleAsync(id);
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
