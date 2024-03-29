using System;
using System.IO;
using System.Runtime.CompilerServices;
using Itino.Assurance.Auxiliary;

namespace Itino.Assurance;

/// <summary>
/// Provides extension methods for asserting conditions on file system entities.
/// </summary>
public static class FileSystemExtensions
{
    internal static readonly Func<string, string?, string> AssureFileExistsErrorMessage =
        (path, name) => $"The{Utils.GetName(name)}file not found: '{path}'.";

    internal static readonly Func<string, string?, string> AssureFileDoesNotExistErrorMessage =
        (path, name) => $"The{Utils.GetName(name)}file exists: '{path}'.";

    internal static readonly Func<string, string?, string> AssureDirectoryExistsErrorMessage =
        (path, name) => $"The{Utils.GetName(name)}directory not found: '{path}'.";

    internal static readonly Func<string, string?, string> AssureDirectoryDoesNotExistErrorMessage =
        (path, name) => $"The{Utils.GetName(name)}directory exists: '{path}'.";

    /// <summary>
    /// Asserts that the specified file exists.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context for the file path.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the file does not exist.</exception>
    public static IAssuranceContext<TException, string> FileExists<TException>(
        this IAssuranceContext<TException, string> context)
        where TException : Exception
    {
        context
            .WithException(message => new FileNotFoundException(message))
            .Assure(value => !AssuranceEnvironment.Operations.FileExists(value), AssureFileExistsErrorMessage);

        return context;
    }

    /// <summary>
    /// Asserts that the specified file exists.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The file path to be checked.</param>
    /// <param name="name">The optional name of the file path (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the file path.</returns>
    /// <exception><cref>TException</cref> thrown if the file does not exist.</exception>
    public static IAssuranceContext<TException, string> FileExists<TException>(
        this IAssuranceContext<TException> context,
        string value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).FileExists();

    /// <summary>
    /// Asserts that the specified file does not exist.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context for the file path.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the file exists.</exception>
    public static IAssuranceContext<TException, string> FileDoesNotExist<TException>(
        this IAssuranceContext<TException, string> context)
        where TException : Exception
        => context.Assure(AssuranceEnvironment.Operations.FileExists, AssureFileDoesNotExistErrorMessage);

    /// <summary>
    /// Asserts that the specified file does not exist.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The file path to be checked.</param>
    /// <param name="name">The optional name of the file path (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the file path.</returns>
    /// <exception><cref>TException</cref> thrown if the file exists.</exception>
    public static IAssuranceContext<TException, string> FileDoesNotExist<TException>(
        this IAssuranceContext<TException> context,
        string value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).FileDoesNotExist();

    /// <summary>
    /// Asserts that the specified directory exists.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context for the directory path.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the directory does not exist.</exception>
    public static IAssuranceContext<TException, string> DirectoryExists<TException>(
        this IAssuranceContext<TException, string> context)
        where TException : Exception
    {
        context
            .WithException(message => new DirectoryNotFoundException(message))
            .Assure(value => !AssuranceEnvironment.Operations.DirectoryExists(value), AssureDirectoryExistsErrorMessage);

        return context;
    }

    /// <summary>
    /// Asserts that the specified directory exists.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The directory path to be checked.</param>
    /// <param name="name">The optional name of the directory path (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the directory path.</returns>
    /// <exception><cref>TException</cref> thrown if the directory does not exist.</exception>
    public static IAssuranceContext<TException, string> DirectoryExists<TException>(
        this IAssuranceContext<TException> context,
        string value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).DirectoryExists();

    /// <summary>
    /// Asserts that the specified directory does not exist.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context for the directory path.</param>
    /// <returns>The original assurance context.</returns>
    /// <exception><cref>TException</cref> thrown if the directory exists.</exception>
    public static IAssuranceContext<TException, string> DirectoryDoesNotExist<TException>(
        this IAssuranceContext<TException, string> context)
        where TException : Exception
        => context.Assure(AssuranceEnvironment.Operations.DirectoryExists, AssureDirectoryDoesNotExistErrorMessage);

    /// <summary>
    /// Asserts that the specified directory does not exist.
    /// </summary>
    /// <typeparam name="TException">The type of exception to be thrown if the condition is not met.</typeparam>
    /// <param name="context">The assurance context without a value.</param>
    /// <param name="value">The directory path to be checked.</param>
    /// <param name="name">The optional name of the directory path (used for the error message enhancement). The name is auto-populated when recent .Net frameworks are used.</param>
    /// <returns>The assurance context for the directory path.</returns>
    /// <exception><cref>TException</cref> thrown if the directory exists.</exception>
    public static IAssuranceContext<TException, string> DirectoryDoesNotExist<TException>(
        this IAssuranceContext<TException> context,
        string value,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).DirectoryDoesNotExist();
}
