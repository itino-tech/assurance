using Itino.Assurance.Tests.TestUtilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Itino.Assurance.Tests.GeneralExtensionsTesting;

public sealed class PairGeneralExtensionsTests : GeneralExtensionsTests<KeyValuePair<int, int>>
{
    protected override KeyValuePair<int, int> Convert(int value) => TestConvert.ToPair(value);
}

public sealed class TupleGeneralExtensionsTests : GeneralExtensionsTests<Tuple<int>>
{
    protected override Tuple<int> Convert(int value) => TestConvert.ToTuple(value);
}

public sealed class ObjectGeneralExtensionsTests : GeneralExtensionsTests<object>
{
    protected override object Convert(int value) => TestConvert.ToTuple(value);
}

public sealed class StringGeneralExtensionsTests : GeneralExtensionsTests<string>
{
    protected override string Convert(int value) => TestConvert.ToString(value);
}

public sealed class IntGeneralExtensionsTests : GeneralExtensionsTests<int>
{
    protected override int Convert(int value) => value;
}

public abstract class GeneralExtensionsTests<T> : AbstractCompareTests<T>
{
    [Theory]
    [ClassData(typeof(EqualIntegerPairs))]
    public void EqualTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Run((v, vc) => Assure.Is.Equal(v, vc));

    [Theory]
    [ClassData(typeof(GreaterThanIntegerPairs))]
    public void EqualThrowsTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Expect((v, vc) => GeneralExtensions.AssureEqualErrorMessage(v, nameof(v), vc))
        .Run((v, vc) => Assure.Is.Equal(v, vc));

    [Theory]
    [ClassData(typeof(GreaterThanIntegerPairs))]
    public void NotEqualTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Run((v, vc) => Assure.Is.NotEqual(v, vc));

    [Theory]
    [ClassData(typeof(EqualIntegerPairs))]
    public void NotEqualThrowsTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Expect((v, _) => GeneralExtensions.AssureNotEqualErrorMessage(v, nameof(v)))
        .Run((v, vc) => Assure.Is.NotEqual(v, vc));
}
