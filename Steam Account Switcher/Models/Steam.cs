using SteamAccountSwitcher.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountSwitcher.Models
{
    class Steam
    {
        public string installDir = Properties.Settings.Default.SteamDirectory;

        public Steam(string installDir)
        {
            this.installDir = installDir;
        }
        
        public bool IsSteamRunning()
        {
            Process[] processName = Process.GetProcessesByName("steam");
            if (processName.Length == 0)
                return false;
            else
                return true;
        }

        public void KillSteam()
        {
            foreach (var process in Process.GetProcessesByName("steam"))
            {
                process.Kill();
            }
        }

        public bool StartSteamAccount(SteamAccount account)
        {
            bool finished = false;

            if(IsSteamRunning())
            {
                KillSteam();
            }

            while (finished == false)
            {
                if (IsSteamRunning() == false)
                {
                    Process p = new Process();
                    if (File.Exists(installDir))
                    {
                        p.StartInfo = new ProcessStartInfo(installDir, account.getStartParameters());
                        p.Start();
                        finished = true;
                        return true;
                    }
                }
            }
            return false;
        }


        public bool LogoutSteam()
        {
            Process process = new Process();
            if (File.Exists(installDir))
            {
                process.StartInfo = new ProcessStartInfo(installDir, "-shutdown");
                process.Start();
                return true;
            }
            return false;
            
        }
    }
}
