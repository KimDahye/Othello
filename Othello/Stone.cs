using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    class Stone
    {
        private string Color;
        private int X;
        private int Y;

        public Stone(string Color, int X, int Y)
        {
            this.Color = Color;
            this.X = X;
            this.Y = Y;
        }

        public int GetX()
        {
            return X;
        }

        public int GetY()
        {
            return Y;
        }

        public string GetColor()
        {
            return Color;
        }

        public string PrintStone()
        {
            if (this.Color == "Black")
            {
                return "*";
            }
            else if(this.Color == "White")
            {
                return "o";
            }
            else
            {
                return " ";
            }
        }

        public void ChangeColor()
        {
            if (Color == "Black")
            {
                Color = "White";
            }
            else if (Color == "White")
            {
                Color = "Black";
            }
        }
    }
}
