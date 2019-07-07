using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerInquiry.Commons;
using CustomerInquiry.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        [Route("[action]")]
        public async Task<IActionResult> Inquiry(InquiryRequest req)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    IEnumerable<string> allErrors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage));
                    return BadRequest(allErrors);
                }

                if (!ObjectHelper.IsValidRequest(req))
                {
                    return BadRequest(ValidationMessage.NoInquiryCriteria);
                }
            }
            catch(Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
