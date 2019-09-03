using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class InspectionValidator : AbstractValidator<Inspection>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - OpensOn
        /// - ClosesOn
        /// </para>
        /// </summary>
        public InspectionValidator()
        {
            RuleFor(inspection => inspection.OpensOn)
                .SetValidator(new ListingDateTimeValidator());

            RuleFor(inspection => inspection.ClosesOn)
                .Must((inspection, closesOn) => closesOn > inspection.OpensOn)
                .When(inspection => inspection.ClosesOn.HasValue)
                .WithMessage("The Date/Time value is illegal. Please use a valid value which is -after- the 'OpensOn' value or a NULL value if you're not sure when it closes on).");
        }
    }
}
