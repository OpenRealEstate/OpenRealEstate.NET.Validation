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
        /// - Sides
        /// </para>
        /// </summary>
        public LandDetailsValidator()
        {
            RuleFor(land => land.Area)
                .SetValidator(new UnitOfMeasureValidator());

            RuleForEach(land => land.Sides)
                .SetValidator(new SideValidator());
        }
    }
}
