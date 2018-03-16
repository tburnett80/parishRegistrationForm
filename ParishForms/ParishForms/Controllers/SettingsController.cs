using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Models;

namespace ParishForms.Controllers
{
    [Produces("application/json")]
    [Route("api/settings")]
    public class SettingsController : Controller
    {
        private readonly ConfigSettingsDto _settings;

        public SettingsController(ConfigSettingsDto settings)
        {
            _settings = settings
                ?? throw new ArgumentNullException(nameof(settings));
        }

        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            return Ok(new Dictionary<string, string>
            {
                { "redirectUrl", _settings.RedirectUrl }
            });
        }
    }
}