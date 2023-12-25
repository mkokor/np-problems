using Exceptions;

namespace BooleanSatisfiabilityProblem
{
    // This class represents 3-cnf logical formula, and provides methods for 
    // solving 3-SAT problem (with brute force), and verifying provided solution.
    // CONSTRAINT: 8 clauses (24 variables) maximum
    public class ThreeCnfFormula
    {
        private List<List<int>> formula;

        public ThreeCnfFormula()
        {
            formula = new List<List<int>>();
        }

        #region GetterAndSetter
        // Setter is made public only for testing purposes.
        public void SetFormula(List<List<int>> formula)
        {
            this.formula = formula;
        }

        private int GetNumberOfClauses()
        {
            return formula.Count;
        }
        #endregion

        #region FormulaInput
        // This method was made public only for testing purposes.
        public static List<List<int>> ValidateFormulaInput(string formulaInput)
        {
            List<List<int>> formula = new();
            try
            {
                formula = formulaInput.Trim()
                                      .Split("\n")
                                      .Select(clause => clause.Split(","))
                                      .Select(clause => clause.Select(literal => int.Parse(literal)).ToList())
                                      .ToList();
            }
            catch (Exception)
            {
                throw new InvalidInputException();
            }

            if (formula.Count > 8) throw new InvalidInputException();

            formula.ForEach(clause =>
            {
                if (clause.Count != 3) throw new InvalidInputException();
                foreach (int literal in clause)
                    if (Math.Abs(literal) > formula.Count * 3 || Math.Abs(literal) > 24) throw new InvalidInputException();
            });
            return formula;
        }

        private static string EnterClauses()
        {
            string formulaInput = "";
            while (true)
            {
                string? clauseInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(clauseInput)) break;
                formulaInput += $"\n{clauseInput}";
            }
            return formulaInput;
        }

        public void InputFormula()
        {
            Console.WriteLine("Enter 3-cnf formula (8 clauses maximum, empty row - exit):");
            while (true)
                try
                {
                    SetFormula(ValidateFormulaInput(EnterClauses()));
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input! Try again.");
                    Console.WriteLine("\nEnter 3-cnf formula (8 clauses maximum, empty row - exit):");
                }
        }
        #endregion

        #region FormulaSatisfiabilityCheck
        private int GetHighestVariable()
        {
            return formula.Select(clause => clause.Select(literal => Math.Abs(literal)))
                          .Select(clause => clause.Max())
                          .Max();
        }

        // This method is public only for testing purposes.
        public bool GetInterpretation(List<bool> variables, int index = 0)
        {
            if (index == variables.Count)
                if (VerifyFormula(variables))
                    return true;
                else return false;

            variables[index] = true;
            if (GetInterpretation(variables, index + 1)) return true;

            variables[index] = false;
            if (GetInterpretation(variables, index + 1)) return true;

            return false;
        }

        public bool IsSatisfiable()
        {
            if (formula.Count == 0) throw new InvalidOperationException("Formula is not provided.");
            List<bool> variables = new(new bool[GetHighestVariable()]);
            if (GetInterpretation(variables)) return true;
            return false;
        }
        #endregion

        #region FormulaVerification
        public bool VerifyFormula(List<bool> values)
        {
            if (formula.Count == 0) throw new InvalidOperationException("Formula is not provided.");
            if (values.Count != GetHighestVariable()) throw new InvalidInputException();
            foreach (var clause in formula)
            {
                bool clauseResult = false;
                foreach (var literal in clause)
                {
                    bool value = values[Math.Abs(literal) - 1];
                    if (literal < 0) value = !value;
                    if (value)
                    {
                        clauseResult = true;
                        break;
                    }
                }
                if (!clauseResult) return false;
            }
            return true;
        }
        #endregion

