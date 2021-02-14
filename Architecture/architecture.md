# Program Organization


# Code Design

![Class Diagram](ClassDiagram.png)
Description of the major classes:

Board: The board class is used to create an instance of a board and monitors the state of the game (whose turn it is and if the game is over). Since our current idea of the game does not have multiple instances of games running concurrently there is no need to have a higher level class track these attributes. The board class maintains a double dimension array of Square instances and at least one instance of a store object.

Square: The square class includes attributes such as the color and position of a square on a board. Important methods from the square class are addPiece and removePiece.

Store: The store class generates a list of power-ups that can be purchased with coins and applied to the squares in a board. Each store creates a storage object to track purchased power-ups before they are applied.

Coins: The coins class exists to track the in-game economy. The class includes simple set and get methods.

PowerUp: The powerup class is used to generate advantages from a pre-generated range that will be used to populate the Store. Powerups can be stored in a storage object and applied to a square.


| Class | User Story |
| ----- | ---------- |
| Board | 000, 003, 009, 010, 012 |
| Square | 000, 010 |
| Piece | 000, 003 |
| Store | 002, 004, 005 |
| Coins | 001, 004, 009 |
| Storage | 006, 012 |
| Powerup | 002, 004, 006 |
| Dice | 004, 012 |

# Data Design

Our game does not require a database for storing information. Any data that needs to be collected on behalf of the user will be stored in the various classes that make up the program. A user's pieces and their location will be stored in Square and Board, they number of coins they have collected will be stored in Coins, the powerups they have purchased will be in Storage, etc. All data is stored in the form of arrays. At this moment we do not see a need for dynamic storage since there are strict bounds on the data being monitored. (A board is always 8 x 8, and there are a finite number of powerups to choose from). Our data is stored sequentially since data such as pieces and power-ups have a value associated with them. This makes storage and access easy.

# Business Rules


# User Interface Design

![UI Design](UI-Design.png)

The top diagram shows what a player will mainly see. Given it is a chess game, the chess board will be the main focus. The player will move the pieces on the board, as in a normal chess game, and they can click on the "Store" tab to the left or the "My Powerups" tab at the bottom to view available/purchased powerups. They can see how many coins they have gained in the top left corner. <br />The bottom left diagram gives an idea of what the store will look like. Here, the player can look at the various powerups they can buy. <br />The bottom right diagram shows a player's storage where the powerups they have purchased can be found.

# Resource Management


# Security

Our system will not require a user account, profile, or password.

# Performance


# Scalability


# Interoperability


# Internationalization/Localization


# Input/Output


# Error Processing


# Fault Tolerance

If an error is detected, the system will revert back to how it was before the error occurred. For example, if a player puts a chess piece on a square that is not availabe or out of that piece's capability, it will go back to it's original location. Or, if a player tries to move something when it is not their turn, it will go back to its original location and an error message will give them a warning.

# Architectural Feasibility


# Overengineering


# Build-vs-Buy Decisions

We are using the Unity game engine and therefore utilizing the added functionality that comes with UnityEditor. Also, we will be importing most of our graphics from the Unity Assets store. 

# Reuse


# Change Strategy
