using System;
using FluentValidation;
using OpenRealEstate.NET.Core;
using OpenRealEstate.NET.Core.Rental;

namespace OpenRealEstate.NET.Validation.Rental
{
    public class RentalListingValidator : ListingValidator<RentalListing>
    {
        public RentalListingValidator()
        {
            // Can have a NULL AvailableOn date. Just can't have a MinValue one.
            RuleFor(listing => listing.AvailableOn).NotEqual(DateTime.MinValue);

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