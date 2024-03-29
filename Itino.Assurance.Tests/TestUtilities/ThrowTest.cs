using System;
using Xunit;

namespace Itino.Assurance.Tests.TestUtilities;

internal static class ThrowsTest
{
    public static TestRunner<AssuranceException> Expect(string errorMessage)
        => Expect<AssuranceException>(errorMessage);

    public static TestRunner<TException> Expect<TException>(string errorMessage)
        where TException : Exception
        => new(errorMessage);

    public sealed class TestRunner<TException>(string errorMessage)
        where TException : Exception
    {
        public void Run(Action testAction)
        {
            var exception = Assert.Throws<TException>(testAction);
            Assert.Equal(exception.Message, errorMessage);
        }
    }
}
