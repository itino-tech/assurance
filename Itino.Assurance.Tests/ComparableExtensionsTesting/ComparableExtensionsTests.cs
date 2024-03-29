using System;
using Itino.Assurance.Tests.TestUtilities;
using Xunit;

namespace Itino.Assurance.Tests.ComparableExtensionsTesting;

public sealed class CustomComparableExtensionsTests : ComparableExtensionsTests<CustomComparable>
{
    protected override CustomComparable Convert(int value) => new(value);
}

public sealed class FloatComparableExtensionsTests : ComparableExtensionsTests<float>
{
    protected override float Convert(int value) => value;
}

public sealed class IntegerComparableExtensionsTests : ComparableExtensionsTests<int>
{
    protected override int Convert(int value) => value;
}

public abstract class ComparableExtensionsTests<T> : AbstractCompareTests<T> where T : IComparable<T>, IComparable
{
    [Theory]
    [ClassData(typeof(LessThanIntegerPairs))]
    public void LessThanTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Run((v, vc) => Assure.Is.LessThan(v, vc));

    [Theory]
    [ClassData(typeof(GreaterThanOrEqualIntegerPairs))]
    public void LessThanThrowsTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Expect((v, vc) => ComparableExtensions.AssureLessThanErrorMessage(v, nameof(v), vc))
        .Run((v, vc) => Assure.Is.LessThan(v, vc));

    [Theory]
    [ClassData(typeof(LessThanOrEqualIntegerPairs))]
    public void LessThanOrEqualTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Run((v, vc) => Assure.Is.LessThanOrEqual(v, vc));

    [Theory]
    [ClassData(typeof(GreaterThanIntegerPairs))]
    public void LessThanOrEqualThrowsTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Expect((v, vc) => ComparableExtensions.AssureLessThanOrEqualErrorMessage(v, nameof(v), vc))
        .Run((v, vc) => Assure.Is.LessThanOrEqual(v, vc));

    [Theory]
    [ClassData(typeof(GreaterThanIntegerPairs))]
    public void GreaterThanTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Run((v, vc) => Assure.Is.GreaterThan(v, vc));

    [Theory]
    [ClassData(typeof(LessThanOrEqualIntegerPairs))]
    public void GreaterThanThrowsTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Expect((v, vc) => ComparableExtensions.AssureGreaterThanErrorMessage(v, nameof(v), vc))
        .Run((v, vc) => Assure.Is.GreaterThan(v, vc));

    [Theory]
    [ClassData(typeof(GreaterThanOrEqualIntegerPairs))]
    public void GreaterThanOrEqualTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Run((v, vc) => Assure.Is.GreaterThanOrEqual(v, vc));

    [Theory]
    [ClassData(typeof(LessThanIntegerPairs))]
    public void GreaterThanOrEqualThrowsTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Expect((v, vc) => ComparableExtensions.AssureGreaterThanOrEqualErrorMessage(v, nameof(v), vc))
        .Run((v, vc) => Assure.Is.GreaterThanOrEqual(v, vc));
}
