using _05_Slot_Machine;

namespace _05_Slot_Machine
{
    internal class Program
    {
        static void Main()
        {
            Random random = new();

            int bet = 0;
            double bank = GameData.STARTING_BANK;
            string gameMode = "";
            string nextMessage = "";
            double costMultiplier = 1;
            int quit = 0;

            UI.ShowIntro();

            while (true)
            {
                if (!string.IsNullOrEmpty(nextMessage))
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(nextMessage);
                    Console.WriteLine();
                    nextMessage = "";
                }
                if (quit == 1)
                {
                    return;
                }

                Console.WriteLine();
                Console.WriteLine($"Your current bank: {bank}");
                Console.WriteLine();

                if (string.IsNullOrEmpty(gameMode))
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

                        if (int.TryParse(input, out bet) && bet >= 0 && (bet * costMultiplier) <= bank)
                        {
                            bank -= bet * costMultiplier;
                            break;
                        }
                        if (bet * costMultiplier > bank)
                        {
                            Console.WriteLine("Insufficient funds. Please enter a valid bet.");
                        }
                        else if (bet == 0)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid bet. Please enter a valid amount.");
                        }
                    }
                    if (bet == 0)
                    {
                        nextMessage = "Exiting the game. Thank you for playing!";
                        quit = 1;
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine($"Game mode: {gameMode}");
                    Console.WriteLine($"Your current bank: {bank}");
                    Console.WriteLine($"Your bet: {bet} (Cost: {costMultiplier * bet})");
                    Console.WriteLine();

                    int[,] slotMachine = GameLogic.GenerateGrid(GameData.ROWS, GameData.COLUMNS, random);

                    PrintGrid(slotMachine);

                    int hasWon = 0;
                    if (Enum.TryParse<GameData.GameMode>(gameMode, ignoreCase: true, out var parsedMode))
                    {
                        switch (parsedMode)
                        {
                            case GameData.GameMode.Center:
                                hasWon = GameLogic.CountCenterMatch(slotMachine);
                                if (hasWon > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER;
                                    Console.WriteLine($"You won {hasWon} time in Center mode!");
                                }
                                else
                                {
                                    Console.WriteLine("No matches in Center mode.");
                                }
                                break;
                            case GameData.GameMode.Horizontal:
                                hasWon = GameLogic.CountHorizontalMatches(slotMachine);
                                if (hasWon > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER * hasWon;
                                    Console.WriteLine($"You won {hasWon} time(s) in Horizontal mode!");
                                }
                                else
                                {
                                    Console.WriteLine("No matches in Horizontal mode.");
                                }
                                break;
                            case GameData.GameMode.Vertical:
                                hasWon = GameLogic.CountVerticalMatches(slotMachine);
                                if (hasWon > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER * hasWon;
                                    Console.WriteLine($"You won {hasWon} time(s) in Vertical mode!");
                                }
                                else
                                {
                                    Console.WriteLine("No matches in Vertical mode.");
                                }
                                break;
                            case GameData.GameMode.Diagonal:
                                hasWon = GameLogic.CountDiagonalMatches(slotMachine);
                                if (hasWon > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER * hasWon;
                                    Console.WriteLine($"You won {hasWon} time(s) in Diagonal mode!");
                                }
                                else
                                {
                                    Console.WriteLine("No matches in Diagonal mode.");
                                }
                                break;
                            case GameData.GameMode.All:
                                int totalWins = 0;
                                totalWins += GameLogic.CountCenterMatch(slotMachine);
                                totalWins += GameLogic.CountHorizontalMatches(slotMachine);
                                totalWins += GameLogic.CountVerticalMatches(slotMachine);
                                totalWins += GameLogic.CountDiagonalMatches(slotMachine);
                                if (totalWins > 0)
                                {
                                    bank += bet * costMultiplier * GameData.BETMULTIPLIER * totalWins;
                                    Console.WriteLine($"You won {totalWins} time(s) in All mode!");
                                }
                                else
                                {
                                    Console.WriteLine("No matches in All mode.");
                                }
                                break;
                            default:
                                Console.WriteLine("Unknown game mode.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid game mode.");
                    }
                    Console.WriteLine();
                    Console.WriteLine($"Your bank after this round: {bank}");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    gameMode = ""; // Reset for next round
                    break;
                }
            }
        }

        private static void PrintGrid(int[,] grid)
        {
            int cellWidth = 3;
            string rowSeparator = "+";
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                rowSeparator += new string('-', cellWidth) + "+";
            }
            Console.WriteLine(rowSeparator);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    string numStr = grid[i, j].ToString();
                    Console.Write(FormatCell(numStr, cellWidth) + "|");
                }
                Console.WriteLine();
                Console.WriteLine(rowSeparator);
            }
        }

        // Helper method to format a cell with centered text
        private static string FormatCell(string text, int width)
        {
            if (text.Length >= width)
                return text.Substring(0, width);
            int padTotal = width - text.Length;
            int padLeft = padTotal / 2 + text.Length;
            // PadLeft first, then PadRight to fill to width
            return text.PadLeft(padLeft).PadRight(width);
        }
    }
}