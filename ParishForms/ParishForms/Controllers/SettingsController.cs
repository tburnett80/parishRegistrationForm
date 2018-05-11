using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Models;

namespace ParishForms.Controllers
{
    [AllowAnonymous]
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
        public IActionResult GetSettings()
        {
            return Ok(new Dictionary<string, string>
            {
                { "redirectUrl", _settings.RedirectUrl }
            });
        }
    }
}