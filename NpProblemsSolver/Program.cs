using BooleanSatisfiabilityProblem;
using Exceptions;

internal class Program
{
    private static readonly List<int> _availableOptions = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
    private static readonly ThreeCnfFormula _formula = new();
    private static readonly CliqueIndependentSetGraph.CliqueIndependentSetGraph _graph = new();

    private delegate void InputValidation(string? inputValue);

    private static string GetUserInput(string inputRequestMessage, InputValidation validateInput)
    {
        while (true)
        {
            Console.Write(inputRequestMessage);
            try
            {
                string input = Console.ReadLine() ?? throw new InvalidInputException();
                validateInput(input);
                return input;
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input! Try again.");
            }
        }
    }

    #region SubsetSizeValueInput
    private static void ValidateSubsetSizeInput(string? inputValue)
    {
        try
        {
            string choice = inputValue ?? throw new InvalidInputException();
            int.Parse(choice.Trim());
        }
        catch (Exception)
        {
            throw new InvalidInputException();
        }
    }

    private static int GetSubsetSize()
    {
        Console.WriteLine();
        while (true)
        {
            Console.Write("\nEnter subset size: ");
            try
            {
                string input = Console.ReadLine() ?? throw new InvalidInputException();
                ValidateSubsetSizeInput(input);
                return int.Parse(input);
            }
            catch (Exception)
            {
                Console.WriteLine("\nInvalid input! Try again.");
            }
        }
    }
    #endregion

    #region OptionInput
    private static void ValidateOptionInput(string? inputValue)
    {
        try
        {
            string choice = inputValue ?? throw new InvalidInputException();
            int selectedOption = int.Parse(choice.Trim());
            if (!_availableOptions.Contains(selectedOption))
                throw new InvalidInputException();
        }
        catch (Exception)
        {
            throw new InvalidInputException();
        }
    }

    private static int GetSelectedOption()
    {
        string validInput = GetUserInput("Enter your choice: ", ValidateOptionInput);
        return int.Parse(validInput);
    }
    #endregion

    private static void VerifyFormula()
    {
        while (true)
            try
            {
                Console.Write("\n\nEnter interpretation: ");
                string? interpretation = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(interpretation)) throw new InvalidInputException();
                var input = interpretation.Trim()
                                          .Split(",")
                                          .Select(bool.Parse)
                                          .ToList();
                string verification = _formula.VerifyFormula(input) ? "" : "not ";
                Console.WriteLine($"\nInterpretation is {verification}valid.");
                return;
            }
            catch (Exception exception)
            {
                if (exception.Message.Equals("Formula is not provided."))
                {
                    Console.Write("\nFormula is not provided.\n");
                    return;
                }
                Console.Write("\nInvalid input! Try again.");
            }
    }

    private static List<int> InputSubset()
    {
        Console.Write("\nEnter subset: ");
        string? subsetInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(subsetInput)) throw new InvalidInputException();
        return subsetInput.Trim()
                          .Split(",")
                          .Select(int.Parse)
                          .Select(value => value - 1)
                          .ToList();
    }

    private static void VerifyIndependentSet()
    {
        while (true)
            try
            {
                Console.Write("\n");
                var subset = InputSubset();
                string verification = _graph.VerifyIndependentSet(subset) ? "" : "not ";
                Console.WriteLine($"\nSubset is {verification}independent.");
                return;
            }
            catch (Exception exception)
            {
                if (exception.Message.Equals("Graph is not provided."))
                {
                    Console.Write("\nGraph is not provided.\n");
                    return;
                }
                Console.Write("\nInvalid input! Try again.");
            }
    }

    private static void VerifyClique()
    {
        while (true)
            try
            {
                Console.Write("\n");
                var subset = InputSubset();
                string verification = _graph.VerifyClique(subset) ? "" : "not ";
                Console.WriteLine($"\nSubset is {verification}clique.");
                return;
            }
            catch (Exception exception)
            {
                if (exception.Message.Equals("Graph is not provided."))
                {
                    Console.Write("\nGraph is not provided.\n");
                    return;
                }
                Console.Write("\nInvalid input! Try again.");
            }
    }

    private static void PrintMatrix(bool[,] matrix)
    {
        for (int row = 0; row < (int)Math.Sqrt(matrix.Length); row++)
        {
            for (int column = 0; column < (int)Math.Sqrt(matrix.Length); column++)
                Console.Write((row != 0 && column == 0 ? "        " : "") + (matrix[row, column] ? "1 " : "0 "));
            Console.WriteLine();
        }
    }

    private static void ReduceThreeSat(bool toIdenpendentSet = true)
    {
        try
        {
            var independentSetEntry = toIdenpendentSet ? _formula.ReduceToIndependentSet() : _formula.ReduceToClique();
            string problem = toIdenpendentSet ? "idenpendent set" : "clique";
            Console.Write($"\n\nArguments for {problem} problem:\n\ngraph = ");
            PrintMatrix(independentSetEntry.Item1);
            Console.WriteLine($"\nk = {independentSetEntry.Item2}");
        }
        catch (InvalidOperationException exception)
        {
            Console.WriteLine($"\n\n{exception.Message}");
        }
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("\nWelcome!");
        while (true)
        {
            Console.WriteLine("\n\nAvailable options: \n    1 - Formula input\n    2 - Graph input\n    3 - Is formula satisfiable?\n    4 - Is there independent set?\n    5 - Is there clique?\n    6 - Formula verification\n    7 - Independent set verification\n    8 - Clique verification\n    9 - Reduce 3-SAT to independent set problem\n    10 - Reduce 3-SAT to clique problem\n    11 - Exit");
            int userChoice = GetSelectedOption();
            if (userChoice == 1)
            {
                Console.Write("\n\n");
                _formula.InputFormula();
                Console.WriteLine("Formula successfully added!");
            }
            else if (userChoice == 2)
            {
                Console.Write("\n\n");
                _graph.InputGraph();
                Console.WriteLine("Graph successfully added!");
            }
            else if (userChoice == 3)
            {
                try
                {
                    string satisfiable = _formula.IsSatisfiable() ? "" : "not ";
                    Console.WriteLine($"\n\nFormula is {satisfiable}satisfiable.");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("\n\nFormula is not provided.");
                }
            }
            else if (userChoice == 4)
            {
                try
                {
                    int subsetSize = GetSubsetSize();
                    string hasIndependentSet = _graph.HasIndependentSet(subsetSize) ? "" : "no ";
                    Console.WriteLine($"\nGraph has {hasIndependentSet}independent set with size {subsetSize}.");
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine($"\n{exception.Message}");
                }
            }
            else if (userChoice == 5)
            {
                try
                {
                    int subsetSize = GetSubsetSize();
                    string hasIndependentSet = _graph.HasClique(subsetSize) ? "" : "no ";
                    Console.WriteLine($"\nGraph has {hasIndependentSet}clique with size {subsetSize}.");
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine($"\n{exception.Message}");
                }
            }
            else if (userChoice == 6)
                VerifyFormula();
            else if (userChoice == 7)
                VerifyIndependentSet();
            else if (userChoice == 8)
                VerifyClique();
            else if (userChoice == 9)
                ReduceThreeSat();
            else if (userChoice == 10)
                ReduceThreeSat(toIdenpendentSet: false);
            else if (userChoice == 10)
            {
                Console.Write("\n\nThank you for your time! Goodbye.\n");
                break;
            }
        }
    }
}