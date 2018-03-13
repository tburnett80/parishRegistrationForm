using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParishForms.Common.Contracts.Managers;
using ParishForms.ViewModels;

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
            return Ok(states.Select(s => new DropDownRow { Value = s.Abbreviation, Text = s.Name }));
        }

        [HttpGet("labels/{culture}")]
        public async Task<IActionResult> GetFormLabels(string culture)
        {
            var translations = await _manager.GetTranslations(culture);
            return Ok(translations.ToDictionary(k => k.KeyText, v => v.LocalizedText));
        }

        [HttpGet("list-cultures")]
        public async Task<IActionResult> GetSupportedCultures()
        {
            var cults = await _manager.GetCultureList();
            return Ok(cults.Select(c => new DropDownRow { Value = c.Culture, Text = c.Name }));
        }
    }
}