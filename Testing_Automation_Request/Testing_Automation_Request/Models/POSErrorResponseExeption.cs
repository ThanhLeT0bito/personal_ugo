using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Automation_Request.Models
{
    public class POSErrorResponseExeption : Exception
    {
        public ErrorResponseModel ErrorResponseDetails { get; set; }
        public POSErrorResponseExeption(ErrorResponseModel errorResponse)
        {
            ErrorResponseDetails = errorResponse;
        }
    }
}
