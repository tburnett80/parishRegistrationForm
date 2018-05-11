using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Contracts.Managers;
using ParishForms.Common.Models.Directory;
using ParishForms.ViewModels;

namespace ParishForms.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/directory")]
    public class DirectoryController : Controller
    {
        #region Constructor and Private Members
        private readonly IDirectoryManager _manager;

        public DirectoryController(IDirectoryManager manager)
        {
            _manager = manager
                ?? throw new ArgumentNullException(nameof(manager));
        }
        #endregion

        [HttpGet("limits")]
        public async Task<IActionResult> GetFormLimits()
        {
            var lengths = await _manager.GetFormLimits();
            return Ok(lengths);
        }

        [HttpPost]
        public async Task<IActionResult> StoreDirectoryForm([FromBody] DirectoryFormViewModel frm)
        {
            var result = await _manager.StoreSubmision(frm.ToDto());
            switch (result.Type)
            {
                case ResultType.Success:
                    return Ok();
                case ResultType.SaveFailure:
                    return BadRequest("failed to save all records");
                case ResultType.ValidationFailed:
                    return BadRequest("Validation failed.");
                case ResultType.Exception:
                    return StatusCode(500, result.Message);
                default:
                    return BadRequest("Unknown failure.");
            }
        }
    }
}
