using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.Controllers
{
    [Route("")]
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;
        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<string> ReturnChallangeName()
        {
            string response = String.Empty;
            try
            {
                response = "Fullstack Challenge 20201026";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
