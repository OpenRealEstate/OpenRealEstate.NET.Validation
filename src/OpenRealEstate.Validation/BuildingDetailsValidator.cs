using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class BuildingDetailsValidator : AbstractValidator<BuildingDetails>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - Area
        /// - EnergyRating
        /// </para>
        /// </summary>
        public BuildingDetailsValidator()
        {
            RuleFor(building => building.Area)
                .SetValidator(new UnitOfMeasureValidator());

            RuleFor(building => building.EnergyRating)
                .GreaterThan(0)
                .LessThanOrEqualTo(10);
        }
    }
}
