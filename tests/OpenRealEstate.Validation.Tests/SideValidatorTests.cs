using FluentValidation.TestHelper;
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
    }
}
