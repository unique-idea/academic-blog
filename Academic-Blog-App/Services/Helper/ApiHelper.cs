using Academic_Blog_App.Services.ClientAjax;
using Academic_Blog_App.Services.ClientEnum;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Academic_Blog.PayLoad.Response;
using Academic_Blog_App.Services.Helper.Odata;
using Microsoft.AspNetCore.Mvc;

namespace Academic_Blog_App.Services.Helper
{
    public class ApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string Scheme = SchemeEnum.HTTP.ToString();
        private readonly string Host = "localhost";
        private readonly string Port = "5047";
        private string RootUrl = "";
        private string CallUrl = "", JsonContent = "", ResponseContent = "";
        private HttpResponseMessage Response = default!;

        public ApiHelper(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpContextAccessor = httpContextAccessor; ;
        }

        public async Task<ResultHelper<T>> FetchApiAsync<T>(EndPointEnum endPoint, string postFixUrl, MethodEnum method, object data)
        {

            RootUrl = Scheme + "://" + Host + ":" + Port + "/" + PatchEnum.api.ToString() + "/";
            CallUrl = RootUrl + endPoint.ToString() + postFixUrl;

            JsonSerializerOptions options = SetHeader();
            JsonContent = data != null ? JsonSerializer.Serialize(data) : "";
            await DoApi(method);

            if (Response!.IsSuccessStatusCode)
            {
                ResponseContent = await Response.Content.ReadAsStringAsync();
            }
            else
            {
                return ResultHelper<T>.Fail($"Error: {Response.StatusCode}");
            }

            if (string.IsNullOrEmpty(ResponseContent))
            {
                return ResultHelper<T>.Success();
            }

            T result = JsonSerializer.Deserialize<T>(ResponseContent, options)!;
            return ResultHelper<T>.Success(result);
        }

        public async Task<ResultHelper<T>> FetchODataAsync<T>(EndPointEnum endPoint, string postFixUrl)
        {
            RootUrl = Scheme + "://" + Host + ":" + Port + "/" + PatchEnum.odata.ToString() + "/";
            CallUrl = RootUrl + endPoint.ToString() + postFixUrl;

            JsonSerializerOptions options = SetHeader();

            Response = await _httpClient.GetAsync(CallUrl);

            if (Response!.IsSuccessStatusCode)
            {
                ResponseContent = await Response.Content.ReadAsStringAsync();
            }
            else
            {
                return ResultHelper<T>.Fail($"Error: {Response.StatusCode}");
            }

            if (string.IsNullOrEmpty(ResponseContent))
            {
                return ResultHelper<T>.Success();
            }
            try
            {
                ODataResponse<T> odataResponse = JsonSerializer.Deserialize<ODataResponse<T>>(ResponseContent, options)!;
                T result = odataResponse.Value!;
                return ResultHelper<T>.Success(result);
            }
            catch(Exception e)
            {
                return ResultHelper<T>.Fail($"Error: {e.Message}");
            }
        }

        private async Task DoApi(MethodEnum method)
        {
            switch (method)
            {
                case MethodEnum.GET:
                    Response = await _httpClient.GetAsync(CallUrl);
                    break;
                case MethodEnum.POST:
                    Response = await _httpClient.PostAsync(CallUrl, new StringContent(JsonContent, Encoding.UTF8, "application/json"));
                    break;
                case MethodEnum.PUT:
                    if (JsonContent != "" || JsonContent != null)
                    {
                        Response = await _httpClient.PutAsync(CallUrl, new StringContent(JsonContent, Encoding.UTF8, "application/json"));
                        break;
                    }
                    Response = await _httpClient.PutAsync(CallUrl, null);
                    break;
                case MethodEnum.DELETE:
                    Response = await _httpClient.DeleteAsync(CallUrl);
                    break;
            }
        }

        private JsonSerializerOptions SetHeader()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var LoginResponse = SessionHelper.GetObjectFromJson<LoginResponse>(_httpContextAccessor.HttpContext!.Session, "Account");

            if (LoginResponse != null)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginResponse.AccessToken);
            }

            return options;
        }

    }
}
