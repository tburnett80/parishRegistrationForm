using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Contracts.Managers;
using ParishForms.ViewModels;

namespace ParishForms.Controllers
{
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
            if (frm == null)
                return BadRequest("Could not deserialize the form.");

            var result = await _manager.StoreSubmision(frm.ToDto());
            if (result == -3)
                return BadRequest("required fields were missing, could not save.");

            return Ok();
        }
    }
}
