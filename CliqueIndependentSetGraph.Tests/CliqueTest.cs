namespace CliqueIndependentSetGraph.Tests
{
    [TestClass]
    public class CliqueTest
    {
        [TestMethod]
        public void HasClique_NoEdges_ReturnsFalse()
        {
            CliqueIndependentSetGraph graph = new();
            graph.SetAdjacencyMatrix(new List<List<int>>(), 5);

            List<int> clique = new();

            Assert.IsFalse(graph.GetSubset(clique, 0, 4, 5, graph.VerifyClique));
        }

        [TestMethod]
        public void HasClique_MultipleEdges1_ReturnsCorrectResult()
        {
            CliqueIndependentSetGraph graph = new();
            graph.SetAdjacencyMatrix(new List<List<int>> {
                new() { 1, 2 },
                new() { 2, 1 },
            }, 6);

            List<int> clique = new();

            Assert.IsTrue(graph.GetSubset(clique, 0, 2, 6, graph.VerifyClique));
            CollectionAssert.AreEqual(new List<int> { 0, 1 }, clique);
        }

        [TestMethod]
        public void HasClique_MultipleEdges2_ReturnsCorrectResult()
        {
            CliqueIndependentSetGraph graph = new();
            graph.SetAdjacencyMatrix(new List<List<int>> {
                new() { 1, 2 },
                new() { 1, 4 },
                new() { 1, 7 },
                new() { 2, 1 },
                new() { 2, 3 },
                new() { 2, 5 },
                new() { 2, 6 },
                new() { 2, 7 },
                new() { 3, 2 },
                new() { 3, 5 },
                new() { 3, 6 },
                new() { 3, 7 },
                new() { 4, 1 },
                new() { 4, 5 },
                new() { 5, 2 },
                new() { 5, 3 },
                new() { 5, 4 },
                new() { 5, 6 },
                new() { 5, 7 },
                new() { 6, 2 },
                new() { 6, 3 },
                new() { 6, 5 },
                new() { 6, 7 },
                new() { 7, 1 },
                new() { 7, 2 },
                new() { 7, 3 },
                new() { 7, 4 },
                new() { 7, 5 },
                new() { 7, 6 }
            }, 7);

            List<int> clique = new();

            Assert.IsTrue(graph.GetSubset(clique, 0, 5, 7, graph.VerifyClique));
            CollectionAssert.AreEqual(new List<int> { 1, 2, 4, 5, 6 }, clique);
        }

        [TestMethod]
        public void HasClique_MultipleEdges3_ReturnsCorrectResult()
        {
            CliqueIndependentSetGraph graph = new();
            graph.SetAdjacencyMatrix(new List<List<int>> {
                new() { 1, 2 },
                new() { 1, 4 },
                new() { 1, 7 },
                new() { 2, 1 },
                new() { 2, 3 },
                new() { 2, 5 },
                new() { 2, 6 },
                new() { 2, 7 },
                new() { 3, 2 },
                new() { 3, 5 },
                new() { 3, 6 },
                new() { 3, 7 },
                new() { 4, 1 },
                new() { 4, 5 },
                new() { 5, 2 },
                new() { 5, 3 },
                new() { 5, 4 },
                new() { 5, 6 },
                new() { 5, 7 },
                new() { 6, 2 },
                new() { 6, 3 },
                new() { 6, 5 },
                new() { 6, 7 },
                new() { 7, 1 },
                new() { 7, 2 },
                new() { 7, 3 },
                new() { 7, 4 },
                new() { 7, 5 },
                new() { 7, 6 }
            }, 7);

            List<int> clique = new();

            Assert.IsTrue(graph.GetSubset(clique, 0, 4, 7, graph.VerifyClique));
            CollectionAssert.AreEqual(new List<int> { 1, 2, 4, 5 }, clique);
        }
    }
}