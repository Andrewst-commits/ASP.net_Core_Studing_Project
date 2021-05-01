using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySwaggerUIServer.Attributes
{
    public class RangeAttribute : Attribute, IModelValidator
    {
        public int MaxNum { get; set; }
        public int MinNum { get; set; }
        public string ErrorMessage { get; set; }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            if (context.Model is int)
            {
                var number = (int)context.Model;
                if (number < MinNum || number > MaxNum)
                {
                    return new List<ModelValidationResult>
                    {
                        new ModelValidationResult(context.ModelMetadata.PropertyName, $"{ErrorMessage} ({MinNum}..{MaxNum})")
                    };
                }

            }

            return Enumerable.Empty<ModelValidationResult>();
        }

    }
}
