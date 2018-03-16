

using ParishForms.Common.Models.Directory;
using ParishForms.ViewModels;

namespace ParishForms
{
    public static class Mapper
    {
        internal static SubmisionDto ToDto(this DirectoryFormViewModel model)
        {
            if (model == null)
                return null;

            return new SubmisionDto
            {

            };
        }
    }
}
