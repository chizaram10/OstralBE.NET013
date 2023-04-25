using Newtonsoft.Json;
using System.Net;

namespace Ostral.Core.DTOs
{
    public class ResponseDTO<T>
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
        public T? Data { get; set; }

        public static ResponseDTO<T> Fail(IEnumerable<string> errors, int statusCode = (int)HttpStatusCode.NotFound)
        {
            return new ResponseDTO<T>
            {
                Status = false,
                Errors = errors,
                StatusCode = statusCode
            };
        }
        
        public static ResponseDTO<T> Success(T data, string successMessage = "", int statusCode = (int)HttpStatusCode.OK)
        {
            return new ResponseDTO<T>
            {
                Status = true,
                Message = successMessage,
                Data = data,
                StatusCode = statusCode
            };
        }
        
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}