namespace _05_Slot_Machine
{
    internal class Program
    {
        static void Main()
        {
            const int ROWS = 3;
            const int COLUMNS = 3;
            const string GAME_MODE_CENTER = "center";
            const string GAME_MODE_HORIZONTAL = "horizontal";
            const string GAME_MODE_VERTICAL = "vertical";
            const string GAME_MODE_DIAGONAL = "diagonal";
            const string GAME_MODE_ALL = "all";
            const int COST_CENTER = 1;
            const int COST_HORIZONTAL = 2;
            const int COST_VERTICAL = 2;
            const double COST_DIAGONAL = 1.5;
            const int COST_ALL = 3;
            const int BETMULTIPLIER = 2;

            List<string> intro = [
                "05 - Slot Machine",
                "=================",
                "",
                "Welcome to the slot machine game!",
                "There are several game mode that you can play, depending on the gamemode",
                "your bet and winnings will be priced accordingly.",
                "",
                "Game Modes:",
                "1. Center: Match the center column.        Cost: 1 x bet.",
                "2. Horizontal: Match any row.              Cost: 2 x bet.",
                "3. Vertical: Match any column.             Cost: 2 x bet.",
                "4. Diagonal: Match the diagonal lines.     Cost: 1.5 x bet.",
                "5. All: Match all of the above.            Cost: 3 x bet",
                "",
                "You can exit the game at any time by entering 0 as your bet.",
                "Good luck and have fun!"
            ];

            Random random = new();

            // Display intro
            foreach (string line in intro)
            {
                Console.WriteLine(line);
            }
            int bet = 0;
            double bank = 10; // Starting bank amount
            double costMultiplier = 1; // Multiplier for the cost of the game mode
            string gameMode = ""; // Variable to store the selected gamemode
            string nextMessage = ""; // Variable to store the message for the next iteration of the loop

            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"Your current bank: {bank}");
                Console.WriteLine();

