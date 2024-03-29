using Itino.Assurance.Tests.TestUtilities;
using Xunit;

namespace Itino.Assurance.Tests;

public sealed class TextExtensionsTests
{
    [Theory]
    [InlineData("_")]
    [InlineData("a    ")]
    [InlineData("  o  ")]
    [InlineData("    z")]
    public void NotWhiteSpaceTest(string text) => 
        Assure.Is.NotWhiteSpace(text);

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(" \t ")]
    public void NotWhiteSpaceThrowsTest(string text) => ThrowsTest
        .Expect(TextExtensions.AssureNotWhiteSpaceErrorMessage(nameof(text)))
        .Run(() => Assure.Is.NotWhiteSpace(text));
}