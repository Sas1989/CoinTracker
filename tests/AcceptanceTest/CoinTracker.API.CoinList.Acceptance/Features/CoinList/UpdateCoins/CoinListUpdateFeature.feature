Feature: CoinUpdates

A short summary of the feature


Scenario: Update Coin Value by Id
	Given A coin BTC with description bitcoin and 20000 in the database
	When Put a new value 40000 using his Id
	Then The coin is updated sucessfully

Scenario: Update Coin Value by Symbol
	Given A coin Eth with description Ethereum and 1336 in the database
	When Put a new value 40000 using his Symbol
	Then The coin is updated sucessfully

Scenario: Update Symbol and description Coin
	Given A coin Luna with description Terra and 100 in the database
	When Put a new symbol Lunc and TerraClassic
	Then The coin is updated sucessfully

Scenario: Update Coin with a wrong Id
	Given A new Id no present in the application with random data
	When Put a new value 10 using his Id 
	Then Recive a 404 status