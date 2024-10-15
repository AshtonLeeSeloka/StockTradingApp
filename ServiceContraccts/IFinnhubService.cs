namespace ServiceContraccts
{
    //Service Contract (Interface)
    public interface IFinnhubService
    {

       public Task<Dictionary<string, object>?> GetCompanyProfile(string companyId);
       public  Task<Dictionary<string, object>?> GetStockPriceQuote(string companyId);

    }
}
