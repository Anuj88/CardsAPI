using System.Drawing;

namespace CardAPI.Helper
{
    public interface ICardHelper
    {
        HttpClient ApiClient { get; }
    }
    public class CardHelper : ICardHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        public CardHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

      
        public HttpClient ApiClient
        {
            get {
                _httpClient = _httpClientFactory.CreateClient();
                _httpClient.Timeout = new TimeSpan(0, 0, 30);
                return _httpClient;
            }
        }
        private void IntializeClientRequestHeader()
        {
            if (_httpClient != null)
            {
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
    }
}
