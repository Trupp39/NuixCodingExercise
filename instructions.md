General Information

	- It is a restAPI written in C# .NET 9
	- The database I used was SqlServer
	- Routes
		- Get a list of current investments for the user
		- http://localhost:5146/user/{userId}/investments
		- The return object looks like the following
			- [
				{
					"investmentId": 2,
					"investmentName": "SPY"
				},
				{
					"investmentId": 3,
					"investmentName": "SPX"
				}
			  ]
			  
		- Get details for a user's investment  
		- http://localhost:5146/user/{userId}/investment/{investmentId}
		- The return object looks like the following
			- {
				"id": 2,
				"name": "SPY",
				"costBasisPerShare": 4.00,
				"currentValue": 5.00,
				"currentPrice": 5.00,
				"term": "Short",
				"totalGains": 12.00,
				"numberOfShares": 2.00
			  }
			  
	
Setup

	- The project needs the following packages to be installed
		- EntityFrameworkCore 9.0.11
		- EntityFrameworkCore.InMemory 9.0.11
		- EntityFrameworkCore.SqlServer 9.0.11
		- EntityFrameworkCore.Tools 9.0.11
		- Microsoft.NET.Test.Sdk 18.0.1
		- Moq 4.20.72
		- NUnit 4.4.0
		- NUnitTestAdapter 5.2.0
		
	- The integration test project needs the following to be installed
		- coverlet.collector 6.0.2
		- Microsoft.AspNetCore.Mvc.Testing 9.0.11
		- EntityFrameworkCore 9.0.11
		- EntityFrameworkCore.InMemory 9.0.11
		- Microsoft.NET.Test.Sdk 18.0.1
		- NUnit 4.4.0
		- NUnitTestAdapter 5.2.0
		
		
	I used .NET migrations to handle the database to get your database ready
		- Update the connectionString in appsettings.json
		- run Update-Database in the Package Manager Console
		
	At this point if you are running in Visual Studio you can press Play to start the API
	or navigate to InvestmentPerformanceAPI and run dotnet run
	
	
Testing

	- There are integration tests and unit tests
	- Unit Tests are located in the InvestmentPerfomanceAPI and Integration Tests are located in InvestmentPerfomanceAPI.IntegrationTests
	
	-Integration Tests use an inMemoryDatabase
	
	- All tests should be passing and can be run via the test explorer in Visual Studio
	
Misc

	- I added a little frontend mockup as a png in the project
	- Since a frontend component was not a part of this exercise, I wanted to illustrate how you might use both of these endpoints together