using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Models.DTOs.Response
{
    public class GeneralResponse
    {
        public static GeneralResponse Create(HttpStatusCode statusCode, object result = null, string responseMsg = null, object error = null)
        {
            return new GeneralResponse(statusCode, result, responseMsg, error);
        }

        

        public int statusCode { get; set; }
        public string requestId { get; }
        public object error { get; set; }
        public object result { get; set; }
        public bool success { get; set; }
        public string responseMessage { get; set; }
        

        protected GeneralResponse(HttpStatusCode statusCode, object result = null, string responseMsg = null, object error = null)
        {
            requestId = Guid.NewGuid().ToString();
            this.statusCode = (int)statusCode;
            success = (int)statusCode == 200 ? true : false;
            this.result = result;
            responseMessage = responseMsg;
            this.error = error != null ? error : new string[] { };

        }

       
    }

    
}
