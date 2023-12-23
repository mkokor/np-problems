using BooleanSatisfiabilityProblem.Exceptions;

namespace CliqueIndependentSetGraph
{
    // This class represents graph, and provides methods for 
    // solving clique and independent set problems (wiht brute force).
    // CONSTRAINT: 15 nodes maximum
    public class CliqueIndependentSetGraph
    {
        private bool[,] adjacencyMatrix;

        public CliqueIndependentSetGraph()
        {
            adjacencyMatrix = new bool[0, 0];
        }

        #region GetterAndSetter
        // This methods were made public only for testing purposes.
        public void SetAdjacencyMatrix(List<List<int>> edges, int numberOfNodes)
        {
            adjacencyMatrix = new bool[numberOfNodes, numberOfNodes];
            edges.ForEach(edge =>
            {
                adjacencyMatrix[edge[0] - 1, edge[1] - 1] = true;
            });
        }

        public bool[,] GetAdjacencyMatrix()
        {
            return adjacencyMatrix;
        }
        #endregion

        #region GraphInput
        public static int GetNumberOfNodes()
        {
            while (true)
            {
                Console.Write("\nEnter number of nodes (15 maximum): ");
                string? input = Console.ReadLine();
                try
                {
                    if (string.IsNullOrWhiteSpace(input)) throw new InvalidInputException();
                    int numberOfNodes = int.Parse(input);
                    if (numberOfNodes < 1) throw new InvalidInputException();
                    return numberOfNodes;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nInvalid input! Try again.");
                }
            }
        }

        // This method was made public only for testing purposes.
        public static List<List<int>> ValidateEdgesInput(string edgesInput, int numberOfNodes)
        {
            List<List<int>> edges = new();
            if (edgesInput == "") return edges;

            try
            {
                edges = edgesInput.Trim()
                                  .Split("\n")
                                  .Select(edge => edge.Trim('(').Trim(')').Split(","))
                                  .Select(edge => edge.Select(node => int.Parse(node)).ToList())
                                  .ToList();
            }
            catch (Exception)
            {
                throw new InvalidInputException();
            }

            edges.ForEach(edge =>
            {
                if (edge.Count != 2) throw new InvalidInputException();
                foreach (int node in edge)
                    if (node > numberOfNodes || node < 1) throw new InvalidInputException();
            });
            return edges;
        }

        private static string EnterEdges()
        {
            string edgesInput = "";
            while (true)
            {
                string? edgeInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(edgeInput)) break;
                edgesInput += $"\n{edgeInput}";
            }
            return edgesInput;
        }

        public static List<List<int>> GetEdges(int numberOfNodes)
        {
            Console.WriteLine("\nEnter edges in format (i,j) (empty row - exit):");
            while (true)
                try
                {
                    return ValidateEdgesInput(EnterEdges(), numberOfNodes);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input! Try again.");
                    Console.WriteLine("\nEnter edges in format (i,j) (empty row - exit):");
                }
        }

        public void InputGraph()
        {
            int numberOfNodes = GetNumberOfNodes();
            SetAdjacencyMatrix(GetEdges(numberOfNodes), numberOfNodes);
        }
        #endregion

        #region IndependentSetCheck
        private bool IsIndependent(List<int> set)
        {
            for (int source = 0; source < set.Count; source++)
                for (int destination = source + 1; destination < set.Count; destination++)
                    if (adjacencyMatrix[set[source], set[destination]])
                        return false;
            return true;
        }

        // This method was made public only for testing purposes.
        public bool GetIndependentSet(List<int> currentSet, int index, int independentSetSize, int numberOfNodes)
        {
            if (independentSetSize == 0)
            {
                if (IsIndependent(currentSet)) return true;
                return false;
            }

            for (int i = index; i < numberOfNodes; i++)
            {
                currentSet.Add(i);
                if (GetIndependentSet(currentSet, i + 1, independentSetSize - 1, numberOfNodes))
                    return true;
                currentSet.RemoveAt(currentSet.Count - 1);
            }

            return false;
        }

        // This method checks if there is an independent set with size at least k in current graph (with brute force).
        public bool HasIndependentSet(int k)
        {
            if (k > (int)Math.Sqrt(adjacencyMatrix.Length)) return false;
            List<int> independentSet = new();
            if (GetIndependentSet(independentSet, 0, k, (int)Math.Sqrt(adjacencyMatrix.Length)))
                return true;
            return false;
        }
        #endregion    
    }
}