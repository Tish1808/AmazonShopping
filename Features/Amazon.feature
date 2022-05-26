Feature: Buying 'Harry Potter and the Cursed Child'

@mytag
Scenario: Amazon Search Test
	Given I navigate to https://www.amazon.co.uk/
	Then I am on https://www.amazon.co.uk/
	When I search for 'Harry Potter and the Cursed Child'
	Then the page first items has the title 'Harry Potter and the Cursed Child - Parts One and Two'
	And the select type is 'Harry Potter and the Cursed Child - Parts One and Two'
	And the price is not null
	When I add book to basket
	Then I verify title of book is displayed as 'Harry Potter and the Cursed Child - Parts One and Two' and quantity is 1
