using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Utilities {
	public partial class SelectForm:Form {
		public SelectForm() {
			InitializeComponent();
		}

		public string Title {
			set { Text = value; }
		}
		private string[] _choices;
		public string[] Choices {
			set {
				_choices = value;
				comboBox.Items.Clear();
				comboBox.Items.AddRange(value);
			}
			get { return _choices; }
		}
		public string Value => _choices[comboBox.SelectedIndex];

		private static SelectForm form = null;
		public static string Select(string name, params string[] choices) {
			if (choices == null || choices.Length <= 0)
				throw new CException("Invalid choices!", "Choose Form", -1);

			Tools.StopWatch?.Stop();

			if (form == null) form = new SelectForm();
			form.Title = name;
			form.Choices = choices;

			var result = form.ShowDialog();
			Tools.StopWatch?.Start();
			if (result == DialogResult.OK)
				return form.Value;
			return null;
		}
		public static string Select(string name, IEnumerable<string> choices) {
			return Select(name, choices.ToArray());
		}

		private bool instantReturn = true;
		private void comboBox_KeyUp(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Tab:
					instantReturn = false;
					comboBox.SelectedIndex = (comboBox.SelectedIndex + 1) % _choices.Length;
					instantReturn = true;
					break;
				case Keys.Enter:
					DialogResult = DialogResult.OK;
					break;
			}
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (instantReturn) DialogResult = DialogResult.OK;
		}
	}
}
