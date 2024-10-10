using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{
	
	public class HomeController : Controller
	{
		private readonly FinnhubService _finnhubService;
		private readonly IOptions<TradingOptions> _tradingOptions;

		public HomeController(FinnhubService finnhubService,IOptions<TradingOptions> options) 
		{
			_finnhubService = finnhubService;
			_tradingOptions = options;
		}


		[Route("/Home")]
		//since methode is asynchronus add async and Type is Task<>
		public async Task<IActionResult> Index(string? Ticker)
		{

			Dictionary<string,object>? responseDictionary = await _finnhubService.GetStockPriceQuote(_tradingOptions.Value.DefaultStockSymbol);
			Stock stock = new Stock()
			{
				StockSymbol = _tradingOptions.Value.DefaultStockSymbol,
				CurrentPrice = responseDictionary["c"].ToString(),
				HighestPrice = responseDictionary["h"].ToString(),
				LowestPrice = responseDictionary["l"].ToString(),
				OpenPrice = responseDictionary["o"].ToString()
			};
			return View(stock);
		}
	}
}
