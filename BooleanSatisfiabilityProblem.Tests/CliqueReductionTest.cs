namespace BooleanSatisfiabilityProblem.Tests
{
    [TestClass]
    public class CliqueReductionProblem
    {
        [TestMethod]
        public void ReduceToClique_MultipleClauses_ReturnsCorrectResult()
        {
            ThreeCnfFormula formula = new();

            formula.SetFormula(new List<List<int>>
            {
                new() { 1, 1, 2 },
                new() { -1, -2, -2 },
                new() { -1, 2, 2 }
            });

            (bool[,] actualGraph, int subsetSize) = formula.ReduceToClique();

            bool[,] expectedGraph = new bool[,]
            {
                { false, false, false, false, true, true, false, true, true },
                { false, false, false, false, true, true, false, true, true },
                { false, false, false, true, false, false, true, true, true },
                { false, false, true, false, false, false, true, true, true },
                { true, true, false, false, false, false, true, false, false },
                { true, true, false, false, false, false, true, false, false },
                { false, false, true, true, true, true, false, false, false },
                { true, true, true, true, false, false, false, false, false },
                { true, true, true, true, false, false, false, false, false }
            };

            Assert.IsTrue(subsetSize == 3);
            CollectionAssert.AreEqual(actualGraph, expectedGraph);
        }
    }
}