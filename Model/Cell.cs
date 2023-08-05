using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife.Model
{
    public class Cell
    {
        public int Age { get; set; }
        public Brush CellColor { get; set; }
        public Color ColorItem { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(int x, int y, int r, int g, int b)
        {
            Age = 0;
            X = x;
            Y = y;
            ColorItem = Color.FromArgb(255, r, g, b);
            CellColor = new SolidBrush(ColorItem);
        }
    }
}
