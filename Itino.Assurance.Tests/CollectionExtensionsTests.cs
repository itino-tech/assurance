using Itino.Assurance.Tests.TestUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Itino.Assurance.Tests;

public sealed class TupleArrayExtensionsTests : ArrayExtensionsTests<Tuple<int>>
{
    protected override Tuple<int> Convert(int value) => TestConvert.ToTuple(value);
}

public sealed class PairArrayExtensionsTests : ArrayExtensionsTests<KeyValuePair<int, int>>
{
    protected override KeyValuePair<int, int> Convert(int value) => TestConvert.ToPair(value);
}

public sealed class StringArrayExtensionsTests : ArrayExtensionsTests<string>
{
    protected override string Convert(int value) => TestConvert.ToString(value);
}

public sealed class IntArrayExtensionsTests : ArrayExtensionsTests<int>
{
    protected override int Convert(int value) => value;
}

public sealed class TupleCollectionExtensionsTests : CollectionExtensionsTests<Tuple<int>>
{
    protected override Tuple<int> Convert(int value) => TestConvert.ToTuple(value);
}

public sealed class PairCollectionExtensionsTests : CollectionExtensionsTests<KeyValuePair<int, int>>
{
    protected override KeyValuePair<int, int> Convert(int value) => TestConvert.ToPair(value);
}

public sealed class StringCollectionExtensionsTests : CollectionExtensionsTests<string>
{
    protected override string Convert(int value) => TestConvert.ToString(value);
}

public sealed class IntCollectionExtensionsTests : CollectionExtensionsTests<int>
{
    protected override int Convert(int value) => value;
}

public abstract class ArrayExtensionsTests<T> : EnumerableExtensionsTests<T[]>
{
    protected override void ExecuteEmpty(IReadOnlyCollection<int> integerCollection)
        => Assure.Is.Empty(integerCollection.Select(Convert).ToArray(), ValueName);

    protected override void ExecuteNotEmpty(IReadOnlyCollection<int> integerCollection)
        => Assure.Is.NotEmpty(integerCollection.Select(Convert).ToArray(), ValueName);

    protected override string GetEmptyErrorMessage()
        => EnumerableExtensions.AssureArrayEmptyErrorMessage(ValueName);

    protected override string GetNotEmptyErrorMessage()
        => EnumerableExtensions.AssureArrayNotEmptyErrorMessage(ValueName);

    protected abstract T Convert(int value);
}

public abstract class CollectionExtensionsTests<T> : EnumerableExtensionsTests<IReadOnlyCollection<T>>
{
    protected override void ExecuteEmpty(IReadOnlyCollection<int> integerCollection)
        => Assure.Is.Empty(integerCollection.Select(Convert).ToList(), ValueName);

    protected override void ExecuteNotEmpty(IReadOnlyCollection<int> integerCollection)
        => Assure.Is.NotEmpty(integerCollection.Select(Convert).ToList(), ValueName);

    protected override string GetEmptyErrorMessage()
        => EnumerableExtensions.AssureCollectionEmptyErrorMessage(ValueName);

    protected override string GetNotEmptyErrorMessage()
        => EnumerableExtensions.AssureCollectionNotEmptyErrorMessage(ValueName);

    protected abstract T Convert(int value);
}

public abstract class EnumerableExtensionsTests<TEnumerable>
{
    protected const string ValueName = nameof(ValueName);

    public static readonly IEnumerable<object[]> EmptyIntegerCollection = [[(IReadOnlyCollection<int>)[]]];

    public static readonly IEnumerable<object[]> NotEmptyIntegerCollection 
        = [[StaticIntegerCollectionFactory.CreateSingleValues(3)]];

    [Theory]
    [MemberData(nameof(EmptyIntegerCollection))]
    public void EmptyTest(IReadOnlyCollection<int> integerCollection) =>
        ExecuteEmpty(integerCollection);

    [Theory]
    [MemberData(nameof(NotEmptyIntegerCollection))]
    public void EmptyThrowsTest(IReadOnlyCollection<int> integerCollection) => ThrowsTest
        .Expect(GetEmptyErrorMessage())
        .Run(() => ExecuteEmpty(integerCollection));

    [Theory]
    [MemberData(nameof(NotEmptyIntegerCollection))]
    public void NotEmptyTest(IReadOnlyCollection<int> integerCollection) => 
        ExecuteNotEmpty(integerCollection);

    [Theory]
    [MemberData(nameof(EmptyIntegerCollection))]
    public void NotEmptyThrowsTest(IReadOnlyCollection<int> integerCollection) => ThrowsTest
        .Expect(GetNotEmptyErrorMessage())
        .Run(() => ExecuteNotEmpty(integerCollection));

    protected abstract void ExecuteEmpty(IReadOnlyCollection<int> integerCollection);

    protected abstract void ExecuteNotEmpty(IReadOnlyCollection<int> integerCollection);

    protected abstract string GetEmptyErrorMessage();

    protected abstract string GetNotEmptyErrorMessage();
}
