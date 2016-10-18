using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Utilities {
	/// <summary> Do not forget to close, to release resources. </summary>
	public static class Tools {
		/// <summary> Register all disposable resources here. </summary>
		public static void Close() {
			NotifyIcon?.Dispose();
		}

		public static Stopwatch StopWatch;
		public static NotifyIcon NotifyIcon = null;

		public static void Notify(object message, string title = null) {
			if (NotifyIcon == null) {
				NotifyIcon = new NotifyIcon() {
					Visible = true,
					Icon = SystemIcons.Information,
				};
			}
			if (title != null) NotifyIcon.BalloonTipTitle = title;
			NotifyIcon.BalloonTipText = message.ToString();
			NotifyIcon.ShowBalloonTip(10000);
		}

		public static void ShowError(string message, string title = "Error", int exitCode = -999) {
			MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
			if (exitCode != -999) Environment.Exit(exitCode);
		}

		static private Random r = new Random();
		public static T Pick<T>(IList<T> a) {
			return a[r.Next(a.Count)];
		}

		#region Read-only
		public static void SetReadOnly(string fileName) {
			File.SetAttributes(fileName, File.GetAttributes(fileName)
				| FileAttributes.ReadOnly);
		}
		public static void RemoveReadOnly(string fileName) {
			File.SetAttributes(fileName, File.GetAttributes(fileName)
				& ~FileAttributes.ReadOnly);
		}
		public static bool isReadOnly(string fileName) {
			return (File.GetAttributes(fileName) & FileAttributes.ReadOnly) != 0;
		}
		#endregion

		#region Process
		public static void Explorer(string path) {
			var startInfo = new ProcessStartInfo() {
				FileName = "explorer",
				WorkingDirectory = Environment.GetEnvironmentVariable("WinDir"),
				Arguments = path.Enquote()
			};
			Process.Start(startInfo);
		}
		#endregion
	}
}
