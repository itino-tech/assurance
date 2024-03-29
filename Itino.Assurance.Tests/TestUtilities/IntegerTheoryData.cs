using System.Collections.Generic;
using Xunit;

namespace Itino.Assurance.Tests.TestUtilities;

public sealed class EqualIntegerPairs : TheoryData<int, int>
{
    public EqualIntegerPairs()
    {
        this.Add(StaticIntegerCollectionFactory.CreateEqualPairs(IntegerTheoryData.TotalCount));
    }
}

public sealed class LessThanIntegerPairs : TheoryData<int, int>
{
    public LessThanIntegerPairs()
    {
        this.Add(StaticIntegerCollectionFactory.CreateLessThanPairs(IntegerTheoryData.TotalCount));
    }
}

public sealed class GreaterThanIntegerPairs : TheoryData<int, int>
{
    public GreaterThanIntegerPairs()
    {
        this.Add(StaticIntegerCollectionFactory.CreateGreaterThanPairs(IntegerTheoryData.TotalCount));
    }
}

public sealed class LessThanOrEqualIntegerPairs : TheoryData<int, int>
{
    public LessThanOrEqualIntegerPairs()
    {
        this.Add(StaticIntegerCollectionFactory.CreateLessThanOrEqualPairs(
            IntegerTheoryData.MixingDifferentCount,
            IntegerTheoryData.MixingEqualCount));
    }
}

public sealed class GreaterThanOrEqualIntegerPairs : TheoryData<int, int>
{
    public GreaterThanOrEqualIntegerPairs()
    {
        this.Add(StaticIntegerCollectionFactory.CreateGreaterThanOrEqualPairs(
            IntegerTheoryData.MixingDifferentCount,
            IntegerTheoryData.MixingEqualCount));
    }
}

internal static class IntegerTheoryData
{
    public const int MixingEqualCount = 4;

    public const int MixingDifferentCount = 4;

    public const int TotalCount = MixingEqualCount + MixingDifferentCount;

    public static void Add(this TheoryData<int, int> theoryData, IEnumerable<(int, int)> pairs)
    {
        foreach (var (left, right) in pairs)
        {
            theoryData.Add(left, right);
        }
    }
}
