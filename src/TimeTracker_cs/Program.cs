using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace TimeTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // A mutex is visible across processes.
            // If a mutex with a given name cannot be owned,
            // it must be owned by another process.
            bool created = false;
            using (Mutex appMutex = new Mutex(true, "ATK-TimeTracker", out created))
            {
                if (created)
                {
                    // Slight modification from the default Program.cs
                    // By creating the main form object first, then calling
                    // Application.Run() without a form, the form will not
                    // appear until explicitly shown, and the application will
                    // run even if it is unloaded.
                    ProjectsForm mainForm = new ProjectsForm();
                    Application.Run();
                }
                else
                {
                    MessageBox.Show("Another instance of TimeTracker is already running.");
                    // TODO: Alternatively, simply open the running instance by sending it a message...
                }
            }
        }
    }
}