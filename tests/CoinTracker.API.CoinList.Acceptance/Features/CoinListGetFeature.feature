Feature: CoinListGetFeature

Scenario: Get all the coins
	Given The following coin in the database
		| Symbol | Name			| Value		|
		| BTC    | Bitcoin		| 169270.72 |
		| ETH    | Etherium		| 16927.72  |
		| USDT	 | Thether		| 0.9854	|
		| USDC	 | Usd Coin		| 0.9855	|
	When Request All the coin
	Then I recive all the coin

Scenario: Get one specific coin by ID
	Given A coin BNB with description BNB and 288.27 in the database
	When Request coin using his ID
	Then I recive the requested coin

@COIN-1
Scenario: Get one specific coin by Symbol
	Given A coin XRP with description XRP and 0.4948 in the database
	When Request coin using his Symbol
	Then I recive the requested coin

	Given A coin SAS not presen in the database
	When Request coin using his Symbol
	Then I recive a 404 error

Scenario: Get one coin that change Symbol
	Given A coin Luna Terra with value 1000
	And Change his symbol to Lunc
	When Request coin using his new Symbol
	Then I recive the requested coin