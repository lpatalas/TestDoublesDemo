using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestDoublesDemo.TestDoubles
{
    public class MockPhoneNumberValidator : IPhoneNumberValidator
    {
        private readonly IReadOnlyList<string> _expectedIsValidCalls;
        private readonly List<string> _actualIsValidCalls = new List<string>();

        public MockPhoneNumberValidator(
            IEnumerable<string> expectedIsValidCalls)
            => _expectedIsValidCalls = expectedIsValidCalls.ToList();

        public bool IsValid(string phoneNumber)
        {
            _actualIsValidCalls.Add(phoneNumber);
            return true;
        }

        public void Verify()
        {
            _actualIsValidCalls.Should().BeEquivalentTo(_expectedIsValidCalls);
        }
    }

    public class MockTests
    {
        [Fact]
        public void Create_ShouldCallPhoneNumberValidatorForInputPhoneNumber()
        {
            // Given
            var validatorMock = new MockPhoneNumberValidator(
                new[] { "+48123456789" });
            var repository = new CustomerRepository(
                validatorMock);

            // When
            var result = repository.Create("John Smith", "+48123456789");

            // Then
            validatorMock.Verify();
        }
    }
}
