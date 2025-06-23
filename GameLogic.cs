// Logic.cs
namespace _05_Slot_Machine
{
    internal static class GameLogic
    {
        public static int[,] GenerateGrid(int rows, int columns, Random random)
        {
            int[,] grid = new int[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    grid[i, j] = random.Next(1, 10);
            return grid;
        }

        public static int CalculateWinnings(int[,] grid, string gameMode, int bet)
        {
            throw new NotImplementedException("Not yet implemented");
        }
    }
}
