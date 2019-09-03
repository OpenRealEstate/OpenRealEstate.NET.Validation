using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class AgentValidator : AbstractValidator<Agent>
    {
        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - Name
        /// - Communications
        /// </para>
        /// </summary>
        public AgentValidator()
        {
            RuleFor(agent => agent.Name).NotEmpty();
            RuleForEach(agent => agent.Communications)
                .SetValidator(new CommunicationValidator());
        }
    }
}
