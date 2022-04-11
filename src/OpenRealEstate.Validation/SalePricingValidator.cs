using System;
using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class SalePricingValidator : AbstractValidator<SalePricing>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - SalePrice
        /// - SoldPrice (Optional)
        /// - SoldOn (Optional) 
        /// </para>
        /// </summary>
        public SalePricingValidator()
        {
            // Required.
            // (Not all agents always provide a price. Which sucks, but has to be legit)
            RuleFor(salePricing => salePricing.SalePrice)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            // Optional.
            RuleFor(salePricing => salePricing.SoldPrice)
                .GreaterThanOrEqualTo(0)
                .When(salePricing => salePricing.SoldPrice.HasValue);

            RuleFor(salePricing => (DateTime)salePricing.SoldOn)
                .SetValidator(new ListingDateTimeValidator())
                .When(salePricing => salePricing.SoldOn.HasValue);
        }
    }
}
