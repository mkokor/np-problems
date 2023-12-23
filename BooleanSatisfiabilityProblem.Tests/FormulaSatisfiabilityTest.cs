namespace BooleanSatisfiabilityProblem.Tests
{
    [TestClass]
    public class FormulaSatisfiabilityTest
    {
        [TestMethod]
        public void IsFormulaSatisfiable_EmptyFormula_ThrowsInvalidOperationException()
        {
            var threeCnfFormula = new ThreeCnfFormula();

            Assert.ThrowsException<InvalidOperationException>(() => threeCnfFormula.IsSatisfiable());
        }

        [TestMethod]
        public void IsFormulaSatisfiable_OneClauseOneVariable_ReturnsCorrectResult()
        {
            var threeCnfFormula = new ThreeCnfFormula();
            threeCnfFormula.SetFormula(new List<List<int>>() { new() { -1, -1, -1, } });

            List<bool> actualResult = new() { true };
            threeCnfFormula.GetInterpretation(actualResult);
            List<bool> expectedResult = new() { false };

            Assert.IsTrue(threeCnfFormula.IsSatisfiable());
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void IsFormulaSatisfiable_ThreeFalseVariables_ReturnsCorrectResult()
        {
            var threeCnfFormula = new ThreeCnfFormula();
            threeCnfFormula.SetFormula(new List<List<int>>()
            {
                new() { -1, -1, -1 },
                new() { -2, -2, -2 },
                new() { -3, -3, -3 }
            });

            List<bool> actualResult = new() { true, true, true };
            threeCnfFormula.GetInterpretation(actualResult);
            List<bool> expectedResult = new() { false, false, false };

            Assert.IsTrue(threeCnfFormula.IsSatisfiable());
            CollectionAssert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void IsFormulaSatisfiable_UnsatisfiableFormula_ReturnsFalse()
        {
            var threeCnfFormula = new ThreeCnfFormula();
            threeCnfFormula.SetFormula(new List<List<int>>()
            {
                new() { -1, -1, -1 },
                new() { 1, 1, 1 }
            });

            Assert.IsFalse(threeCnfFormula.IsSatisfiable());
        }
    }
}