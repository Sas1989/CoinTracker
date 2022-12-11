Feature: Wallet Crud
Manage a crud for Wallet entity

Scenario Outline: Insert A new Wallet
	Given A new wallet with name <Name> 
	And a <Description> as description
	When I post this wallet
	Then Post action return Ok
	And The wallet is well saved

Examples: 
	| Name            | Description             |
	| My first wallet | This is my first wallet |
	| Second Wallet   |                         |