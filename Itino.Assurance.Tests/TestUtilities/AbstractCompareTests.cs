namespace Itino.Assurance.Tests.TestUtilities;

public abstract class AbstractCompareTests<T>
{
    protected abstract T Convert(int value);

    protected CompareTest<T> Set(int value, int valueToCompare)
        => new(Convert(value), Convert(valueToCompare));
}
