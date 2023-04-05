using System;
using System.Linq;
using FluentValidation;
using OpenRealEstate.Core;

namespace OpenRealEstate.Validation
{
    public class ListingValidator<T> : AggregateRootValidator<T> where T : Listing
    {
        

        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default):
        /// - *Common data
        /// - AgencyId
        /// - StatusType
        /// - CreatedOn
        /// </para>
        /// <para>
        /// Normal:
        /// - Title
        /// - Address
        /// - Agents
        /// - Images
        /// - Flooplans
        /// - Videos
        /// - Inspections
        /// - LandDetails
        /// - Features
        /// </para>
        /// <para>
        /// Strict:
        /// - Links (Optional)</para>
        /// </summary>
        public ListingValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            // Minimum required data required to have a listing.
            RuleFor(listing => listing.AgencyId)
                .NotEmpty()
                .WithMessage("Every listing needs at least one 'AgencyId'. eg. FancyPants-1234a or 456123, etc.");

            RuleFor(listing => listing.StatusType)
                .NotEqual(StatusType.Unknown)
                .WithMessage("Invalid 'StatusType'. Please choose any status except Unknown.");

            RuleFor(listing => listing.CreatedOn).SetValidator(new ListingDateTimeValidator());

            // Normal rules to check, when we have a property to check.
            RuleSet(RuleSetKeys.NormalRuleSetKey, () =>
            {
                // Required.
                RuleFor(listing => listing.Title)
                    .NotEmpty();

                RuleFor(listing => listing.Address)
                    .NotNull()
                    .SetValidator(new AddressValidator());

                // Required where it exists.
                RuleForEach(listing => listing.Agents)
                    .SetValidator(new AgentValidator());

                RuleForEach(listing => listing.Images)
                    .SetValidator(new MediaValidator());

                RuleForEach(listing => listing.FloorPlans)
                    .SetValidator(new MediaValidator());

                RuleForEach(listing => listing.Videos)
                    .SetValidator(new MediaValidator());

                RuleForEach(listing => listing.Documents)
                    .SetValidator(new MediaValidator());

                RuleForEach(listing => listing.Inspections)
                    .SetValidator(new InspectionValidator());

                RuleFor(listing => listing.LandDetails)
                    .SetValidator(new LandDetailsValidator());

                RuleFor(listing => listing.Features)
                    .SetValidator(new FeaturesValidator());
            });

            // Strictest of rules to check existing properties.
            RuleSet(RuleSetKeys.StrictRuleSetKey, () =>
            {
                // Required where it exists.
                RuleForEach(listing => listing.Links)
                    .Must(LinkMustBeAUri)
                    .When(listing => listing.Links?.Any() == true)
                    .WithMessage("Link '{PropertyValue}' must be a valid URI. eg: http://www.SomeWebSite.com.au");
            });
        }

        private static bool LinkMustBeAUri(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
            {
                return false;
            }

            return Uri.TryCreate(link, UriKind.Absolute, out var result) &&
                   (result.Scheme == Uri.UriSchemeHttp ||
                    result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
