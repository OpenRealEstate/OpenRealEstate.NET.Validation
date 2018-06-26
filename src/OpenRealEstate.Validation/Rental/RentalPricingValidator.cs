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
            // We always need to have a rental price.
            RuleFor(rentalPricing => rentalPricing.RentalPrice).GreaterThanOrEqualTo(1);

            // Bond is optional. If there's a price, it needs to be a positive value.
            RuleFor(rentalPricing => rentalPricing.Bond).GreaterThanOrEqualTo(0);

            // NOTE: We might not have a Payment frequency type, so Unknown is allowed.
        }
    }
}