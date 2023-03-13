Feature: Get One specific coin using an ID

Get one specific coin using an ID

Scenario: Coin exist in the database
	Given A coin BTC with description Bitcoin and 20000 in the database
	When Get coin using ID
	Then I recive the requested coin

Scenario: Coin doesn't exist in the database
	Given A coin not present in the database
	When Get coin using ID
	Then Recive a 404 status