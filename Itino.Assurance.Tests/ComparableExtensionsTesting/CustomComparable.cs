using System;

namespace Itino.Assurance.Tests.ComparableExtensionsTesting;

public sealed class CustomComparable(int value) : IComparable<CustomComparable>, IComparable
{
    private readonly int _value = value;

    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            CustomComparable comparable => _value.CompareTo(comparable._value),
            _ => throw new ArgumentException($"The argument must be '{typeof(CustomComparable)}' type.")
        };
    }

    public int CompareTo(CustomComparable? other) => CompareTo((object?)other);
}
