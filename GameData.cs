// GameData.cs
namespace _05_Slot_Machine
{
    internal static class GameData
    {
        public const int ROWS = 3;
        public const int COLUMNS = 3;
        public const int COST_CENTER = 1;
        public const int COST_HORIZONTAL = 2;
        public const int COST_VERTICAL = 2;
        public const double COST_DIAGONAL = 1.5;
        public const int COST_ALL = 3;
        public const int BETMULTIPLIER = 2;
        public const int STARTING_BANK = 100;

        public enum GameMode
        {
            Quit,
            Center,
            Horizontal,
            Vertical,
            Diagonal,
            All
        }

        public static List<string> GameModes =>
        [
            $"{(int) GameMode.Quit}. {GameMode.Quit}: Exit the game.",
            $"{(int) GameMode.Center}. {GameMode.Center}: Match the center column.        Cost: {COST_CENTER} x bet.",
            $"{(int) GameMode.Horizontal}. {GameMode.Horizontal}: Match any row.              Cost: {COST_HORIZONTAL} x bet.",
            $"{(int) GameMode.Vertical}. {GameMode.Vertical}: Match any column.             Cost: {COST_VERTICAL} x bet.",
            $"{(int) GameMode.Diagonal}. {GameMode.Diagonal}: Match the diagonal lines.     Cost: {COST_DIAGONAL} x bet.",
            $"{(int) GameMode.All}. {GameMode.All}: Match all of the above.            Cost: {COST_ALL} x bet"
        ];

        public static string Intro =>
        $@"
        05 - Slot Machine
        ========================================================================
        Welcome to the slot machine game!
        There are several game mode that you can play, depending on the gamemode
        your bet and winnings will be priced accordingly.

        Game Modes:
{string.Join(Environment.NewLine, GameModes.Select(mode => "\t" + mode))}

        You can exit the game at any time by entering 0 as your bet.
        Good luck and have fun!
        ========================================================================
        ";
    }
}
