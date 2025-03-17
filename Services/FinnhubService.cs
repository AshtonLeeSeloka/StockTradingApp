using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceContraccts;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {

        private readonly IHttpClientFactory _httpClientFactory;
		private readonly IConfiguration _configuration;

		public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration ) 
        { 
            _httpClientFactory = httpClientFactory;
			_configuration = configuration;
		}

		public async Task< Dictionary<string, object>?> GetCompanyProfile(string companyId)
		{

            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {

                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/stock/profile2?symbol={companyId}&token={_configuration["FinHubApi"]}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync( httpRequestMessage );
                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader( stream );
                string response = streamReader.ReadToEnd();
                Dictionary<string,Object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string,Object>>(response);

				if (responseDictionary == null)
				{
					throw new InvalidOperationException("No Response from finhubb service");
				}

				//If Error throw Exception
				if (responseDictionary.ContainsKey("error"))
				{
					throw new InvalidOperationException(Convert.ToString(responseDictionary["Error"]));
				}

				return responseDictionary;
			}
		}

		//using acync
		public async Task<Dictionary< string,object>?> GetStockPriceQuote(string companyId)
        {
            //_httpClientFactory must be within using block as it is disposed of when closed
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                //To handle Request messages HttpRequest is used
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                { 
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={companyId}&token={_configuration["FinHubApi"]}"),
                    Method = HttpMethod.Get
                };

                //because calling an asynchronus methode use await
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                //to read response body 
                Stream stream = httpResponseMessage.Content.ReadAsStream();

                //create stream reader to read the stream 
                StreamReader streamReader = new StreamReader(stream);

                //read complete response from begginning to end and save as a string variable
                string response = streamReader.ReadToEnd();   

                //Converting Json to Dicstionary Object, add using System.Text.Json
                Dictionary<string,Object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                //If errors throw exception
                if (responseDictionary == null) 
                {
                    throw new InvalidOperationException("No Response from finhubb service");
                }

				//If Error throw Exception
				if (responseDictionary.ContainsKey("error"))
				{
					throw new InvalidOperationException(Convert.ToString(responseDictionary["Error"]));
				}

				return responseDictionary;

            }
        }
	}
}
