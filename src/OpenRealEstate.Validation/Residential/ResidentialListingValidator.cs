using System;
using FluentValidation;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Residential;
using OpenRealEstate.Validation.Extensions;

namespace OpenRealEstate.Validation.Residential
{
    public class ResidentialListingValidator : ListingValidator<ResidentialListing>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default) when 'Available or Unknown':
        /// - *Common Listing data
        /// - AuctionOn
        /// - BuildingDetails
        /// </para>
        /// <para>
        /// Normal when 'Available or Unknown':
        /// - PropertyType
        /// - Pricing
        /// </para>
        /// </summary>
        public ResidentialListingValidator()
        {
            RuleFor(listing => (DateTime)listing.AuctionOn).SetValidator(new ListingDateTimeValidator())
                .When(listing => listing.AuctionOn.HasValue)
                .WhenStatusTypeIsAvailableOrUnknown();

            // Can have NULL building details. But if it's not NULL, then check it.
            RuleFor(listing => listing.BuildingDetails).SetValidator(new BuildingDetailsValidator())
                 .When(listing => listing.StatusType == StatusType.Available);

            RuleSet(NormalRuleSetKey, () =>
            {
                // Required.
                RuleFor(listing => listing.PropertyType).NotEqual(PropertyType.Unknown)
                    .WhenStatusTypeIsAvailableOrUnknown()
                    .WithMessage("Invalid 'PropertyType'. Please choose any property except Unknown.");

                RuleFor(listing => listing.Pricing).SetValidator(new SalePricingValidator())
                    .WhenStatusTypeIsAvailableOrUnknown();
            });
        }
    }
}
