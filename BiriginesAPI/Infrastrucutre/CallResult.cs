using System.Net;

namespace BiriginesAPI.Infrastrucutre
{
    public class CallResult
    {
        public static CallResult Success(object? data = null)
        {
            return new CallResult(data is null ? HttpStatusCode.NoContent : HttpStatusCode.OK, data: data);
        }

        public static CallResult Failure(HttpStatusCode statusCode, string message, object? data = null)
        {
            if ((int)statusCode < 300)
                throw new InvalidOperationException("The status code is'nt correct");

            return new CallResult(statusCode, message, data);
        }

        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        private CallResult(HttpStatusCode statusCode, string? message = null, object? data = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

    }
}//end name space 
