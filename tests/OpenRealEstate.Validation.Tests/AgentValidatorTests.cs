﻿using System.Collections.Generic;
using FluentValidation;
using FluentValidation.TestHelper;
using OpenRealEstate.Core;
using Shouldly;
using Xunit;

namespace OpenRealEstate.Validation.Tests
{
    public class AgentValidatorTests
    {
        public AgentValidatorTests()
        {
            _validator = new AgentValidator();
        }

        private readonly AgentValidator _validator;

        [Fact]
        public void GiveACommunicationWithAnUnknownType_Validate_ShouldHaveAValidationError()
        {
            // Arrange.
            var Agent = new Agent
            {
                Name = "a",
                Communications = new List<Communication>
                {
                    new Communication
                    {
                        CommunicationType = CommunicationType.Unknown,
                        Details = "a"
                    }
                }
            };

            // Act.
            _validator.ShouldHaveChildValidator(agent => agent.Communications, typeof(CommunicationValidator));
            var result = _validator.Validate(Agent);
            //_validator.ShouldNotHaveValidationErrorFor(agent => agent.Communications, new[] {communication});

            // Assert.
            result.Errors.ShouldContain(x => x.PropertyName == "Communications[0].CommunicationType");
        }

        [Fact]
        public void GivenAName_Validate_ShouldNotHaveAValidationError()
        {
            _validator.ShouldNotHaveValidationErrorFor(agent => agent.Name, "a");
        }

        [Fact]
        public void GivenAnInvalidOpensOn_Validate_ShouldHaveAValidationError()
        {
            _validator.ShouldHaveValidationErrorFor(agent => agent.Name, "");
        }

        [Fact]
        public void GiveSomeCommunication_Validate_ShouldNotHaveAValidationError()
        {
            // Arrange.
            var Agent = new Agent
            {
                Name = "a",
                Communications = new List<Communication>
                {
                    new Communication
                    {
                        CommunicationType = CommunicationType.Email,
                        Details = "a"
                    }
                }
            };

            // Act.
            _validator.ShouldHaveChildValidator(agent => agent.Communications, typeof(CommunicationValidator));
            var result = _validator.Validate(Agent);
            //_validator.ShouldNotHaveValidationErrorFor(agent => agent.Communications, new[] {communication});

            // Assert.
            result.Errors.Count.ShouldBe(0);
        }
    }
}