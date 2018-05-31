using System;
using FluentValidation;
using OpenRealEstate.Core.Land;

namespace OpenRealEstate.Validation.Land
{
    public class LandListingValidator : ListingValidator<LandListing>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - *Common Listing data
        /// - AuctionOn
        /// - Pricing</para>
        /// </summary>
        public LandListingValidator()
        {
            // Can have a NULL AuctionOn date. Just can't have a MinValue one.
            RuleFor(listing => listing.AuctionOn).NotEqual(DateTime.MinValue);

            // Can have NULL Pricing. But if it's not NULL, then check it.
            RuleFor(listing => listing.Pricing).SetValidator(new SalePricingValidator());

            // NOTE: No rules needed for listing.LandEstate.
        }
    }
}