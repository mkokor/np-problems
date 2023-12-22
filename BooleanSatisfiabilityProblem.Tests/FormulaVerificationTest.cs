using BooleanSatisfiabilityProblem.Exceptions;

namespace BooleanSatisfiabilityProblem.Tests
{
    [TestClass]
    public class FormulaVerificationTest
    {
        [TestMethod]
        public void VerifyFormula_InvalidInput_ThrowsInvalidInputException()
        {
            var threeCnfFormula = new ThreeCnfFormula();
            threeCnfFormula.SetFormula(new List<List<int>>() { new() { 1, 1, 1 } });

            Assert.ThrowsException<InvalidInputException>(() => threeCnfFormula.VerifyFormula(new List<bool>()));
        }

        [TestMethod]
        public void VerifyFormula_TrueInterpretation_ReturnsTrue()
        {
            var threeCnfFormula = new ThreeCnfFormula();
            threeCnfFormula.SetFormula(new List<List<int>>()
            {
                new() { 1, -2, 3 },
                new() { -4, 2, -3 },
                new() { 5, 9, -4 },
                new() { 2, 1, -1 },
            });

            Assert.IsTrue(threeCnfFormula.VerifyFormula(new List<bool>() { false, true, true, false, true, true, false, false, true }));
        }

        [TestMethod]
        public void VerifyFormula_FalseInterpretation_ReturnsFalse()
        {
            var threeCnfFormula = new ThreeCnfFormula();
            threeCnfFormula.SetFormula(new List<List<int>>()
            {
                new() { 1, -2, 3 },
                new() { 5, -9, 6 },
                new() { 2, 3, 1 },
            });

            Assert.IsFalse(threeCnfFormula.VerifyFormula(new List<bool>() { true, false, false, false, false, false, false, false, true }));
        }
    }
}