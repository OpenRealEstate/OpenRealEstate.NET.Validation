using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Shouldly;

namespace OpenRealEstate.Validation.Tests
{
    // TODO: TEMPORARY extension method while fluentvalidation has a bug checking for Nullable<DateTime>.
    public static class ValidationTestExtensions
    {
        public static ValidationFailure ShouldHaveValidationErrorFor<T>(this IValidator<T> validator,
                                                                     object valueToValidate,
                                                                     string propertyNameThatErrored)
        {
            // Arrange & Act.
            var result = validator.Validate(valueToValidate);

            // Assert.
            result.IsValid.ShouldBe(false);
            var error = result.Errors.Where(x => x.PropertyName == propertyNameThatErrored).SingleOrDefault();

            if (error == null)
            {
                var errorProperties = string.Join(", ", result.Errors.Select(x => x.PropertyName));
                throw new Exception($"Expected an error against the property {propertyNameThatErrored} but none was found. Instead found: {errorProperties}");
            }

            return error;
        }
    }
}
