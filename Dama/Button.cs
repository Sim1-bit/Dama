using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Dama
{
    class Button
    {
        public event EventHandler Click;
        public event EventHandler Releas;
        public event EventHandler MouseOn;
        public event EventHandler MouseLeft;

        public RectangleShape shape;
        private bool isPressed;
        private bool isMouseOver;

        public Button(Vector2f position, Vector2f size, Color colorButton)
        {
            isPressed = false;
            isMouseOver = false;
            shape = new RectangleShape(size)
            {
                Position = position,
                FillColor = colorButton
            };
            this.Size = size;
        }

        public Button(Vector2f position, Color colorButton, Vector2f size)
        {
            isPressed = false;
            isMouseOver = false;
            shape = new RectangleShape(size)
            {
                Position = position,
                FillColor = colorButton
            };
            this.Size = size;
        }
        public Button(Button value) : this(value.Position, value.Size, value.shape.FillColor)
        {

        }

        public virtual Vector2f Position
        {
            get
            {
                return shape.Position;
            }
            set
            {
                shape.Position = value;
            }
        }

        public Vector2f Size
        {
            get
            {
                return shape.Size;
            }
            set
            {
                shape.Size = value;
            }
        }

        public virtual void Draw(RenderWindow window)
        {
            DrawAspect(window);
            Update(window);
        }

        public void DrawAspect(RenderWindow window)
        {
            window.Draw(shape);
        }

        public virtual void Update(RenderWindow window)
        {

            Vector2i mousePosition = Mouse.GetPosition(window);
            bool previouslyOver = isMouseOver;
            isMouseOver = shape.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y);

            if(isMouseOver && ! previouslyOver)
            {
                OnMouseEntered();
            }
            else if(!isMouseOver && previouslyOver)
            {
                OnMouseLeft();
            }

            if (isMouseOver && Mouse.IsButtonPressed(Mouse.Button.Left) && !isPressed)
            {
                OnClicked();
                isPressed = true;
            }
            else if (isPressed && window.HasFocus() && !Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                OnReleased();
                isPressed = false;
            }
        }

        protected virtual void OnClicked()
        {
            Click?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnReleased()
        {
            Releas?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMouseEntered()
        {
            MouseOn?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnMouseLeft()
        {
            MouseLeft?.Invoke(this, EventArgs.Empty);
        }
    }
}
