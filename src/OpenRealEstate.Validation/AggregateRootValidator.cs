using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class AggregateRootValidator<T> : AbstractValidator<T> where  T : AggregateRoot
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - Id
        /// - UpdatedOn
        /// </para>
        /// </summary>
        public AggregateRootValidator()
        {
            RuleFor(aggregateRoot => aggregateRoot.Id)
                .NotEmpty()
                .WithMessage("An 'Id' is required. eg. RayWhite.Kew, Belle.Mosman69, 12345XXAbCdE");

            RuleFor(aggregateRoot => aggregateRoot.UpdatedOn)
                .SetValidator(new ListingDateTimeValidator());
        }
    }
}
