Feature: Delete Coin By Id

Delete Coin by ID


Scenario: Delete existing coin by Id
	Given A coin BTC with description Bitcoin and 20000 in the database
	When Delete coin using ID
	Then Coin is deleted

Scenario: Try To delete coin not in database
	Given A new Id no present in the application with random data
	When Delete coin using ID
	Then Recive a 404 status