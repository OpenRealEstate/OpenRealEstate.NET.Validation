using System;
using Shouldly;
using Xunit;

namespace OpenRealEstate.Validation.Tests
{
    public class ListingDateTimeValidatorTests
    {
        private readonly ListingDateTimeValidator _validator;

        public ListingDateTimeValidatorTests()
        {
            _validator = new ListingDateTimeValidator();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(1799)]
        public void GivenAValidDateTime_Validate_ShouldNotHaveAValidationError(int year)
        {
            // Arrange.
            var dateTime = new DateTime(year, 12, 31, 23, 59, 59);

            // Act.
            var result = _validator.Validate(dateTime);

            // Assert.
            result.IsValid.ShouldBe(false);
        }

        [Theory]
        [InlineData(1800)]
        [InlineData(2030)]
        public void GivenAnInvalidDateTime_Validate_ShouldHaveAValidationError(int year)
        {
            // Arrange.
            var dateTime = new DateTime(year, 1, 1);

            // Act.
            var result = _validator.Validate(dateTime);

            // Assert.
            result.IsValid.ShouldBe(true);
        }
    }
}
