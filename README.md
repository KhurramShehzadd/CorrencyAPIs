# CorrencyAPIs
The project is created in .NET core 3.1 using ONION architecture. 

## Prerequisites
- [.NET Core SDK 3.1](https://dotnet.microsoft.com/download) installed on your machine.
- [Any other dependencies, e.g., a specific database or service]

## Getting Started

### Clone the Repository
Clone this repository to your local machine using the following command:

`git clone https://github.com/username/repositoryname.git`

# Run the Application
1. Nevigate to project directory:
    cd path/to/your/project
2. Restore the project dependencies:
    dotnet restore
3. Build the project:
    dotnet build
4. Run the project:
    dotnet run

The API should be accessible at http://localhost:5000

## API Endpoints
### 1. api/Currency/latest
**Description**: It will provide information of latest rates for all currencies

**Parameters**:
baseCurrency (query string): The base currency for which historical rates are requested (e.g., "USD").

**Response**: Example JSON response:
{
"amount": 1,
"base": "USD",
"rates": {
"AUD": 1.4823,
"BGN": 1.7579,
"BRL": 5.552,
"CAD": 1.358,
"CHF": 0.84433,
"CNY": 7.0963,
"CZK": 22.585,
"DKK": 6.7069,
"EUR": 0.8988,
"GBP": 0.75749,
"HKD": 7.7946,
"HUF": 353.97,
"IDR": 15366,
"ILS": 3.7427,
"INR": 83.86,
"ISK": 137.07,
"JPY": 139.91,
"KRW": 1318.91,
"MXN": 19.2369,
"MYR": 4.3015,
"NOK": 10.5878,
"NZD": 1.6141,
"PHP": 55.81,
"PLN": 3.8386,
"RON": 4.471,
"SEK": 10.1739,
"SGD": 1.2949,
"THB": 33.24,
"TRY": 33.978,
"ZAR": 17.6413
},
"date": "2024-09-16"
}

### 2. POST /api/Currency/convert
**Description**: This API can be used for conversion between two currencies.
**Request Body**:
{
  "baseCurrency": "string",
  "targetCurrency": "string",
  "amount": 0
}
**Response**: Example JSON response:
{
  "amount": 1,
  "base": "USD",
  "date": "2024-09-16",
  "rate": {
    "INR": 83.86
  }
}

### 3. GET /api/Currency/historical
**Description**: This API can be used to get historial data for any given currecny using pagination.

**Parameters**:
baseCurrency (query string): The base currency for which historical rates are requested (e.g., "USD").
startDate (query string): The start date for the historical data in YYYY-MM-DD format (e.g., "2024-01-01").
endDate (query string): The end date for the historical data in YYYY-MM-DD format (e.g., "2024-09-16").
page (query string, optional): The page number for pagination. Default is 1.
pageSize (query string, optional): The number of items per page for pagination. Default is 10.

**Response**: Example JSON response:
{
  "amount": 1,
  "base": "USD",
  "rates": {
    "2024-01-02": {
      "AUD": 1.4738,
      "BGN": 1.7851,
      "BRL": 4.8888,
      "CAD": 1.3294,
      "CHF": 0.84931,
      "CNY": 7.1435,
      "CZK": 22.533,
      "DKK": 6.8046,
      "EUR": 0.91274,
      "GBP": 0.79085,
      "HKD": 7.8139,
      "HUF": 348.76,
      "IDR": 15524,
      "ILS": 3.624,
      "INR": 83.32,
      "ISK": 137.55,
      "JPY": 142.1,
      "KRW": 1313.23,
      "MXN": 17.058,
      "MYR": 4.6025,
      "NOK": 10.2971,
      "NZD": 1.5947,
      "PHP": 55.66,
      "PLN": 3.9894,
      "RON": 4.5368,
      "SEK": 10.1812,
      "SGD": 1.3265,
      "THB": 34.285,
      "TRY": 29.726,
      "ZAR": 18.5889
    },
    "2024-01-03": {
      "AUD": 1.4869,
      "BGN": 1.7912,
      "BRL": 4.9326,
      "CAD": 1.3347,
      "CHF": 0.85374,
      "CNY": 7.1487,
      "CZK": 22.598,
      "DKK": 6.8304,
      "EUR": 0.91583,
      "GBP": 0.79192,
      "HKD": 7.8081,
      "HUF": 348.7,
      "IDR": 15564,
      "ILS": 3.6512,
      "INR": 83.3,
      "ISK": 138.02,
      "JPY": 143.02,
      "KRW": 1311.73,
      "MXN": 17.097,
      "MYR": 4.631,
      "NOK": 10.3672,
      "NZD": 1.6041,
      "PHP": 55.59,
      "PLN": 3.9965,
      "RON": 4.554,
      "SEK": 10.2496,
      "SGD": 1.3282,
      "THB": 34.45,
      "TRY": 29.781,
      "ZAR": 18.8048
    }
  },
  "startDate": "2024-01-02",
  "endDate": "2024-01-19",
  "pageNumber": "1",
  "pageSize": "10"
}

# Enhancements:
**Add Advanced Filtering**: Include more options for filtering historical data based on different criteria.
**Optimize Pagination**: Implement more advanced pagination techniques for better performance.
**Tests**: Separate project for all type of Tests, currently Tests are part of same project.
**Caching**: In order to make it more performant, should introduce caching for faster responses and reducing the load on Frankfurt APIs
**Detailed Responses**: Error response could be enhanced further in order to keep the applciation scalable and more maintainble.
