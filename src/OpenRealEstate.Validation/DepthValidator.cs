using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class DepthValidator : AbstractValidator<Depth>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - Value
        /// - Side
        /// </para>
        /// </summary>
        public DepthValidator()
        {
            RuleFor(depth => depth.Value)
                .GreaterThanOrEqualTo(0);

            RuleFor(depth => depth.Side)
                .NotEmpty()
                .When(depth => depth.Value > 0)
                .WithMessage("If a depth's 'Value' is provided, then a 'Side' also needs to be provided.");
        }
    }
}
