Feature: Languages Feature in the Profile Module

As a signed-in user of the Mars website,
I am able to add, update and delete the languages 
in the languages feature of Profile module

Background: Login

	Given I login to the website with valid email '<email>' and password '<password>'
	When I am on Profile page
	And I navigate to Languages section
	And I clear langauges data
	Then Langauges data is cleared

@Language
Scenario Outline: Add a Language with a valid language name and valid language level
	When I add a language in the Languages feature with a valid '<language>' name and valid level '<languageLevel>'
	Then the language is added successfully with the valid language '<language>' name and level and success message '<successMessage>' is displayed.
Examples:
	| language | languageLevel | successMessage                   |
	| Tamil    | Fluent        | has been added to your languages |

@Language
Scenario Outline: Add a Language with a combination of special characters inputs
	When I try to add a language with special characters as the '<language>' name and with a valid level '<languageLevel>'
	Then the language cannot be added with special characters '<language>', and an error '<message>' is displayed.
Examples:
	| language  | languageLevel | errorMessage                    |
	| abc#45### | Basic         | Please enter language and level |

@Language
Scenario Outline: Add a Language with long text as the language name
	When I try to add a language with very long text as the '<language>' name and with a valid level '<languageLevel>'
	Then the language '<language>' with long text cannot be added and an error '<errorMessage>' is displayed
Examples:
	| language                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            | languageLevel  | errorMessage                    |
	| In a world where technology evolves at an unprecedented pace, the ability to adapt and innovate has become more critical than ever. Every individual and organization must embrace a mindset of continuous learning and improvement to thrive in such an environment. Challenges will arise, but they present opportunities to grow stronger, wiser, and more resilient. Collaboration, creativity, and perseverance are the pillars of success, enabling us to overcome obstacles and build a brighter future together. Growth starts with action! | Conversational | Please enter language and level |

@Language
Scenario Outline: Add a language with spaces as the language name
	When I add a language with only spaces "    "  as the language name in the language textbox and with valid level '<languageLevel>'
	Then the language '<language>' is not added with spaces as the language name and an error '<errorMessage>' is displayed
Examples:
	| languageLevel | errorMessage                    |
	| Fluent        | Please enter language and level |

@Language
Scenario Outline: Add a language with malicious text as the language name
	When I add a language with malicious text '<language>' and valid level '<languageLevel>'
	Then the language '<language>' is not added with the malicious text and an error message '<errorMessage>' is displayed
Examples:
	| language                     | languageLevel | errorMessage                    |
	| <img src=x onerror=alert(1)> | Fluent        | Please enter language and level |

@Language
Scenario Outline: Add a Language without entering a language name and without selecting a language level
	When I add a language without entering a language name '<language>' in the language textbox and without selecting a language level
	Then the language is not added with an empty language textbox and language level fields and an error message '<errorMessage>' is displayed
Examples:
	| errorMessage                    |
	| Please enter language and level |

@Language
Scenario Outline: Add a Language with a valid language name and without selecting a level
	When I try to add a language with a valid language name '<language>' but without selecting a language level '<languageLevel>'
	Then the language is not added without selecting the language level and an error message '<errorMessage>'is displayed
Examples:
	| language | errorMessage                    |
	| Hindi    | Please enter language and level |

@Language
Scenario Outline: Add a Language without entering a language name but with a valid language level
	When I try to add a language '<language>' with an empty language textbox but with a valid language level '<languageLevel>'
	Then the language is not added with an empty language textbox and an error message '<errorMessage>' is displayed
Examples:
	| languageLevel    | errorMessage                    |
	| Native/Bilingual | Please enter language and level |

@Language
Scenario Outline: Add a Language with an existing language name in the language list
	When I add a valid language '<language>' and level '<languageLevel>'
	And I try to add a language with a duplicate language name '<language>' and level '<languageLevel>' in the language list
	Then the duplicate language cannot be added and an error message '<errorMessageOne>' or '<errorMessageTwo>' is displayed

Examples:
	| language | languageLevel | errorMessageOne | errorMessageTwo                                       |
	| Korean   | Basic         | Duplicated data | This language is already exist in your language list. |

@language
Scenario Outline: Update a Language with a valid language name and valid language level
	When I add a valid language '<language>' and level '<languageLevel>'
	And I try to update any language in the language list with a valid language name '<languageUpdate>' and valid language level '<languageLevelUpdate>'
	Then the language can be updated successfully with the valid language name '<languageUpdate>' and valid language level and success message '<successMessage>' is displayed

Examples:
	| language | languageLevel | languageUpdate | languageLevelUpdate | successMessage                     |
	| Russian  | Fluent        | Telugu         | Conversational      | has been updated to your languages |


