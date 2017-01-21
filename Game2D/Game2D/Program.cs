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
        static RenderWindow window = new RenderWindow(new VideoMode(1200, 600), "Game", Styles.Default, settings);

        static void Main(string[] args)
        {
            //inits
            Textures.init();
            window.Closed += Window_Closed;
            //

            Computer computer = new Computer();
            
            while (window.IsOpen())
            {
                window.Clear();

                computer.RequestActions();
                computer.PerformActions();
                computer.DrawObjects(window);

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
