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
            RuleFor(depth => depth.Name)
                .NotEmpty();

            RuleFor(side => side.Length)
                .NotNull()
                .SetValidator(new UnitOfMeasureValidator());
        }
    }
}
