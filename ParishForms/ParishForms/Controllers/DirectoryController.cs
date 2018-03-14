using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Contracts.Managers;

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
    }
}
