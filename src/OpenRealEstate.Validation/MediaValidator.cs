using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class MediaValidator : AbstractValidator<Media>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - Url
        /// - Order
        /// </para>
        /// </summary>
        public MediaValidator()
        {
            RuleFor(media => media.Url)
                .NotEmpty();

            RuleFor(media => media.Order)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
