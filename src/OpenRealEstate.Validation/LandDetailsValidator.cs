using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class LandDetailsValidator : AbstractValidator<LandDetails>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - Area
        /// - Frontage
        /// - Depths
        /// </para>
        /// </summary>
        public LandDetailsValidator()
        {
            RuleFor(land => land.Area)
                .SetValidator(new UnitOfMeasureValidator());

            RuleFor(land => land.Frontage)
                .GreaterThanOrEqualTo(0);

            RuleForEach(land => land.Depths)
                .SetValidator(new DepthValidator());
        }
    }
}
