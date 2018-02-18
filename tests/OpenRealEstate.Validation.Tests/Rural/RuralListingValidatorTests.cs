using FluentValidation;
using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Rural;
using OpenRealEstate.Validation.Rural;
using Shouldly;
using System;
using Xunit;
using RuralCategoryType = OpenRealEstate.Core.Rural.CategoryType;

namespace OpenRealEstate.Validation.Tests.Rural
{
    public class RuralListingValidatorTests : TestBase
    {
        public class RuleSetFacts: TestBase
        {
            [Fact]
            public void GivenAListingAndADefaultRuleSet_Validate_ShouldHaveNotHaveAnyValidationErrors()
            {
                // Arrange.
                var listing = GetListing<RuralListing>("Rural\\REA-Rural-Withdrawn.xml");
                var validator = new RuralListingValidator();

                // Act.
                var result = validator.Validate(listing);

                // Assert.
                result.Errors.Count.ShouldBe(0);
            }

            [Fact]
            public void GivenAMinimumRuleSet_Validate_ShouldNotHaveAnyValidationErrors()
            {
                // Arrange.
                var listing = GetListing<RuralListing>();
                var validator = new RuralListingValidator();

                // Act.
                var result = validator.Validate(listing,
                                                ruleSet: RuralListingValidator.NormalRuleSet);

                // Assert.
                result.Errors.Count.ShouldBe(0);
            }

            [Fact]
            public void GivenAnAuctionDataAndACommonRuleSet_Validate_ShouldNotHaveAnyValidationErrors()
            {
                // Arrange.
                var listing = GetListing<RuralListing>();
                var validator = new RuralListingValidator();
                listing.AuctionOn = DateTime.UtcNow;

                // Act.
                var result = validator.Validate(listing,
                                                ruleSet: RuralListingValidator.NormalRuleSet);

                // Assert.
                result.Errors.Count.ShouldBe(0);
            }

            [Fact]
            public void GivenAnInvalidAuctionDataAndACommonRuleSet_Validate_ShouldNotHaveAnyValidationErrors()
            {
                // Arrange.
                var listing = GetListing<RuralListing>();
                var validator = new RuralListingValidator();
                listing.AuctionOn = DateTime.UtcNow;

                // Act.
                var result = validator.Validate(listing,
                                                ruleSet: RuralListingValidator.NormalRuleSet);

                // Assert.
                result.Errors.Count.ShouldBe(0);
            }
        }

        public class SimpleValidationFacts
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
                _validator.ShouldHaveValidationErrorFor(listing => listing.CategoryType,
                                                        RuralCategoryType.Unknown,
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
        }
    }
}