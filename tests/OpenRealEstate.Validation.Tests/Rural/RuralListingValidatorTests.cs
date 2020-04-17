using System;
using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Rural;
using OpenRealEstate.Validation.Rural;
using Xunit;
using RuralCategoryType = OpenRealEstate.Core.Rural.CategoryType;

namespace OpenRealEstate.Validation.Tests.Rural
{
    public class RuralListingValidatorTests
    {
        public class SimpleValidationFacts : TestBase
        {
            public SimpleValidationFacts()
            {
                _validator = new RuralListingValidator();
            }

            private readonly RuralListingValidator _validator;

            [Fact]
            public void GivenACategoryType_Validate_ShouldNotHaveAValidationError()
            {
                _validator.ShouldNotHaveValidationErrorFor(listing => listing.CategoryType,
                                                           RuralCategoryType.Cropping,
                                                           RuralListingValidator.NormalRuleSet);
            }

            [Fact]
            public void GivenAnAuctionOn_Validate_ShouldNotHaveAValidationError()
            {
                _validator.ShouldNotHaveValidationErrorFor(listing => listing.AuctionOn, DateTime.UtcNow);
            }

            [Fact]
            public void GivenAnInvalidAuctionOn_Validate_ShouldHaveAValidationError()
            {
                // Arrange.
                var listing = FakeData.FakeListings.CreateAFakeRuralListing();
                listing.AuctionOn = DateTime.MinValue;

                // Act & Assert.
                _validator.ShouldHaveValidationErrorFor(x => x.AuctionOn, listing);
            }

            [Fact]
            public void GivenANullAuctionOn_Validate_ShouldNotHaveAValidationError()
            {
                _validator.ShouldNotHaveValidationErrorFor(listing => listing.AuctionOn, (DateTime?) null);
            }

            [Fact]
            public void GivenAnUnknownCategoryType_Validate_ShouldHaveAValidationError()
            {
                // Arrange.
                var listing = FakeData.FakeListings.CreateAFakeRuralListing();
                listing.CategoryType = RuralCategoryType.Unknown;

                // Act & Assert.
                _validator.ShouldHaveValidationErrorFor(x => x.CategoryType,
                                                        listing,
                                                        RuralListingValidator.NormalRuleSet);
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
}
