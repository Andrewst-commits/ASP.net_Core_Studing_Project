using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySwaggerUIServer.Exceptions
{
    public class UserAgeException : UserValidationException
    {
        public int Age { get; }
        private const string message = "Uncorrect age";

        public UserAgeException(string fieldName, int age)
            : base(message, age.ToString(), fieldName)
        {
            Age = age;
        }
    }


    public class UserValidationException : Exception
    {
        public string ProblemFieldValue { get; }
        public string FieldName { get; }

        public UserValidationException(string message, string problemFieldValue, string fieldName) : base(message)
        {
            ProblemFieldValue = problemFieldValue;
            FieldName = fieldName;
        }




    }
}
