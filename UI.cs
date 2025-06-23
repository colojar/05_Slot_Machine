// UI.cs
namespace _05_Slot_Machine
{
    internal static class UI
    {
        public static void ShowIntro()
        {
            Console.WriteLine(GameData.Intro);
        }

        public static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static (string gameMode, double costMultiplier, int quit, string nextMessage) SelectGameMode(int bet)
        {
            while (true)
            {
                Console.WriteLine("Choose a gamemode.");
                foreach (string item in GameData.GameModes)
                {
                    Console.WriteLine(item);
                }
                Console.Write("Enter your choice (0-5): ");
                string? choice = Console.ReadLine();

                string gameMode = "";
                double costMultiplier = 1;
                int quit = 0;
                string nextMessage = "";

                if (int.TryParse(choice, out int mode))
                {
                    switch (mode)
                    {
                        case (int)GameData.GameMode.Quit:
                            nextMessage = "Exiting the game. Thank you for playing!";
                            quit = 1;
                            break;
                        case (int)GameData.GameMode.Center:
                            Console.WriteLine($"You chose '{GameData.GameMode.Center}' mode. Cost: {GameData.COST_CENTER * bet}");
                            gameMode = GameData.GameMode.Center.ToString();
                            costMultiplier = GameData.COST_CENTER;
                            break;
                        case (int)GameData.GameMode.Horizontal:
                            Console.WriteLine($"You chose '{GameData.GameMode.Horizontal}' mode. Cost: {GameData.COST_HORIZONTAL * bet}");
                            gameMode = GameData.GameMode.Horizontal.ToString();
                            costMultiplier = GameData.COST_HORIZONTAL;
                            break;
                        case (int)GameData.GameMode.Vertical:
                            Console.WriteLine($"You chose '{GameData.GameMode.Vertical}' mode. Cost: {GameData.COST_VERTICAL * bet}");
                            gameMode = GameData.GameMode.Vertical.ToString();
                            costMultiplier = GameData.COST_VERTICAL;
                            break;
                        case (int)GameData.GameMode.Diagonal:
                            Console.WriteLine($"You chose '{GameData.GameMode.Diagonal}' mode. Cost: {GameData.COST_DIAGONAL * bet}");
                            gameMode = GameData.GameMode.Diagonal.ToString();
                            costMultiplier = GameData.COST_DIAGONAL;
                            break;
                        case (int)GameData.GameMode.All:
                            Console.WriteLine($"You chose '{GameData.GameMode.All}' mode. Cost: {GameData.COST_ALL * bet}");
                            gameMode = GameData.GameMode.All.ToString();
                            costMultiplier = GameData.COST_ALL;
                            break;
                        default:
                            nextMessage = "Invalid choice. Please enter a number between 0 and 5.";
                            continue; // Prompt for choice again
                    }
                    return (gameMode, costMultiplier, quit, nextMessage);
                }
            }
        }
    }
}
