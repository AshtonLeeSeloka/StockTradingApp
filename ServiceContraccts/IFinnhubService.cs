namespace ServiceContraccts
{
    //Service Contract (Interface)
    public interface IFinnhubService
    {

        //Dictionary<string,object>? GetCompanyProfile(string companyId);
        Dictionary<string, object>? GetStockPriceQuote(string companyId);

    }
}
