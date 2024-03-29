using Itino.Assurance.Tests.TestUtilities;
using Xunit;

namespace Itino.Assurance.Tests.CustomExtensionsTesting;

public sealed class CustomExtensionsTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("\t", "\t")]
    [InlineData("abc", "abc")]
    public void CustomExtensionPassesTest(string text, string textToCompare) =>
        Assure.Is.EqualToSpecificText(text, textToCompare);

    [Theory]
    [InlineData("", "\t")]
    [InlineData("abc", "def")]
    public void CustomExtensionThrowsTest(string text, string textToCompare) => ThrowsTest
        .Expect(CustomExtensions.SpecificTextErrorMessage(text, nameof(text), textToCompare))
        .Run(() => Assure.That.EqualToSpecificText(text, textToCompare));
}
