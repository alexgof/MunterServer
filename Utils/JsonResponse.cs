using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MunterService.Utils
{
    public class ResponseData
    {
        public string url { get; set; }
    }

    [DataContract]
    public class JSONResponse
    {
        [DataMember(EmitDefaultValue = true, Order = 0)]
        public HttpStatusCode Status { get; set; }
             
        [DataMember(EmitDefaultValue = false, Order = 1)]
        public Original[] Data { get; set; }

        [DataMember(EmitDefaultValue = false, Order = 1)]
        public string Message { get; set; }

        public void SetError(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            Status = status;
            Message = errorMessage;
        }
    }

    public class GiphyResponse
    {
        [JsonProperty("data")]
        public Gif[] Data { get; set; }
    }

    public class Gif
    {
        [JsonProperty("images")]
        public Images Images { get; set; }
    }

    public class Images
    {
        [JsonProperty("original")]
        public Original Original { get; set; }
    }

    public class Original
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }


}
