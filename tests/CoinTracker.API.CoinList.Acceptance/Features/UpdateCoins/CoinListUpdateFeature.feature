Feature: CoinUpdates

A short summary of the feature


Scenario: Update Coin Value by Id
	Given The BTC bitcoin in the database with value 20000
	When Post a new value 40000 using his Id
	Then The value is changed sucessfully

Scenario: Update Coin Value by Symbol
	Given The Eth Ethereum in the database with value 1336
	When Post a new value 40000 using his Symbol
	Then The value is changed sucessfully

Scenario: Update Symbol and description Coin
	Given The Luna Terra in the database with value 100
	When Post a new symbol Lunc and TerraClassic
	Then The symbol and the description are changed

