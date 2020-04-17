using System;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Land;
using OpenRealEstate.Core.Rental;
using OpenRealEstate.Core.Residential;
using OpenRealEstate.Core.Rural;

namespace OpenRealEstate.Validation.Tests
{
    public abstract class TestBase
    {
        protected static SalePricing FakeSalePricing => new SalePricing
        {
            SoldOn = DateTime.UtcNow.AddDays(-10),
            SoldPrice = 580000,
            SoldPriceText = "$580,000"
        };

        protected static ResidentialListing FakeResidentialListing(StatusType statusType, SalePricing salePricing = null)
        {
            var listing = new ResidentialListing();
            SetupListing(listing, statusType);
            listing.Pricing = salePricing;

            return listing;
        }

        protected static RentalListing FakeRentallListing(StatusType statusType)
        {
            var listing = new RentalListing();
            SetupListing(listing, statusType);

            return listing;
        }

        protected static RuralListing FakeRuralListing(StatusType statusType)
        {
            var listing = new RuralListing();
            SetupListing(listing, statusType);

            return listing;
        }

        protected static LandListing FakeLandListing(StatusType statusType)
        {
            var listing = new LandListing();
            SetupListing(listing, statusType);

            return listing;
        }

        private static void SetupListing(Listing listing, StatusType statusType)
        {
            if (listing is null)
            {
                throw new ArgumentNullException(nameof(listing));
            }

            if (statusType == StatusType.Unknown)
            {
                throw new ArgumentException($"'{nameof(statusType)}' should be any value -except- 'Unknown'.");
            }

            listing.Id = "FancyPants123";
            listing.AgencyId = "ABC123";
            listing.CreatedOn = DateTime.UtcNow.AddDays(-10);
            listing.UpdatedOn = DateTime.UtcNow.AddDays(-10);
            listing.StatusType = statusType;
        }
    }
}
