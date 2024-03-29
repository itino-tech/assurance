using System;
using System.Collections.Generic;
using System.Linq;

namespace Itino.Assurance.Tests.TestUtilities;

internal static class StaticIntegerCollectionFactory
{
    private const int Seed = 0;

    private const int Margin = 1;

    public static IReadOnlyCollection<(int, int)> CreateGreaterThanOrEqualPairs(int greaterThanCount, int equalCount)
    {
        var random = new Random(Seed);
        return random.CreateGreaterThanPairs(greaterThanCount).Union(random.CreateEqualPairs(equalCount)).ToList();
    }

    public static IReadOnlyCollection<(int, int)> CreateLessThanOrEqualPairs(int lessThanCount, int equalCount)
    {
        var random = new Random(Seed);
        return random.CreateLessThanPairs(lessThanCount).Union(random.CreateEqualPairs(equalCount)).ToList();
    }

    public static IReadOnlyCollection<(int, int)> CreateEqualPairs(int count)
        => new Random(Seed).CreateEqualPairs(count).ToList();

    public static IReadOnlyCollection<(int, int)> CreateGreaterThanPairs(int count)
        => new Random(Seed).CreateGreaterThanPairs(count).ToList();

    public static IReadOnlyCollection<(int, int)> CreateLessThanPairs(int count)
        => new Random(Seed).CreateLessThanPairs(count).ToList();

    public static IReadOnlyCollection<int> CreateSingleValues(int count)
        => new Random(Seed).CreateSingleValues(count).ToList();

    private static IEnumerable<(int, int)> CreateEqualPairs(this Random random, int count)
        => random.CreateSingleValues(count).Select(i => (i, i));

    private static IEnumerable<(int, int)> CreateGreaterThanPairs(this Random random, int count)
        => random.CreatePairs(count, (r, i) => r.GetLessThan(i));

    private static IEnumerable<(int, int)> CreateLessThanPairs(this Random random, int count)
        => random.CreatePairs(count, (r, i) => r.GetGreaterThan(i));

    private static IEnumerable<int> CreateSingleValues(this Random random, int count)
        => Enumerable.Range(0, count).Select(i => random.GetMargined());

    private static IEnumerable<(int, int)> CreatePairs(
        this Random random,
        int count,
        Func<Random, int, int> pairFactory)
        => random.CreateSingleValues(count).Select(i => (i, pairFactory(random, i)));

    private static int GetLessThan(this Random random, int value)
        => random.Next(int.MinValue, value);

    private static int GetGreaterThan(this Random random, int value)
        => random.Next(value, int.MaxValue);

    private static int GetMargined(this Random random)
        => random.Next(int.MinValue + Margin, int.MaxValue - Margin);
}
