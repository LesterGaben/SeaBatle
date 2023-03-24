using System;
using System.Windows.Forms;

namespace SeaBatle {
    /// <summary>
    /// Головна точка входу до програми
    /// </summary>
    internal static class Program {
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }
    }
}
