// GameLogic.cs
namespace _05_Slot_Machine
{
    internal static class GameLogic
    {
        public static int[,] GenerateGrid(int rows, int columns, Random random)
        {
            ArgumentNullException.ThrowIfNull(random);
            if (rows <= 0)
                throw new ArgumentOutOfRangeException(nameof(rows), rows, "Number of rows must be greater than zero.");
            if (columns <= 0)
                throw new ArgumentOutOfRangeException(nameof(columns), columns, "Number of columns must be greater than zero.");

            int[,] grid = new int[rows, columns];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    grid[i, j] = random.Next(1, 10);
            return grid;
        }

        public static int CountCenterMatch(int[,] grid)
        {
            int centerCol = grid.GetLength(1) / 2;
            int value = grid[0, centerCol];
            for (int i = 1; i < grid.GetLength(0); i++)
            {
                if (grid[i, centerCol] != value)
                    return 0;
            }
            return 1;
        }

        public static int CountHorizontalMatches(int[,] grid)
        {
            int matches = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                int value = grid[i, 0];
                bool rowMatch = true;
                for (int j = 1; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != value)
                    {
                        rowMatch = false;
                        break;
                    }
                }
                if (rowMatch) matches++;
            }
            return matches;
        }

        public static int CountVerticalMatches(int[,] grid)
        {
            int matches = 0;
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                int value = grid[0, j];
                bool colMatch = true;
                for (int i = 1; i < grid.GetLength(0); i++)
                {
                    if (grid[i, j] != value)
                    {
                        colMatch = false;
                        break;
                    }
                }
                if (colMatch) matches++;
            }
            return matches;
        }

        public static int CountDiagonalMatches(int[,] grid)
        {
            int size = Math.Min(grid.GetLength(0), grid.GetLength(1));
            int matches = 0;
            // Top-left to bottom-right
            int value1 = grid[0, 0];
            bool diag1 = true;
            for (int i = 1; i < size; i++)
            {
                if (grid[i, i] != value1)
                {
                    diag1 = false;
                    break;
                }
            }
            if (diag1) matches++;
            // Top-right to bottom-left
            int value2 = grid[0, size - 1];
            bool diag2 = true;
            for (int i = 1; i < size; i++)
            {
                if (grid[i, size - 1 - i] != value2)
                {
                    diag2 = false;
                    break;
                }
            }
            if (diag2) matches++;
            return matches;
        }
    }
}