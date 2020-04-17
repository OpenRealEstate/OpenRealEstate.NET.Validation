using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using Xunit;

namespace OpenRealEstate.Validation.Tests
{
    public class SideValidatorTests
    {
        private readonly SideValidator _sideValidator;

        public SideValidatorTests()
        {
            _sideValidator = new SideValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenAMissingName_Validate_ShouldHaveValidationErrors(string name)
        {
            _sideValidator.ShouldHaveValidationErrorFor(side => side.Name, name);
        }

        [Fact]
        public void GivenAName_Validate_ShouldNotHaveValidationErrors()
        {
            _sideValidator.ShouldNotHaveValidationErrorFor(side => side.Name, "a");
        }

        [Fact]
        public void GivenALength_Validate_ShouldNotHaveValidationErrors()
        {
            // Arrange.
            var existingLength = new UnitOfMeasure
            {
                Type = "a",
                Value = 1
            };

            // Act & Assert.
            _sideValidator.ShouldHaveChildValidator(side => side.Length, typeof(UnitOfMeasureValidator));
            _sideValidator.ShouldNotHaveValidationErrorFor(side => side.Length, existingLength);
        }

        [Fact]
        public void GivenNoLength_Validate_ShouldHaveValidationErrors()
        {
            // Arrange, Act & Assert.
            _sideValidator.ShouldHaveValidationErrorFor(side => side.Length, (UnitOfMeasure)null);
        }
    }
}
