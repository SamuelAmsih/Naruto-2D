# Game-development-2D

Unity CI (https://github.com/SamuelAmsih/Naruto-2D/actions/workflows/ci.yml/badge.svg)



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

https://github.com/users/SamuelAmsih/projects/2/views/1?system_template=kanban <br>


## Instructions to run the project locally


 1. Clone this repository or download the source files to your local machine. <br>

 2. Open in Unity and select the downloaded project. <br>

 3. Install requied packages (optional). <br>

 4. Build the Project <br>

   In the Unity Editor, open File > Build Settings. <br>

   Select your target platform (e.g., PC, Mac, Linux). <br>

   Click Build or Build and Run to create/launch the game. <br>

5. Run the project by pressing play <br>

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

Unity doesn’t include a built-in C# linter. However, Roslyn Analyzers let you enforce coding conventions and catch issues in your .NET/C# projects. In VS Code, you can install and configure these analyzers to get warnings and suggestions in real-time.

1. Add Roslyn Analyzers <br>
  
Typically, you would add them as NuGet packages in your .csproj file—for example, if you want a specific analyzer package (e.g., Microsoft.CodeAnalysis.FxCopAnalyzers or other community analyzers). <br>

However, Unity automatically generates and regenerates .csproj files, which can overwrite manual changes. <br>

- One approach is to keep your analyzers in a separate solution or use a package manager approach that Unity supports (like embedding them in your project as part of a custom package). <br>

- Another approach is to use an .editorconfig file to configure rules. Some analyzers are built into the .NET compiler and can be configured at the editor level.
  
2. Using Roslyn Analyzers in VS Code <br>

Open any C# file in your Unity project via VS Code. <br>

As you code, watch for squiggly underlines or messages in the Problems panel. These come from Roslyn-based analyzers. <br>

Hover over each warning or suggestion for more info and potential quick fixes. <br>
