using FluentAssertions;
using Xunit;

namespace TestDoublesDemo.TestDoubles
{
    public class FakePhoneNumberValidator : IPhoneNumberValidator
    {
        public bool IsValid(string phoneNumber)
        {
            return phoneNumber.StartsWith("+46");
        }
    }

    public class FakeTests
    {
        [Fact]
        public void Create_ShouldStoreOnlyValidCustomers()
        {
            // Given
            var inputs = new[]
            {
                ("John Smith", "+4612345678"),
                ("Joe Doe", "+4812345678"),
                ("Mark Williams", "+4687654321")
            };

            var repository = new CustomerRepository(
                new FakePhoneNumberValidator());

            // When
            foreach (var (name, phoneNumber) in inputs)
            {
                try
                {
                    repository.Create(name, phoneNumber);
                }
                catch { }
            }

            // Then
            repository.GetAll().Should().HaveCount(2);
        }
    }
}
