using BooleanSatisfiabilityProblem.Exceptions;

namespace CliqueIndependentSetGraph.Tests
{
    [TestClass]
    public class GraphInputTest
    {
        [TestMethod]
        public void ValidateEdgesInput_EmptyInput_ThrowsInvalidInputException()
        {
            Assert.ThrowsException<InvalidInputException>(() => CliqueIndependentSetGraph.ValidateEdgesInput("", 0));
        }

        [TestMethod]
        public void ValidateEdgesInput_EdgeWithThreeNodes_ThrowsInvalidInputException()
        {
            Assert.ThrowsException<InvalidInputException>(() => CliqueIndependentSetGraph.ValidateEdgesInput("(1,2,3)\n(2,3,4)", 5));
        }

        [TestMethod]
        public void ValidateEdgesInput_OutOfRangeNode_ThrowsInvalidInputException()
        {
            Assert.ThrowsException<InvalidInputException>(() => CliqueIndependentSetGraph.ValidateEdgesInput("(1,2)\n(1,10)", 9));
        }

        [TestMethod]
        public void ValidateEdgesInput_Letters_ThrowsInvalidInputException()
        {
            Assert.ThrowsException<InvalidInputException>(() => CliqueIndependentSetGraph.ValidateEdgesInput("rAnDoMsTrInGiNpUt", 0));
        }

        [TestMethod]
        public void ValidateEdgesInput_CorrectInput_ReturnsCorrectAdjacencyMatrix()
        {
            List<List<int>> edges = CliqueIndependentSetGraph.ValidateEdgesInput("(1,2)\n(5,6)\n(3,4)", 6);
            var graph = new CliqueIndependentSetGraph();
            graph.SetAdjacencyMatrix(edges, 6);

            bool[,] expectedResult = {
                { false, true, false, false, false, false },
                { false, false, false, false, false, false },
                { false, false, false, true, false, false },
                { false, false, false, false, false, false },
                { false, false, false, false, false, true },
                { false, false, false, false, false, false }
            };

            CollectionAssert.AreEqual(expectedResult, graph.GetAdjacencyMatrix());
        }
    }
}