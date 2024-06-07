using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama
{
    class Piece : Button
    {
        public readonly bool color;
        public Vector2i position;
        public override Vector2f Position
        {
            get => new Vector2f(position.X, position.Y);
            set
            {
                position = new Vector2i((int)value.X, (int)value.Y);
                shape.Position = new Vector2f(position.X * Program.proportion + Program.proportion / 4, position.Y * Program.proportion + Program.proportion / 4);
            }
        }
        public Piece(bool color, int x, int y) : base(new Vector2f(x * Program.proportion + Program.proportion / 4, y * Program.proportion + Program.proportion / 4), color ? Color.White : Color.Black, new Vector2f(Program.proportion / 2, Program.proportion / 2))
        {
            this.color = color;
            this.position = new Vector2i(x, y);
        }
    }
}
