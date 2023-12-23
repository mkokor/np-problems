using BooleanSatisfiabilityProblem.Exceptions;

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

        #region Setter
        // Setter is made public only for testing purposes.
        public void SetFormula(List<List<int>> formula)
        {
            this.formula = formula;
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
    }
}