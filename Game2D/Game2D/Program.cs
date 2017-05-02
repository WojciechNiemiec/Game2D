using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

using Game2D.Controler;
using Game2D.Model;
using Game2D.Model.Components;

namespace Game2D
{
    class Program
    {
        static ContextSettings settings = new ContextSettings();
        static RenderWindow window = new RenderWindow(new VideoMode(1200, 700), "Game", Styles.Default, settings);
        static Color backgroundColor = new Color(5, 70, 55, 1);

        static void Main(string[] args)
        {
            Textures.init();
            window.Closed += Window_Closed;

            Computer computer = new Computer();
            
            while (window.IsOpen())
            {
                window.Clear(/*backgroundColor*/);

                computer.RequestActions();
                computer.PerformActions();
                computer.DrawObjects(window);
                computer.RemoveNotAliveObjects();

                window.DispatchEvents();
                window.Display();

                System.Threading.Thread.Sleep(15);
            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}
