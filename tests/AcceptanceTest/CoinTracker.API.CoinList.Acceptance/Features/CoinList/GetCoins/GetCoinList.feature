Feature: Get all the coin list

Get all the coin list 


Scenario: Get all the coins
	Given The following coin in the database
		| Symbol | Name			| Value		|
		| BTC    | Bitcoin		| 169270.72 |
		| ETH    | Etherium		| 16927.72  |
		| USDT	 | Thether		| 0.9854	|
		| USDC	 | Usd Coin		| 0.9855	|
	When Get All coins
	Then I recive all the coin