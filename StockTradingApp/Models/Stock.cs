namespace StockTradingApp.Models
{
	public class Stock
	{
		public string? stockName { get; set; }
		public string? StockSymbol {  get; set; }
		public string? CurrentPrice {  get; set; }
		public string? LowestPrice { get; set; }
		public string? HighestPrice { get; set; }
		public string? OpenPrice { get; set; }

		public double getCurrentPrice() 
		{
			double price_Current = Convert.ToDouble(CurrentPrice);
			return price_Current;
		}

		public double getLowestPrice()
		{
			double price_Lowest = Convert.ToDouble(LowestPrice);
			return price_Lowest;
		}

		public double getHighestPrice()
		{
			double price_Highest = Convert.ToDouble(HighestPrice);
			return price_Highest;
		}

		public double getOpenPrice()
		{
			double price_Open = Convert.ToDouble(OpenPrice);
			return price_Open;
		}
	}
}
