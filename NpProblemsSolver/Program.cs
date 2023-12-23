internal class Program
{
  private static void Main(string[] args)
  {
    var a = new CliqueIndependentSetGraph.CliqueIndependentSetGraph();
    a.InputGraph();

    var q = a.GetAdjacencyMatrix();
  }
}