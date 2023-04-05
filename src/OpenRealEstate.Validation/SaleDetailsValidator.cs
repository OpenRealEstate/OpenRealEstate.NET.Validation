using System;
using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class SaleDetailsValidator : AbstractValidator<ISaleDetails>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - Area
        /// - EnergyRating
        /// </para>
        /// </summary>
        public SaleDetailsValidator()
        {
            RuleFor(saleDetails => (DateTime)saleDetails.AuctionOn).SetValidator(new ListingDateTimeValidator())
                .When(saleDetails => saleDetails.AuctionOn.HasValue);

            RuleFor(saleDetails => saleDetails.YearBuilt)
                .LessThanOrEqualTo(DateTime.UtcNow.Year) // Shouldn't be in the future.
                .GreaterThan(1990) // We shouldn't have any buildings older than 100 years old, right?
                .When(saleDetails => saleDetails.YearBuilt.HasValue); 

            RuleFor(saleDetails => saleDetails.YearLastRenovated)
                .LessThanOrEqualTo(DateTime.UtcNow.Year) // Shouldn't be in the future.
                .GreaterThan(1990) // We shouldn't have any buildings older than 100 years old, right?
                .When(saleDetails => saleDetails.YearLastRenovated.HasValue);

            RuleSet(RuleSetKeys.NormalRuleSetKey, () =>
            {
                // Required.
                RuleFor(saleDetails => saleDetails.Pricing).SetValidator(new SalePricingValidator());
            });
        }
}
}
