# Itino.Assurance Overview

A tiny validation library that simplifies arguments' and an application state's validation.

## Installation

```bash
dotnet add package Itino.Assurance --version 1.0.0
```

NuGet.org page [link](https://www.nuget.org/packages/Itino.Assurance/1.0.0).

## Usage Examples

For all argument validation failures ("Assure.Argument") the "System.ArgumentException" exception is thrown with the message that includes a variable name automatically obtained from the context. Other exception types and the "fluent" interface is supported.

### String Arguments Validation

```C#
void TextValidationExample(string textArgument)
{
    Assure.Argument.NotWhiteSpace(textArgument);
}

TextValidationExample(""); // The exception's message: "The string 'textArgument' cannot be blank.".
```

### Comparison

Multiple arguments:

```C#
void ComparableValidationExample(int positiveArgument, int negativeOrZeroArgument)
{
    Assure.Argument
        .GreaterThan(positiveArgument, 0)
        .LessThanOrEqual(negativeOrZeroArgument, 0);
}

ComparableValidationExample(-5, 4); // The exception's message: "The 'positiveArgument' value '-5' is less than '0'.".

ComparableValidationExample(2, 3); // The exception's message: "The 'negativeOrZeroArgument' value '3' is greater than '0'.".
```

Range validation:

```C#
void RangeValidationExample(int inRangeArgument)
{
    Assure.Argument
        .GreaterThanOrEqual(inRangeArgument, 3)
        .LessThan(inRangeArgument, 7);
}

RangeValidationExample(2); // The exception's message: "The 'inRangeArgument' value '2' is less than '3'.".

RangeValidationExample(9); // The exception's message: "The 'inRangeArgument' value '9' is greater than '7'.".
```

Collections/arrays validation:

```C#
void NotEmptyValidationExample(IReadOnlyCollection<int> collectionArgument)
{
    Assure.Argument.NotEmpty(collectionArgument);
}

NotEmptyValidationExampleExample([]); // The exception's message: "The collection 'collectionArgument' is empty.".
```

```C#
void EmptyValidationExample(int[] arrayArgument)
{
    Assure.Argument.Empty(arrayArgument);
}

EmptyValidationExample(new int[1]); // The exception's message: "The array 'arrayArgument' is not empty.".
```

File system validation:

```C#
void DirectoryDoesNotExistValidationExample(string directoryPathArgument)
{
    Assure.Argument.DirectoryDoesNotExist(directoryPathArgument);
}

DirectoryDoesNotExistValidationExample("C:/temp"); // If the directory exists, the code throws the "System.ArgumentException" exception with the message: "The 'directoryPathArgument' directory exists: 'C:/temp'.".
```

```C#
void FileExistsValidationExample(string filePathArgument)
{
    Assure.Argument.FileExists(filePathArgument);
}

FileExistsValidationExample("C:/non-existing/file.path"); // If the file does not exists, the code throws the "System.IO.FileNotFoundException" exception with the message: "The 'filePathArgument' file not found: 'C:/non-existing/file.path'.".
```

Application flow validation:

```C#
Assure.Operation.NotReached(); // If the code is reached, it throws the "System.InvalidOperationException" exception with the message: "The statement is not supposed to be reached.".
```

Chained validation:

```C#
void ChainedValidationExample(string directoryPath)
{
    Assure.Argument
        .NotWhiteSpace(directoryPath)
        .DirectoryExists(directoryPath);
}

ChainedValidationExample("   "); // Throws the "System.ArgumentException" exception with the message: "The string 'directoryPath' cannot be blank.".

ChainedValidationExample("C:/non-existing-directory/path"); // Throws the "System.IO.DirectoryNotFoundException" exception with the message: "The 'directoryPath' directory not found: 'C:/non-existing-directory/path'.".
```

Multiple arguments:

```C#
void ChainedMultipleArgumentsValidationExample(string argument1, int argument2, int[] argument3)
{
    Assure.Argument
        .NotWhiteSpace(argument1)
        .GreaterThanOrEqual(argument2, 0)
        .NotEmpty(argument3);
}
```

Single line assignment:

```C#
void SingleLineAssignmentExample(string textArgument, int intArgument)
{
    var textVariable = Assure.Argument.NotWhiteSpace(textArgument).Value;

    SomeMethod(Assure.Argument.GreaterThan(intArgument, 0));
}
```

Custom exceptions:

```C#
void CustomExceptionExample(int intArgument)
{
    Assure
        .WithException(message => new ArgumentOutOfRangeException(message))
        .LessThan(intArgument, 5);
}

CustomExceptionExample(8); // Throws the "System.ArgumentOutOfRangeException" with the message: "Specified argument was out of the range of valid values. (Parameter 'The 'intArgument' value '8' is greater than '5'.')".
```

## Predefined Exception Types And Keywords

```C#
Assure.Is.NotWhiteSpace(argument); // Throws "Itino.Assurance.AssuranceException".
Assure.That.NotWhiteSpace(argument); // Throws "Itino.Assurance.AssuranceException" ("That" is an alias of "Is").
Assure.Argument.NotWhiteSpace(argument); // Throws "System.ArgumentException".
Assure.Operation.NotWhiteSpace(argument); // Throws "System.InvalidOperationException".
Assure.WithException(message => new NotSupportedException(message)).NotWhiteSpace(argument); // Throws "System.NotSupportedException".
```

## Validation Functionality Custom Extensions

```C#
static class CustomValidationExtensions
{
    internal static readonly Func<string, string?, string, string> SpecificTextErrorMessage =
        (value, name, textToCompare) => $"The '{name}' value '{value}' is not equal to '{textToCompare}'.";

    static IAssuranceContext<TException, string> EqualToSpecificText<TException>(
        this IAssuranceContext<TException, string> context,
        string textToCompare)
        where TException : Exception
        => context.Assure(
            value => !value.Equals(textToCompare, StringComparison.InvariantCulture),
            (value, name) => SpecificTextErrorMessage(value, name, textToCompare));

    static IAssuranceContext<TException, string> EqualToSpecificText<TException>(
        this IAssuranceContext<TException> context,
        string value,
        string textToCompare,
        [CallerArgumentExpression(nameof(value))] string? name = null)
        where TException : Exception
        => context.WithValue(value, name).EqualToSpecificText(textToCompare);
}

// Usage

void CustomValidationExample(string textArgument)
{
    Assure.Argument.EqualToSpecificText(textArgument, "specific text to compare");
}

Assure.Is.EqualToSpecificText(text, "some text"); // Throws the "System.ArgumentException" exception with message "The 'textArgument' value 'some text' is not equal to 'specific text to compare'.".

// or

var textVariable = "some text";
Assure.Is.EqualToSpecificText(textVariable, "another text"); // Throws the "Itino.Assurance.AssuranceException" exception with the message "The 'textVariable' value 'some text' is not equal to 'another text'.".
```
