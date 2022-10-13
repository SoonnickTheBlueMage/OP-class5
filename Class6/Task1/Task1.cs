using Microsoft.Extensions.Logging;
namespace Task1;

public class Task1
{
    public static readonly ILogger<Task1> Logger =
        LoggerFactory.Create(builder => { builder.AddSimpleConsole(); }).CreateLogger<Task1>();

    internal static int ApplyOperation(char op, int arg1, int arg2)
    {
        switch (op)
        {
            case '*': return arg1 * arg2;
            case '/':
            {
                if (arg2 == 0)
                    throw new DivByZero();

                return arg1 / arg2;
            }
            default: throw new UnsupportedOperation(op);
        }
    }

    private static Func<List<int>, int> ApplySchema(string schema)
    {
        var ops = schema.ToCharArray();
        return args =>
        {
            var res = args[0];
            var i = 1;

            foreach (var op in ops)
                res = ApplyOperation(op, res, args[i++]);

            return res;
        };
    }

    private static string FormatLhs(string schema, string[] numbers)
    {
        var outputString = $"{schema[0]}";

        for (var i = 0; i < schema.Count(); i++)
            outputString += $"{numbers[i]}{schema[i + 1]}";

        return outputString;
    }

    private static string ProcessString(string schema, string input)
    {
        var transformation = ApplySchema(schema);
        var numbers = input.Split(",");

        if (numbers.Length < schema.Length + 1)
            throw new NotEnoughNumbers(numbers.Count(), input);

        if (numbers.Length > schema.Length + 1)
            throw new NotEnoughOperations(schema.Length, schema);

        if (numbers.Select(int.Parse).Count() != numbers.Length)
            throw new WrongNumbersInput(string.Join(" ", numbers));

        var result = transformation(numbers.Select(int.Parse).ToList());
        return FormatLhs(schema, numbers) + $"={result}";
    }

    private static void ProcessFiles(string schemasFile, string dataFile, string outputFile)
    {
        if (!File.Exists(@schemasFile))
            throw new FileDoesNotExist(schemasFile);

        if (!File.Exists(@dataFile))
            throw new FileDoesNotExist(dataFile);

        var schemas = File.ReadAllLines(@schemasFile);
        var dataLines = File.ReadAllLines(@dataFile);

        if (schemas.Length != dataLines.Length)
            throw new FilesOfDifferentSize();

        if (File.Exists(@outputFile))
            throw new FileAlreadyExists(outputFile);

        var output = new List<string>();

        for (var i = 0; i < schemas.Length; i++)
            output.Add(ProcessString(schemas[i], dataLines[i]));

        try
        {
            var streamWriter = new StreamWriter(@outputFile);
            foreach (var line in output)
                streamWriter.WriteLine(line);
            streamWriter.Close();
        }
        catch (Exception)
        {
            throw new FailedToCreateFile(outputFile);
        }
    }

    private static List<string> ParseInput(string[] args)
    {
        if (args.Length != 3)
            throw new WrongArgsFormat(string.Join(" ", args));

        return new List<string> {args[0], args[1], args[2]};
    }

    public static void Main(string[] args)
    {
        Logger.LogInformation("program started");
        var parsedInput = ParseInput(args);
        ProcessFiles(parsedInput[0], parsedInput[1], parsedInput[2]);
        Logger.LogInformation("program completed");
    }
}