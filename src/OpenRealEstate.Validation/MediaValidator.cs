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
        /// - Id
        /// - Url
        /// - Order
        /// </para>
        /// </summary>
        public MediaValidator()
        {
            RuleFor(media => media.Id)
                .NotEmpty();

            RuleFor(media => media.Url)
                .NotEmpty();

            RuleFor(media => media.Order)
                .GreaterThan(0);
        }
    }
}
