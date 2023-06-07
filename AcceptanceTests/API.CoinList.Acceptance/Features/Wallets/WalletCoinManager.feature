Feature: Manage Coin in a Wallet

Manage a coin in a wallet

Background: 
	Given The following coin in the database
		| Symbol | Name			| Value		|
		| BTC    | Bitcoin		| 169270.72 |
		| ETH    | Etherium		| 16927.72  |
		| USDT	 | Thether		| 0.9854	|
		| USDC	 | Usd Coin		| 0.9855	|


Scenario: Add Coin to a wallet
	Given An existing wallet with TestWallet as name and Test Wallet as description
	When I add 0.5123 BTC to the wallet
	Then Wallet sould have inside the correct value of coin

Scenario: Add Coin to a wallet and check his total value
	Given An existing wallet with TestWallet as name and Test Wallet as description
	When I add 0.5123 BTC to the wallet
	Then His total value should be 86717.39
