using Microsoft.AspNetCore.Mvc;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace PoliteWrite.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenPoliteController : ControllerBase
    {
      
        private readonly ILogger<GenPoliteController> _logger;
        private const string OPENAPI_TOKEN = "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";//输入自己的api-key
        public GenPoliteController(ILogger<GenPoliteController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GenResult")]
        public async Task<string> GenResult([FromQuery] string originText)
        {
            OpenAIService service = new OpenAIService(new OpenAiOptions() { ApiKey = OPENAPI_TOKEN });
            CompletionCreateRequest createRequest = new CompletionCreateRequest()
            {

                Prompt = $@"你作为一个职场沟通大师,请你用更加得体/规范的句子,帮我优化一下我下面的句子,语言保持不变：
                           ```
                            {originText}
                           ```",
                Temperature = 0.3f,
                MaxTokens = 1000
            };

            var res = await service.Completions.CreateCompletion(createRequest, Models.TextDavinciV3);

            if (res.Successful)
            {
                return res.Choices.FirstOrDefault()!.Text;
            }
            return string.Empty;
        }
    }
}