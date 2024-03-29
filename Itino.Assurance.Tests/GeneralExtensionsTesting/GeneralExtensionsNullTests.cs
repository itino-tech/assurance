using Itino.Assurance.Tests.TestUtilities;
using System;
using System.Collections.Generic;
using Xunit;

namespace Itino.Assurance.Tests.GeneralExtensionsTesting;

public sealed class TupleGeneralExtensionsNullTests : GeneralExtensionsNullTests<Tuple<int>>
{
    protected override Tuple<int>? Convert(int value) => value == 0 ? null : TestConvert.ToTuple(value);
}

public sealed class PairGeneralExtensionsNullTests : GeneralExtensionsNullTests<KeyValuePair<int, int>?>
{
    protected override KeyValuePair<int, int>? Convert(int value) => value == 0 ? null : TestConvert.ToPair(value);
}

public sealed class StringGeneralExtensionsNullTests : GeneralExtensionsNullTests<string?>
{
    protected override string? Convert(int value) => value == 0 ? null : TestConvert.ToString(value);
}

public sealed class IntGeneralExtensionsNullTests : GeneralExtensionsNullTests<int?>
{
    protected override int? Convert(int value) => value == 0 ? null : value;
}

public abstract class GeneralExtensionsNullTests<T> : AbstractCompareTests<T?>
{
    [Theory]
    [InlineData(1, 1)]
    [InlineData(0, 0)]
    public void NullEqualTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Run((v, vc) => Assure.Is.Equal(v, vc));

    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    public void NullEqualThrowsTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Expect((v, vc) => GeneralExtensions.AssureEqualErrorMessage(v, nameof(v), vc))
        .Run((v, vc) => Assure.Is.Equal(v, vc));

    [Theory]
    [InlineData(1, 0)]
    [InlineData(0, 1)]
    public void NullNotEqualTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Run((v, vc) => Assure.Is.NotEqual(v, vc));

    [Theory]
    [InlineData(1, 1)]
    [InlineData(0, 0)]
    public void NullNotEqualThrowsTest(int value, int valueToCompare) => Set(value, valueToCompare)
        .Expect((v, _) => GeneralExtensions.AssureNotEqualErrorMessage(v, nameof(v)))
        .Run((v, vc) => Assure.Is.NotEqual(v, vc));
}
