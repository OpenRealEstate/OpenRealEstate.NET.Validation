using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class CommunicationValidator : AbstractValidator<Communication>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - CommunicationType
        /// - Details
        /// </para>
        /// </summary>
        public CommunicationValidator()
        {
            RuleFor(communication => communication.CommunicationType)
                .NotEqual(CommunicationType.Unknown)
                .WithMessage("Please choose any communication type except Unknown.");

            RuleFor(communication => communication.Details)
                .NotEmpty()
                .WithMessage("A commucation type requires some details. Eg. the actual phone number or the actual email address.");
        }
    }
}
