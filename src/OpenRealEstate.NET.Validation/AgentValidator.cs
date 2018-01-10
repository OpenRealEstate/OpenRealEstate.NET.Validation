using FluentValidation;
using OpenRealEstate.NET.Core;

namespace OpenRealEstate.NET.Validation
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