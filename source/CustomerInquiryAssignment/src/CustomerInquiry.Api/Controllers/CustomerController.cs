using System.Threading.Tasks;
using CustomerInquiry.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerInquiry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly JsonSerializerSettings _serializerSettings;
        
        // Constructor
        public CustomerController()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost]
        [Route("action")]
        public async Task<InquiryResponse> Inquiry([FromBody] InquiryRequest req)
        {
            var result = new InquiryResponse();

            return result;
        }
    }
}
