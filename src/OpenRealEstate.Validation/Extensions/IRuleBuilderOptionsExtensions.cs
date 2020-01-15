using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation.Extensions
{
    internal static class IRuleBuilderOptionsExtensions
    {
        internal static IRuleBuilderOptions<T, TProperty> WhenStatusTypeIsAvailableOrUnknown<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule) where T : Listing
        {
            return rule.When(listing => listing.StatusType == StatusType.Available ||
                                        listing.StatusType == StatusType.Unknown);
        }
    }
}
