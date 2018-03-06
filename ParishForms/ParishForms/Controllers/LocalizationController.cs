using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ParishForms.Controllers
{
    [Produces("application/json")]
    [Route("api/Localization")]
    public class LocalizationController : Controller
    {
        //AL|Alabama,AK|Alaska,AZ|Arizona,AR|Arkansas,CA|California,CO|Colorado,CT|Connecticut,DE|Delaware,DC|District Of Columbia,FL|Florida,GA|Georgia,HI|Hawaii,ID|Idaho,IL|Illinois,IN|Indiana,IA|Iowa,KS|Kansas,KY|Kentucky,LA|Louisiana,ME|Maine,MD|Maryland,MA|Massachusetts,MI|Michigan,MN|Minnesota,MS|Mississippi,MO|Missouri,MT|Montana,NE|Nebraska,NV|Nevada,NH|New Hampshire,NJ|New Jersey,NM|New Mexico,NY|New York,NC|North Carolina,ND|North Dakota,OH|Ohio,OK|Oklahoma,OR|Oregon,PA|Pennsylvania,RI|Rhode Island,SC|South Carolina,SD|South Dakota,TN|Tennessee,TX|Texas,UT|Utah,VT|Vermont,VA|Virginia,WA|Washington,WV|West Virginia,WI|Wisconsin,WY|Wyoming
        [HttpGet("states")]
        public async Task<IActionResult> GetStatesList()
        {
            return await Task.Factory.StartNew(() =>
            {
                return Ok(new[]
                {
                    new { Value = "AL", Text = "Alabama" },
                    new { Value = "AK", Text = "Alaska" },
                    new { Value = "AZ", Text = "Arizona" },
                    new { Value = "AR", Text = "Arkansas" },
                    new { Value = "CA", Text = "California" },
                    new { Value = "CO", Text = "Colorado" },
                    new { Value = "CT", Text = "Connecticut" },
                    new { Value = "DE", Text = "Delaware" },
                    new { Value = "DC", Text = "District Of Columbia" },
                    new { Value = "FL", Text = "Florida" },
                    new { Value = "GA", Text = "Georgia" },
                    new { Value = "HI", Text = "Hawaii" },
                    new { Value = "ID", Text = "Idaho" },
                    new { Value = "IL", Text = "Illinois" },
                    new { Value = "IN", Text = "Indiana" },
                    new { Value = "IA", Text = "Iowa" },
                    new { Value = "KS", Text = "Kansas" },
                    new { Value = "KY", Text = "Kentucky" },
                    new { Value = "LA", Text = "Louisiana" },
                    new { Value = "ME", Text = "Maine" },
                    new { Value = "MD", Text = "Maryland" },
                    new { Value = "MA", Text = "Massachusetts" },
                    new { Value = "MI", Text = "Michigan" },
                    new { Value = "MN", Text = "Minnesota" },
                    new { Value = "MS", Text = "Mississippi" },
                    new { Value = "MO", Text = "Missouri" },
                    new { Value = "MT", Text = "Montana" },
                    new { Value = "NE", Text = "Nebraska" },
                    new { Value = "NV", Text = "Nevada" },
                    new { Value = "NH", Text = "New Hampshire" },
                    new { Value = "NJ", Text = "New Jersey" },
                    new { Value = "NM", Text = "New Mexico" },
                    new { Value = "NY", Text = "New York" },
                    new { Value = "NC", Text = "North Carolina" },
                    new { Value = "ND", Text = "North Dakota" },
                    new { Value = "OH", Text = "Ohio" },
                    new { Value = "OK", Text = "Oklahoma" },
                    new { Value = "OR", Text = "Oregon" },
                    new { Value = "PA", Text = "Pennsylvania" },
                    new { Value = "RI", Text = "Rhode Island" },
                    new { Value = "SC", Text = "South Carolina" },
                    new { Value = "SD", Text = "South Dakota" },
                    new { Value = "TN", Text = "Tennessee" },
                    new { Value = "TX", Text = "Texas" },
                    new { Value = "UT", Text = "Utah" },
                    new { Value = "VT", Text = "Vermont" },
                    new { Value = "VA", Text = "Virginia" },
                    new { Value = "WA", Text = "Washington" },
                    new { Value = "WV", Text = "West Virginia" },
                    new { Value = "WI", Text = "Wisconsin" },
                    new { Value = "WY", Text = "Wyoming" }
                });
            });
        }

        [HttpGet("labels/{culture}")]
        public async Task<IActionResult> GetFormLabels(string culture)
        {
            return await Task.Factory.StartNew(() =>
            {
                return Ok(new Dictionary<string, string>
                {
                    { "header", "ES Borromeo Parish Directory Signup 2018" },
                    { "description", "This is the sign up form for the Parish Directory Update" },
                    { "family_name", "Household Name" },
                    { "family_name_ph", "i.e. Smith" },
                    { "home_phone", "Home Phone" },
                    { "home_phone_ph", "i.e. 636-946-1893" },
                    { "home_phone_pub_label", "Publish phone number and address in directory" },
                    { "street_address", "Street Address" },
                    { "street_address_ph", "i.e. 601 N 4th St." },
                    { "city", "City" },
                    { "city_ph", "i.e. St Charles" },
                    { "zip", "Zip" },
                    { "zip_ph", "i.e. 63301" },
                    { "state", "State" }
                });
            });
        }
    }
}