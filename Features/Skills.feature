Feature: Skills Feature in the Profile Module

As a signed-in user of the Mars website,
I am able to add, update and delete the skills 
in the skills feature of Profile module

Background: Login

	Given I login to the website with valid email '<email>' and password '<password>'
	When I am on Profile page
	And I navigate to Skills section
	And I clear skills data
	Then skills data is cleared

@skill
Scenario Outline: Add a skill with a valid skill name and valid skill level

	When I add a skill in the skills feature with a valid '<skill>' name and valid level '<skillLevel>'
	Then the skill is added successfully with the valid skill '<skill>' name and level and success message '<successMessage>' is displayed.
Examples:
	| skill            | skillLevel   | successMessage                |
	| Software Testing | Intermediate | has been added to your skills |

@skill
Scenario Outline: Add a skill with a combination of special characters inputs

	When I try to add a skill with special characters as the '<skill>' name and with a valid level '<skillLevel>'
	Then the skill cann be added with special characters '<skill>', and a success message '<successMessage>' is displayed.
Examples:
	| skill | skillLevel | successMessage                |
	| C++   | Beginner   | has been added to your skills |

@skill
Scenario Outline: Add a skill with long text as the skill name
	When I try to add a skill with very long text as the '<skill>' name and with a valid level '<skillLevel>'
	Then the skill '<skill>' with long text cannot be added and an error '<errorMessage>' is displayed
Examples:
	| skill                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               | skillLevel | errorMessage                            |
	| In a world where technology evolves at an unprecedented pace, the ability to adapt and innovate has become more critical than ever. Every individual and organization must embrace a mindset of continuous learning and improvement to thrive in such an environment. Challenges will arise, but they present opportunities to grow stronger, wiser, and more resilient. Collaboration, creativity, and perseverance are the pillars of success, enabling us to overcome obstacles and build a brighter future together. Growth starts with action! | Expert     | Please enter skill and experience level |

@skill
Scenario Outline: Add a skill with spaces as the skill name
	When I add a skill with only spaces "   " as the name in the skill textbox and with valid level '<skillLevel>'
	Then the skill '<skill>' is not added with spaces as the skill name and an error '<SuccessMessage>' is displayed
Examples:
	| skillLevel | SuccessMessage                          |
	| Expert     | Please enter skill and experience level |

@skill
Scenario Outline: Add a skill with malicious text as the skill name
	When I add a skill with malicious text '<skill>' and valid level '<skillLevel>'
	Then the skill '<skill>' is not added with the malicious text and an error message '<errorMessage>' is displayed
Examples:
	| skill                        | skillLevel   | errorMessage                            |
	| <img src=x onerror=alert(1)> | Intermediate | Please enter skill and experience level |

@skill
Scenario Outline: Add a skill without entering a skill name and without selecting a skill level
	When I add a skill without entering a skill name '<skill>' in the skill textbox and without selecting a skill level
	Then the skill is not added with an empty skill textbox and skill level fields and an error message '<errorMessage>' is displayed
Examples:
	| errorMessage                            |
	| Please enter skill and experience level |

@skill
Scenario Outline: Add a skill with a valid skill name and without selecting a level
	When I try to add a skill with a valid skill name '<skill>' but without selecting a skill level '<skillLevel>'
	Then the skill is not added without selecting the skill level and an error message '<errorMessage>'is displayed
Examples:
	| skill | errorMessage                            |
	| Java  | Please enter skill and experience level |

@skill
Scenario Outline: Add a skill without entering a skill name but with a valid skill level
	When I try to add a skill with an empty skill '<skill>' textbox but with a valid skill level '<skillLevel>'
	Then the skill is not added with an empty skill textbox and an error message '<errorMessage>' is displayed
Examples:
	| skillLevel | errorMessage                            |
	| Expert     | Please enter skill and experience level |

@skill
Scenario Outline: Add a skill with an existing skill name in the skill list
	When I add a valid skill '<skill>' and level '<skillLevel>'
	And I try to add a skill with a duplicate skill name '<skill>' and level '<skillLevel>' in the skill list
	Then the duplicate skill cannot be added and an error message '<errorMessageOne>' or '<errorMessageTwo>' is displayed

Examples:
	| skill              | skillLevel   | errorMessageOne | errorMessageTwo                                 |
	| Automation Testing | Intermediate | Duplicated data | This skill is already exist in your skill list. |

@skill
Scenario Outline: Update a skill with a valid skill name and valid skill level
	When I add a valid skill '<skill>' and level '<skillLevel>'
	And I try to update any skill in the skill list with a valid skill name '<skillUpdate>' and valid skill level '<skillLevelUpdate>'
	Then the skill can be updated successfully with the valid skill name '<skillUpdate>' and valid skill level and success message '<successMessage>' is displayed

Examples:
	| skill         | skillLevel | skillUpdate | skillLevelUpdate | successMessage                  |
	| Agile Testing | Expert     | Java        | Beginner         | has been updated to your skills |


