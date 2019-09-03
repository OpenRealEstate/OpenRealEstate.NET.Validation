using System;
using FluentValidation;
using OpenRealEstate.Core.Rural;

namespace OpenRealEstate.Validation.Rural
{
    public class RuralListingValidator : ListingValidator<RuralListing>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - *Common Listing data
        /// - AuctionOn
        /// - Pricing
        /// - BuildingDetails
        /// </para>
        /// <para>
        /// Normal:
        /// - CategoryType
        /// </para>
        /// </summary>
        public RuralListingValidator()
        {
            RuleFor(listing => (DateTime)listing.AuctionOn)
                .SetValidator(new ListingDateTimeValidator())
                .When(listing => listing.AuctionOn.HasValue);

            // Can have NULL Pricing. But if it's not NULL, then check it.
            RuleFor(listing => listing.Pricing).SetValidator(new SalePricingValidator());

            // Can have NULL building details. But if it's not NULL, then check it.
            RuleFor(listing => listing.BuildingDetails).SetValidator(new BuildingDetailsValidator());

            RuleSet(NormalRuleSetKey, () => RuleFor(listing => listing.CategoryType).NotEqual(CategoryType.Unknown));
        }
    }
}
