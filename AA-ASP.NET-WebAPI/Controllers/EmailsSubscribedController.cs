using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;
using TheAzureArchiveAPI.Helpers;
using TheAzureArchiveAPI.Models;
using TheAzureArchiveAPI.Services;

namespace TheAzureArchiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsSubscribedController : ControllerBase
    {
        private readonly IEmailsSubscribedService _emailsSubscribedService;
        private readonly ILogger<EmailsSubscribedController> _logger;

        public EmailsSubscribedController(IEmailsSubscribedService emailsSubscribedService, ILogger<EmailsSubscribedController> logger)
        {
            _emailsSubscribedService = emailsSubscribedService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
            try
            {
                _logger.LogInformation("GetEmailsAsync started");
                var email = await _emailsSubscribedService.GetEmailsAsync();
                if (email == null || !email.Any())
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(email));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetArticles error: {ex.Message}");
                return NotFound(ErrorMessagesEnum.NoElementFound);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmailById([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("GetEmailByIdAsync started");
                var email = await _emailsSubscribedService.GetEmailByIdAsync(id);
                if (email == null)
                {
                    return NotFound(ErrorMessagesEnum.NoElementFound);
                }
                return new JsonResult(Ok(email));
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetEmailById error: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmail([FromRoute] Guid id, [FromBody] UpdateEmailSubscribed email)
        {
            try
            {
                _logger.LogInformation($"UpdateEmail started");
                if (email == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                UpdateEmailSubscribed updatedEmail = await _emailsSubscribedService.UpdateEmailAsync(id, email);
                if (updatedEmail == null)
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

        [HttpPost]
        public async Task<IActionResult> AddEmail([FromBody] EmailSubscribed email)
        {
            try
            {
                _logger.LogInformation("AddEmailAsync started");
                if (email == null)
                {
                    return BadRequest(ErrorMessagesEnum.BadRequest);
                }
                await _emailsSubscribedService.AddEmailAsync(email);
                return new JsonResult(Ok(SuccessMessageEnum.ElementSuccessfullyCreated));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Validation exception: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail([FromRoute] Guid id)
        {
            try
            {
                _logger.LogInformation("DeleteEmailAsync started");
                bool email = await _emailsSubscribedService.DeleteEmailAsync(id);
                if (email)
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
