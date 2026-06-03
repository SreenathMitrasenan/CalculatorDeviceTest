Feature: CalculatorDeviceTest

As a user I want to test calculator functionalities before major releases

@smoke
Scenario Outline: 01_Verify calculator application displays all numerals/characters on screen
	Given I launch calculator application
	Then I click on <ButtonName> webelement present in CalculatorHome page
	When I verify the element is displayed on screen as <ExpectedValue>
	Then I click on cancelButton webelement present in CalculatorHome page 
  Examples: 
    | ButtonName     | ExpectedValue |
    | zeroButton     | 0             |
    | oneButton      | 1             |
    | twoButton      | 2             |
    | threeButton    | 3             |
    | fourButton     | 4             |
    | fiveButton     | 5             |
    | sixButton      | 6             |
    | sevenButton    | 7             |
    | eightButton    | 8             |
    | nineButton     | 9             |
    | sinButton      | sin           |
    | cosButton      | cos           |
    | tanButton      | tan           |
    | logButton      | log           |
    | multiplyButton | *             |
    | divideButton   | /             |
    | minusButton    | −             |
    | addButton      | +             |
    

@smoke
Scenario: 02_Verify calculator application performs bodmas operations
	Given I launch calculator application
	Then I click on fourButton webelement present in CalculatorHome page 
	Then I click on multiplyButton webelement present in CalculatorHome page 
	Then I click on leftParanthesisButton webelement present in CalculatorHome page 
	Then I click on fourButton webelement present in CalculatorHome page 
	Then I click on plusButton webelement present in CalculatorHome page 
	Then I click on eightButton webelement present in CalculatorHome page 
	Then I click on rightParanthesisButton webelement present in CalculatorHome page 
	Then I click on divideButton webelement present in CalculatorHome page 
	Then I click on twoButton webelement present in CalculatorHome page 
	Then I click on divideButton webelement present in CalculatorHome page 
	Then I click on fiveButton webelement present in CalculatorHome page 
	Then I click on equalButton webelement present in CalculatorHome page 
	When I verify the element is displayed on screen as 4.8

	@smoke
Scenario: 04_Verify calculator application performs minus operations
	Given I launch calculator application
	Then I click on eightButton webelement present in CalculatorHome page 
	Then I click on minusButton webelement present in CalculatorHome page 
	Then I click on fourButton webelement present in CalculatorHome page 
	Then I click on equalButton webelement present in CalculatorHome page 
	When I verify the element is displayed on screen as 4

	@smoke
Scenario: 05_Verify calculator application performs decimal multiplication operations
	Given I launch calculator application
	Then I click on eightButton webelement present in CalculatorHome page 
	Then I click on dotButton webelement present in CalculatorHome page 
	Then I click on zeroButton webelement present in CalculatorHome page
	Then I click on eightButton webelement present in CalculatorHome page
	Then I click on multiplyButton webelement present in CalculatorHome page 
	Then I click on nineButton webelement present in CalculatorHome page 
	Then I click on dotButton webelement present in CalculatorHome page 
	Then I click on zeroButton webelement present in CalculatorHome page
	Then I click on nineButton webelement present in CalculatorHome page 
	Then I click on equalButton webelement present in CalculatorHome page 
	When I verify the element is displayed on screen as 73.4472

Scenario: 06_Verify calculator application performs zero division operations
	Given I launch calculator application
	Then I click on zeroButton webelement present in CalculatorHome page 
	Then I click on divideButton webelement present in CalculatorHome page 
	Then I click on nineButton webelement present in CalculatorHome page
	Then I click on equalButton webelement present in CalculatorHome page 
	When I verify the element is displayed on screen as 0
	Then I click on cancelButton webelement present in CalculatorHome page 
	Then I click on nineButton webelement present in CalculatorHome page 
	Then I click on dvideButton webelement present in CalculatorHome page 
	Then I click on zeroButton webelement present in CalculatorHome page
	Then I click on equalButton webelement present in CalculatorHome page 
	When I verify the element is displayed on screen as infinity

	@smoke
	Scenario: 03_Verify calculator application performs decimal division operations
	Given I launch calculator application
	Then I click on eightButton webelement present in CalculatorHome page 
	Then I click on dotButton webelement present in CalculatorHome page 
	Then I click on zeroButton webelement present in CalculatorHome page
	Then I click on eightButton webelement present in CalculatorHome page
	Then I click on multiplyButton webelement present in CalculatorHome page 
	Then I click on nineButton webelement present in CalculatorHome page 
	Then I click on dotButton webelement present in CalculatorHome page 
	Then I click on zeroButton webelement present in CalculatorHome page
	Then I click on nineButton webelement present in CalculatorHome page 
	Then I click on equalButton webelement present in CalculatorHome page 
	When I verify the element is displayed on screen as 73.4472