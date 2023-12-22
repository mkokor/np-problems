namespace BooleanSatisfiabilityProblem.Tests
{
    [TestClass]
    public class FormulaSatisfiabilityTest
    {
        [TestMethod]
        public void IsFormulaSatisfiable_EmptyFormula_ThrowsInvalidOperationException()
        {
            var threeCnfFormula = new ThreeCnfFormula();

            Assert.ThrowsException<InvalidOperationException>(() => threeCnfFormula.IsFormulaSatisfiable());
        }

        [TestMethod]
        public void IsFormulaSatisfiable_OneClauseOneVariable_ReturnsCorrectResult()
        {
            var threeCnfFormula = new ThreeCnfFormula();
            threeCnfFormula.SetFormula(new List<List<int>>() { new() { -1, -1, -1, } });

            List<bool> actualResult = new() { true };
            threeCnfFormula.GetInterpretation(actualResult);
            List<bool> expectedResult = new() { false };

            Assert.IsTrue(threeCnfFormula.IsFormulaSatisfiable());
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }
    }
}