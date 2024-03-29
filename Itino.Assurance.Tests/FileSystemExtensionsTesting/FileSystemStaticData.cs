using Itino.Assurance.Auxiliary;
using System.Collections.Generic;
using System.Linq;

namespace Itino.Assurance.Tests.FileSystemExtensionsTesting;

internal static class FileSystemStaticData
{
    private static readonly IReadOnlyDictionary<string, bool> PathsData;

    public static readonly IReadOnlyCollection<object[]> ExistingPaths;

    public static readonly IReadOnlyCollection<object[]> NonExistingPaths;

    static FileSystemStaticData()
    {
        PathsData = new Dictionary<string, bool>()
        {
            { @"C:\test\existingPath", true },
            { @"C:\test\existingPath.ext", true },
            { @"C:\test\nonExistingPath", false },
            { @"C:\test\nonExistingPath.ext", false }
        };

        ExistingPaths = PathsData.Where(p => p.Value).Select(p => new object[] { p.Key }).ToList();

        NonExistingPaths = PathsData.Where(p => !p.Value).Select(p => new object[] { p.Key }).ToList();

        AssuranceEnvironment.Operations = new TestStaticOperations();
    }

    public static void Initialize()
    {
        // To make sure the static constructor is executed.
    }

    private sealed class TestStaticOperations : IStaticOperations
    {
        public bool DirectoryExists(string path) => PathsData[path];

        public bool FileExists(string path) => PathsData[path];
    }
}
