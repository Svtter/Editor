using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Editor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
            {
                Application.Run(new Notepad());
            }
            else
            {
                Application.Run(new Notepad(args[0]));
            }
        }
    }
}
