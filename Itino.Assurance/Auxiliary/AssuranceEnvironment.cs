namespace Itino.Assurance.Auxiliary;

internal static class AssuranceEnvironment
{
    public static IStaticOperations Operations { get; set; } = new ProductionStaticOperations();
}
