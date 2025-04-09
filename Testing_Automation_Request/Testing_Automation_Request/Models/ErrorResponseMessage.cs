using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Automation_Request.Models
{
    public class ErrorResponseMessage
    {
        public string ErrorMessage { get; set; }
        public string ExceptionType { get; set; }

        public string InnerExceptionType { get; set; } = null;
        public int? ErrorCode { get; set; } = null;
        public string ErrorDetails { get; set; } = null;
    }
}
