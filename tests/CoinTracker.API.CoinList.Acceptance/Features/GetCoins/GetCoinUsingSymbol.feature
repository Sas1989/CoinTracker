Feature: Get Coin using symbol

Get one coin using symbol

Scenario: Get one specific coin by Symbol
	Given A coin BTC with description Bitcoin and 20000 in the database
	When Request coin using his Symbol
	Then I recive the requested coin

Scenario: Ask for Symbol not in the database
	Given A coin SAS not presen in the database
	When Request coin using his Symbol
	Then Recive a 404 status
