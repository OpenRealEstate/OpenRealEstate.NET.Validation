using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class SideValidator : AbstractValidator<Side>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - Name
        /// - Length
        /// </para>
        /// </summary>
        public SideValidator()
        {
            RuleFor(side => side.Name)
                .NotEmpty();
        }
    }
}
