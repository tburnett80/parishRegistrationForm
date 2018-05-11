using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Contracts.Managers;
using ParishForms.ViewModels;

namespace ParishForms.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Export")]
    public class ExportController : Controller
    {
        #region Constructor and Private members
        //private readonly IDirectoryExportManager _manager;

        public ExportController()
        {
            //_manager = manager
            //    ?? throw new ArgumentNullException(nameof(manager));
        }
        #endregion

        [HttpGet("check/{requestId}")]
        public async Task<ExportQueueResultViewModel> CheckStatus(Guid requestId)
        {
            var ctx = HttpContext.User.Identity;
            var stuff = ctx;
            return null;
        }
    }
}