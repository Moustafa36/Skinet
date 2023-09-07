using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse( int statusCode , string massage = null)
        {
            StatusCode = statusCode;
            Message = massage??GetDefaultMasssgeForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set;}

       private string GetDefaultMasssgeForStatusCode(int statusCode)
       {
           return statusCode switch
           {
              400 => "A bad request, you have made",
              401 => "Authorized,you are not",
              404 => "Resource Found , is not",
              500 => "Error passed to the dark side , Error Leads to anger , anger leads to develope your self" ,
              _ => null
           };
       }
    }
}