using System;
using System.Runtime.CompilerServices;

namespace Itino.Assurance.Tests.CustomExtensionsTesting;

internal static class CustomExtensions
{
    internal static readonly Func<string, string?, string, string> SpecificTextErrorMessage =
        (value, name, textToCompare) => $"The '{name}' value '{value}' is not equal to '{textToCompare}'.";

    public static IAssuranceContext<TException, string> EqualToSpecificText<TException>(
        this IAssuranceContext<TException, string> context,
        string textToCompare)
        where TException : Exception
        => context.Assure(
            value => !value.Equals(textToCompare, StringComparison.InvariantCulture),
            (value, name) => SpecificTextErrorMessage(value, name, textToCompare));

    public static IAssuranceContext<TException, string> EqualToSpecificText<TException>(
        this IAssuranceContext<TException> context,
        string value,
        string textToCompare,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).EqualToSpecificText(textToCompare);
}
