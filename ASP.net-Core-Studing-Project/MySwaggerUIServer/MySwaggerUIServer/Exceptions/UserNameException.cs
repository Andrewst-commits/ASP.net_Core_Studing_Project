using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySwaggerUIServer.Exceptions
{
    public class UserNameException : UserValidationException
    {
        public string Name { get; }
        private const string message = "Uncorrect name";

        public UserNameException(string fieldName, string name)
            : base(message, name, fieldName)
        {
            this.Name = name;
        }
    }

}
