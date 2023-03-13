Feature: CoinListManagement

Insert in the database newcoin

Scenario: Insert A new Coin in the system
	Given A new coin BTC with description Bitcoin and 20000 vaule is addeded
	When Post the coin
	Then Recive a 200 status
	And Recive the coin sent with a new id created
	And The coin is saved sucessfully

Scenario: Insert an Invalid Coin in the system
	Given A new coin ETH with description Etherium with a negative value
	When Post the coin
	Then Recive en error message

Scenario: Insert multiple Coin
	Given A new coins with the following value:
		| Symbol | Name			| Value		|
		| ETH    | Etherium		| 16927.72  |
		| USDT	 | Thether		| 0.9854	|
		| USDC	 | Usd Coin		| 0.9855	|
		| BNB    | BNB			| 288.27    |
	When Post the coins
	Then Recive a 200 status
	And The coins are in the coin list

