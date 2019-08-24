﻿using SteamAccountSwitcher.Models;
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
        string installDir = @"C:\Program Files (x86)\Steam\Steam.exe";

        public Steam(string installDir)
        {
            this.installDir = installDir;
        }

        public string InstallDir = "";
        
        public bool IsSteamRunning()
        {
            Process[] pname = Process.GetProcessesByName("steam");
            if (pname.Length == 0)
                return false;
            else
                return true;
        }

        public void KillSteam()
        {
            Process [] proc = Process.GetProcessesByName("steam");
	        proc[0].Kill();
        }

        public bool StartSteamAccount(SteamAccount a)
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
                        p.StartInfo = new ProcessStartInfo(installDir, a.getStartParameters());
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
            Process p = new Process();
            if (File.Exists(installDir))
            {
                p.StartInfo = new ProcessStartInfo(installDir, "-shutdown");
                p.Start();
                return true;
            }
            return false;
            
        }
    }
}
