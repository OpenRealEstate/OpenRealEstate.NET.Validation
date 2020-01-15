using System;
using FluentValidation;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Land;
using OpenRealEstate.Validation.Extensions;

namespace OpenRealEstate.Validation.Land
{
    public class LandListingValidator : ListingValidator<LandListing>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default) when 'Available or Unknown':
        /// - *Common Listing data
        /// - AuctionOn
        /// - Pricing</para>
        /// </summary>
        public LandListingValidator()
        {
            // Can have a NULL AuctionOn date. Just can't have a MinValue one.
            RuleFor(listing => (DateTime)listing.AuctionOn).SetValidator(new ListingDateTimeValidator())
                .When(listing => listing.AuctionOn.HasValue)
                .WhenStatusTypeIsAvailableOrUnknown();

            // Can have NULL Pricing. But if it's not NULL, then check it.
            RuleFor(listing => listing.Pricing).SetValidator(new SalePricingValidator())
                .WhenStatusTypeIsAvailableOrUnknown();

            // NOTE: No rules needed for listing.LandEstate.
        }
    }
}
