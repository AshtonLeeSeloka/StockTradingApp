using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockTradingApp.Models;
using ServiceContraccts;
using Services;

namespace StockTradingApp.Controllers
{
	public class TradeController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IOptions<TradingOptions> _tradingOptions;
		private readonly IConfiguration _configuration;

		public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> options, IConfiguration configuration)
		{
			_finnhubService = finnhubService;
			_tradingOptions = options;
			_configuration = configuration;
		}


		[Route("/{Ticker?}")]
		[Route("[action]")]
		[Route("~/[controller]")]
		//since methode is asynchronus add async and Type is Task<>
		public async Task<IActionResult> Index(string? Ticker)
		{

			//Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
			Dictionary<string, object>? responseDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
			Dictionary<string, object>? CompanyProfile = await _finnhubService.GetCompanyProfile(_tradingOptions.Value.DefaultStockSymbol);
			Stock stock = new Stock()
			{
				stockName = CompanyProfile["name"].ToString(),
				StockSymbol = CompanyProfile["ticker"].ToString(),
				CurrentPrice = responseDictionary["c"].ToString(),
				HighestPrice = responseDictionary["h"].ToString(),
				LowestPrice = responseDictionary["l"].ToString(),
				OpenPrice = responseDictionary["o"].ToString()
			};

			ViewBag.FinnhubToken = _configuration["FinHubApi"];
			return View(stock);


		}
	}
}
