namespace CliqueIndependentSetGraph.Tests
{
    [TestClass]
    public class IndependentSetTest
    {
        [TestMethod]
        public void HasIndependentSet_NoEdges_ReturnsFirstKNodes()
        {
            CliqueIndependentSetGraph graph = new();
            graph.SetAdjacencyMatrix(new List<List<int>>(), 5);

            List<int> independentSet = new();

            Assert.IsTrue(graph.GetIndependentSet(independentSet, 0, 4, 5));
            CollectionAssert.AreEqual(new List<int> { 0, 1, 2, 3 }, independentSet);
        }

        [TestMethod]
        public void HasIndependentSet_MultipleEdges1_ReturnsCorrectResult()
        {
            CliqueIndependentSetGraph graph = new();
            graph.SetAdjacencyMatrix(new List<List<int>> {
                new() { 1, 2 },
                new() { 2, 3 },
            }, 6);

            List<int> independentSet = new();

            Assert.IsTrue(graph.GetIndependentSet(independentSet, 0, 4, 6));
            CollectionAssert.AreEqual(new List<int> { 0, 2, 3, 4 }, independentSet);
        }

        [TestMethod]
        public void HasIndependentSet_MultipleEdges2_ReturnsCorrectResult()
        {
            CliqueIndependentSetGraph graph = new();
            graph.SetAdjacencyMatrix(new List<List<int>> {
                new() { 1, 2 },
                new() { 1, 3 },
                new() { 2, 3 },
                new() { 2, 4 },
                new() { 2, 5 },
                new() { 3, 1 },
                new() { 3, 6 },
                new() { 3, 7 },
                new() { 4, 2 },
                new() { 4, 7 },
                new() { 5, 2 },
                new() { 5, 7 },
                new() { 6, 3 },
                new() { 6, 7 },
                new() { 7, 4 },
                new() { 7, 5 },
                new() { 7, 6 },
            }, 7);

            List<int> independentSet = new();

            Assert.IsTrue(graph.GetIndependentSet(independentSet, 0, 4, 7));
            CollectionAssert.AreEqual(new List<int> { 0, 3, 4, 5 }, independentSet);
        }

        [TestMethod]
        public void HasIndependentSet_MultipleEdges3_ReturnsCorrectResult()
        {
            CliqueIndependentSetGraph graph = new();
            graph.SetAdjacencyMatrix(new List<List<int>> {
                new() { 1, 2 },
                new() { 1, 3 },
                new() { 2, 3 },
                new() { 2, 4 },
                new() { 2, 5 },
                new() { 3, 1 },
                new() { 3, 6 },
                new() { 3, 7 },
                new() { 4, 2 },
                new() { 4, 7 },
                new() { 5, 2 },
                new() { 5, 7 },
                new() { 6, 3 },
                new() { 6, 7 },
                new() { 7, 4 },
                new() { 7, 5 },
                new() { 7, 6 },
            }, 7);

            List<int> independentSet = new();

            Assert.IsTrue(graph.GetIndependentSet(independentSet, 0, 3, 7));
            CollectionAssert.AreEqual(new List<int> { 0, 3, 4 }, independentSet);
        }
    }
}