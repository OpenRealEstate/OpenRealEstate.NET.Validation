using FluentValidation;
using OpenRealEstate.NET.Core.Rental;

namespace OpenRealEstate.NET.Validation.Rental
{
    public class RentalPricingValidator : AbstractValidator<RentalPricing>
    {
        public RentalPricingValidator()
        {
            RuleFor(rentalPricing => rentalPricing.RentalPrice).GreaterThanOrEqualTo(0);
            RuleFor(rentalPricing => rentalPricing.Bond).GreaterThanOrEqualTo(0);

            // NOTE: We might not have a Payment frequency type, so Unknown is allowed.
        }
    }
}