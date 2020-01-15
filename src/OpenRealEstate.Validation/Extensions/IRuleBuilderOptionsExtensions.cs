using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation.Extensions
{
    internal static class IRuleBuilderOptionsExtensions
    {
        // public static IRuleBuilderOptions<T, TProperty> When<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, 
        //      Func<T, bool> predicate, 
        //      ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators);

        internal static IRuleBuilderOptions<T, TProperty> WhenStatusTypeIsAvailableOrUnknown<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule) where T : Listing
        {
            return rule.When(listing => listing.StatusType == StatusType.Available ||
                                        listing.StatusType == StatusType.Unknown);
        }
    }
}
