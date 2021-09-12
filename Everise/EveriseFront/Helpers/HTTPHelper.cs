using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EveriseFront.Helpers
{
    public class HTTPHelper
    {
        private static HTTPHelper instance = null;
        private static HttpClient httpClient = null;

        public static HTTPHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HTTPHelper();
                    httpClient = new HttpClient(new HttpClientHandler { UseProxy = false });
                    //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                return instance;
            }
        }

        public async Task<ResponseInfo> Post(string apiUrl,  EveriseFront.Models.Data data)
        {
            HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync(apiUrl, data);
            var resultString = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                return new ResponseInfo(ResponseStatus.Successful, resultString);
            }
            else
            {
                return new ResponseInfo(ResponseStatus.Unsuccessful, GenerateErrorMessage(responseMessage.StatusCode));
            }

        }

        private string GenerateErrorMessage(HttpStatusCode errorCode)
        {
            string message = "";

            switch (errorCode)
            {
                case HttpStatusCode.BadRequest:
                    message = "Sent data is not correct";
                    break;
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.Forbidden:
                    message = "Invalid authentication";
                    break;
                case HttpStatusCode.NotFound:
                    message = "Invalid API URL";
                    break;

                case HttpStatusCode.InternalServerError:
                case HttpStatusCode.BadGateway:
                case HttpStatusCode.GatewayTimeout:
                    message = "Server can not handle request";
                    break;
            }

            return message;
        }

        public class ResponseInfo
        {
            public ResponseStatus Status;
            public string Message;

            public ResponseInfo(ResponseStatus status, string message)
            {
                Status = status;
                Message = message;
            }
        }

        public enum ResponseStatus
        {
            Successful,
            Unsuccessful
        }

    }
}
