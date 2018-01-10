using FluentValidation;
using OpenRealEstate.NET.Core;

namespace OpenRealEstate.NET.Validation
{
    public class LandDetailsValidator : AbstractValidator<LandDetails>
    {
        public LandDetailsValidator()
        {
            RuleFor(land => land.Area).SetValidator(new UnitOfMeasureValidator());
            RuleFor(land => land.Frontage).SetValidator(new UnitOfMeasureValidator());
            RuleFor(land => land.Depths).SetCollectionValidator(new UnitOfMeasureValidator());
        }
    }
}