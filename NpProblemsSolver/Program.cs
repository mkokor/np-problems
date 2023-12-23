internal class Program
{
  private static void Main(string[] args)
  {
    var a = new CliqueIndependentSetGraph.CliqueIndependentSetGraph();
    a.InputGraph();

    var q = a.GetAdjacencyMatrix();

    for (int i = 0; i < 6; i++)
    {
      for (int j = 0; j < 6; j++)
      {
        Console.Write(q[i, j] + " ");
      }
      Console.WriteLine();  // Prijelaz u novi red nakon svakog reda matrice
    }
  }
}