using BooleanSatisfiabilityProblem.Exceptions;

namespace BooleanSatisfiabilityProblem.Tests;

[TestClass]
public class FormulaInputTest
{
    [TestMethod]
    public void EnterFormula_EmptyInput_ThrowsInvalidInputException()
    {
        Assert.ThrowsException<InvalidInputException>(() => ThreeCnfFormula.ValidateFormulaInput(""));
    }

    [TestMethod]
    public void ValidateFormulaInput_FourCnfFormula_ThrowsInvalidInputException()
    {
        Assert.ThrowsException<InvalidInputException>(() => ThreeCnfFormula.ValidateFormulaInput("1,2,3,4\n1,2,3,4"));
    }

    [TestMethod]
    public void ValidateFormulaInput_GeneralCnfFormula_ThrowsInvalidInputException()
    {
        Assert.ThrowsException<InvalidInputException>(() => ThreeCnfFormula.ValidateFormulaInput("1,2,3\n1,2\n1"));
    }

    [TestMethod]
    public void ValidateFormulaInput_OutOfRangeVariable_ThrowsInvalidInputException()
    {
        Assert.ThrowsException<InvalidInputException>(() => ThreeCnfFormula.ValidateFormulaInput("2,2,2\n1,7,2"));
    }

    [TestMethod]
    public void ValidateFormulaInput_Letters_ThrowsInvalidInputException()
    {
        Assert.ThrowsException<InvalidInputException>(() => ThreeCnfFormula.ValidateFormulaInput("rAnDoMsTrInGiNpUt"));
    }

    [TestMethod]
    public void ValidateFormulaInput_CorrectInput_ReturnsCorrectFormula()
    {
        List<List<int>> actualResult = ThreeCnfFormula.ValidateFormulaInput("2,2,2\n1,7,2\n5,6,7");

        List<List<int>> expectedResult = new()
        {
            new() { 2, 2, 2 },
            new() { 1, 7, 2 },
            new() { 5, 6, 7 }
        };

        CollectionAssert.AreEqual(expectedResult[0], actualResult[0]);
        CollectionAssert.AreEqual(expectedResult[1], actualResult[1]);
        CollectionAssert.AreEqual(expectedResult[2], actualResult[2]);
    }
}