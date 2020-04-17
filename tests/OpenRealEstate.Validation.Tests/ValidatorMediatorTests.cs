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
            var listing = FakeData.FakeListings.CreateAFakeResidentialListing();

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenACurrentResidentialListingWithSomeRequiredMissingData_Validate_ValidatesTheListingWithSomeErrors()
        {
            // Arrange.
            var listing = FakeData.FakeListings.CreateAFakeResidentialListing();
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
            var listing = FakeData.FakeListings.CreateAFakeLandListing();

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenALandListingWithSomeRequiredMissingData_Validate_ValidatesTheListingWithSomeErrors()
        {
            // Arrange.
            var listing = FakeData.FakeListings.CreateAFakeLandListing();
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
            var listing = FakeData.FakeListings.CreateAFakeRentalListing();

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenARentalListingWithSomeRequiredMissingData_Validate_ValidatesTheListingWithSomeErrors()
        {
            // Arrange.
            var listing = FakeData.FakeListings.CreateAFakeRentalListing();
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
            var listing = FakeData.FakeListings.CreateAFakeRuralListing();
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
            var listing = FakeData.FakeListings.CreateAFakeRuralListing();

            // Arrange.
            var result = ValidatorMediator.Validate(listing);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenARemovedResidentialListing_Validate_ValidatesTheListingWithNoErrors()
        {
            // Arrange.
            var listing = FakeResidentialListing(StatusType.Removed);

            // Arrange.
            var result = ValidatorMediator.Validate(listing, ListingValidatorRuleSet.Minimum);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }
    }
}
