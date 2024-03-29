using Itino.Assurance.Tests.TestUtilities;
using System;
using Xunit;

namespace Itino.Assurance.Tests;

public sealed class OperationExtensionsTests
{
    [Fact]
    public void NotReachedTest() => ThrowsTest
        .Expect<InvalidOperationException>(OperationExtensions.AssureNotReachedErrorMessage)
        .Run(() => Assure.Operation.NotReached());
}
