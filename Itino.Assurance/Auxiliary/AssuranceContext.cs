using System;

namespace Itino.Assurance.Auxiliary;

internal sealed class AssuranceContext<TException>(Func<string, TException> exceptionFactory)
    : IAssuranceContext<TException>
    where TException : Exception
{
    public Func<string, TException> ExceptionFactory => exceptionFactory;
}

internal sealed class AssuranceContext<TException, TValue>(
    Func<string, TException> exceptionFactory,
    TValue value,
    string? name)
    : IAssuranceContext<TException, TValue>
    where TException : Exception
{
    public Func<string, TException> ExceptionFactory => exceptionFactory;

    public TValue Value => value;

    public string? Name => name;
}
