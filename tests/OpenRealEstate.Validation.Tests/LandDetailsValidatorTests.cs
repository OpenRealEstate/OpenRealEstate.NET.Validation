using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using Shouldly;
using Xunit;

namespace OpenRealEstate.Validation.Tests
{
    public class LandDetailsValidatorTests
    {
        private readonly LandDetailsValidator _landDetailsValidator;

        public LandDetailsValidatorTests()
        {
            _landDetailsValidator = new LandDetailsValidator();
        }

        [Fact]
        public void GivenAnArea_Validate_ShouldNotHaveAValidationError()
        {
            // Arrange.
            var landDetails = new LandDetails
            {
                Area = new UnitOfMeasure
                {
                    Type = "a",
                    Value = 1m
                }
            };

            // Act.
            _landDetailsValidator.ShouldHaveChildValidator(land => land.Area, typeof(UnitOfMeasureValidator));
            var result = _landDetailsValidator.Validate(landDetails);
            //validator.ShouldHaveValidationErrorFor(land => land.Area, area);

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }

        [Fact]
        public void GivenAnAreaWithNoType_Validate_ShouldHaveAValidationError()
        {
            // Arrange.
            var landDetails = new LandDetails
            {
                Area = new UnitOfMeasure
                {
                    Type = null,
                    Value = 1m
                }
            };

            // Act.
            _landDetailsValidator.ShouldHaveChildValidator(land => land.Area, typeof(UnitOfMeasureValidator));
            var result = _landDetailsValidator.Validate(landDetails);
            //validator.ShouldHaveValidationErrorFor(land => land.Area, area);

            // Assert.
            result.Errors.ShouldContain(x => x.PropertyName == "Area.Type");
        }
    }
}
