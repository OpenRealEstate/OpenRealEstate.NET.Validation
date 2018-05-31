using FluentValidation;
using OpenRealEstate.Core.Rental;

namespace OpenRealEstate.Validation.Rental
{
    public class RentalPricingValidator : AbstractValidator<RentalPricing>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - RentalPricing
        /// - Bond
        /// </para>
        /// </summary>
        public RentalPricingValidator()
        {
            RuleFor(rentalPricing => rentalPricing.RentalPrice).GreaterThanOrEqualTo(0);
            RuleFor(rentalPricing => rentalPricing.Bond).GreaterThanOrEqualTo(0);

            // NOTE: We might not have a Payment frequency type, so Unknown is allowed.
        }
    }
}