namespace Exceptions
{
    public class UnsatisfiableFormulaException : Exception
    {
        public UnsatisfiableFormulaException(string message = "Provided formula is unsatisfiable.") : base(message) { }
    }
}