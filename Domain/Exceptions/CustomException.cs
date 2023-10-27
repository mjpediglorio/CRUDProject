using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
            
        }
        public int Code { get; set; }
        public string? Message { get; set; }
    }
}
