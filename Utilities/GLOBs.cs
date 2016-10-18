using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

class GLOBs {
	private const string SETTINGS_PATH = "Settings.xml";

	public static Dictionary<string, string> VARs = new Dictionary<string, string>();

	private static string _basePath = null;
	public static string BasePath {
		get {
			if (_basePath == null)
				_basePath = AppDomain.CurrentDomain.BaseDirectory;
			return _basePath;
		}
	}

	private static string _settingsPath = null;
	public static string SettingsPath {
		get {
			if (_settingsPath == null)
				_settingsPath = Path.Combine(BasePath, SETTINGS_PATH);
			return _settingsPath;
		}
	}

	private static bool settingsChanged = false;
	public static void Save() {
		if (settingsChanged == true)
			document.Save(SettingsPath);
	}

	private static XmlDocument document;

	public static string GameDirectory {
		get {	return document.Id("Game Directory")?.Value; }
		set {
			if (document == null) {
				document = new XmlDocument();
				string path = Path.Combine(SettingsPath, SETTINGS_PATH);
				if (File.Exists(path)) {
					document.Load(path);
				} else {
					document.CreateXmlDeclaration("1.0", "UTF-8", null);
					document.CreateElement("Settings");
				}
			}
			var elem = document.Id("Game Directory");
			if (elem.Value != value) {
				settingsChanged = true;
				elem.Value = value;
			}
		}
	}
}

