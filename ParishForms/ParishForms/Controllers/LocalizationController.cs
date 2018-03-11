using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Contracts.Managers;

namespace ParishForms.Controllers
{
    [Produces("application/json")]
    [Route("api/Localization")]
    public class LocalizationController : Controller
    {
        #region Constructor and Private Members
        private readonly ILocalizationManager _manager;

        public LocalizationController(ILocalizationManager manager)
        {
            _manager = manager
                ?? throw new ArgumentNullException(nameof(manager));
        }
        #endregion

        [HttpGet("states")]
        public async Task<IActionResult> GetStatesList()
        {
            var states = await _manager.GetStates();
            return Ok(states.ToDictionary(k => k.Abbreviation, v => v.Name));
        }

        [HttpGet("labels/{culture}")]
        public async Task<IActionResult> GetFormLabels(string culture)
        {
            return await Task.Factory.StartNew(() =>
            {
                return Ok(new Dictionary<string, string>
                {
                    { "Household Name", "Familia" },
                    { "Home Phone", "Teléfono de casa" },
                    { "Street Address", "Dirección" },
                    { "City", "Ciudad" },
                    { "State", "Estado" },
                    { "Zip", "Codigo" },
                    { "Adult 1", "Adulto 1" },
                    { "Adult 2", "Adulto 2" },
                    { "Email Address", "correo electronico" },
                    { "Mobile Phone", "Teléfono móvil" }
                });
            });
        }
    }
}