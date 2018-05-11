using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Contracts.Managers;
using ParishForms.ViewModels;

namespace ParishForms.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/export")]
    public class ExportController : Controller
    {
        #region Constructor and Private members
        private readonly IExportManager _manager;

        public ExportController(IExportManager manager)
        {
            _manager = manager
                ?? throw new ArgumentNullException(nameof(manager));
        }
        #endregion

        [HttpGet("check/{requestId}")]
        public async Task<ExportQueueResultViewModel> CheckStatus(Guid requestId)
        {
            var ctx = HttpContext.User.Identity;
            var stuff = ctx;


            return null;
        }

        /// <summary>
        /// This endpoint will queue up an export request
        /// that will result in an export being generated and compressed to email out
        /// to the email address in the Azure claim for email.
        /// 
        /// path : 'api/export/queue'
        /// </summary>
        /// <returns></returns>
        [HttpGet("queue")]
        public async Task<IActionResult> QueueExport()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));

            if (string.IsNullOrEmpty(claim?.Value))
                return BadRequest("No email claim found.");

            var result = await _manager.ExportDirectoryResults(claim.Value);
            if (!result.IsSuccessResult)
                return BadRequest(result.Message);

            return await Task.FromResult(
                new ObjectResult(new
                {
                    result.IsSuccessResult,
                    result.Message
                }));
        }
    }
}