using OpenRealEstate.Core;
using OpenRealEstate.Core.Land;
using OpenRealEstate.Core.Rental;
using OpenRealEstate.Core.Residential;
using OpenRealEstate.Core.Rural;
using Shouldly;
using Xunit;
using LandCategoryType = OpenRealEstate.Core.Land.CategoryType;

namespace OpenRealEstate.Validation.Tests
{
    public class ValidatorMediatorTests : TestBase
    {
        [Fact]
        public void GivenACurrentResidentialListing_Validate_ValidatesTheListingWithNoErrors()
        {
            // Arrange.
            var listing = GetListing<ResidentialListing>();

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenACurrentResidentialListingWithSomeRequiredMissingData_Validate_ValidatesTheListingWithSomeErrors()
        {
            // Arrange.
            var listing = GetListing<ResidentialListing>();
            listing.Id = null;
            listing.AgencyId = null;
            listing.Pricing.SalePrice = -1;

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(3);
        }

        [Fact]
        public void GivenALandListing_Validate_ValidatesTheListingWithNoErrors()
        {
            // Arrange.
            var listing = GetListing<LandListing>();

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenALandListingWithSomeRequiredMissingData_Validate_ValidatesTheListingWithSomeErrors()
        {
            // Arrange.
            var listing = GetListing<LandListing>();
            listing.Id = null;
            listing.AgencyId = null;
            listing.CategoryType = LandCategoryType.Unknown; // That's allowed, now :(

            // Arrange.
            var result = ValidatorMediator.Validate(listing, ListingValidatorRuleSet.Minimum);

            // Assert.
            result.Errors.Count.ShouldBe(2);
        }

        [Fact]
        public void GivenARentalListing_Validate_ValidatesTheListingWithNoErrors()
        {
            // Arrange.
            var listing = GetListing<RentalListing>();

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenARentalListingWithSomeRequiredMissingData_Validate_ValidatesTheListingWithSomeErrors()
        {
            // Arrange.
            var listing = GetListing<RentalListing>();
            listing.Id = null;
            listing.AgencyId = null;
            listing.PropertyType = PropertyType.Unknown;

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(3);
        }

        [Fact]
        public void GivenARuralListingWithSomeRequiredMissingData_Validate_ValidatesTheListingWithSomeErrors()
        {
            // Arrange.
            var listing = GetListing<RuralListing>();
            listing.Id = null;
            listing.AgencyId = null;
            listing.Pricing.SalePrice = -1;

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(3);
        }

        [Fact]
        public void GivenARurallListing_Validate_ValidatesTheListingWithNoErrors()
        {
            // Arrange.
            var listing = GetListing<RuralListing>();

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenAWithdrawnResidentialListing_Validate_ValidatesTheListingWithNoErrors()
        {
            // Arrange.
            var listing = GetListing<ResidentialListing>("Residential\\REA-Residential-Withdrawn.xml");

            // Arrange.
            var result = ValidatorMediator.Validate(listing, ListingValidatorRuleSet.Minimum);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }
    }
}
