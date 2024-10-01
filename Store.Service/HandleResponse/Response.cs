using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandleResponse
{
    public class Response{
        public Response(int statusCode, string? message)
        {
            StatusCode = statusCode;
            Message = message??GetDefaultMessageForStatusCode(statusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int StatusCode)
        => StatusCode switch
        {
               100=>"Continue",
               200=>"OK",
               201 =>"Created",
               202 => "Accepted",
               203 =>"Information",
               204 =>"No Content",
               205=>"Reset Content",
               206=> "Partial Content",
            _=>"Unknown Status Code"
        };

        
    }
}
