using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Itino.Assurance.Auxiliary;

[ExcludeFromCodeCoverage]
internal sealed class ProductionStaticOperations : IStaticOperations
{
    public bool DirectoryExists(string path) => Directory.Exists(path);

    public bool FileExists(string path) => File.Exists(path);
}
