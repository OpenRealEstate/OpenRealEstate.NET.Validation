using FluentValidation.TestHelper;
using Xunit;

namespace OpenRealEstate.NET.Validation.Tests
{
    public class FeaturesValidatorTests
    {
        private readonly FeaturesValidator _featuresValidator;

        public FeaturesValidatorTests()
        {
            _featuresValidator = new FeaturesValidator();
        }

        [Fact]
        public void GivenAFeature_SetValidator_ShouldHaveACarParkingValidator()
        {
            _featuresValidator.ShouldHaveChildValidator(feature => feature.CarParking, typeof(CarParkingValidator));
        }
    }
}