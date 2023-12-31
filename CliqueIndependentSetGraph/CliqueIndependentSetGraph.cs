using Exceptions;

namespace CliqueIndependentSetGraph
{
    // This class represents graph, and provides methods for 
    // solving clique and independent set problems (with brute force).
    // CONSTRAINT: 15 nodes maximum (this constraint is not valid when using setter), node can not be related to itself (program will ignore it)
    public class CliqueIndependentSetGraph
    {
        private bool[,] adjacencyMatrix;

        public CliqueIndependentSetGraph()
        {
            adjacencyMatrix = new bool[0, 0];
        }

        #region GetterAndSetter
        private static void ValidateEdges(List<List<int>> edges, int numberOfNodes)
        {
            foreach (var edge in edges)
                if (edge[0] > numberOfNodes || edge[1] > numberOfNodes) throw new ArgumentException("Provided edges are not valid.");
        }

        public void SetAdjacencyMatrix(List<List<int>> edges, int numberOfNodes)
        {
            ValidateEdges(edges, numberOfNodes);
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
                Console.Write("Enter number of nodes (15 maximum): ");
                string? input = Console.ReadLine();
                try
                {
                    if (string.IsNullOrWhiteSpace(input)) throw new InvalidInputException();
                    int numberOfNodes = int.Parse(input);
                    if (numberOfNodes < 1 || numberOfNodes > 15) throw new InvalidInputException();
                    return numberOfNodes;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nInvalid input! Try again.\n");
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

        #region GettingSubsetWithCertainConditionW
        // This method was made public only for testing purposes.
        public bool GetSubset(List<int> currentSet, int index, int cliqueSize, int numberOfNodes, SubsetCondition subsetCondition)
        {
            if (cliqueSize == 0)
            {
                if (subsetCondition(currentSet)) return true;
                return false;
            }

            for (int i = index; i < numberOfNodes; i++)
            {
                currentSet.Add(i);
                if (GetSubset(currentSet, i + 1, cliqueSize - 1, numberOfNodes, subsetCondition))
                    return true;
                currentSet.RemoveAt(currentSet.Count - 1);
            }

            return false;
        }

        public delegate bool SubsetCondition(List<int> subset);
        #endregion

        #region IndependentSetCheck
        public bool VerifyIndependentSet(List<int> set)
        {
            if (adjacencyMatrix.Length == 0) throw new InvalidOperationException("Graph is not provided.");
            if (set.Count < 2) throw new InvalidOperationException("Subset size must be at least 2.");
            if (set.Max() > (int)Math.Sqrt(adjacencyMatrix.Length))
                throw new InvalidInputException("Invalid input.");
            for (int source = 0; source < set.Count; source++)
                for (int destination = source + 1; destination < set.Count; destination++)
                    if (adjacencyMatrix[set[source], set[destination]])
                        return false;
            return true;
        }

        // This method checks if there is an independent set with size at least k in current graph (with brute force).
        public bool HasIndependentSet(int k)
        {
            if (adjacencyMatrix.Length == 0) throw new InvalidOperationException("Graph is not provided.");
            if (k < 2) throw new InvalidOperationException("Subset size must be at least 2.");
            if (k > (int)Math.Sqrt(adjacencyMatrix.Length)) return false;
            List<int> independentSet = new();
            if (GetSubset(independentSet, 0, k, (int)Math.Sqrt(adjacencyMatrix.Length), VerifyIndependentSet))
                return true;
            return false;
        }
        #endregion

        #region CliqueCheck
        public bool VerifyClique(List<int> set)
        {
            if (adjacencyMatrix.Length == 0) throw new InvalidOperationException("Graph is not provided.");
            if (set.Count < 2) throw new InvalidOperationException("Subset size must be at least 2.");
            if (set.Max() > (int)Math.Sqrt(adjacencyMatrix.Length))
                throw new InvalidInputException("Invalid input.");
            for (int source = 0; source < set.Count; source++)
                for (int destination = source + 1; destination < set.Count; destination++)
                    // Node can not be related to itself (program will ignore it).
                    if (source != destination && !adjacencyMatrix[set[source], set[destination]])
                        return false;
            return true;
        }

        // This method checks if there is an clique with size at least k in current graph (with brute force).
        public bool HasClique(int k)
        {
            if (adjacencyMatrix.Length == 0) throw new InvalidOperationException("Graph is not provided.");
            if (k < 2) throw new InvalidOperationException("Subset size must be at least 2.");
            if (k > (int)Math.Sqrt(adjacencyMatrix.Length)) return false;
            List<int> clique = new();
            if (GetSubset(clique, 0, k, (int)Math.Sqrt(adjacencyMatrix.Length), VerifyClique))
                return true;
            return false;
        }
        #endregion
    }
}