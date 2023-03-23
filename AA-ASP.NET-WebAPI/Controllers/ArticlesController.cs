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
            catch (Exception ex)
            {
                _logger.LogError($"GetArticleById error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateArticleAsync([FromBody] Article article)
        {
            try
            {
                _logger.LogInformation("CreateArticleAsync started");
                if (article == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _articlesService.CreateArticleAsync(article);
                return new JsonResult(Ok(SuccessMessageEnum.ElementSuccessfullyCreated));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] Guid id, [FromBody] UpdateArticle article)
        {
            try
            {
                _logger.LogInformation($"UpdateArticle started");
                if (article == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                UpdateArticle updatedArticle = await _articlesService.UpdateArticleAsync(id, article);
                if (updatedArticle == null)
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
        public async Task<IActionResult> PartiallyUpdateArticle([FromRoute] Guid id, [FromBody] PatchArticle article)
        {
            try
            {
                _logger.LogInformation($"PartiallyUpdateArticle started");
                if (article == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }

                PatchArticle updatedArticle = await _articlesService.PartiallyUpdateArticleAsync(id, article);
                if (updatedArticle == null)
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
