using System;
using System.Collections.Generic;

namespace Itino.Assurance.Tests.TestUtilities;

internal static class TestConvert
{
    public static string ToString(int value) => $"Value {value}";

    public static Tuple<int> ToTuple(int value) => Tuple.Create(value);

    public static KeyValuePair<int, int> ToPair(int value) => new(value, value);
}
