using FluentAssertions;
using Xunit;

namespace TestDoublesDemo.TestDoubles
{
    public class StubPhoneNumberValidator : IPhoneNumberValidator
    {
        public bool IsValid(string phoneNumber)
            => true;
    }

    public class StubTests
    {
        [Fact]
        public void Create_WhenCalledWithValidData_ShouldReturnNewCustomerInstance()
        {
            // Given
            var repository = new CustomerRepository(
                new StubPhoneNumberValidator());

            // When
            var result = repository.Create("John Smith", "+48123456789");

            // Then
            result.Name.Should().Be("John Smith");
            result.PhoneNumber.Should().Be("+48123456789");
        }
    }
}
