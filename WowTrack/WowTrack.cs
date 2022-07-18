using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Threading;


namespace WowTrack
{
    [RunInstaller(true)]
    public partial class WowTrack : ServiceBase
    {
        int scheduleTime = Convert.ToInt32(ConfigurationSettings.AppSettings["ThreadTime"]);
        public Thread worker = null;
        public WowTrack()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ThreadStart start = new ThreadStart(Working);
                worker = new Thread(start);
                worker.Start();
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public void Working()
        {
            StartZoomLoop();
        }

        public static void StartZoomLoop()
        {
            //Checks if zoom is currently running
            Process[] list = Process.GetProcessesByName("zoom");
            if (list.Length < 1)
            {
                // Recheck zoom if is not running
                string path = "C:\\wowtrack.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(string.Format("The tracking has started... "+DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")+" No zoom running, checking again in 3 seconds"));
                    writer.Close();
                }
                int sleepTime = 3000; //60000; // in mills
                Task.Delay(sleepTime).Wait();
                // or
                //Thread.Sleep(sleepTime);
                StartZoomLoop();
            }
            else
            {
                // Executes if zoom is running
                try
                {

                    foreach (Process p in list)
                    {
                        p.Kill();
                        //Write a Dialog message
                        //asking if the user accepts Wowbii's terms of usage and charges associated with it,
                        //if No then close all Zoom processes and startzoomloop again
                        //if Yes then calculate how long zoom is active for and send it after sending a log to the database


                        ///////////////////
                        //Process[] cam = Process.GetProcessesByName("camera");
                        //checking if camera and audio are in use
                    }
                    StartZoomLoop();
                }
                catch (Exception e)
                {
                    string path = "C:\\wowtrack.txt";
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLine(string.Format("Error code "+e));
                        writer.Close();
                    }
                    StartZoomLoop();
                }
            }
        }

        protected override void OnStop()
        {
            try
            {
                if ((worker != null) & worker.IsAlive)
                {
                    worker.Abort();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
