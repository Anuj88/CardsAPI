using CardAPI.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text.Json;
using System.Web;

namespace CardAPI.Services
{
    public interface ICardService
    {
       Task<string> GetCardsAsync();
       Task<string> GetFilterCardsAsync(string? name, string? color, string? type);
    }

    public class CardService : ICardService
    {
        private readonly ICardHelper _cardHelper;
        private readonly IConfiguration _configuration;
        private readonly string apiUrl;
        public CardService(ICardHelper cardHelper, IConfiguration configuration)
        {
            _cardHelper = cardHelper;
            _configuration = configuration;
            apiUrl = _configuration.GetValue<string>("AppSettings:CardApi");
        }

        public async Task<string> GetCardsAsync()
        {
            using (HttpResponseMessage response = await _cardHelper.ApiClient.GetAsync(apiUrl))
            {
                if(response.IsSuccessStatusCode)
                {

                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }
        
        public async Task<string> GetFilterCardsAsync(string? name, string? color, string? type)
        {
            NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);
            if (!string.IsNullOrEmpty(name))
            {
                query.Add("name", name);
            }
            if (!string.IsNullOrEmpty(color))
            {
                query.Add("color", color);
            }
            if(!string.IsNullOrEmpty(type))
            {
                query.Add("type", type);
            }

            using (HttpResponseMessage response = await _cardHelper.ApiClient.GetAsync($"{apiUrl}?{query.ToString()}"))
            {
                if (response.IsSuccessStatusCode)
                {

                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
