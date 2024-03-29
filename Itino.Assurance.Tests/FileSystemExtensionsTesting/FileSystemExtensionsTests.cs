using Itino.Assurance.Tests.TestUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Itino.Assurance.Tests.FileSystemExtensionsTesting;

public sealed class FileSystemExtensionsTests
{
    public static readonly IReadOnlyCollection<object[]> ExistingPaths = FileSystemStaticData.ExistingPaths;

    public static readonly IReadOnlyCollection<object[]> NonExistingPaths = FileSystemStaticData.NonExistingPaths;

    public FileSystemExtensionsTests()
    {
        FileSystemStaticData.Initialize();
    }

    [Theory]
    [MemberData(nameof(ExistingPaths))]
    public void FileExistsTest(string path) =>
        Assure.That.FileExists(path);

    [Theory]
    [MemberData(nameof(NonExistingPaths))]
    public void FileExistsThrowsTest(string path) => ThrowsTest
        .Expect<FileNotFoundException>(FileSystemExtensions.AssureFileExistsErrorMessage(path, nameof(path)))
        .Run(() => Assure.That.FileExists(path));

    [Theory]
    [MemberData(nameof(NonExistingPaths))]
    public void FileDoesNotExistTest(string path) =>
        Assure.That.FileDoesNotExist(path);

    [Theory]
    [MemberData(nameof(ExistingPaths))]
    public void FileDoesNotExistThrowsTest(string path) => ThrowsTest
        .Expect(FileSystemExtensions.AssureFileDoesNotExistErrorMessage(path, nameof(path)))
        .Run(() => Assure.That.FileDoesNotExist(path));

    [Theory]
    [MemberData(nameof(ExistingPaths))]
    public void DirectoryExistsTest(string path) =>
        Assure.That.DirectoryExists(path);

    [Theory]
    [MemberData(nameof(NonExistingPaths))]
    public void DirectoryExistsThrowsTest(string path) => ThrowsTest
        .Expect<DirectoryNotFoundException>(FileSystemExtensions.AssureDirectoryExistsErrorMessage(path, nameof(path)))
        .Run(() => Assure.That.DirectoryExists(path));

    [Theory]
    [MemberData(nameof(NonExistingPaths))]
    public void DirectoryDoesNotExistTest(string path) =>
        Assure.That.DirectoryDoesNotExist(path);

    [Theory]
    [MemberData(nameof(ExistingPaths))]
    public void DirectoryDoesNotExistThrowsTest(string path) => ThrowsTest
        .Expect(FileSystemExtensions.AssureDirectoryDoesNotExistErrorMessage(path, nameof(path)))
        .Run(() => Assure.That.DirectoryDoesNotExist(path));

    [Theory]
    [MemberData(nameof(ExistingPaths))]
    public void ChainedAssuranceTest(string path) =>
        Assure.Argument.NotWhiteSpace(path).DirectoryExists();

    [Theory]
    [MemberData(nameof(NonExistingPaths))]
    public void ChaineEndAssuranceThrowsTest(string path) => ThrowsTest
        .Expect<FileNotFoundException>(FileSystemExtensions.AssureFileExistsErrorMessage(path, nameof(path)))
        .Run(() => Assure.Is.NotWhiteSpace(path).FileExists());

    [Fact]
    public void ChaineStartAssuranceThrowsTest() => ThrowsTest
        .Expect<InvalidOperationException>(TextExtensions.AssureNotWhiteSpaceErrorMessage(null))
        .Run(() => Assure.Operation.NotWhiteSpace(string.Empty, null).FileExists());
}