                if (!string.IsNullOrEmpty(nextMessage))
                {
                    Console.WriteLine();
                    Console.WriteLine(nextMessage); // Display the message from the previous iteration
                    Console.WriteLine();
                    nextMessage = ""; // Clear the message after displaying it
                }
                while (true)
                {
                    Console.WriteLine("Choose a gamemode. 1-5");
                    Console.WriteLine("0: Exit the game.");
                    Console.WriteLine($"1. {GAME_MODE_CENTER}");
                    Console.WriteLine($"2. {GAME_MODE_HORIZONTAL}");
                    Console.WriteLine($"3. {GAME_MODE_VERTICAL}");
                    Console.WriteLine($"4. {GAME_MODE_DIAGONAL}");
                    Console.WriteLine($"5. {GAME_MODE_ALL}");
                    Console.Write("Enter your choice (1-5): ");
                    string? choice = Console.ReadLine();
                    if (int.TryParse(choice, out int mode))
                    {
                        switch (mode)
                        {
                            case 0:
                                Console.WriteLine("Exiting the game. Thank you for playing!");
                                return; // Exit the game
                            case 1:
                                Console.WriteLine($"You chose '{GAME_MODE_CENTER}' mode. Cost: {COST_CENTER * bet}");
                                gameMode = GAME_MODE_CENTER;
                                costMultiplier = COST_CENTER;
                                break;
                            case 2:
                                Console.WriteLine($"You chose '{GAME_MODE_HORIZONTAL}' mode. Cost: {COST_HORIZONTAL * bet}");
                                gameMode = GAME_MODE_HORIZONTAL;
                                costMultiplier = COST_HORIZONTAL;
                                break;
                            case 3:
                                Console.WriteLine($"You chose '{GAME_MODE_VERTICAL}' mode. Cost: {COST_VERTICAL * bet}");
                                gameMode = GAME_MODE_VERTICAL;
                                costMultiplier = COST_VERTICAL;
                                break;
                            case 4:
                                Console.WriteLine($"You chose '{GAME_MODE_DIAGONAL}' mode. Cost: {COST_DIAGONAL * bet}");
                                gameMode = GAME_MODE_DIAGONAL;
                                costMultiplier = COST_DIAGONAL;
                                break;
                            case 5:
                                Console.WriteLine($"You chose '{GAME_MODE_ALL}' mode. Cost: {COST_ALL * bet}");
                                gameMode = GAME_MODE_ALL;
                                costMultiplier = COST_ALL;
                                break;
                            default:
                                nextMessage = "Invalid choice. Please enter a number between 1 and 5.";
                                continue; // Prompt for choice again
                        }
                        break; // Exit the loop if a valid choice is made
                    }
                }

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
                    if (bet < 0)
                    {
                        Console.WriteLine("Exiting the game. Thank you for playing!");
                        return; // Exit the game
                    }
                    Console.WriteLine("Invalid bet. Please enter a valid amount.");
                }

                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"Game mode: {gameMode}");
                Console.WriteLine($"Your current bank: {bank}");
                Console.WriteLine();

                // Generate a grid
                int[,] slotMachine = new int[ROWS, COLUMNS];
                for (int i = 0; i < ROWS; i++)
                {
                    for (int j = 0; j < COLUMNS; j++)
                    {
                        slotMachine[i, j] = random.Next(1, 10);
                    }
                }

                // Display the slot machine
                string rowSeparator = "+";
                for (int j = 0; j < COLUMNS; j++)
                {
                    rowSeparator += "---+";
                }
                for (int i = 0; i < ROWS; i++)
                {
                    Console.WriteLine(rowSeparator);
                    Console.Write("|");
                    for (int j = 0; j < COLUMNS; j++)
                    {
                        Console.Write($" {slotMachine[i, j]} |");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(rowSeparator);

                // Checking for matches
                int hasWon = 0;
                if (gameMode == GAME_MODE_CENTER)
                {
                    // Check center column
                    if (slotMachine[0, 1] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 1])
                    { hasWon = 1; }
                    if (hasWon > 0)
                    {
                        bank += bet * costMultiplier * BETMULTIPLIER;
                        Console.WriteLine($"You won {hasWon} times in {GAME_MODE_CENTER} mode!");
                    }
                    Console.WriteLine($"No matches in {GAME_MODE_CENTER} mode.");
                   
                }
                if (gameMode == GAME_MODE_HORIZONTAL)
                {
                    // Check horizontal rows
                    for (int i = 0; i < ROWS; i++)
                    {
                        if (slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
                        { hasWon += 1; }

                    }
                    bank += bet * costMultiplier * BETMULTIPLIER * hasWon;
                    Console.WriteLine($"You won {hasWon} times in {GAME_MODE_HORIZONTAL} mode!");
                }
                if (gameMode == GAME_MODE_VERTICAL)
                {
                    // Check vertical columns
                    for (int j = 0; j < COLUMNS; j++)
                    {
                        if (slotMachine[0, j] == slotMachine[1, j] && slotMachine[1, j] == slotMachine[2, j])
                        { hasWon += 1; }

                    }
                    if (hasWon > 0)
                    {
                        bank += bet * costMultiplier * BETMULTIPLIER * hasWon;
                        Console.WriteLine($"You won {hasWon} times in {GAME_MODE_VERTICAL} mode!");
                    }
                    Console.WriteLine($"No matches in {GAME_MODE_VERTICAL} mode.");
                }
                if (gameMode == GAME_MODE_DIAGONAL)
                {
                    // Check diagonals
                    if (slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2])
                        { hasWon += 1; }

                    if (slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                        { hasWon += 1; }

                    if (hasWon > 0)
                    {
                        bank += bet * costMultiplier * BETMULTIPLIER * hasWon;
                        Console.WriteLine($"You won {hasWon} times in {GAME_MODE_DIAGONAL} mode!");
                    }
                    Console.WriteLine($"No matches in {GAME_MODE_DIAGONAL} mode.");
                }
                if (gameMode == GAME_MODE_ALL)
                {
                    // Check center column
                    if (slotMachine[0, 1] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 1])
                    {
                        hasWon += 1;
                        bank += bet * costMultiplier * BETMULTIPLIER;
                    }
                    // Check horizontal rows
                    for (int i = 0; i < ROWS; i++)
                    {
                        if (slotMachine[i, 0] == slotMachine[i, 1] && slotMachine[i, 1] == slotMachine[i, 2])
                        { hasWon += 1; }
                    }
                    // Check vertical columns
                    for (int j = 0; j < COLUMNS; j++)
                    {
                        if (slotMachine[0, j] == slotMachine[1, j] && slotMachine[1, j] == slotMachine[2, j])
                        { hasWon += 1; }
                    }
                    // Check diagonals
                    if (slotMachine[0, 0] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 2])
                    { hasWon += 1; }
                    if (slotMachine[0, 2] == slotMachine[1, 1] && slotMachine[1, 1] == slotMachine[2, 0])
                    { hasWon += 1; }
                    if (hasWon > 0)
                    {
                        bank += bet * costMultiplier * BETMULTIPLIER * hasWon;
                        Console.WriteLine($"You won {hasWon} times in {GAME_MODE_ALL} mode!");
                    }
                    Console.WriteLine($"No matches in {GAME_MODE_ALL} mode.");
                }

                Console.WriteLine();
                Console.WriteLine($"Game mode: {gameMode}");
                Console.WriteLine($"Your current bank: {bank}");
                Console.WriteLine();

                while (true)
                {
                    Console.Write("Do you want to play again? (y/n): ");
                    string? playAgain = Console.ReadLine()?.ToLower();
                    if (playAgain == "y" || playAgain == "yes")
                    {
                        nextMessage = "Starting a new game...";
                        break; // Exit the loop to start a new game
                    }
                    else if (playAgain == "n" || playAgain == "no")
                    {
                        Console.WriteLine("Thank you for playing! Goodbye!");
                        return; // Exit the game
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                    }
                }
            }
        }
    }
}
