using FluentValidation.TestHelper;
using OpenRealEstate.Validation.Rental;
using Xunit;

namespace OpenRealEstate.Validation.Tests.Rental
{
    public class RentalPricingValidatorTests
    {
        public RentalPricingValidatorTests()
        {
            _validator = new RentalPricingValidator();
        }

        private readonly RentalPricingValidator _validator;

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void GivenAValidBond_Validate_ShouldNotHaveAValidationError(int bond)
        {
            _validator.ShouldNotHaveValidationErrorFor(rentalPricing => rentalPricing.Bond, bond);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void GivenAnInvalidBond_Validate_ShouldHaveAValidationError(int bond)
        {
            _validator.ShouldHaveValidationErrorFor(rentalPricing => rentalPricing.Bond, bond);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void GivenAnInvalidRentalPrice_Validate_ShouldHaveAValidationError(int rentalPrice)
        {
            _validator.ShouldHaveValidationErrorFor(rentalPricing => rentalPricing.RentalPrice, rentalPrice);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void GivenAValidRentalPrice_Validate_ShouldNotHaveAValidationError(int rentalPrice)
        {
            _validator.ShouldNotHaveValidationErrorFor(rentalPricing => rentalPricing.RentalPrice, rentalPrice);
        }
    }
}