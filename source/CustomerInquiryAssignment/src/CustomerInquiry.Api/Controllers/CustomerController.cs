using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerInquiry.Commons;
using CustomerInquiry.Services;
using CustomerInquiry.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerInquiry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly JsonSerializerSettings _serializerSettings;
        
        // Constructor
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

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
                // Validate
                if (!ModelState.IsValid)
                {
                    IEnumerable<string> allErrors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage));
                    return BadRequest(allErrors);
                }

                if (!ObjectHelper.IsValidRequest(req))
                {
                    return BadRequest(ValidationMessage.NoInquiryCriteria);
                }

                // Action
                var response = await _customerService.InquiryAsync(req);
                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
            }
            catch (InquiryException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
