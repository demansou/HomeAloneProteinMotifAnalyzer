using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using HomeAloneBackend.Models;
using HomeAloneBackend.Services;

namespace HomeAloneBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitController : ControllerBase
    {
        private readonly IFileUploadService _fileUploadService;

        public SubmitController(IFileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        // POST: api/Submit
        [HttpPost, EnableCors(Startup.MY_CORS_ORIGINS), DisableRequestSizeLimit]
        public async Task<ActionResult> Post([FromForm] ApiDataModel formData)
        {
            await _fileUploadService.SaveAsync(formData);

            return Ok();
        }
    }
}