@skill
Scenario Outline: Update a skill with special characters inputs
	When I add a valid skill '<skill>' and level '<skillLevel>'
	When I try to update a skill with special characters inputs '<skillUpdate>' and with valid level '<skillLevelUpdate>'
	Then the skill cannot be updated with special characters '<skillUpdate>' and an error message '<successMessage>' is displayed

Examples:
	| skill       | skillLevel | skillUpdate | skillLevelUpdate | successMessage                  |
	| API Testing | Expert     | C#          | Beginner         | has been updated to your skills |


@skill
Scenario Outline: Update a skill with long text as the skill name
	When I add a valid skill '<skill>' and level '<skillLevel>'
	When I try to update an existing skill with very long text '<skillUpdate>' as the skill name and with level '<updateSkillLevel>'
	Then the skill cannot be updated with a long text input '<skillUpdate>' and an error message '<errorMessage>' is displayed
Examples:
	| skill | skillLevel   | skillUpdate                                                                                                                                                                                                                                                                                       | updateSkillLevel | errorMessage                            |
	| SQL   | Intermediate | The golden sun dipped below the horizon, painting the sky with hues of amber and violet as a gentle breeze rustled through the towering trees. The distant waves crashed rhythmically against the shore, their soothing melody blending with the whispers of the wind, creating a serene harmony. | Intermediate     | Please enter skill and experience level |

@skill
Scenario Outline: Update a skill with spaces as the skill name

	When I add a valid skill '<skill>' and level '<skillLevel>'
	When I try to update a skill with only spaces '<skillUpdate>' in the skill textbox and with '<skillLevelUpdate>' skill level
	Then the skill is not updated with spaces '<skillUpdate>' and an error message '<errorMessage>' is displayed

Examples:
	| skill                | skillLevel | skillUpdate | skillLevelUpdate | errorMessage                            |
	| FrontEnd Development | Expert     |             | Beginner         | Please enter skill and experience level |

@skill
Scenario Outline: Update a skill with malicious text as the skill name
	When I add a valid skill '<skill>' and level '<skillLevel>'
	When I try to update a skill and enter malicious text '<skillUpdate>' in the skill textbox and with skill level '<skillLevelUpdate>'
	Then the skill is not updated with malicious data '<skillUpdate>' and an error message '<errorMessage>' is displayed
Examples:
	| skill  | skillLevel | skillUpdate                                  | skillLevelUpdate | errorMessage                            |
	| Python | Beginner   | background: url(https://evil.com/steal.png); | Intermediate     | Please enter skill and experience level |

@skill
Scenario Outline: Update a skill without entering a skill name and without selecting skill level
	When I add a valid skill '<skill>' and level '<skillLevel>'
	When I try to update a skill without entering a skill name '<skill>' and without selecting skill level
	Then the skill is not updated without entering a skill name and without selecting skill level and an error message '<errorMessage>' is displayed
Examples:
	| skill | skillLevel | skillUpdate | skillLevelUpdate | errorMessage                            |
	| Ruby  | Expert     |             |                  | Please enter skill and experience level |

@skill
Scenario Outline: Update a skill with valid skill name and without selecting skill level
	When I add a valid skill '<skill>' and level '<skillLevel>'
	When I try to update a skill with valid skill name '<skillUpdate>' and without selecting skill level '<skillLevelUpdate>'
	Then the skill '<skillUpdate>' is not updated without selecting skill level and an error '<errorMessage>' is displayed
Examples:
	| skill | skillLevel | skillUpdate | skillLevelUpdate | errorMessage                            |
	| DBMS  | Beginner   | MySQL       |                  | Please enter skill and experience level |

@skill
Scenario Outline: Update a skill without entering a skill name but selecting a skill level
	When I add a valid skill '<skill>' and level '<skillLevel>'
	And I try to update a skill without entering a skill name '<skillUpdate>' but selecting a skill level '<skillLevelUpdate>'
	Then the skill is not updated with an empty skill textbox and an error message '<errorMessage>' is displayed
Examples:
	| skill | skillLevel   | skillUpdate | skillLevelUpdate | errorMessage                            |
	| Java  | Intermediate |             | Expert           | Please enter skill and experience level |

@skill
Scenario Outline: Update a skill with an existing skill name in the skill list
	When I add a valid skill '<skill>' and level '<skillLevel>'
	And I try to update a skill with a duplicate skill name '<skill>' and level '<skillLevel>' in the skill list
	Then the duplicate skill cannot be updated and an error message '<errorMessageOne>' or '<errorMessageTwo>' is displayed

Examples:
	| skill  | skillLevel | errorMessageOne | errorMessageTwo                                 |
	| Python | Expert     | Duplicated data | This skill is already added to your skill list. |

@skill
Scenario Outline: Delete a skill from the skill list
	When I add a valid skill '<skill>' and level '<skillLevel>'
	And I try to delete a skill '<skill>' from the skill list
	Then the skill '<skill>' from the list is deleted successfully and a successful deletion message '<SuccessMessage>' is displayed
Examples:
	| skill | skillLevel | SuccessMessage   |
	| C++   | Beginner   | has been deleted |


