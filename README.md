## Stock Trading App

A simple stock viewer which consumes an external API from https://finnhub.io/

## Key Features

- Consumes External API using IHttpClientFactory
- Displays Ticker name and Stock price
- Dependancy Injection
- Razor Pages

## Application Setup

### Pre-requisites 
 - Create an account with https://finnhub.io/
 - Copy your API Key

### Running Application
- Initialize user Secret
  ```C#
  dotnet user-secrets init
  ```
- Set a Secret
  ```C#
  dotnet user-secrets set "FinHubApi" "you-API-key"
  ```
- Run the Application
- Add "/Ticker-Name" to the URL to View stock information
  
## UI Samples
### Main View
![UI](https://github.com/AshtonLeeSeloka/StockTradingApp/blob/master/StockTradingApp/wwwroot/Resources/Screenshot%202025-03-17%20185541.png)

 ## Stack
- C#
- Asp.Net Core
- Razor Pages
