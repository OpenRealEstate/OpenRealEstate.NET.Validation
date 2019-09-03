using System;
using FluentValidation;

namespace OpenRealEstate.Validation
{
    public class ListingDateTimeValidator : AbstractValidator<DateTime>
    {
        private static readonly DateTime MinimumDateTimeOn = new DateTime(1800, 1, 1); // There shouldn't be any date before this point in time. Seriously ...

        public ListingDateTimeValidator()
        {
            RuleFor(dateTimeValue => dateTimeValue)
                .GreaterThanOrEqualTo(MinimumDateTimeOn)
                .WithMessage("The Date/Time value is illegal. Please use a valid Date/Time value which is a more current value .. like .. something from this century, please.");
        }
    }
}
