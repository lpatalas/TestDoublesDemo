using Moq;
using Xunit;

namespace TestDoublesDemo.TestDoubles
{
    public class MoqTests
    {
        [Fact]
        public void Create_ShouldCallPhoneNumberValidatorForInputPhoneNumber()
        {
            // Given
            var validatorMock = new Mock<IPhoneNumberValidator>();
            validatorMock
                .Setup(m => m.IsValid(It.IsAny<string>()))
                .Returns(true);

            var repository = new CustomerRepository(validatorMock.Object);

            // When
            var result = repository.Create("John Smith", "+48123456789");

            // Then
            validatorMock.Verify(m => m.IsValid("+48123456789"), Times.Once);
        }
    }
}
