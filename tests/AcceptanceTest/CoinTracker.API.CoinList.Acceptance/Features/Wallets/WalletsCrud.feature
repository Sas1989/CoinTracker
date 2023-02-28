Feature: Wallet Crud
Manage a crud for Wallet entity

Scenario Outline: Insert A new Wallet
	Given A new wallet with name <Name> and description <Description>
	When I post this wallet
	Then Recive a 200 status
	And The wallet is well saved

Examples: 
	| Name            | Description             |
	| My first wallet | This is my first wallet |
	| Second Wallet   |                         |

Scenario: Update a wallet
	Given An existing wallet with WalletToUpdate as name and WalletDescription as description
	When I update name with New Name 
	And I update description with New Description
	And I put this wallet
	Then Recive a 200 status
	And The wallet is well saved

Scenario: Update not exising wallet
	Given a wallet not in the database
	When I put this wallet
	Then Recive a 404 status
