using System;
using FluentValidation;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Rental;

namespace OpenRealEstate.Validation.Rental
{
    public class RentalListingValidator : ListingValidator<RentalListing>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - *Common Listing data
        /// - AvailableOn
        /// - BuildingDetails
        /// </para>
        /// <para>
        /// Normal:
        /// - PropertyType
        /// - Pricing
        /// </para>
        /// </summary>
        public RentalListingValidator()
        {
            // Can have a NULL AvailableOn date. Just can't have a MinValue one.
            RuleFor(listing => (DateTime)listing.AvailableOn).SetValidator(new ListingDateTimeValidator())
                .When(listing => listing.AvailableOn.HasValue);

            // Can have NULL building details. But if it's not NULL, then check it.
            RuleFor(listing => listing.BuildingDetails).SetValidator(new BuildingDetailsValidator());

            RuleSet(NormalRuleSetKey, () =>
            {
                // Required.
                RuleFor(listing => listing.PropertyType).NotEqual(PropertyType.Unknown);
                RuleFor(listing => listing.Pricing).NotNull().SetValidator(new RentalPricingValidator());
            });
        }
    }
}
