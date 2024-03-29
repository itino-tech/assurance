namespace Itino.Assurance.Auxiliary;

internal interface IStaticOperations
{
    bool FileExists(string path);

    bool DirectoryExists(string path);
}
