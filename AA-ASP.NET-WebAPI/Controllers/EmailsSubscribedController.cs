using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.Helpers;
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

        [HttpPost]
        public async Task<IActionResult> AddEmailAsync([FromBody] EmailSubscribed email)
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
    }
}
