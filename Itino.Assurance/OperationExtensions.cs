using System;

namespace Itino.Assurance;

/// <summary>
/// Provides extension methods for asserting that certain operations are not reached.
/// </summary>
public static class OperationExtensions
{
    internal const string AssureNotReachedErrorMessage = "The statement is not supposed to be reached.";

    /// <summary>
    /// Asserts that the statement is not supposed to be reached.
    /// </summary>
    /// <param name="context">The assurance context for the operation.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown to indicate that the statement is reached.</exception>
    public static IAssuranceContext<InvalidOperationException> NotReached(
        this IAssuranceContext<InvalidOperationException> context)
        => context.Assure(true, AssureNotReachedErrorMessage);
}
