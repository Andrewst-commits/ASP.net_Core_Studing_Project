using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySwaggerUIServer.Attributes
{
    public class OnlyLatinAttribute : Attribute, IModelValidator
    {
        public string ErrorMessage { get; set; }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (!(context.Model is string))
                return Enumerable.Empty<ModelValidationResult>();

            var str = (string)context.Model;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'a' && str[i] <= 'z' || str[i] >= 'A' && str[i] <= 'Z')
                    continue;

                return new List<ModelValidationResult>
                {
                   new ModelValidationResult(context.ModelMetadata.PropertyName, $"{ErrorMessage}")
                };

            }
            // рефакторинг инверсией 
            // рефакторинг выделение идентичных действий 

            //LINQ или Regex (скорее всего второй)
            return Enumerable.Empty<ModelValidationResult>(); // возможно выброс исключения
        }

    }
}
