using FluentValidation;
using OpenRealEstate.Core;
using OpenRealEstate.Validation.Extensions;
using System;
using System.Linq;

namespace OpenRealEstate.Validation
{
    public class ListingValidator<T> : AggregateRootValidator<T> where T : Listing
    {
        public const string NormalRuleSet = "default," + NormalRuleSetKey;
        public const string StrictRuleSet = NormalRuleSet + "," + StrictRuleSetKey;
        protected const string NormalRuleSetKey = "Normal";
        protected const string StrictRuleSetKey = "Strict";

        /// <summary>
        /// Validates the following:
        /// <para>
        /// Minimum (Default) when 'Available or Unknown':
        /// - *Common data
        /// - AgencyId
        /// - StatusType
        /// - CreatedOn
        /// </para>
        /// <para>
        /// Normal when 'Available or Unknown':
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
        /// Strict when 'Available or Unknown':
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
            RuleSet(NormalRuleSetKey, () =>
            {
                // Required.
                RuleFor(listing => listing.Title)
                    .NotEmpty()
                    .WhenStatusTypeIsAvailableOrUnknown();

                RuleFor(listing => listing.Address)
                    .NotNull()
                    .SetValidator(new AddressValidator())
                    .WhenStatusTypeIsAvailableOrUnknown();

                // Required where it exists.
                RuleForEach(listing => listing.Agents)
                    .SetValidator(new AgentValidator())
                    .WhenStatusTypeIsAvailableOrUnknown();

                RuleForEach(listing => listing.Images)
                    .SetValidator(new MediaValidator())
                    .WhenStatusTypeIsAvailableOrUnknown();

                RuleForEach(listing => listing.FloorPlans)
                    .SetValidator(new MediaValidator())
                    .WhenStatusTypeIsAvailableOrUnknown();

                RuleForEach(listing => listing.Videos)
                    .SetValidator(new MediaValidator())
                    .WhenStatusTypeIsAvailableOrUnknown();

                RuleForEach(listing => listing.Inspections)
                    .SetValidator(new InspectionValidator())
                    .WhenStatusTypeIsAvailableOrUnknown();

                RuleFor(listing => listing.LandDetails)
                    .SetValidator(new LandDetailsValidator())
                    .WhenStatusTypeIsAvailableOrUnknown();

                RuleFor(listing => listing.Features)
                .SetValidator(new FeaturesValidator())
                .WhenStatusTypeIsAvailableOrUnknown();
            });

            // Strictest of rules to check existing properties.
            RuleSet(StrictRuleSetKey, () =>
            {
                // Required where it exists.
                RuleForEach(listing => listing.Links)
                    .Must(LinkMustBeAUri)
                    .When(listing => listing.Links?.Any() == true)
                    .WhenStatusTypeIsAvailableOrUnknown()
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
