Feature: Skills Feature in the Profile Module

As a signed-in user of the Mars website,
I am able to add, update and delete the skills 
in the skills feature of Profile module

Background:
Given I am on Profile page
And I navigate to Skills section

@skill
Scenario: Add a skill with a valid skill name and valid skill level

When I add a skill in the skills feature with a valid skill name and valid skill level,
Then the skill is added successfully with the valid skill name and skill level.

@skill
Scenario: Add a skill with a combination of special characters and alphabetic text inputs

When I try to add a skill with a combination of special characters and alphabetic text as the skill name,
Then the skill cannot be added with special characters and alphabetic text, and an error message is displayed.


@skill
Scenario: Add a skill with long text as the skill name
When I try to add a skill with very long text as the skill name
Then the skill with long text cannot be added and an error message is displayed

@skill
Scenario: Add a skill with spaces as the skill name
When I add a skill with only spaces as the skill name in the skill textbox
Then the skill is not added with spaces as the skill name and an error message is displayed

@skill
Scenario: Add a skill with malicious text as the skill name
When I add a skill with malicious text as the skill name in the skill textbox
Then the skill is not added with the malicious text and an error message is displayed

@skill
Scenario: Add a skill without entering a skill name and without selecting a skill level
When I add a skill without entering a skill name in the skill textbox and without selecting a skill level
Then the skill is not added with empty skill textbox and skill level fields and an error message is displayed

@skill
Scenario: Add a skill with a valid skill name and without selecting a level
When I try to add a skill with a valid skill name but without selecting a skill level
Then the skill is not added without selecting the skill level and an error is displayed

@skill
Scenario: Add a skill without entering a skill name but with a valid skill level
When I try to add a skill with an empty skill textbox but with a valid skill level
Then the skill is not added with an empty skill textbox and an error message is displayed

@skill
Scenario: Add a skill with an existing skill name in the skill list
When I try to add a skill with an existing skill name in the skill list
Then the duplicate skill cannot be added and an error is displayed

@skill @skillRequired
Scenario: Update a skill with a valid skill name and valid skill level
Given that skills exist under the skills feature
When I try to update any skill in the skill list with a valid skill name and valid skill level
Then the skill can be edited successfully with the valid skill name and valid skill level

@skill @skillRequired
Scenario: Update a skill with a combination of special characters and alphabetic text inputs
When I try to update a skill with a combination of special characters and alphabetic text inputs
Then the skill cannot be updated with special characters and an error message is displayed

@skill
Scenario: Update a skill with long text as the skill name
When I try to update an existing skill with very long text as the skill name
Then the skill cannot be updated with a long text input and an error message is displayed

@skillRequired @skill
Scenario: Update a skill with spaces as the skill name
When I try to update a skill and leave only spaces in the skill textbox
Then the skill is not updated with spaces and an error message is displayed

@skillRequired @skill
Scenario: Update a skill with malicious text as the skill name
When I try to update a skill and enter malicious text in the skill textbox
Then the skill is not updated with malicious data and an error message is displayed

@skill @skillRequired
Scenario: Update a skill without entering a skill name and without selecting skill level
When I try to update a skill without entering a skill name and without selecting skill level
Then the skill is not updated without entering a skill name and without selecting skill level

@skill @skillRequired
Scenario: Update a skill with valid skill name and without selecting skill level
When I try to update a skill with valid skill name and without selecting skill level
Then the skill is not updated without selecting skill level and an error is displayed

@skill @skillRequired
Scenario: Update a skill without entering a skill name but selecting a skill level
When I try to update a skill without entering a skill name but selecting a skill level
Then the skill is not updated with an empty skill textbox and an error message is displayed

@skill @skillRequired
Scenario: Update a skill with an existing skill name in the skill list
When I try to update a skill with an existing skill name in the skill list
Then the duplicate skill cannot be updated and an error is displayed

@skillRequired @skill
Scenario: Delete a skill from the skill list
When I try to delete a skill from the skill list
Then the skill from the list is deleted successfully

