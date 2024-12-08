using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace ProxyChangerOblivion
{
    internal class Program
    {
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption
          (IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int SETTINGS_CHANGED = 39;
        public const int REFRESH = 37;
        
        static void Main(string[] args)
        {
            int op;
            bool settingsReturn, refreshReturn;

            Console.WriteLine("*************Proxy Changer for Oblivion*************");
            Console.WriteLine("1 for ON");
            Console.WriteLine("0 for OFF");

            op = int.Parse(Console.ReadLine());

            RegistryKey registry = Registry.CurrentUser.OpenSubKey
               ("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            switch (op)
            {
                case 1:
                    {
                        registry.SetValue("ProxyEnable", 1);
                        registry.SetValue
                        ("ProxyServer", "127.0.0.1:8080");
                        if ((int)registry.GetValue("ProxyEnable", 0) == 0)
                            Console.WriteLine("Unable to enable the proxy.");
                        else
                            Console.WriteLine("The proxy has been turned on.");
                        break;
                    }
                case 0:
                    {
                        registry.SetValue("ProxyEnable", 0);
                        registry.SetValue("ProxyServer", 0);
                        if ((int)registry.GetValue("ProxyEnable", 1) == 1)
                            Console.WriteLine("Unable to disable the proxy.");
                        else
                            Console.WriteLine("The proxy has been turned off.");

                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalidooooo");
                        Console.ReadKey();
                        return;
                    }
            }
            registry.Close();
            settingsReturn = InternetSetOption
            (IntPtr.Zero, SETTINGS_CHANGED, IntPtr.Zero, 0);
            refreshReturn = InternetSetOption
            (IntPtr.Zero, REFRESH, IntPtr.Zero, 0);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey ();
        }
    }
}
 
