using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class AgentValidator : AbstractValidator<Agent>
    {
        public AgentValidator()
        {
            RuleFor(agent => agent.Name).NotEmpty();
            RuleFor(agent => agent.Communications).SetCollectionValidator(new CommunicationValidator());
        }
    }
}