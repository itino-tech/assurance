using Itino.Assurance.Tests.TestUtilities;
using System;
using Xunit;

namespace Itino.Assurance.Tests;

public sealed class AssuranceContextTests
{
    private const string TestErrorMessage = "Test error message";

    private const string TestValue = nameof(TestValue);

    private static readonly string EmptyTestValue = string.Empty;

    [Fact]
    public void ArgumentExceptionTest() => ThrowsTest
        .Expect<ArgumentException>(TextExtensions.AssureNotWhiteSpaceErrorMessage(nameof(EmptyTestValue)))
        .Run(() => Assure.Argument.NotWhiteSpace(EmptyTestValue));

    [Fact]
    public void ArgumentExceptionNoVariableTest() => ThrowsTest
        .Expect<ArgumentException>(TextExtensions.AssureNotWhiteSpaceErrorMessage("string.Empty"))
        .Run(() => Assure.Argument.NotWhiteSpace(string.Empty));

    [Fact]
    public void ArgumentExceptionNoNameTest() => ThrowsTest
        .Expect<ArgumentException>(TextExtensions.AssureNotWhiteSpaceErrorMessage(null))
        .Run(() => Assure.Argument.WithValue(string.Empty, null).NotWhiteSpace());

    [Fact]
    public void ExceptionConversionTest() => ThrowsTest
        .Expect<InvalidOperationException>(TextExtensions.AssureNotWhiteSpaceErrorMessage(nameof(EmptyTestValue)))
        .Run(() => Assure.Argument.WithValue(TestValue).WithException(m => new InvalidOperationException(m)).NotWhiteSpace(EmptyTestValue));

    [Fact]
    public void BasicContextAssureTest() => 
        Assure.That.Assure(false, TestErrorMessage);

    [Fact]
    public void BasicContextAssureThrowsTest() => ThrowsTest
        .Expect<CustomTestException>(TestErrorMessage)
        .Run(() => Assure.WithException(m => new CustomTestException(m)).Assure(true, TestErrorMessage));

    [Fact]
    public void ContextAssureTest() => Assure
        .Operation
        .WithValue(TestValue)
        .Assure(string.IsNullOrWhiteSpace, (_, _) => TestErrorMessage);

    [Fact]
    public void ContextAssureThrowsTest() => ThrowsTest
        .Expect(TestErrorMessage)
        .Run(() => Assure.That.WithValue(EmptyTestValue).Assure(string.IsNullOrWhiteSpace, (_, _) => TestErrorMessage));

    [Fact]
    public void ContextNoValueAssureTest() => Assure
        .Operation
        .WithValue(TestValue)
        .Assure(string.IsNullOrWhiteSpace, _ => TestErrorMessage);

    [Fact]
    public void ContextNoValueAssureThrowsTest() => ThrowsTest
        .Expect<ArgumentException>(TestErrorMessage)
        .Run(() => Assure.Argument.WithValue(EmptyTestValue).Assure(string.IsNullOrWhiteSpace, _ => TestErrorMessage));
}
