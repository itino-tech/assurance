using System;
using System.Runtime.CompilerServices;
using Itino.Assurance.Auxiliary;

namespace Itino.Assurance;

/// <summary>
/// Provides extension methods for asserting conditions on text values.
/// </summary>
public static class TextExtensions
{
    internal static readonly Func<string?, string> AssureNotWhiteSpaceErrorMessage =
        name => $"The string{Utils.GetName(name)}cannot be blank.";

    /// <summary>
    /// Asserts that the specified string is not white space.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context for the string.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the string is white space.</exception>
    public static IAssuranceContext<TException, string> NotWhiteSpace<TException>(this IAssuranceContext<TException, string> context)
        where TException : Exception
        => context.Assure(string.IsNullOrWhiteSpace, AssureNotWhiteSpaceErrorMessage);

    /// <summary>
    /// Asserts that the specified string is not white space.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The string to be checked for white space.</param>
    /// <param name="name">The optional name of the string (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the string.</returns>
    /// <exception><cref>TException</cref> thrown if the string is white space.</exception>
    public static IAssuranceContext<TException, string> NotWhiteSpace<TException>(
        this IAssuranceContext<TException> context,
        string value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).NotWhiteSpace();
}
