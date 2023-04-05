using System;
using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using Xunit;

namespace OpenRealEstate.Validation.Tests
{
    public class SaleDetailsValidatorTests
    {
        private class FakeSaleDetails : ISaleDetails
        {
            public AuthorityType Authority { get; set; }
            public SalePricing Pricing { get; set; }
            public DateTime? AuctionOn { get; set; }
            public int? YearBuilt { get; set; }
            public int? YearLastRenovated { get; set; }
        }

        private readonly SaleDetailsValidator _saleDetailsValidator;

        public SaleDetailsValidatorTests()
        {
            _saleDetailsValidator = new SaleDetailsValidator();
        }

        [Fact]
        public void GivenAnInvalidAuctionOn_Validate_ShouldHaveAValidationError()
        {
            var fakeSaleDetails = new FakeSaleDetails
            {
                AuctionOn = DateTime.MinValue
            };
            _saleDetailsValidator.ShouldHaveValidationErrorFor(sd => sd.AuctionOn, fakeSaleDetails);
        }

        [Fact]
        public void GivenAnInvalidYearBuilt_Validate_ShouldHaveAValidationError()
        {
            var fakeSaleDetails = new FakeSaleDetails
            {
                YearBuilt = DateTime.MinValue.Year
            };
            _saleDetailsValidator.ShouldHaveValidationErrorFor(sd => sd.YearBuilt, fakeSaleDetails);
        }

        [Fact]
        public void GivenAnInvalidYearLastRenovated_Validate_ShouldHaveAValidationError()
        {
            var fakeSaleDetails = new FakeSaleDetails
            {
                YearLastRenovated = DateTime.MinValue.Year
            };
            _saleDetailsValidator.ShouldHaveValidationErrorFor(sd => sd.YearLastRenovated, fakeSaleDetails);
        }

        public static TheoryData<DateTime?> ValidAuctionOnData => new()
        {
            { DateTime.UtcNow },
            { null }
        };

        [Theory]
        [MemberData(nameof(ValidAuctionOnData))]
        public void GivenAnAuctionOn_Validate_ShouldNotHaveAValidationError(DateTime? someDateTime)
        {
            var fakeSaleDetails = new FakeSaleDetails
            {
                AuctionOn = someDateTime
            };
            _saleDetailsValidator.ShouldNotHaveValidationErrorFor(sd => sd.AuctionOn, fakeSaleDetails);
        }

        public static TheoryData<int?> ValidYearBuiltData => new()
        {
            { DateTime.UtcNow.Year },
            { null }
        };

        [Theory]
        [MemberData(nameof(ValidYearBuiltData))]
        public void GivenAYearBuilt_Validate_ShouldNotHaveAValidationError(int? someDateTime)
        {
            var fakeSaleDetails = new FakeSaleDetails
            {
                YearBuilt = someDateTime
            };
            _saleDetailsValidator.ShouldNotHaveValidationErrorFor(sd => sd.YearBuilt, fakeSaleDetails);
        }

        public static TheoryData<int?> ValidYearLastRenovatedData => new()
        {
            { DateTime.UtcNow.Year },
            { null }
        };

        [Theory]
        [MemberData(nameof(ValidYearLastRenovatedData))]
        public void GivenAYearLastRenovated_Validate_ShouldNotHaveAValidationError(int? someDateTime)
        {
            var fakeSaleDetails = new FakeSaleDetails
            {
                YearLastRenovated = someDateTime
            };
            _saleDetailsValidator.ShouldNotHaveValidationErrorFor(sd => sd.YearLastRenovated, fakeSaleDetails);
        }
    }
}
