﻿using FluentValidation.TestHelper;
using OpenRealEstate.NET.Validation.Residential;
using Xunit;

namespace OpenRealEstate.NET.Validation.Tests
{
    public class ResidentialListingValidatorTests
    {
        public ResidentialListingValidatorTests()
        {
            _listingValidator = new ResidentialListingValidator();
        }

        private readonly ResidentialListingValidator _listingValidator;

        [Fact]
        public void GivenAnAgentId_Validate_ShouldNotHaveValidationErrors()
        {
            _listingValidator.ShouldNotHaveValidationErrorFor(listing => listing.AgencyId, "a");
        }
    }
}