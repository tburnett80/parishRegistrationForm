using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Contracts.Managers;

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

        /// <summary>
        /// Get status of export
        /// 
        /// path: 'api/export/check/requestid/'
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        [HttpGet("check/{requestId}")]
        public async Task<IActionResult> CheckStatus(Guid requestId)
        {
            var email = GetEmailFromClaim(HttpContext);
            if (string.IsNullOrEmpty(email))
                return BadRequest("No email claim found.");

            var result = await _manager.CheckStatus(requestId);
            if (!result.IsSuccessResult)
                return BadRequest(result.Message);

            return new ObjectResult(new
            {
                result.IsSuccessResult,
                result.Message
            });
        }

        /// <summary>
        /// This endpoint will queue up an export request
        /// that will result in an export being generated and compressed to email out
        /// to the email address in the Azure claim for email.
        /// 
        /// path: 'api/export/queue'
        /// </summary>
        /// <returns></returns>
        [HttpGet("queue")]
        public async Task<IActionResult> QueueExport()
        {
            var email = GetEmailFromClaim(HttpContext);
            if (string.IsNullOrEmpty(email))
                return BadRequest("No email claim found.");

            var result = await _manager.ExportDirectoryResults(email);
            if (!result.IsSuccessResult)
                return BadRequest(result.Message);

            return new ObjectResult(new
            {
                result.IsSuccessResult,
                result.Message
            });
        }

        private string GetEmailFromClaim(HttpContext ctx)
        {
            var claim = ctx.User.Claims.FirstOrDefault(c => c.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"));
            return claim?.Value;
        }
    }
}