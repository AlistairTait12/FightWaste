# Fight Waste
## What is this repo?
This is a small console application I am working on. It currently has one job and that is to make a shopping list of ingredients based on what meals you enter into your meal plan for a week.

The app is written in C# using the .NET platform and I am using Visual Studio 2022 to develop it.

You can find planned issues in the FightWaste project linked to this repo.

## How do I use it?
- Build the application
- Click on the built exe or run from Visual Studio
- A console application will load
- You will be asked to enter a series of meals which can be found in the Meals.json at the top level of the folder structure
- When you are finished type "END" into the console
- This will then output a list of ingredients

## How do I make changes?
1. Create a branch with a meaningful name relating to the change you are making (I usually create a GitHub Issue to document my new changes and use it to generate a branch).
2. Write at _least_ one unit test to cover the changes you are introducing. Ideally there should be at least one unit test covering each scenario your changes.
3. The unit test project mirrors the folder structure of the main project, so if you are writing a new class, add a new test class and tests under the corresponding folder path. If you are writing a new method, find the existing test class and add a new test(s) for the method.
4. When the project is building and all test passing, raise a PR for merging.