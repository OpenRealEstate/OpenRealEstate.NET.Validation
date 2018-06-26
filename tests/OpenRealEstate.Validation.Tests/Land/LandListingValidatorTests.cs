using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Land;
using OpenRealEstate.Validation.Land;
using System;
using Xunit;

namespace OpenRealEstate.Validation.Tests.Land
{
    public class LandListingValidatorTests
    {
        public LandListingValidatorTests()
        {
            _validator = new LandListingValidator();
        }

        private readonly LandListingValidator _validator;

        [Fact]
        public void GivenACategoryType_Validate_ShouldNotHaveAValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(listing => listing.CategoryType, CategoryType.Residential);
        }

        [Fact]
        public void GivenAnAuctionOn_Validate_ShouldNotHaveAValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(listing => listing.AuctionOn, DateTime.UtcNow);
        }

        [Fact]
        public void GivenAnInvalidAuctionOn_Validate_ShouldHaveAValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(listing => listing.AuctionOn, DateTime.MinValue);
        }

        [Fact]
        public void GivenANullAuctionOn_Validate_ShouldNotHaveAValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(listing => listing.AuctionOn, (DateTime?) null);
        }

        [Fact]
        public void GivenAnUnknownCategoryType_Validate_ShouldHaveAValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(listing => listing.CategoryType, CategoryType.Unknown);
        }

        [Fact]
        public void GivenASalePricing_Validate_ShouldNotHaveAValidationError()
        {
            // Arrange.
            var salePricing = new SalePricing
            {
                SalePrice = 1234,
                SalePriceText = "Contact agent"
            };

            // Act & Assert.
            _validator.ShouldHaveChildValidator(listing => listing.Pricing, typeof(SalePricingValidator));
            _validator.ShouldNotHaveValidationErrorFor(listing => listing.Pricing, salePricing);
        }

        [Fact]
        public void GivenNoSalePricing_Validate_ShouldNotHaveAValidationError()
        {
            // Arrange.
            SalePricing salePricing = null;

            // Act & Assert.
            _validator.ShouldHaveChildValidator(listing => listing.Pricing, typeof(SalePricingValidator));
            _validator.ShouldNotHaveValidationErrorFor(listing => listing.Pricing, salePricing);
        }
    }
}