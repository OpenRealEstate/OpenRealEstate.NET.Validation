using FluentValidation;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Rural;
using OpenRealEstate.Validation.Rural;
using Shouldly;
using System;
using Xunit;

namespace OpenRealEstate.Validation.Tests.Rural
{
    public class RuleSetTests : TestBase
    {
        [Fact]
        public void GivenAListingAndADefaultRuleSet_Validate_ShouldHaveNotHaveAnyValidationErrors()
        {
            // Arrange.
            var listing = FakeRuralListing(StatusType.Removed);
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
            var listing = FakeData.FakeListings.CreateAFakeRuralListing();
            var validator = new RuralListingValidator();

            // Act.
            var result = validator.Validate(listing,
                                            ruleSet: RuleSetKeys.NormalRuleSet);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenAnAuctionDataAndACommonRuleSet_Validate_ShouldNotHaveAnyValidationErrors()
        {
            // Arrange.
            var listing = FakeData.FakeListings.CreateAFakeRuralListing();
            var validator = new RuralListingValidator();
            listing.AuctionOn = DateTime.UtcNow;

            // Act.
            var result = validator.Validate(listing,
                                            ruleSet: RuleSetKeys.NormalRuleSet);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenAnInvalidAuctionDataAndACommonRuleSet_Validate_ShouldNotHaveAnyValidationErrors()
        {
            // Arrange.
            var listing = FakeData.FakeListings.CreateAFakeRuralListing();
            var validator = new RuralListingValidator();
            listing.AuctionOn = DateTime.UtcNow;

            // Act.
            var result = validator.Validate(listing,
                                            ruleSet: RuleSetKeys.NormalRuleSet);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }
    }
}
