using System;
using System.Collections.Generic;
using System.Text;

namespace Sysplan.Application.Models
{
    public class ValidationItem
    {
        public string PropertyName { get; }
        public string ValidationName { get; }
        public object Parameter { get; }
        public string ValidationMessage { get; }

        public ValidationItem(string propertyName, string validationName, string validationMessage, object parameter = null)
        {
            PropertyName = propertyName;
            ValidationName = validationName;
            Parameter = parameter;
            ValidationMessage = validationMessage;
        }
    }
}
