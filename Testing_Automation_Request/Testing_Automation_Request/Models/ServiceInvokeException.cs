using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Automation_Request.Models
{
    public class ServiceInvokeException : Exception
    {
        public HttpResponseMessage ResponseMessage { get; }
        public ErrorResponseMessage ErrorResponseDetails { get; set; }

        public ServiceInvokeException(HttpResponseMessage responseMessage, ErrorResponseMessage errorResponse)
        {
            ResponseMessage = responseMessage;
            ErrorResponseDetails = errorResponse;
        }
    }
}
