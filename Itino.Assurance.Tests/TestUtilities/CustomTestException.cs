using System;

namespace Itino.Assurance.Tests.TestUtilities;

internal sealed class CustomTestException(string message) : Exception(message)
{
}
