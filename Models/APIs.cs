using System.Net;

namespace BharatMedicsV2.Models
{
    public class APIs
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<String> ErrorMessages { get; set; }
        public bool IsSuccess = true;
        public object Response {get;set;}
    }
}
