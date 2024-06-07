using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama
{
    class Field
    {
        private object _lock = new object();

        private bool turn;
        private Piece selected;

        public Piece[,] field;
        public Field()
        {
            field = new Piece[Program.window.Size.X / Program.proportion, Program.window.Size.Y / Program.proportion];
            turn = true;
            InizializePieces();
        }

        public void InizializePieces()
        {
            int numPiece = 20;
            for (int i = 0; i < field.GetLength(0) && numPiece > 0; i++)
            {
                for (int j = 0; j < field.GetLength(0) && numPiece > 0; j++)
                {
                    if (i % 2 != j % 2 && numPiece > 0)
                    {
                        field[j, i] = new Piece(false, j, i);
                        numPiece--;
                    }
                }
            }

            numPiece = 20;
            for (int i = 6; i < field.GetLength(0) && numPiece > 0; i++)
            {
                for (int j = 0; j < field.GetLength(0) && numPiece > 0; j++)
                {
                    if (i % 2 != j % 2 && numPiece > 0)
                    {
                        field[j, i] = new Piece(true, j, i);
                        field[j, i].Click += ClickedPiece;
                        numPiece--;
                    }
                }
            }
        }

        public void ClieckedField(object sender, MouseButtonEventArgs e)
        {
            lock (_lock)
            {
                if (!IsFree(e.X / Program.proportion, e.Y / Program.proportion) || selected == null)
                    return;

                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if (field[i, j] == selected)
                        {
                            Console.WriteLine("X: {0}  Y: {1}", i, j);
                            field[i, j] = null;
                            goto jump;
                        }
                    }
                }
            jump:
                Console.WriteLine(selected == null);
                field[e.X / Program.proportion, e.Y / Program.proportion] = selected;
                selected = null;
            }       
        }

        public void ClickedPiece(object sender, EventArgs e)
        {
            lock (_lock)
            {
                if (turn == (sender as Piece).color)
                    selected = sender as Piece;
            }    
        }

        public bool IsFree(int x, int y)
        {
            bool aux = true;
            if (x % 2 != y % 2)
                return false;
            
            foreach (var piece in field)
            {
                if (piece != null && piece.position.X == x && piece.position.Y == y)
                    aux = false;
            }
            return aux;
        }

        public void DrawField()
        {
            RectangleShape shape = new RectangleShape(new SFML.System.Vector2f(Program.proportion, Program.proportion));
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(0); j++)
                {
                    if (i % 2 == j % 2)
                    {
                        shape.FillColor = new Color(200, 200, 200);
                        shape.Position = new SFML.System.Vector2f(i * Program.proportion, j * Program.proportion);
                        Program.window.Draw(shape);
                    }
                }
            }
        }

        public void Draw()
        {
            DrawField();
            foreach (var aux in field)
            {
                if (aux != null)
                    aux.Draw(Program.window);
            }
        }
    }
}
