using System;
using FluentValidation;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Rural;
using OpenRealEstate.Validation.Extensions;

namespace OpenRealEstate.Validation.Rural
{
    public class RuralListingValidator : ListingValidator<RuralListing>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default) when 'Available or Unknown':
        /// - *Common Listing data
        /// - AuctionOn
        /// - Pricing
        /// - BuildingDetails
        /// </para>
        /// <para>
        /// Normal when 'Available or Unknown':
        /// - CategoryType
        /// </para>
        /// </summary>
        public RuralListingValidator()
        {
            RuleFor(listing => (DateTime)listing.AuctionOn)
                .SetValidator(new ListingDateTimeValidator())
                .When(listing => listing.AuctionOn.HasValue)
                .WhenStatusTypeIsAvailableOrUnknown();

            // Can have NULL Pricing. But if it's not NULL, then check it.
            RuleFor(listing => listing.Pricing).SetValidator(new SalePricingValidator())
                .WhenStatusTypeIsAvailableOrUnknown();

            // Can have NULL building details. But if it's not NULL, then check it.
            RuleFor(listing => listing.BuildingDetails).SetValidator(new BuildingDetailsValidator())
                .WhenStatusTypeIsAvailableOrUnknown();

            RuleSet(NormalRuleSetKey, () => RuleFor(listing => listing.CategoryType)
                .NotEqual(CategoryType.Unknown)
                .WhenStatusTypeIsAvailableOrUnknown());
        }
    }
}
