Feature: Languages Feature in the Profile Module

As a signed-in user of the Mars website,
I am able to add, update and delete the languages 
in the languages feature of Profile module

Background: 
Given I am on Profile page
And I navigate to Languages section

@Language
Scenario: Add a Language with a valid language name and valid language level

When I add a language in the Languages feature with a valid language name and valid language level,
Then the language is added successfully with the valid language name and language level.

@Language
Scenario: Add a Language with a combination of special characters and alphabetic text inputs

When I try to add a language with a combination of special characters and alphabetic text as the language name,
Then the language cannot be added with special characters and alphabetic text, and an error message is displayed.


@Language
Scenario: Add a Language with long text as the language name
When I try to add a language with very long text as the language name
Then the language with long text cannot be added and an error message is displayed

@Language
Scenario: Add a language with spaces as the language name
When I add a language with only spaces as the language name in the language textbox
Then the language is not added with spaces as the language name and an error message is displayed

@Language
Scenario: Add a language with malicious text as the language name
When I add a language with malicious text as the language name in the language textbox
Then the language is not added with the malicious text and an error message is displayed

@Language
Scenario: Add a Language without entering a language name and without selecting a language level
When I add a language without entering a language name in the language textbox and without selecting a language level
Then the language is not added with an empty language textbox and language level fields and an error message is displayed

@Language
Scenario: Add a Language with a valid language name and without selecting a level
When I try to add a language with a valid language name but without selecting a language level
Then the language is not added without selecting the language level and an error is displayed

@Language
Scenario: Add a Language without entering a language name but with a valid language level
When I try to add a language with an empty language textbox but with a valid language level
Then the language is not added with an empty language textbox and an error message is displayed

@Language
Scenario: Add a Language with an existing language name in the language list
When I try to add a language with an existing language name in the language list
Then the duplicate language cannot be added and an error is displayed

@language 
Scenario: Update a Language with a valid language name and valid language level
Given that languages exist under the languages feature
When I try to update any language in the language list with a valid language name and valid language level
Then the language can be updated successfully with the valid language name and valid language level

@language @
Scenario: Update a Language with a combination of special characters and alphabetic text inputs
When I try to update a language with a combination of special characters and alphabetic text inputs
Then the language cannot be updated with special characters and an error message is displayed

@Language
Scenario: Update a Language with long text as the language name
When I try to update an existing language with very long text as the language name
Then the language cannot be updated with a long text input and an error message is displayed

@ @language
Scenario: Update a language with spaces as the language name
When I try to update a language and leave only spaces in the language textbox
Then the language is not updated with spaces and an error message is displayed

@ @language
Scenario: Update a language with malicious text as the language name
When I try to update a language and enter malicious text in the language textbox
Then the language is not updated with malicious data and an error message is displayed

@Language @
Scenario: Update a Language without entering a language name and without selecting language level
When I try to update a language without entering a language name and without selecting language level
Then the language is not updated without entering a language name and without selecting language level

@Language @
Scenario: Update a Language with valid language name and without selecting language level
When I try to update a language with valid language name and without selecting language level
Then the language is not updated without selecting language level and an error is displayed

@Language @
Scenario: Update a Language without entering a language name but selecting a language level
When I try to update a language without entering a language name but selecting a language level
Then the language is not updated with an empty language textbox and an error message is displayed

@Language @
Scenario: Update a Language with an existing language name in the language list
When I try to update a language with an existing language name in the language list
Then the duplicate language cannot be updated and an error is displayed

@ @Language
Scenario: Delete a Language from the language list
When I try to delete a language from the language list
Then the language from the list is deleted successfully

