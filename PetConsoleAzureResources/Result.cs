using System;
using System.Collections.Generic;
using System.Text;

namespace PetConsoleAzureResources
{
    public class AzureActionResult
    {
        public bool Succeed { get; set; }
        public object Value { get; set; }
        public string Message { get; set; }

        public AzureActionResult()
        {
            Succeed = false;
            Value = null;
            Message = string.Empty;
        }
    }
}
