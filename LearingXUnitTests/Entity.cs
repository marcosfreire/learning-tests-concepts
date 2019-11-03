using FluentValidation.Results;
using System;

namespace LearingXUnitTests
{
    public class Entity
    {
        protected Guid Id { get; set; }
        protected ValidationResult ValidationResult { get; set; }
    }
}
