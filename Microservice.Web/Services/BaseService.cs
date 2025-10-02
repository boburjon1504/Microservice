using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Microservice.Web.Utility.SD;

namespace Microservice.Web.Services;

public class BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider) : IBaseService
{
    public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true)
    {

        try
        {
            HttpClient client = httpClientFactory.CreateClient();

            HttpRequestMessage message = new();

            message.Headers.Add("Accept", "application/json");

            if (withBearer)
            {
                var token = tokenProvider.GetToken();

                message.Headers.Add("Authorization", $"Bearer {token}");
            }

            message.RequestUri = new Uri(client.BaseAddress + requestDto.Url);
            SetDataToRequest(requestDto, message);
            SetHttpRequestType(requestDto, message);

            HttpResponseMessage? apiResponse = await client.SendAsync(message);

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message = "Not Found!" };
                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal server error!" };
                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access denied" };
                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Unauthorized" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonSerializer.Deserialize<ResponseDto>(apiContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return apiResponseDto;
            }
        }
        catch (Exception ex)
        {
            var dto = new ResponseDto
            {
                Message = ex.Message,
                IsSuccess = false
            };

            return dto;
        }
    }

    private static void SetHttpRequestType(RequestDto requestDto, HttpRequestMessage message)
    {
        switch (requestDto.ApiType)
        {
            case ApiType.POST:
                message.Method = HttpMethod.Post;
                break;
            case ApiType.PUT:
                message.Method = HttpMethod.Put;
                break;
            case ApiType.DELETE:
                message.Method = HttpMethod.Delete;
                break;
            default:
                message.Method = HttpMethod.Get;
                break;
        }
    }

    private static void SetDataToRequest(RequestDto requestDto, HttpRequestMessage message)
    {
        if (requestDto.Data is not null && !string.IsNullOrWhiteSpace(Convert.ToString(requestDto.Data)))
        {
            message.Content = new StringContent(JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json");
        }
    }
}
