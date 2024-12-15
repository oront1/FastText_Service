using FastTextService.Models;
using Microsoft.AspNetCore.Mvc;

namespace Snitch_ForbiddenWordsMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyzeController : ControllerBase
    {
        private readonly ForbiddenWordsChecker _checker;

        public AnalyzeController(ForbiddenWordsChecker checker)
        {
            _checker = checker;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AnalyzeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
                return BadRequest("Text cannot be empty.");

            string forbiddenWords = _checker.ContainsSimilarWords(request.Text);
            return Ok(new { ForbiddenWords = forbiddenWords });
        }
    }
}
