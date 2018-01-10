using System;
using FluentValidation;
using OpenRealEstate.NET.Core;
using OpenRealEstate.NET.Core.Residential;

namespace OpenRealEstate.NET.Validation.Residential
{
    public class ResidentialListingValidator : ListingValidator<ResidentialListing>
    {
        public ResidentialListingValidator()
        {
            // Can have a NULL Auction date. Just can't have a MinValue one.
            RuleFor(listing => listing.AuctionOn).NotEqual(DateTime.MinValue);

            // Can have NULL building details. But if it's not NULL, then check it.
            RuleFor(listing => listing.BuildingDetails).SetValidator(new BuildingDetailsValidator());

            RuleSet(NormalRuleSetKey, () =>
            {
                // Required.
                RuleFor(listing => listing.PropertyType).NotEqual(PropertyType.Unknown)
                    .WithMessage("Invalid 'PropertyType'. Please choose any property except Unknown.");
                
                RuleFor(listing => listing.Pricing).SetValidator(new SalePricingValidator());    
            });
        }
    }
}