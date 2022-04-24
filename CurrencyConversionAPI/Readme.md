**(1)** Requirement:


	Create CurrencyConversionAPI that

 	(a) Calls third party service for currency conversion
 
 	(b) Stores userid and currency conversion information in database
 
 	(c) Fetches currency conversion data for given user



**(2)** Tech stack

	(a) ASP.NET WEB API (NET5.0)

	(b) Visual Studio 19, Oracle DB

	(c) Third party Web service (https://www.exchangerate-api.com/docs/pair-conversion-requests)



**(3)** Design:

	(a) Create an ASP.NET Core Web API (Model View Controller with Entity Framework)

	(b) API Endpoints

		(i)   GET:  https://localhost:5001/api/currencyconversion/               -get all currency conversion data from database

		(ii)  GET:  https://localhost:5001/api/currencyconversion/{cutomerid}    -get all currency conversion data for customerid

		(iii) POST: https://localhost:5001/api/currencyconversion/               -validate input, call third party api for target amount, save data in database
                                                                          -### If customerid is empty then exchange data will be provided but 
                                                                           not saved to DB ###
																																					 
																																					 
		Post Request in JSON format : 
		{"customerid": "<validid>", "basecurrency": "<valid currency>", "baseamount": <valid amount>, "targetcurrency": "valid currency>" } 
  
(c) Validation
	
     - BaseCurrency, TargetCurrency should be valid
	
     - BaseAmount should be > 0 and < 1000000
																						 
     - CustomerId if present the should be max 10 chars. And should not have numbers.
                                           
          
																						 
																						 
**(4)** Testing
																						 
    (a) Postman local testing (https://localhost:5001/api/currencyconversion/)
																						 
    Example Request
    {
        "customerid": "sumtest",
        "basecurrency": "USD",
        "baseamount": 1.5,
        "targetcurrency": "EUR"
    }
    
    (b) Nunit test cases
																						 
																						 
