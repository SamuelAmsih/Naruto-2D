# Game-development-2D


A re-creation of the classic Super Mario Bros. 2D from NES, built with our own creative twist. This is a fun side-scrolling platformer developed as part of a passion project to dive into game development, practice programming, and honor a timeless classic.

## Group Members:
Mojo0224    - John Molin <br>
Majd01M     - Majd Morad <br>
Itachi-nika - Filip Yousif <br>
SamuelAmsih - Samuel Yousef <br>
 
## Implementation

World map <br>
Main character <br>
Enemies <br>
Obstacles <br>
Animations <br>
Retro pixel art, (hopefully sound effects if there is time)


## Features

”Classic” Mario-style gameplay<br>
Player movement: <br>
-jumping <br>
-idlestance <br>
-running<br>



## language

C#

## Build-System
 
Unity


## link to Kanbon:

https://github.com/users/SamuelAmsih/projects/2/views/1?system_template=kanban

## Unit tests in Unity

1. Open the Unity menu: Window > General > Test Runner (in some older versions, it’s Window > Test Runner) <br>

2. Choose the type of test you want to run:
EditMode Tests: Run in Edit Mode, without starting the game.
PlayMode Tests: Run in a simulated play session. <br>

3. Select the test category or click "Run All" to run all tests. <br>

4. You´ll see the results in the Test Runner window, green for passed tests, red for failed. <br>

## Code coverage 

1. Install Code Coverage for Unity. <br>

2. Open the Test Runner window as before (Window > General > Test Runner).<br>

3. Switch to the Coverage tab or window (depending on your Unity version, you may see a “Coverage” button in the Test Runner or need to open Window > Analysis > Code Coverage)

4. Make sure Coverage is enabled in that window <br>

5. Click Run All in the Test Runner. This will run your tests while also gathering coverage data <br>

6. Once the tests finish, you can click Generate Report to create a coverage report <br>

## Run linter in Unity

Unity doesn’t include a built-in C#. However, you can use external tools and we will use VS Code. We will stick to C# extension StyleCop

1. Install StyleCop <br>
  
Open VS Code and go to the Extensions panel. <br>

Install the C# extension (if you haven’t already). <br>

If you want stricter style checks or guidelines, you can install StyleCop.Analyzers <br>
  
2. How linting works in VS Code <br>

With the C# extension and StyleCop/analyzers installed, open any C# file. <br>

You’ll see linting errors or warnings in the Problems panel in VS Code, or as underlines in the editor. <br>

Hover over the underline to see the rule or suggestion (for example, naming conventions, spacing, missing XML comments, etc.). <br>
