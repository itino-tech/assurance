using Itino.Assurance.Auxiliary;
using System;
using System.Runtime.CompilerServices;

namespace Itino.Assurance;

/// <summary>
/// Provides extension methods for enhancing the functionality of <see cref="IAssuranceContext{TException}"/>.
/// </summary>
public static class AssuranceContextExtensions
{
    /// <summary>
    /// Assures a condition and throws an exception if the condition is not met.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown.</typeparam>
    /// <param name="context">The assurance context.</param>
    /// <param name="assuranceFailedCondition">The condition that, if true, triggers the exception.</param>
    /// <param name="message">The message to be included in the exception if the condition is not met.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the assurance condition is not met.</exception>
    public static IAssuranceContext<TException> Assure<TException>(
        this IAssuranceContext<TException> context,
        bool assuranceFailedCondition,
        string message)
        where TException : Exception
    {
        if (assuranceFailedCondition)
        {
            throw context.ExceptionFactory(message);
        }

        return context;
    }

    /// <summary>
    /// Creates a new assurance context with a specified value and an optional name. 
    /// </summary>
    /// <typeparam name="TException">The type of exception associated with the assurance context.</typeparam>
    /// <typeparam name="TValue">The type of value to be associated with the assurance context.</typeparam>
    /// <param name="context">The original assurance context.</param>
    /// <param name="value">The value to be associated with the new assurance context.
    /// For the recent .Net framework versions, the name is automatically populated.
    /// </param>
    /// <param name="name">The optional name of the value used for the error message enhancement.</param>
    /// <returns>A new assurance context with the specified value.</returns>
    public static IAssuranceContext<TException, TValue> WithValue<TException, TValue>(
        this IAssuranceContext<TException> context,
        TValue value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => new AssuranceContext<TException, TValue>(context.ExceptionFactory, value, name);

    /// <summary>
    /// Creates a new assurance context with a different exception type and the same value and name.
    /// </summary>
    /// <typeparam name="TException">The original type of exception associated with the assurance context.</typeparam>
    /// <typeparam name="TValue">The type of value associated with the assurance context.</typeparam>
    /// <typeparam name="TExceptionTo">The new type of exception to be associated with the assurance context.</typeparam>
    /// <param name="context">The original assurance context.</param>
    /// <param name="exceptionFactory">A function to create an exception of the new type.</param>
    /// <returns>A new assurance context with a different exception type.</returns>
    public static IAssuranceContext<TExceptionTo, TValue> WithException<TException, TValue, TExceptionTo>(
        this IAssuranceContext<TException, TValue> context,
        Func<string, TExceptionTo> exceptionFactory)
        where TException : Exception
        where TExceptionTo : Exception
        => new AssuranceContext<TExceptionTo, TValue>(exceptionFactory, context.Value, context.Name);

    /// <summary>
    /// Assures a condition based on the current value and throws an exception if the condition is not met.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown.</typeparam>
    /// <typeparam name="TValue">The type of value associated with the assurance context.</typeparam>
    /// <param name="context">The assurance context.</param>
    /// <param name="assuranceFailedCondition">The condition that, if true, triggers the exception.</param>
    /// <param name="messageFactory">A function to create the message for the exception.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the assurance condition is not met.</exception>
    public static IAssuranceContext<TException, TValue> Assure<TException, TValue>(
        this IAssuranceContext<TException, TValue> context,
        Predicate<TValue> assuranceFailedCondition,
        Func<TValue, string?, string> messageFactory)
        where TException : Exception
    {
        if (assuranceFailedCondition(context.Value))
        {
            throw context.ExceptionFactory(messageFactory(context.Value, context.Name));
        }

        return context;
    }

    /// <summary>
    /// Assures a condition based on the current value and throws an exception if the condition is not met.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown.</typeparam>
    /// <typeparam name="TValue">The type of value associated with the assurance context.</typeparam>
    /// <param name="context">The assurance context.</param>
    /// <param name="assuranceFailedCondition">The condition that, if true, triggers the exception.</param>
    /// <param name="messageFactory">A function to create the message for the exception.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the assurance condition is not met.</exception>
    public static IAssuranceContext<TException, TValue> Assure<TException, TValue>(
        this IAssuranceContext<TException, TValue> context,
        Predicate<TValue> assuranceFailedCondition,
        Func<string?, string> messageFactory)
        where TException : Exception
        => context.Assure(assuranceFailedCondition, (_, name) => messageFactory(name));
}
