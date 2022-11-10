using FluentValidation.TestHelper;
using Xunit;

namespace OpenRealEstate.Validation.Tests
{
    public class MediaValidatorTests
    {
        public MediaValidatorTests()
        {
            _mediaValidator = new MediaValidator();
        }

        private readonly MediaValidator _mediaValidator;

        [Fact]
        public void GivenANegativeOrder_Validate_ShouldHaveAValidationError()
        {
            _mediaValidator.ShouldHaveValidationErrorFor(media => media.Order, -1);
        }

        [Fact]
        public void GivenAnOrder_Validate_ShouldNotHaveAValidationError()
        {
            _mediaValidator.ShouldNotHaveValidationErrorFor(media => media.Order, 1);
        }

        [Fact]
        public void GivenAUrl_Validate_ShouldNotHaveAValidationError()
        {
            _mediaValidator.ShouldNotHaveValidationErrorFor(media => media.Url, "a");
        }

        [Fact]
        public void GivenNoOrder_Validate_ShouldHaveAValidationError()
        {
            _mediaValidator.ShouldHaveValidationErrorFor(media => media.Order, 0);
        }

        [Fact]
        public void GivenNoUrl_Validate_ShouldHaveAValidationError()
        {
            _mediaValidator.ShouldHaveValidationErrorFor(media => media.Url, "");
        }

        [Fact]
        public void GivenAnId_Validate_ShouldNotHaveAValidationError()
        {
            const string validId = "a";
            _mediaValidator.ShouldNotHaveValidationErrorFor(media => media.Id, validId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenNullId_Validate_ShouldHaveAValidationError(string id)
        {
            _mediaValidator.ShouldHaveValidationErrorFor(media => media.Id, id);
        }
    }
}
