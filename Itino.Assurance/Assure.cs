using Itino.Assurance.Auxiliary;
using System;

namespace Itino.Assurance;

/// <summary>
/// Provides a set of static methods to create and configure instances of <see cref="IAssuranceContext{TException}"/>.
/// </summary>
public static class Assure
{
    /// <summary>
    /// Gets an assurance context for <see cref="AssuranceException"/>.
    /// An alias for <see cref="Is"/>
    /// </summary>
    public static IAssuranceContext<AssuranceException> That => Is;

    /// <summary>
    /// Gets an assurance context for <see cref="AssuranceException"/>.
    /// </summary>
    public static IAssuranceContext<AssuranceException> Is => WithException(message => new AssuranceException(message));

    /// <summary>
    /// Gets an assurance context for <see href="https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception">ArgumentException</see>.
    /// </summary>
    public static IAssuranceContext<ArgumentException> Argument => WithException(message => new ArgumentException(message));

    /// <summary>
    /// Gets an assurance context for <see href="https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception">InvalidOperationException</see>.
    /// </summary>
    public static IAssuranceContext<InvalidOperationException> Operation
        => WithException(message => new InvalidOperationException(message));

    /// <summary>
    /// Creates an assurance context with a specified exception factory.
    /// </summary>
    /// <typeparam name="TException">The type of exception associated with the assurance context.</typeparam>
    /// <param name="exceptionFactory">A function to create an exception of the specified type.</param>
    /// <returns>An assurance context with the specified exception factory.</returns>
    public static IAssuranceContext<TException> WithException<TException>(Func<string, TException> exceptionFactory)
        where TException : Exception
        => new AssuranceContext<TException>(exceptionFactory);
}
