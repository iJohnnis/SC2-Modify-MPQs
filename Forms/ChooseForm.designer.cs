namespace Utilities {
	partial class SelectForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.comboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// comboBox
			// 
			this.comboBox.Cursor = System.Windows.Forms.Cursors.Hand;
			this.comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox.FormattingEnabled = true;
			this.comboBox.Location = new System.Drawing.Point(12, 12);
			this.comboBox.MaxDropDownItems = 12;
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new System.Drawing.Size(203, 21);
			this.comboBox.TabIndex = 1;
			this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
			this.comboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.comboBox_KeyUp);
			// 
			// ChooseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(227, 43);
			this.Controls.Add(this.comboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.Name = "ChooseForm";
			this.Text = "ChooseForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox;
	}
}