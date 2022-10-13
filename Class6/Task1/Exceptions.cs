using Microsoft.Extensions.Logging;

namespace Task1;

internal class UnsupportedOperation : Exception
{
    public UnsupportedOperation(char op) : base($"Operation '{op}' is not supported")
    {
        Console.WriteLine($"Operation '{op}' is not supported");
        Task1.Logger.LogInformation("Stopped program due to UnsupportedOperation error");
        //Environment.Exit(0);
    }
}

internal class NotEnoughNumbers : Exception
{
    public NotEnoughNumbers(int available, string line) : base($"More than {available} numbers required in ${line}")
    {
        Console.WriteLine($"More than {available} numbers required in ${line}");
        Task1.Logger.LogInformation("Stopped program due to NotEnoughNumbers error");
        //Environment.Exit(0);
    }
}

internal class NotEnoughOperations : Exception
{
    public NotEnoughOperations(int available, string line) : base(
        $"More than {available} operations required in ${line}")
    {
        Console.WriteLine($"More than {available} operations required in ${line}");
        Task1.Logger.LogInformation("Stopped program due to NotEnoughOperations error");
        //Environment.Exit(0);
    }
}

internal class WrongArgsFormat : Exception
{
    public WrongArgsFormat(string args) : base($"Program args are incorrect: {args}")
    {
        Console.WriteLine($"Program args are incorrect: {args}");
        Task1.Logger.LogInformation("Stopped program due to WrongArgsFormat error");
        //Environment.Exit(0);
    }
}

internal class DivByZero : Exception
{
    public DivByZero() : base($"Division by zero catched")
    {
        Console.WriteLine($"Division by zero catched");
        Task1.Logger.LogInformation("Stopped program due to DivByZero error");
        //Environment.Exit(0);
    }
}

internal class WrongNumbersInput : Exception
{
    public WrongNumbersInput(string numbers) : base($"Failed to parse [{numbers}] to ints")
    {
        Console.WriteLine($"Failed to parse [{numbers}] to ints");
        Task1.Logger.LogInformation("Stopped program due to WrongNumbersInput error");
        //Environment.Exit(0);
    }
}

internal class FileDoesNotExist : Exception
{
    public FileDoesNotExist(string filePath) : base($"No such file: {filePath}")
    {
        Console.WriteLine($"No such file: {filePath}");
        Task1.Logger.LogInformation("Stopped program due to FileDoesNotExist error");
        //Environment.Exit(0);
    }
}

internal class FilesOfDifferentSize : Exception
{
    public FilesOfDifferentSize() : base("Input files contain different amount of lines")
    {
        Console.WriteLine("Input files contain different amount of lines");
        Task1.Logger.LogInformation("Stopped program due to FilesOfDifferentSize error");
        //Environment.Exit(0);
    }
}

internal class FileAlreadyExists : Exception
{
    public FileAlreadyExists(string filePath) : base($"File {filePath} already exists")
    {
        Console.WriteLine($"File {filePath} already exists");
        Task1.Logger.LogInformation("Stopped program due to FileAlreadyExists error");
        //Environment.Exit(0);
    }
}

internal class FailedToCreateFile : Exception
{
    public FailedToCreateFile(string filePath) : base($"Failed to create file: {filePath}")
    {
        Console.WriteLine($"Failed to create file: {filePath}");
        Task1.Logger.LogInformation("Stopped program due to FailedToCreateFile error");
        //Environment.Exit(0);
    }
}