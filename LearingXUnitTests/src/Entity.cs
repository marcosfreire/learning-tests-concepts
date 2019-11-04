using FluentValidation.Results;
using System;

namespace LearingXUnitTests
{
    public class Entity
    {
        public Guid Id { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}