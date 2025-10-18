# Two-Player Board Game Framework

## Introduction

This project presents an extensible C# framework built on .NET 8.0 for developing and playing variation of Connect-Four-style board game. 

This framework includes full support for several versions of the **LineUp** game:

- **LineUp Basic**: A basic version using only Ordinary Discs, with a fixed grid size.
- **LineUp Classic**: Standard LineUp game with all Special Discs and customisable grid size.
- **LineUp Spin**: A variant of LineUp Basic where the board rotates 90° clockwise after every five moves, causing disc positions to shift under gravity.

All games are designed to share core logic and infrastructure, demonstrating the extensibility, reusability and maintainability of the underlying framework.

------

## Features

- **Modular Game Framework**  
  Core logic is abstracted to support multiple game variants with shared infrastructure and extensible design principles.

- **Game Modes**  
  - Human vs Human  
  - Human vs Computer (Computer will prioritise immediate win; otherwise, will plays a random valid move)

- **Game Saving & Loading**  
  - Save any game state to a JSON file.
  - Load from a saved state and resume play seamlessly.
  - All game states, including game mode (HvH or HvC) is preserved across saves.

- **Undo/Redo System**  
  - Unlimited undo and redo of previous moves.
  - Undo/redo is supported immediately after loading a saved game.

- **Dynamic Board Transformations** (LineUp Spin)  
  - Board rotates 90° clockwise every 5 turns.
  - Gravity is reapplied after rotating, potentially changing gameplay experience and winning conditions.

- **Move Validation & Command Help**  
  - Human inputs are validated in real-time via error handling.
  - In-game help system guides players on valid commands and usage examples.

---

## Technologies Used

- **Language**: C#
- **Framework**: .NET 8.0
- **IDE**: Visual Studio / Visual Studio Code

---

## Getting Started

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Running the Application

1. Clone or download this repository.
2. Open the project folder in Visual Studio or Visual Studio Code.
3. Open a new terminal within the IDE.
4. Run the following command:

```bash
dotnet run

5. Enjoy the game

(C) TacNap, meowliodaz, lkplucy, Sanjika97, yeeweilimmy