using Microsoft.Extensions.Logging;

namespace Task1
{
    class UnsupportedOperation : Exception
    {
        public UnsupportedOperation(char op) : base($"Operation '{op}' is unsupported")
        {
        }
    }

    class NotEnoughNumbers : Exception
    {
        public NotEnoughNumbers(int available, string line) : base($"More than {available} numbers required in ${line}")
        {
        }
    }

    class NotEnoughOperations : Exception
    {
        public NotEnoughOperations(int available, string line) : base($"More than {available} operations required in ${line}")
        {
        }
    }

    class WrongArgsFormat : Exception
    {
        public WrongArgsFormat(string args) : base($"Porgram args are incorrect: {args}")
        {
        }
    }

    class DivByZero : Exception
    {
        public DivByZero() : base($"Division by zero cathed")
        {
        }
    }

    class WrongNumbersInput : Exception
    {
        public WrongNumbersInput(string numbers) : base($"Failed to parse [{numbers}] to ints")
        {
        }
    }

    class FileDoesNotExist : Exception
    {
        public FileDoesNotExist(string filePath) : base($"No such fille: {filePath}")
        {
        }
    }

    class FilesOfDifferentSize : Exception
    {
        public FilesOfDifferentSize()
        {
        }
    }

    class FileAlreadyExists : Exception
    {
        public FileAlreadyExists(string filePath) : base($"File {filePath} already exists")
        {
        }
    }

    class FailedToCreateFile : Exception
    {
        public FailedToCreateFile(string filePath) : base($"Failed to create file: {filePath}")
        {
        }
    }

    public class Task1
    {
        private static readonly ILogger<Task1> Logger =
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
                    };
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

            for (int i = 0; i < schema.Count(); i++)
                outputString += $"{numbers[i]}{schema[i + 1]}";

            return outputString;
        }

        internal static string ProcessString(string schema, string input)
        {
            var transformation = ApplySchema(schema);
            var numbers = input.Split(",");

            if (numbers.Count() < schema.Count() + 1)
                throw new NotEnoughNumbers(numbers.Count(), input);

            if (numbers.Count() > schema.Count() + 1)
                throw new NotEnoughOperations(schema.Count(), schema);

            if (numbers.Select(int.Parse).Count() != numbers.Count())
                throw new WrongNumbersInput(string.Join(" ", numbers));

            var result = transformation(numbers.Select(int.Parse).ToList());
            return FormatLhs(schema, numbers) + $"={result}";
        }

        internal static void ProcessFiles(string schemasFile, string dataFile, string outputFile)
        {
            if (!File.Exists(@schemasFile))
                throw new FileDoesNotExist(schemasFile);

            if (!File.Exists(@dataFile))
                throw new FileDoesNotExist(dataFile);

            var schemas = File.ReadAllLines(@schemasFile);
            var dataLines = File.ReadAllLines(@dataFile);

            if (schemas.Count() != dataLines.Count())
                throw new FilesOfDifferentSize();

            if (File.Exists(@outputFile))
                throw new FileAlreadyExists(outputFile);

            var outut = new List<string>();

            for (int i = 0; i < schemas.Count(); i++)
                outut.Add(ProcessString(schemas[i], dataLines[i]));

            try
            {
                var streamWriter = new StreamWriter(@outputFile);
                foreach (var line in outut)
                    streamWriter.WriteLine(line);
                streamWriter.Close();
            }
            catch (System.Exception)
            {
                throw new FailedToCreateFile(outputFile);
            }
        }

        internal static List<string> ParseInput(string[] args)
        {
            if (args.Count() != 3)
                throw new WrongArgsFormat(string.Join(" ", args));

            return new List<string> { args[0], args[1], args[2] };
        }

        public static void Main(string[] args)
        {
            Logger.LogInformation("program started");
            var parsedInput = ParseInput(args);
            ProcessFiles(parsedInput[0], parsedInput[1], parsedInput[2]);
            Logger.LogInformation("program completed");
        }
    }
}