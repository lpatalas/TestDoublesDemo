using FluentAssertions;
using System;
using Xunit;

namespace TestDoublesDemo.TestDoubles
{
    public class DummyPhoneNumberValidator : IPhoneNumberValidator
    {
        public bool IsValid(string phoneNumber)
            => throw new NotImplementedException();
    }

    public class DummyTests
    {
        [Fact]
		public void GetAll_WhenNoCustomersWereCreated_ShouldReturnEmptyList()
        {
            // Given
            var repository = new CustomerRepository(
                new DummyPhoneNumberValidator());

            // When
            var result = repository.GetAll();

            // Then
            result.Should().BeEmpty();
        }
    }
}
