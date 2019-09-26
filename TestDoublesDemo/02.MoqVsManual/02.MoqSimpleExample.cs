using System;
using System.Collections.Generic;
using System.Text;

namespace TestDoublesDemo.MoqVsManual.SimpleExample
{
    using FluentAssertions;
    using Moq;

    public interface IFoo
    {
        string DoSomething(string value);
    }

    public class MoqExamples
    {
        private Mock<IFoo> mock = new Mock<IFoo>();

        public void Example1()
        {
            mock.Setup(foo => foo.DoSomething("ping"))
                .Returns("abc");
        }

        public class FakeFoo1 : IFoo
        {
            public string DoSomething(string value)
                => value == "ping" ? "abc" : string.Empty;
        }

        public void Example2()
        {
            // access invocation arguments when returning a value
            mock.Setup(x => x.DoSomething(It.IsAny<string>()))
                .Returns((string s) => s.ToLower());
        }

        public class FakeFoo2 : IFoo
        {
            public string DoSomething(string value)
                => value.ToLower();
        }

        public void Example3()
        {
            // throwing when invoked with specific parameters
            mock.Setup(foo => foo.DoSomething("reset"))
                .Throws<InvalidOperationException>();
            mock.Setup(foo => foo.DoSomething(""))
                .Throws(new ArgumentException("command"));
        }

        public class ThrowingFakeFoo : IFoo
        {
            public string DoSomething(string value)
            {
                if (value == "reset")
                    throw new InvalidOperationException();
                else if (value == "")
                    throw new ArgumentException("command");

                return string.Empty;
            }
        }

        public void Verify1()
        {
            mock.Verify(foo => foo.DoSomething(It.IsAny<string>()));
            mock.Verify(foo => foo.DoSomething(It.IsAny<string>()), Times.Never());
        }

        public class SpyFoo1 : IFoo
        {
            public bool WasDoSomethingCalled { get; private set; }
            public string DoSomething(string value)
            {
                WasDoSomethingCalled = true;
                return string.Empty;
            }
        }

        public void Verify1Manual()
        {
            var spy = new SpyFoo1();
            // ...
            spy.WasDoSomethingCalled.Should().BeTrue();
            spy.WasDoSomethingCalled.Should().BeFalse();
        }

        public void Verify2()
        {
            mock.Verify(foo => foo.DoSomething("ping"), Times.AtLeastOnce());
        }

        public class SpyFoo2 : IFoo
        {
            private readonly string _spiedValue;

            public int DoSomethingPingCallCount { get; private set; }

            public SpyFoo2(string spiedValue)
            {
                _spiedValue = spiedValue;
            }

            public string DoSomething(string value)
            {
                if (value == _spiedValue)
                    DoSomethingPingCallCount++;
                return string.Empty;
            }
        }

        public void Verify2Manual()
        {
            var spy = new SpyFoo2("ping");
            // ...
            spy.DoSomethingPingCallCount.Should().BeGreaterOrEqualTo(1);
        }
    }
}
