using System;
using FluentValidation;
using FluentValidation.TestHelper;
using OpenRealEstate.Core.Rental;
using OpenRealEstate.Validation.Rental;
using Shouldly;
using Xunit;

namespace OpenRealEstate.Validation.Tests.Rental
{
    public class RentalListingValidatorTests
    {
        public class SimpleValidationFacts
        {
            public SimpleValidationFacts()
            {
                _validator = new RentalListingValidator();
            }

            private readonly RentalListingValidator _validator;

            [Fact]
            public void GivenAnAvailableOn_Validate_ShouldNotHaveAValidationError()
            {
                _validator.ShouldNotHaveValidationErrorFor(listing => listing.AvailableOn, DateTime.UtcNow);
            }

            [Fact]
            public void GivenAnInvalidAvailableOn_Validate_ShouldHaveAValidationError()
            {
                _validator.ShouldHaveValidationErrorFor(listing => listing.AvailableOn, DateTime.MinValue);
            }

            [Fact]
            public void GivenNoAvailableOn_Validate_ShouldNotHaveAValidationError()
            {
                _validator.ShouldNotHaveValidationErrorFor(listing => listing.AvailableOn, (DateTime?) null);
            }
        }

        public class ValidationFacts : TestBase
        {
            [Fact]
            public void GivenAnIncompleteRentalListing_Validate_ShouldHaveAnyValidationErrors()
            {
                // Arrange.
                var validator = new RentalListingValidator();

                // Act.
                var result = validator.Validate(new RentalListing(),
                                                ruleSet: RuleSetKeys.NormalRuleSet);

                // Assert.
                result.Errors.Count.ShouldBe(9);
                result.Errors.ShouldContain(x => x.PropertyName == "AgencyId");
                result.Errors.ShouldContain(x => x.PropertyName == "StatusType");
                result.Errors.ShouldContain(x => x.PropertyName == "CreatedOn");
                result.Errors.ShouldContain(x => x.PropertyName == "Id");
                result.Errors.ShouldContain(x => x.PropertyName == "UpdatedOn");
                result.Errors.ShouldContain(x => x.PropertyName == "Title");
                result.Errors.ShouldContain(x => x.PropertyName == "Address");
                result.Errors.ShouldContain(x => x.PropertyName == "PropertyType");
                result.Errors.ShouldContain(x => x.PropertyName == "Pricing");
            }

            [Fact]
            public void GivenARentalListing_Validate_ShouldNotHaveAnyValidationErrors()
            {
                // Arrange.
                var validator = new RentalListingValidator();
                var listing = FakeData.FakeListings.CreateAFakeRentalListing();

                // Act.
                var result = validator.Validate(listing, ruleSet: RuleSetKeys.NormalRuleSet);

                // Assert.
                result.Errors.Count.ShouldBe(0);
            }
        }
    }
}
