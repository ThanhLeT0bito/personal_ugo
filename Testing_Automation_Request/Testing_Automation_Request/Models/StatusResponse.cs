using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Automation_Request.Models
{
    public class StatusResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }

        public StatusResponse()
        {
        }
    }
}
