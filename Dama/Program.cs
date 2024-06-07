using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace Dama
{
    class Program
    {
        public static RenderWindow window;
        public const int proportion = 100;
        public static Field field;
        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(proportion * 10, proportion * 10), "Dama");
            field = new Field();
            window.SetVerticalSyncEnabled(true);
            window.Closed += (sender, args) => Close();
            window.MouseButtonPressed += field.ClieckedField;
            Logger.Grade = 600;

            

            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(new Color(50, 50, 50));

                field.Draw();

                window.Display();
            }
        }

        public static void Close()
        {
            window.Close();
        }
    }
}
