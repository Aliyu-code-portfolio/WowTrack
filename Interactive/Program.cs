using SendGrid;
using SendGrid.Helpers.Mail;

namespace Interactive
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            Interact.AcceptTerms();

        }

    }
    public class Interact
    {
        public static void AcceptTerms()
        {
            DialogResult dialogResult = MessageBox.Show("By clicking below, you have accepted the charges attached to using this Wowbudd", "Accept Terms of Use", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    //SendEmail().Wait();
                    SaveToDB("Yes wow");
                }
                catch (Exception)
                {

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }


        static async Task SendEmail()
        {
           
        }
        static string[] GetModelAndClient()
        {
            try
            {
                int counter = 0;
                string[] info;
                info = new string[2];
                foreach (string line in System.IO.File.ReadLines(@"c:\Windows\Web\info.txt"))
                {
                    info[counter] = line;
                    counter++;
                }

                return info;
            }
            catch (IOException)
            {
                string[] info;
                info = new string[2];
                info[0] = "Can not find client name";
                info[1] = "Cannot find model number, please check if a file was deleted from the web folder of the wowbudd the wowbudd ";
                return info;
            }
        }
        static async void SaveToDB(string pen)
        {

        }

    }
}
