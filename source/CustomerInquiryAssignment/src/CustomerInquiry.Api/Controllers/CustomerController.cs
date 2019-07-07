using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerInquiry.Commons;
using CustomerInquiry.Services;
using CustomerInquiry.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomerInquiry.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        // Constructor
        public CustomerController(ICustomerService customerService
                                    , ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
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

                    _logger.LogDebug("Bad Request with message = {0}", string.Join(',', allErrors));
                    return BadRequest(allErrors);
                }

                if (!ObjectHelper.IsValidRequest(req))
                {
                    _logger.LogDebug("Bad Request with message = {0}", ValidationMessage.NoInquiryCriteria);
                    return BadRequest(ValidationMessage.NoInquiryCriteria);
                }

                // Action
                var response = await _customerService.InquiryAsync(req);
                if (response == null)
                {
                    _logger.LogDebug("Not found with value -> CustomerID: {0}, Email: {1}", req.CustomerID, req.Email);
                    return NotFound();
                }

                _logger.LogInformation("Success with value -> CustomerID: {0}, Email: {1}", req.CustomerID, req.Email);
                return Ok(response);
            }
            catch (InquiryException)
            {
                _logger.LogError("Bad Request");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
