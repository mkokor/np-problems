using BooleanSatisfiabilityProblem;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("3-SAT problem solution!");
        List<List<int>> actualResult = ThreeCnfFormula.ValidateFormulaInput("2,2,2\n1,7,2\n5,6,7");
        for (int i = 0; i < actualResult.Count; i++)
        {
            Console.WriteLine($"Redak {i + 1}:");
            foreach (int broj in actualResult[i])
            {
                Console.WriteLine(broj);
            }
            Console.WriteLine();  // Dodaj praznu liniju za razdvajanje redaka
        }
    }
}