using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Shell
{
    class Shell
    {
        public void ExecuteSingleProcess(string sCommand)
        {
            string[] asCommand = sCommand.Split(' ');
            Process p = new Process();
            p.StartInfo.FileName = asCommand[0];
            p.StartInfo.UseShellExecute = false;
            
            p.Start();
            if (asCommand[asCommand.Length-1] != "&"){
                p.WaitForExit();
            } 
        }
        public void KillProcess(string sCommand)
        {
            string[] asCommand = sCommand.Split(' ');
            int iPid = 0;
            if (int.TryParse(asCommand[1].Trim(), out iPid))
            {
                Process.GetProcessById(int.Parse(asCommand[1])).Kill();
            }
            else
            {
                foreach(Process p in Process.GetProcessesByName(asCommand[1])){
                    p.Kill();
                }
            }
        }
        public void Execute(string sFullCommand)
        {
            try
            {
                if (sFullCommand == "")
                    return;
                if (sFullCommand.StartsWith("kill"))
                {
                    KillProcess(sFullCommand);
                }
                else if (sFullCommand == "exit")
                {
                    Environment.Exit(0);
                }
                else
                {
                    ExecuteSingleProcess(sFullCommand);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Run()
        {
            int cLines = 0;
            while (true)
            {
                Console.Write(cLines + " > ");
                string sLine = Console.ReadLine();
                Execute(sLine.Trim());
                cLines++;
            }
        }
    }
}
