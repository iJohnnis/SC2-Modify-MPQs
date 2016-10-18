using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SC2Editor {
	class Modifier {
		public static void Modify(string directoryPath) {
			var paramDoc = new XmlDocument();
			paramDoc.Load(Path.Combine(directoryPath, "Params.xml"));
			var paramsElem = paramDoc.DocumentElement;

			// Clean up work directory
			string workPath = Path.Combine(GLOBs.BasePath, "Work");
			if (Directory.Exists(workPath)) {
				foreach (var filePath in Directory.GetFiles(workPath))
					File.Delete(filePath);
				foreach (var dirPath in Directory.GetDirectories(workPath))
					Directory.Delete(dirPath, true);
			} else Directory.CreateDirectory(workPath);

			foreach (var useTemplate in paramsElem.Es("UseTemplate")) {
				addTemplateFiles(useTemplate.Value);
			}
		}

		private static void addTemplateFiles(string templateName) {
			var tdir = Path.Combine(GLOBs.BasePath, templateName);
			var files = Directory.GetFiles(tdir, "*.xml");
		}
		private static void addXML(string source, string target) {
			if (File.Exists(target)) {
				File.Copy(source, target);
				return;
			}
	}
}
