using System;

namespace Itino.Assurance.Tests.TestUtilities;

public sealed class CompareTest<T>(T value, T valueToCompare)
{
    public void Run(Action<T, T> testAction) => testAction(value, valueToCompare);

    public ThrowsTestRunner Expect(Func<T, T, string> errorMessageFactory)
        => new(value, valueToCompare, errorMessageFactory(value, valueToCompare));

    public sealed class ThrowsTestRunner(T value, T valueToCompare, string errorMessage)
    {
        public void Run(Action<T, T> testAction) => ThrowsTest
            .Expect(errorMessage)
            .Run(() => testAction(value, valueToCompare));
    }
}
