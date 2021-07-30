using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UnlaunchEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.Title = "Unlaunch.dsi Editor - Console Log";
            Console.WriteLine("The application is currently in early stages, more will come soon.");
            Console.WriteLine();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}