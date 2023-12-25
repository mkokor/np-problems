namespace BooleanSatisfiabilityProblem.Tests
{
    [TestClass]
    public class IndependentSetReductionTest
    {
        [TestMethod]
        public void ReduceToIndependentSet_MultipleClauses_ReturnsCorrectResult()
        {
            ThreeCnfFormula formula = new();

            formula.SetFormula(new List<List<int>>
            {
                new() { 1, -2, -3 },
                new() { -1, 2, 3 },
                new() { -1, 2, -3 },
                new() { 1, -2, 3 },
            });

            (bool[,] actualGraph, int subsetSize) = formula.ReduceToIndependentSet();

            bool[,] expectedGraph = new bool[,]
            {
                { false, true, true, true, false, false, true, false, false, false, false, false },
                { true, false, true, false, true, false, false, true, false, false, false, false },
                { true, true, false, false, false, true, false, false, false, false, false, true },
                { true, false, false, false, true, true, false, false, false, true, false, false },
                { false, true, false, true, false, true, false, false, false, false, true, false },
                { false, false, true, true, true, false, false, false, true, false, false, false },
                { true, false, false, false, false, false, false, true, true, true, false, false },
                { false, true, false, false, false, false, true, false, true, false, true, false },
                { false, false, false, false, false, true, true, true, false, false, false, true },
                { false, false, false, true, false, false, true, false, false, false, true, true },
                { false, false, false, false, true, false, false, true, false, true, false, true },
                { false, false, true, false, false, false, false, false, true, true, true, false }
            };

            Assert.IsTrue(subsetSize == 4);
            CollectionAssert.AreEqual(actualGraph, expectedGraph);
        }
    }
}