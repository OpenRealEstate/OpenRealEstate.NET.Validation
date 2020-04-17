using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using Xunit;

namespace OpenRealEstate.Validation.Tests
{
    public class DepthValidatorTests
    {
        private readonly DepthValidator _depthValidator;

        public DepthValidatorTests()
        {
            _depthValidator = new DepthValidator();
        }

        [Fact]
        public void GivenANegativeValue_Validate_ShouldHaveValidationErrors()
        {
            _depthValidator.ShouldHaveValidationErrorFor(depth => depth.Value, -1m);
        }

        [Fact]
        public void GivenAValue_Validate_ShouldNotHaveValidationErrors()
        {
            _depthValidator.ShouldNotHaveValidationErrorFor(depth => depth.Value, 0m);
        }

        [Fact]
        public void GivenAValueAndAType_Validate_ShouldNotHaveValidationErrors()
        {
            // Arrange.
            var existingDepth = new Depth
            {
                Side = "a",
                Value = 1
            };

            // Act & Assert.
            _depthValidator.ShouldNotHaveValidationErrorFor(depth => depth.Value, existingDepth);
        }

        [Fact]
        public void GivenAValueButNoType_Validate_ShouldHaveValidationErrors()
        {
            // Arrange.
            var existingDepth = new Depth
            {
                Value = 1
            };

            // Act & Assert.
            _depthValidator.ShouldHaveValidationErrorFor(depth => depth.Side, existingDepth);
        }
    }
}
