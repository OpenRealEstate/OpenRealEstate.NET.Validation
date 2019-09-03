using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using Shouldly;
using System;
using Xunit;

namespace OpenRealEstate.Validation.Tests
{
    public class SalePricingValidatorTests
    {
        public SalePricingValidatorTests()
        {
            _salePricingValidator = new SalePricingValidator();
        }

        private readonly SalePricingValidator _salePricingValidator;

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void GivenAnInvalidSalePrice_Validate_ShouldHaveAValidationError(int salePrice)
        {
            _salePricingValidator.ShouldHaveValidationErrorFor(salePricing => salePricing.SalePrice, salePrice);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void GivenAnInvalidSoldPrice_Validate_ShouldHaveAValidationError(int soldPrice)
        {
            _salePricingValidator.ShouldHaveValidationErrorFor(salePricing => salePricing.SoldPrice, soldPrice);
        }

        [Fact]
        public void GivenAnInvalidSoldOn_Validate_ShouldHaveAValidationError()
        {
            _salePricingValidator.ShouldHaveValidationErrorFor(salePrice => salePrice.SoldOn, DateTime.MinValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void GivenASalePrice_Validate_ShouldNotHaveAValidationError(int salePrice)
        {
            _salePricingValidator.ShouldNotHaveValidationErrorFor(salePricing => salePricing.SalePrice, salePrice);
        }

        [Fact]
        public void GivenASoldOn_Validate_ShouldNotHaveAValidationError()
        {
            _salePricingValidator.ShouldNotHaveValidationErrorFor(salePrice => salePrice.SoldOn, DateTime.UtcNow);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void GivenASoldPrice_Validate_ShouldNotHaveAValidationError(int soldPrice)
        {
            _salePricingValidator.ShouldNotHaveValidationErrorFor(salePrice => salePrice.SoldPrice, soldPrice);
        }

        [Fact]
        public void GivenNoSoldOn_Validate_ShouldNotHaveAValidationError()
        {
            _salePricingValidator.ShouldNotHaveValidationErrorFor(salePrice => salePrice.SoldOn, (DateTime?) null);
        }
    }
}
