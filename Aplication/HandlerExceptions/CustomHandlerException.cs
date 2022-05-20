using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.HandlerExceptions
{
    public  class CustomHandlerException: Exception
    {
        public HttpStatusCode Code { get; set; }
        public object Errors { get; }

        public CustomHandlerException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}
