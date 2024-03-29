using System;
using System.Runtime.CompilerServices;
using Itino.Assurance.Auxiliary;

namespace Itino.Assurance;

/// <summary>
/// Provides general extension methods for asserting equality conditions.
/// </summary>
public static class GeneralExtensions
{
    internal static readonly Func<object?, string?, object?, string> AssureEqualErrorMessage =
        (value, name, valueToCompare) => $"The{Utils.GetName(name)}value '{value}' is not equal to the expected value '{valueToCompare}'.";

    internal static readonly Func<object?, string?, string> AssureNotEqualErrorMessage =
        (value, name) => $"The{Utils.GetName(name)}value is equal to the not expected value '{value}'.";

    /// <summary>
    /// Asserts that the specified value is equal to the expected value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of values being compared.</typeparam>
    /// <param name="context">The assurance context for the value.</param>
    /// <param name="valueToCompare">The expected value for comparison.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the values are not equal.</exception>
    public static IAssuranceContext<TException, TValue> Equal<TException, TValue>(
        this IAssuranceContext<TException, TValue> context,
        TValue valueToCompare)
        where TException : Exception
        => context.Assure(
            value => value == null ? valueToCompare != null : !value.Equals(valueToCompare),
            (value, name) => AssureEqualErrorMessage(value, name, valueToCompare));

    /// <summary>
    /// Asserts that the specified value is equal to the expected value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of values being compared.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The value to be checked for equality.</param>
    /// <param name="valueToCompare">The expected value for comparison.</param>
    /// <param name="name">The optional name of the value (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the value.</returns>
    /// <exception><cref>TException</cref> thrown if the values are not equal.</exception>
    public static IAssuranceContext<TException, TValue> Equal<TException, TValue>(
        this IAssuranceContext<TException> context,
        TValue value,
        TValue valueToCompare,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).Equal(valueToCompare);

    /// <summary>
    /// Asserts that the specified value is not equal to the expected value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of values being compared.</typeparam>
    /// <param name="context">The assurance context for the value.</param>
    /// <param name="valueToCompare">The expected value for comparison.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the values are equal.</exception>
    public static IAssuranceContext<TException, TValue> NotEqual<TException, TValue>(
        this IAssuranceContext<TException, TValue> context,
        TValue valueToCompare)
        where TException : Exception
        => context.Assure(
            value => value == null ? valueToCompare == null : value.Equals(valueToCompare),
            name => AssureNotEqualErrorMessage(context.Value, name));

    /// <summary>
    /// Asserts that the specified value is not equal to the expected value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of values being compared.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The value to be checked for inequality.</param>
    /// <param name="valueToCompare">The expected value for comparison.</param>
    /// <param name="name">The optional name of the value (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the value.</returns>
    /// <exception><cref>TException</cref> thrown if the values are equal.</exception>
    public static IAssuranceContext<TException, TValue> NotEqual<TException, TValue>(
        this IAssuranceContext<TException> context,
        TValue value,
        TValue valueToCompare,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).NotEqual(valueToCompare);
}
