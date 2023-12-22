using System.Linq.Expressions;
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

        public void EnterFormula()
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

        #region GetterAndSetter
        private void SetFormula(List<List<int>> formula)
        {
            this.formula = formula;
        }
        #endregion
    }
}