using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace TestDoublesDemo.TestDoubles
{
    public class SpyPhoneNumberValidator : IPhoneNumberValidator
    {
        private readonly List<string> _validatedNumbers = new List<string>();
        public IReadOnlyList<string> ValidatedNumbers => _validatedNumbers;

        public bool IsValid(string phoneNumber)
        {
            _validatedNumbers.Add(phoneNumber);
            return true;
        }
    }

    public class SpyTests
    {
        [Fact]
        public void Create_ShouldCallPhoneNumberValidatorForInputPhoneNumber()
        {
            // Given
            var validatorSpy = new SpyPhoneNumberValidator();
            var repository = new CustomerRepository(validatorSpy);

            // When
            var result = repository.Create("John Smith", "+48123456789");

            // Then
            validatorSpy.ValidatedNumbers
                .Should().BeEquivalentTo("+48123456789");
        }
    }
}
