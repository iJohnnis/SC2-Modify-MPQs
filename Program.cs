using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC2Editor {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Modifier.Modify(@"D:\Games\StarCraft II\SC2 - Editor\Mods\Mass Recall - Zerg");
			//Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new Form1());
			Utilities.Tools.Close(); // Always last!
		}
	}
}
