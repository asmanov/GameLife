using System;

namespace GameLife.Model
{
    public class Game
    {
        public int CurrentGeneration { get; private set; }
        private Cell[,] field;
        private readonly int rows;
        private readonly int columns;
        private Random random = new Random();

        public Game(int rows, int columns, int density)
        {
            int r = random.Next(256);
            int g = random.Next(256);
            int b = random.Next(256);
            this.rows = rows;
            this.columns = columns;
            field = new Cell[columns, rows];
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (random.Next(density) == 0)
                    {
                        field[x, y] = new Cell(x, y, r, g, b);

                    }
                }
            }
        }

        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + columns) % columns;
                    int row = (y + j + rows) % rows;

                    bool isSelfCheking = col == x && row == y;
                    var hasLife = field[col, row];
                    if (field[col, row] != null && !isSelfCheking)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public void NextGeneration()
        {
            var newField = new Cell[columns, rows];
            int r = random.Next(256);
            int g = random.Next(256);
            int b = random.Next(256);
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);
                    //var hasLife = field[x, y];
                    if (field[x, y] == null && neighboursCount == 3)
                    {
                        newField[x, y] = new Cell(x, y, r, g, b); ;
                    }
                    else if (field[x, y] != null && (neighboursCount < 2 || neighboursCount > 3))
                    {
                        newField[x, y] = null;
                    }
                    else
                    {
                        newField[x, y] = field[x, y];
                    }
                }
            }
            field = newField;
            CurrentGeneration++;
        }

        public Cell[,] GetCurrentGeneration()
        {
            var result = new Cell[columns, rows];
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    result[x, y] = field[x, y];
                }
            }
            return result;
        }

        private bool ValidateCellPosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < columns && y < rows;
        }

        private void UpdateCell(int x, int y, Cell state)
        {
            if (ValidateCellPosition(x, y))
                field[x, y] = state;
        }

        public void AddCell(int x, int y)
        {
            int r = random.Next(256);
            int g = random.Next(256);
            int b = random.Next(256);
            UpdateCell(x, y, new Cell(x, y, r, g, b));
        }
        public void RemoveCell(int x, int y)
        {
            UpdateCell(x, y, null);
        }
    }
}
