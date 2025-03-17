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
			Dictionary<string, object>? responseDictionary = new Dictionary<string, object>();
			Dictionary<string, object>? CompanyProfile = new Dictionary<string, object>();

			if (Ticker == null) 
			{
				responseDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
				CompanyProfile = await _finnhubService.GetCompanyProfile(_tradingOptions.Value.DefaultStockSymbol);
			}
			else 
			{
				responseDictionary = await _finnhubService.GetStockPriceQuote(Ticker);
				CompanyProfile = await _finnhubService.GetCompanyProfile(Ticker);
			}
		
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
