using _05_Slot_Machine;

namespace _05_Slot_Machine
{
    internal class Program
    {
        static void Main()
        {
            Random random = new();

            int bet = 0;
            double bank = GameData.STARTING_BANK; // Starting bank amount
            string gameMode = ""; // Variable to store the selected gamemode
            string nextMessage = ""; // Variable to store the message for the next iteration of the loop
            double costMultiplier = 1; // Variable to store the cost multiplier based on the selected gamemode
            int quit = 0; // Variable to track if the user wants to exit the game

            // Display intro
            UI.ShowIntro();

            while (true)
            {
                if (!string.IsNullOrEmpty(nextMessage))
                {
                    Console.Clear(); // Clear the console before displaying the next message
                    Console.WriteLine();
                    Console.WriteLine(nextMessage); // Display the message from the previous iteration
                    Console.WriteLine();
                    nextMessage = ""; // Clear the message after displaying it
                }
                if (quit == 1)
                {
                    return; // Exit the game
                }

                Console.WriteLine();
                Console.WriteLine($"Your current bank: {bank}");
                Console.WriteLine();

                if (gameMode == "")
                {
                    var result = UI.SelectGameMode(bet);
                    gameMode = result.gameMode;
                    costMultiplier = result.costMultiplier;
                    quit = result.quit;
                    nextMessage = result.nextMessage;
                }

                if (quit == 1)
                {
                    continue;
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine($"Game mode: {gameMode}");
                    Console.WriteLine($"Your current bank: {bank}");
                    Console.WriteLine();

                    while (true)
                    {
                        Console.Write("Enter your bet (0 to exit): ");
                        string? input = Console.ReadLine();

                        if (int.TryParse(input, out bet) && (bet + costMultiplier) >= 0 && (bet * costMultiplier) <= bank)
                        {
                            bank -= bet * costMultiplier; // Deduct the bet amount from the bank
                            break;
                        }
                        if (bet * costMultiplier > bank)
                        {
                            Console.WriteLine("Insufficient funds. Please enter a valid bet.");
                        }
                        if (bet == 0)
                        {
                            break;
                        }
                        Console.WriteLine("Invalid bet. Please enter a valid amount.");
                    }
                    if (bet == 0)
                    {
                        nextMessage = "Exiting the game. Thank you for playing!";
                        quit = 1; // Set quit to 1 to exit the game
                        break; // Exit the inner loop to allow quitting
                    }

                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine($"Game mode: {gameMode}");
                    Console.WriteLine($"Your current bank: {bank}");
                    Console.WriteLine($"Your bet: {bet} (Cost: {costMultiplier * bet})");
                    Console.WriteLine();

                    // Generate a grid
                    int[,] slotMachine = GameLogic.GenerateGrid(GameData.ROWS, GameData.COLUMNS, random);

                    // Display the slot machine
                    string rowSeparator = "+";
                    for (int j = 0; j < GameData.COLUMNS; j++)
                    {
                        rowSeparator += "---+";
                    }
                    for (int i = 0; i < GameData.ROWS; i++)
                    {
                        Console.WriteLine(rowSeparator);
                        Console.Write("|");
                        for (int j = 0; j < GameData.COLUMNS; j++)
                        {
                            Console.Write($" {slotMachine[i, j]} |");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine(rowSeparator);

                    // Checking for matches
                    int hasWon = 0;
                    if (Enum.TryParse<GameData.GameMode>(gameMode, ignoreCase: true, out var parsedMode))
                        if (parsedMode == GameData.GameMode.Center)
                        {
                            // Check center column
                            int centerCol = GameData.COLUMNS / 2;
                            bool centerMatch = true;
                            for (int i = 1; i < GameData.ROWS; i++)
                            {
                                if (slotMachine[i, centerCol] != slotMachine[0, centerCol])
                                {
                                    centerMatch = false;
                                    break;
                                }
                            }
                            if (centerMatch) hasWon = 1;

                            if (hasWon > 0)
                            {
                                bank += bet * costMultiplier * GameData.BETMULTIPLIER;
                                Console.WriteLine($"You won {hasWon} times in {GameData.GameMode.Center} mode!");
                            }
                            else
                            {
                                Console.WriteLine($"No matches in {GameData.GameMode.Center} mode.");
                            }
                            if (parsedMode == GameData.GameMode.Horizontal)
                            {
                                // Check horizontal rows
                                for (int i = 0; i < GameData.ROWS; i++)
                                {
                                    bool rowMatch = true;
                                    for (int j = 1; j < GameData.COLUMNS; j++)
                                    {
                                        if (slotMachine[i, j] != slotMachine[i, 0])
                                        {
                                            rowMatch = false;
                                            break;
                                        }
                                    }
                                    if (rowMatch) hasWon += 1;
                                }

                                if (hasWon > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER * hasWon;
                                    Console.WriteLine($"You won {hasWon} times in {GameData.GameMode.Horizontal} mode!");
                                }
                                else
                                {
                                    Console.WriteLine($"No matches in {GameData.GameMode.Horizontal} mode.");
                                }
                            }

                            if (parsedMode == GameData.GameMode.Vertical)
                            {
                                // Check vertical columns
                                for (int j = 0; j < GameData.COLUMNS; j++)
                                {
                                    bool colMatch = true;
                                    for (int i = 1; i < GameData.ROWS; i++)
                                    {
                                        if (slotMachine[i, j] != slotMachine[0, j])
                                        {
                                            colMatch = false;
                                            break;
                                        }
                                    }
                                    if (colMatch) hasWon += 1;
                                }

                                if (hasWon > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER * hasWon;
                                    Console.WriteLine($"You won {hasWon} times in {GameData.GameMode.Vertical} mode!");
                                }
                                else
                                {
                                    Console.WriteLine($"No matches in {GameData.GameMode.Vertical}");
                                }
                            }

                            if (parsedMode == GameData.GameMode.Diagonal)
                            {
                                // Top-left to bottom-right
                                bool diag1Match = true;
                                for (int i = 1; i < Math.Min(GameData.ROWS, GameData.COLUMNS); i++)
                                {
                                    if (slotMachine[i, i] != slotMachine[0, 0])
                                    {
                                        diag1Match = false;
                                        break;
                                    }
                                }
                                if (diag1Match) hasWon += 1;

                                // Top-right to bottom-left
                                bool diag2Match = true;
                                for (int i = 1; i < Math.Min(GameData.ROWS, GameData.COLUMNS); i++)
                                {
                                    if (slotMachine[i, GameData.COLUMNS - 1 - i] != slotMachine[0, GameData.COLUMNS - 1])
                                    {
                                        diag2Match = false;
                                        break;
                                    }
                                }
                                if (diag2Match) hasWon += 1;

                                if (hasWon > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER * hasWon;
                                    Console.WriteLine($"You won {hasWon} times in {GameData.GameMode.Diagonal} mode!");
                                }
                                else
                                {
                                    Console.WriteLine($"No matches in {GameData.GameMode.Diagonal} mode.");
                                }
                            }
/*                            if (parsedMode == GameData.GameMode.All)
                            {
                                // Check center column
                                bool centerMatch = true;
                                for (int i = 1; i < GameData.ROWS; i++)
                                {
                                    if (slotMachine[i, GameData.COLUMNS / 2] != slotMachine[0, GameData.COLUMNS / 2])
                                    {
                                        centerMatch = false;
                                        break;
                                    }
                                }
                                if (centerMatch) hasWon = 1;

                                // Check horizontal rows
                                for (int i = 0; i < GameData.ROWS; i++)
                                {
                                    bool rowMatch = true;
                                    for (int j = 1; j < GameData.COLUMNS; j++)
                                    {
                                        if (slotMachine[i, j] != slotMachine[i, 0])
                                        {
                                            rowMatch = false;
                                            break;
                                        }
                                    }
                                    if (rowMatch) hasWon += 1;
                                }

                                // Check vertical columns
                                for (int j = 0; j < GameData.COLUMNS; j++)
                                {
                                    bool colMatch = true;
                                    for (int i = 1; i < GameData.ROWS; i++)
                                    {
                                        if (slotMachine[i, j] != slotMachine[0, j])
                                        {
                                            colMatch = false;
                                            break;
                                        }
                                    }
                                    if (colMatch) hasWon += 1;
                                }

                                // Top-left to bottom-right
                                bool diag1Match = true;
                                for (int i = 1; i < Math.Min(GameData.ROWS, GameData.COLUMNS); i++)
                                {
                                    if (slotMachine[i, i] != slotMachine[0, 0])
                                    {
                                        diag1Match = false;
                                        break;
                                    }
                                }
                                if (diag1Match) hasWon += 1;

                                // Top-right to bottom-left
                                bool diag2Match = true;
                                for (int i = 1; i < Math.Min(GameData.ROWS, GameData.COLUMNS); i++)
                                {
                                    if (slotMachine[i, GameData.COLUMNS - 1 - i] != slotMachine[0, GameData.COLUMNS - 1])
                                    {
                                        diag2Match = false;
                                        break;
                                    }
                                }
                                if (diag2Match) hasWon += 1;

                                if (hasWon > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER * hasWon;
                                    Console.WriteLine($"You won {hasWon} times in {GameData.GameMode.All} mode!");
                                }
                                else
                                {
                                    Console.WriteLine($"No matches in {GameData.GameMode.All} mode.");
                                }
                            }*/
                        }
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press enter to continue or 0 to change the gamemode.");
                        string? input = Console.ReadLine();
                        if (input == "0")
                        {
                            nextMessage = "Changing the gamemode.";
                            gameMode = ""; // Reset quit to allow changing gamemode
                            break;
                        }
                        if (string.IsNullOrEmpty(input))
                        {
                            break; // Continue to the next iteration of the loop
                        }
                    }
                    if (gameMode == "")
                    {
                        break; // Exit the inner loop to allow changing gamemode
                    }
                }
            }
        }
    }
}