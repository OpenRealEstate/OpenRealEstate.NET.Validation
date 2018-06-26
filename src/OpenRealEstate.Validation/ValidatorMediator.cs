using System;
using FluentValidation;
using FluentValidation.Results;
using OpenRealEstate.Core;
using OpenRealEstate.Core.Land;
using OpenRealEstate.Core.Rental;
using OpenRealEstate.Core.Residential;
using OpenRealEstate.Core.Rural;
using OpenRealEstate.Validation.Land;
using OpenRealEstate.Validation.Rental;
using OpenRealEstate.Validation.Residential;
using OpenRealEstate.Validation.Rural;

namespace OpenRealEstate.Validation
{
    public static class ValidatorMediator
    {
        public static ValidationResult Validate(Listing listing, 
                                                ListingValidatorRuleSet ruleSet = ListingValidatorRuleSet.Normal)
        {
            var ruleSetKey = ruleSet.ToDescription();

            if (listing == null)
            {
                throw new ArgumentNullException();
            }

            if (listing is ResidentialListing residentialListing)
            {
                return Validate(residentialListing, ruleSetKey);
            }

            if (listing is RentalListing rentalListing)
            {
                return Validate(rentalListing, ruleSetKey);
            }

            if (listing is RuralListing ruralListing)
            {
                return Validate(ruralListing, ruleSetKey);
            }

            if (listing is LandListing landListing)
            {
                return Validate(landListing, ruleSetKey);
            }

            var errorMessage =
                $"Tried to validate an unhandled Listing type: {listing.GetType()}. Only Residental, Rental, Rural and Land listing types are supported.";
            throw new Exception(errorMessage);
        }

        public static ValidationResult Validate(ResidentialListing residentialListing, 
                                                string ruleSet = ResidentialListingValidator.NormalRuleSet)
        {
            var validator = new ResidentialListingValidator();
            return string.IsNullOrWhiteSpace(ruleSet)
                ? validator.Validate(residentialListing)
                : validator.Validate(residentialListing, ruleSet: ruleSet);
        }

        public static ValidationResult Validate(RentalListing rentalListing, 
                                                string ruleSet = RentalListingValidator.NormalRuleSet)
        {
            var validator = new RentalListingValidator();
            return string.IsNullOrWhiteSpace(ruleSet)
                ? validator.Validate(rentalListing)
                : validator.Validate(rentalListing, ruleSet: ruleSet);
        }

        public static ValidationResult Validate(RuralListing ruralListing, 
                                                string ruleSet = RuralListingValidator.NormalRuleSet)
        {
            var validator = new RuralListingValidator();
            return string.IsNullOrWhiteSpace(ruleSet)
                ? validator.Validate(ruralListing)
                : validator.Validate(ruralListing, ruleSet: ruleSet);
        }

        public static ValidationResult Validate(LandListing landListing, 
                                                string ruleSet = ResidentialListingValidator.NormalRuleSet)
        {
            var validator = new LandListingValidator();
            return string.IsNullOrWhiteSpace(ruleSet)
                ? validator.Validate(landListing)
                : validator.Validate(landListing, ruleSet: ruleSet);
        }

    }
}