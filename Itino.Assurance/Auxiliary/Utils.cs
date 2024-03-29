namespace Itino.Assurance.Auxiliary;

internal static class Utils
{
    public static string GetName(string? name)
        => string.IsNullOrWhiteSpace(name) ? " " : $" '{name}' ";
}
