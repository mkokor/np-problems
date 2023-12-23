namespace BooleanSatisfiabilityProblem.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message = "Provided input is invalid.") : base(message) { }
    }
}