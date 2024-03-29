using System;
using System.Diagnostics.CodeAnalysis;

namespace Itino.Assurance;

/// <summary>
/// Represents the default exception thrown when a validation violation condition is met.
/// </summary>
[ExcludeFromCodeCoverage]
public sealed class AssuranceException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssuranceException"/> class.
    /// </summary>
    public AssuranceException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssuranceException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public AssuranceException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AssuranceException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public AssuranceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