        #region Reductions
        // This method returns list of edges and number of nodes.
        private Tuple<List<List<int>>, int> GetGraphForIndependentSet()
        {
            if (formula.Count == 0) throw new InvalidOperationException("Formula is not provided.");
            int subsetSize = GetNumberOfClauses();
            List<List<int>> edges = new();
            for (int clause = 0; clause < subsetSize; clause++)
            {
                edges.Add(new() { clause * 3 + 1, clause * 3 + 2 });
                edges.Add(new() { clause * 3 + 2, clause * 3 + 1 });
                edges.Add(new() { clause * 3 + 2, clause * 3 + 3 });
                edges.Add(new() { clause * 3 + 3, clause * 3 + 2 });
                edges.Add(new() { clause * 3 + 1, clause * 3 + 3 });
                edges.Add(new() { clause * 3 + 3, clause * 3 + 1 });
            }
            for (int i = 0; i < subsetSize; i++)
            {
                List<int> sourceClauseLiterals = new()
                {
                    formula[i][0],
                    formula[i][1],
                    formula[i][2]
                };
                for (int j = 0; j < subsetSize; j++)
                    if (i != j)
                    {
                        List<int> destinationClauseLiterals = new()
                        {
                            formula[j][0],
                            formula[j][1],
                            formula[j][2]
                        };
                        for (int k = 0; k < 3; k++)
                            for (int l = 0; l < 3; l++)
                                if (sourceClauseLiterals[k] == -destinationClauseLiterals[l])
                                    edges.Add(new() { i * 3 + k + 1, j * 3 + l + 1 });
                    }
            }
            return new Tuple<List<List<int>>, int>(edges, subsetSize * 3);
        }

        // This method returns graph (for independent set problem) and k (number of required independent nodes).
        public Tuple<bool[,], int> ReduceToIndependentSet()
        {
            if (formula.Count == 0) throw new InvalidOperationException("Formula is not provided.");
            var graphProperties = GetGraphForIndependentSet();
            var graph = new CliqueIndependentSetGraph.CliqueIndependentSetGraph();
            graph.SetAdjacencyMatrix(graphProperties.Item1, graphProperties.Item2);
            return new Tuple<bool[,], int>(graph.GetAdjacencyMatrix(), GetNumberOfClauses());
        }

        // This method returns list of edges and number of nodes.
        private Tuple<List<List<int>>, int> GetGraphForClique()
        {
            if (formula.Count == 0) throw new InvalidOperationException("Formula is not provided.");
            int subsetSize = GetNumberOfClauses();
            List<List<int>> edges = new();
            for (int i = 0; i < subsetSize; i++)
            {
                List<int> sourceClauseLiterals = new()
                {
                    formula[i][0],
                    formula[i][1],
                    formula[i][2]
                };
                for (int j = 0; j < subsetSize; j++)
                    if (i != j)
                    {
                        List<int> destinationClauseLiterals = new()
                        {
                            formula[j][0],
                            formula[j][1],
                            formula[j][2]
                        };
                        for (int k = 0; k < 3; k++)
                            for (int l = 0; l < 3; l++)
                                if (sourceClauseLiterals[k] != -destinationClauseLiterals[l])
                                    edges.Add(new() { i * 3 + k + 1, j * 3 + l + 1 });
                    }
            }
            return new Tuple<List<List<int>>, int>(edges, subsetSize * 3);
        }

        // This method returns graph (for clique problem) and k (size of required clique subset).
        public Tuple<bool[,], int> ReduceToClique()
        {
            if (formula.Count == 0) throw new InvalidOperationException("Formula is not provided.");
            var graphProperties = GetGraphForClique();
            var graph = new CliqueIndependentSetGraph.CliqueIndependentSetGraph();
            graph.SetAdjacencyMatrix(graphProperties.Item1, graphProperties.Item2);
            return new Tuple<bool[,], int>(graph.GetAdjacencyMatrix(), GetNumberOfClauses());
        }

        // This method converts node index to literal.
        private int GetLiteral(int nodeIndex)
        {
            int clause = nodeIndex / 3;
            int literal = nodeIndex % 3;
            return formula[clause][literal];
        }
        #endregion
    }
}