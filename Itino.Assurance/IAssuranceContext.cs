using System;

namespace Itino.Assurance;

/// <summary>
/// Represents the base interface for an assurance context that defines the exception factory.
/// </summary>
/// <typeparam name="TException">The type of exception associated with the assurance context.</typeparam>
public interface IAssuranceContext<TException> where TException : Exception
{
    /// <summary>
    /// Gets the function responsible for creating an exception with a specified message.
    /// </summary>
    Func<string, TException> ExceptionFactory { get; }
}

/// <summary>
/// Represents an assurance context that includes a value and an optional name associated with the value.
/// </summary>
/// <typeparam name="TException">The type of exception associated with the assurance context.</typeparam>
/// <typeparam name="TValue">The type of the value held by the assurance context.</typeparam>
public interface IAssuranceContext<TException, TValue> : IAssuranceContext<TException>
    where TException : Exception
{
    /// <summary>
    /// Gets the value associated with the assurance context.
    /// </summary>
    TValue Value { get; }

    /// <summary>
    /// Gets the optional name associated with the value (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.
    /// </summary>
    string? Name { get; }
}
