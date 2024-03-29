using System;
using System.Runtime.CompilerServices;
using Itino.Assurance.Auxiliary;

namespace Itino.Assurance;

/// <summary>
/// Provides extension methods for asserting conditions on comparable values.
/// </summary>
public static class ComparableExtensions
{
    internal static readonly Func<IComparable, string?, IComparable, string> AssureLessThanErrorMessage =
        (value, name, valueToCompare) => $"The{Utils.GetName(name)}value '{value}' is {ProcessEqualityText("greater than", value, valueToCompare)} '{valueToCompare}'.";

    internal static readonly Func<object, string?, object, string> AssureLessThanOrEqualErrorMessage =
        (value, name, valueToCompare) => $"The{Utils.GetName(name)}value '{value}' is greater than '{valueToCompare}'.";

    internal static readonly Func<IComparable, string?, IComparable, string> AssureGreaterThanErrorMessage =
        (value, name, valueToCompare) => $"The{Utils.GetName(name)}value '{value}' is {ProcessEqualityText("less than", value, valueToCompare)} '{valueToCompare}'.";

    internal static readonly Func<object, string?, object, string> AssureGreaterThanOrEqualErrorMessage =
        (value, name, valueToCompare) => $"The{Utils.GetName(name)}value '{value}' is less than '{valueToCompare}'.";

    /// <summary>
    /// Asserts that the value is less than the specified value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of the value being compared.</typeparam>
    /// <param name="context">The assurance context for the value.</param>
    /// <param name="valueToCompare">The value to compare against.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the value is not less than the specified value.</exception>
    public static IAssuranceContext<TException, TValue> LessThan<TException, TValue>(
        this IAssuranceContext<TException, TValue> context, TValue valueToCompare)
        where TException : Exception
        where TValue : IComparable<TValue>, IComparable
        => context.Assure(
            value => value.CompareTo(valueToCompare) >= 0,
            (value, name) => AssureLessThanErrorMessage(value, name, valueToCompare));

    /// <summary>
    /// Asserts that the value is less than the specified value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of the value being compared.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The value to be checked.</param>
    /// <param name="valueToCompare">The value to compare against.</param>
    /// <param name="name">The optional name of the value (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the value.</returns>
    /// <exception><cref>TException</cref> thrown if the value is not less than the specified value.</exception>
    public static IAssuranceContext<TException, TValue> LessThan<TException, TValue>(
        this IAssuranceContext<TException> context,
        TValue value,
        TValue valueToCompare,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        where TValue : IComparable<TValue>, IComparable
        => context.WithValue(value, name).LessThan(valueToCompare);

    /// <summary>
    /// Asserts that the value is less than or equal to the specified value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of the value being compared.</typeparam>
    /// <param name="context">The assurance context for the value.</param>
    /// <param name="valueToCompare">The value to compare against.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the value is not less than or equal to the specified value.</exception>
    public static IAssuranceContext<TException, TValue> LessThanOrEqual<TException, TValue>(
        this IAssuranceContext<TException, TValue> context, TValue valueToCompare)
        where TException : Exception
        where TValue : IComparable<TValue>, IComparable
        => context.Assure(
            value => value.CompareTo(valueToCompare) > 0,
            (value, name) => AssureLessThanOrEqualErrorMessage(value, name, valueToCompare));

    /// <summary>
    /// Asserts that the value is less than or equal to the specified value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of the value being compared.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The value to be checked.</param>
    /// <param name="valueToCompare">The value to compare against.</param>
    /// <param name="name">The optional name of the value (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the value.</returns>
    /// <exception><cref>TException</cref> thrown if the value is not less than or equal to the specified value.</exception>
    public static IAssuranceContext<TException, TValue> LessThanOrEqual<TException, TValue>(
        this IAssuranceContext<TException> context,
        TValue value,
        TValue valueToCompare,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        where TValue : IComparable<TValue>, IComparable
        => context.WithValue(value, name).LessThanOrEqual(valueToCompare);

    /// <summary>
    /// Asserts that the value is greater than the specified value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of the value being compared.</typeparam>
    /// <param name="context">The assurance context for the value.</param>
    /// <param name="valueToCompare">The value to compare against.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the value is not greater than the specified value.</exception>
    public static IAssuranceContext<TException, TValue> GreaterThan<TException, TValue>(
        this IAssuranceContext<TException, TValue> context,
        TValue valueToCompare)
        where TException : Exception
        where TValue : IComparable<TValue>, IComparable
        => context.Assure(
            value => value.CompareTo(valueToCompare) <= 0,
            (value, name) => AssureGreaterThanErrorMessage(value, name, valueToCompare));

    /// <summary>
    /// Asserts that the value is greater than the specified value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of the value being compared.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The value to be checked.</param>
    /// <param name="valueToCompare">The value to compare against.</param>
    /// <param name="name">The optional name of the value (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the value.</returns>
    /// <exception><cref>TException</cref> thrown if the value is not greater than the specified value.</exception>
    public static IAssuranceContext<TException, TValue> GreaterThan<TException, TValue>(
        this IAssuranceContext<TException> context,
        TValue value,
        TValue valueToCompare,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        where TValue : IComparable<TValue>, IComparable
        => context.WithValue(value, name).GreaterThan(valueToCompare);

    /// <summary>
    /// Asserts that the value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of the value being compared.</typeparam>
    /// <param name="context">The assurance context for the value.</param>
    /// <param name="valueToCompare">The value to compare against.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the value is not greater than or equal to the specified value.</exception>
    public static IAssuranceContext<TException, TValue> GreaterThanOrEqual<TException, TValue>(
        this IAssuranceContext<TException, TValue> context,
        TValue valueToCompare)
        where TException : Exception
        where TValue : IComparable<TValue>, IComparable
        => context.Assure(
            value => value.CompareTo(valueToCompare) < 0,
            (value, name) => AssureGreaterThanOrEqualErrorMessage(value, name, valueToCompare));

    /// <summary>
    /// Asserts that the value is greater than or equal to the specified value.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TValue">The type of the value being compared.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The value to be checked.</param>
    /// <param name="valueToCompare">The value to compare against.</param>
    /// <param name="name">The optional name of the value (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the value.</returns>
    /// <exception><cref>TException</cref> thrown if the value is not greater than or equal to the specified value.</exception>
    public static IAssuranceContext<TException, TValue> GreaterThanOrEqual<TException, TValue>(
        this IAssuranceContext<TException> context,
        TValue value,
        TValue valueToCompare,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        where TValue : IComparable<TValue>, IComparable
        => context.WithValue(value, name).GreaterThanOrEqual(valueToCompare);

    private static string ProcessEqualityText(string text, IComparable value, IComparable valueToCompare)
        => value.CompareTo(valueToCompare) == 0 ? "equal to" : text;
}