@language
Scenario Outline: Update a Language with special characters inputs
	When I add a valid language '<language>' and level '<languageLevel>'
	When I try to update a language with special characters inputs '<languageUpdate>' and with valid level '<languageLevelUpdate>'
	Then the language cannot be updated with special characters  '<language>' and an error message '<errorMessage>' is displayed

Examples:
	| language | languageLevel | languageUpdate | languageLevelUpdate | errorMessage                    |
	| Russian  | Fluent        | ##%HNVrg6      | Basic               | Please enter language and level |


@Language
Scenario Outline: Update a Language with long text as the language name
	When I add a valid language '<language>' and level '<languageLevel>'
	When I try to update an existing language with very long text '<languageUpdate>' as the language name and with level '<updateLanguageLevel>'
	Then the language cannot be updated with a long text input '<languageUpdate>' and an error message '<errorMessage>' is displayed
Examples:
	| language | languageLevel | languageUpdate                                                                                                                                                                                                                                                                                    | updateLanguageLevel | errorMessage                    |
	| Russian  | Fluent        | The golden sun dipped below the horizon, painting the sky with hues of amber and violet as a gentle breeze rustled through the towering trees. The distant waves crashed rhythmically against the shore, their soothing melody blending with the whispers of the wind, creating a serene harmony. | Conversational      | Please enter language and level |

@language
Scenario Outline: Update a language with spaces as the language name

	When I add a valid language '<language>' and level '<languageLevel>'
	When I try to update a language with only spaces "   "  in the language textbox and with '<languageLevelUpdate>' language level
	Then the language is not updated with spaces '<languageUpdate>' and an error message '<errorMessage>' is displayed

Examples:
	| language | languageLevel | languageUpdate | languageLevelUpdate | errorMessage                    |
	| Russian  | Fluent        |                | Basic               | Please enter language and level |

@language
Scenario Outline: Update a language with malicious text as the language name
	When I add a valid language '<language>' and level '<languageLevel>'
	When I try to update a language and enter malicious text '<languageUpdate>' in the language textbox and with language level '<languageLevelUpdate>'
	Then the language is not updated with malicious data '<languageUpdate>' and an error message '<errorMessage>' is displayed
Examples:
	| language | languageLevel | languageUpdate                               | languageLevelUpdate | errorMessage                    |
	| Russian  | Fluent        | background: url(https://evil.com/steal.png); | Basic               | Please enter language and level |

@Language
Scenario Outline: Update a Language without entering a language name and without selecting language level
	When I add a valid language '<language>' and level '<languageLevel>'
	When I try to update a language without entering a language name '<language>' and without selecting language level
	Then the language is not updated without entering a language name and without selecting language level and an error message '<errorMessage>' is displayed
Examples:
	| language | languageLevel | languageUpdate | languageLevelUpdate | errorMessage                    |
	| Russian  | Fluent        |                |                     | Please enter language and level |

@Language
Scenario Outline: Update a Language with valid language name and without selecting language level
	When I add a valid language '<language>' and level '<languageLevel>'
	When I try to update a language with valid language name '<languageUpdate>' and without selecting language level '<languageLevelUpdate>'
	Then the language '<languageUpdate>' is not updated without selecting language level and an error '<errorMessage>' is displayed
Examples:
	| language | languageLevel | languageUpdate | languageLevelUpdate | errorMessage                    |
	| Russian  | Fluent        | Spanish        |                     | Please enter language and level |

@Language
Scenario Outline: Update a Language without entering a language name but selecting a language level
	When I add a valid language '<language>' and level '<languageLevel>'
	And I try to update a language without entering a language name '<languageUpdate>' but selecting a language level '<languageLevelUpdate>'
	Then the language is not updated with an empty language textbox and an error message '<errorMessage>' is displayed
Examples:
	| language | languageLevel | languageUpdate | languageLevelUpdate | errorMessage                    |
	| Russian  | Fluent        |                | Native/Bilingual    | Please enter language and level |

@Language
Scenario Outline: Update a Language with an existing language name in the language list
	When I add a valid language '<language>' and level '<languageLevel>'
	And I try to update a language with a duplicate language name '<language>' and level '<languageLevel>' in the language list
	Then the duplicate language cannot be updated and an error message '<errorMessageOne>' or '<errorMessageTwo>' is displayed

Examples:
	| language | languageLevel | errorMessageOne | errorMessageTwo                                       |
	| Korean   | Basic         | Duplicated data | This language is already added to your language list. |

@Language
Scenario Outline: Delete a Language from the language list
	When I add a valid language '<language>' and level '<languageLevel>'
	And I try to delete a language '<language>' from the language list
	Then the language '<language>' from the list is deleted successfully and a successful deletion message '<deleteSuccess>' is displayed
Examples:
	| language | languageLevel | deleteSuccess                        |
	| English  | Fluent        | has been deleted from your languages |


