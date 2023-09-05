using SmallFIMTool.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallFIMTool.Options
{
    public class ConsoleOptionsParser
    {
        private const string _Usage = @"

";
        public readonly List<ConsoleOptionKey> PossibleConsoleOptions = new List<ConsoleOptionKey>()
        {
            new ConsoleOptionKey() { 
                LongAttribute = "input", 
                ShortAttribute = 'i', 
                RequiredType = "string", 
                MappedConsoleOption = "SourceDirectoryPath", 
                StacksTogether = false 
            },
            new ConsoleOptionKey() { 
                LongAttribute = "output", 
                ShortAttribute = 'o', 
                RequiredType = "string", 
                MappedConsoleOption = "DestinationDirectoryPath",
                StacksTogether = false 
            },
            new ConsoleOptionKey() { 
                LongAttribute = "compare", 
                ShortAttribute = 'c', 
                RequiredType = "bool",
                MappedConsoleOption = "Compare",
                StacksTogether = true 
            },
            new ConsoleOptionKey() { 
                LongAttribute = "hash", 
                ShortAttribute = 'h', 
                RequiredType = "string",
                MappedConsoleOption = "Hash",
                StacksTogether = false,
                PossibleValues = "sha256;sha512"
            },
            new ConsoleOptionKey() { 
                LongAttribute = "recursive", 
                ShortAttribute = 'r', 
                RequiredType = "bool", 
                MappedConsoleOption = "TraverseRecursively",
                StacksTogether = true 
            },
            new ConsoleOptionKey() { 
                LongAttribute = "skiplargefiles", 
                ShortAttribute = 's', 
                RequiredType = "int", 
                MappedConsoleOption = "SkipLargeFilesMB",
                StacksTogether = false
            }
        };

        public ConsoleOptionsParser(string[] arguments)
        {
            ConsoleOptions options = new ConsoleOptions();
            options.SkipLargeFilesMB = -1;

            try
            {
                for (int i = 0; i < arguments.Length; i++)
                {
                    var argument = arguments[i];
                    var nextArgument = (i + 1 < arguments.Length ? arguments[i + 1] : null);

                    if (argument.StartsWith("-"))
                    {
                        argument = argument.Trim('-');

                        for (int j = 0; j < argument.Length; j++)
                        {
                            var letter = argument[j];

                            MapShortOption(options, letter, argument, nextArgument);
                        }
                    }
                    else if (argument.StartsWith("--"))
                    {
                        argument = argument.Trim('-');

                        MapLongOption(options, argument, nextArgument);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Incorrect usage ...");
            }
        }

        public void MapLongOption(ConsoleOptions options, string longAttribute, string nextArgument)
        {
            var option = PossibleConsoleOptions.SingleOrDefault(o => o.LongAttribute == longAttribute);

            MapOption(options, option, nextArgument);
        }

        private void MapShortOption(ConsoleOptions options, char letter, string argument, string nextArgument)
        {
            var option = PossibleConsoleOptions.SingleOrDefault(o => o.ShortAttribute == letter);

            if (option == null || (!option.StacksTogether && argument.Length > 0))
            {
                throw new IncorrectUsageException();
            }

            MapOption(options, option, nextArgument);
        }

        private void MapOption(ConsoleOptions options, ConsoleOptionKey? option, string nextArgument)
        {
            if (option == null)
            {
                throw new Exception();
            }

            var propertyInfo = options.GetType().GetProperty(option.MappedConsoleOption);

            if (propertyInfo == null)
            {
                throw new Exception();
            }

            if (!propertyInfo.CanWrite)
            {
                throw new Exception();
            }

            switch (option.RequiredType.ToLower())
            {
                case "string":
                    if (!string.IsNullOrWhiteSpace(option.PossibleValues))
                    {
                        var possibleValues = option.PossibleValues.Split(';').ToList();

                        if (!possibleValues.Contains(nextArgument))
                        {
                            throw new IncorrectUsageException(String.Format("Invalid value for \"-{0}\" or \"--{1}\"!",
                                option.ShortAttribute, option.LongAttribute));
                        }
                    }

                    propertyInfo.SetValue(options, nextArgument.Trim('"'));

                    break;
                case "int":
                    propertyInfo.SetValue(options, Int32.Parse(nextArgument));

                    break;
                case "bool":
                    propertyInfo.SetValue(options, true);

                    break;
                default:
                    break;
            }
        }
    }
}
