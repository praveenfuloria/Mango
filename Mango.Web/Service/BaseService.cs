using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<ResponceDto?> SendAsync(RequestDto requestDto)
        {
            try
            {

                HttpClient client = httpClientFactory.CreateClient("MangoAPI");

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Headers.Add("Accept", "application/json");
                httpRequestMessage.RequestUri = new Uri(requestDto.Url);
                httpRequestMessage.Method = new HttpMethod(requestDto.ApiType.ToString());
                if (requestDto.Data != null)
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequestMessage);

                switch (httpResponseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { isSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { isSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.InternalServerError:
                        return new() { isSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.Unauthorized:
                        return new() { isSuccess = false, Message = "Unauthorized" };
                    default:
                        var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<Models.ResponceDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                ResponceDto responceDto = new ResponceDto()
                {
                    isSuccess = false,
                    Message = ex.Message
                };
                return responceDto;
            }
    }
}
