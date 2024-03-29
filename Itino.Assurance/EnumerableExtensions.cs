using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Itino.Assurance.Auxiliary;

namespace Itino.Assurance;

/// <summary>
/// Provides extension methods for asserting conditions on collections.
/// </summary>
public static class EnumerableExtensions
{
    internal static readonly Func<string?, string> AssureArrayNotEmptyErrorMessage =
        name => $"The array{Utils.GetName(name)}is empty.";

    internal static readonly Func<string?, string> AssureCollectionNotEmptyErrorMessage =
        name => $"The collection{Utils.GetName(name)}is empty.";

    internal static readonly Func<string?, string> AssureArrayEmptyErrorMessage =
        name => $"The array{Utils.GetName(name)}is not empty.";

    internal static readonly Func<string?, string> AssureCollectionEmptyErrorMessage =
        name => $"The collection{Utils.GetName(name)}is not empty.";


    /// <summary>
    /// Asserts that the specified array is not empty.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TItem">The type of items in the array.</typeparam>
    /// <param name="context">The assurance context for the array.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the array is empty.</exception>
    public static IAssuranceContext<TException, TItem[]> NotEmpty<TException, TItem>(
        this IAssuranceContext<TException, TItem[]> context)
        where TException : Exception
        => context.Assure(value => value.Length == 0, AssureArrayNotEmptyErrorMessage);

    /// <summary>
    /// Asserts that the specified array is not empty.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TItem">The type of items in the array.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The array to be checked.</param>
    /// <param name="name">The optional name of the array (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the array.</returns>
    /// <exception><cref>TException</cref> thrown if the array is empty.</exception>
    public static IAssuranceContext<TException, TItem[]> NotEmpty<TException, TItem>(
        this IAssuranceContext<TException> context, TItem[] value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).NotEmpty();

    /// <summary>
    /// Asserts that the specified array is empty.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TItem">The type of items in the array.</typeparam>
    /// <param name="context">The assurance context for the array.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the array is not empty.</exception> 
    public static IAssuranceContext<TException, TItem[]> Empty<TException, TItem>(
        this IAssuranceContext<TException, TItem[]> context)
        where TException : Exception
        => context.Assure(value => value.Length != 0, AssureArrayEmptyErrorMessage);

    /// <summary>
    /// Asserts that the specified array is empty.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TItem">The type of items in the array.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The array to be checked.</param>
    /// <param name="name">The optional name of the array (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the array.</returns>
    /// <exception><cref>TException</cref> thrown if the array is not empty.</exception>
    public static IAssuranceContext<TException, TItem[]> Empty<TException, TItem>(
        this IAssuranceContext<TException> context, TItem[] value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).Empty();

    /// <summary>
    /// Asserts that the specified collection is not empty.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TItem">The type of items in the collection.</typeparam>
    /// <param name="context">The assurance context for the collection.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the collection is empty.</exception>
    public static IAssuranceContext<TException, IReadOnlyCollection<TItem>> NotEmpty<TException, TItem>(
        this IAssuranceContext<TException, IReadOnlyCollection<TItem>> context)
        where TException : Exception
        => context.Assure(value => value.Count == 0, AssureCollectionNotEmptyErrorMessage);

    /// <summary>
    /// Asserts that the specified collection is not empty.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TItem">The type of items in the collection.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The collection to be checked.</param>
    /// <param name="name">The optional name of the collection (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the collection.</returns>
    /// <exception><cref>TException</cref> thrown if the collection is empty.</exception>
    public static IAssuranceContext<TException, IReadOnlyCollection<TItem>> NotEmpty<TException, TItem>(
        this IAssuranceContext<TException> context,
        IReadOnlyCollection<TItem> value, [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).NotEmpty();

    /// <summary>
    /// Asserts that the specified collection is empty.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TItem">The type of items in the collection.</typeparam>
    /// <param name="context">The assurance context for the collection.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the collection is not empty.</exception>
    public static IAssuranceContext<TException, IReadOnlyCollection<TItem>> Empty<TException, TItem>(
        this IAssuranceContext<TException, IReadOnlyCollection<TItem>> context)
        where TException : Exception
        => context.Assure(value => value.Count != 0, AssureCollectionEmptyErrorMessage);

    /// <summary>
    /// Asserts that the specified collection is empty.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <typeparam name="TItem">The type of items in the collection.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The collection to be checked.</param>
    /// <param name="name">The optional name of the collection (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the collection.</returns>
    /// <exception><cref>TException</cref> thrown if the collection is not empty.</exception>
    public static IAssuranceContext<TException, IReadOnlyCollection<TItem>> Empty<TException, TItem>(
        this IAssuranceContext<TException> context,
        IReadOnlyCollection<TItem> value, [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).Empty();
}
