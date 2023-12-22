using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooleanSatisfiabilityProblem.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message = "Provided input is invalid.") : base(message) { }
    }
}