namespace ControlWSR
{
	partial class AvailableCommandsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvailableCommandsForm));
            this.richTextBoxAvailableCommands = new System.Windows.Forms.RichTextBox();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.TestingBtn = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxUppercase = new System.Windows.Forms.CheckBox();
            this.checkBoxLowercase = new System.Windows.Forms.CheckBox();
            this.checkBoxRemovePunctuation = new System.Windows.Forms.CheckBox();
            this.checkBoxTreatAsCommand = new System.Windows.Forms.CheckBox();
            this.checkBoxConvertWordsToSymbols = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // richTextBoxAvailableCommands
            // 
            this.richTextBoxAvailableCommands.Dock = System.Windows.Forms.DockStyle.Left;
            this.richTextBoxAvailableCommands.Font = new System.Drawing.Font("Cascadia Code SemiBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxAvailableCommands.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxAvailableCommands.Name = "richTextBoxAvailableCommands";
            this.richTextBoxAvailableCommands.ReadOnly = true;
            this.richTextBoxAvailableCommands.Size = new System.Drawing.Size(565, 782);
            this.richTextBoxAvailableCommands.TabIndex = 0;
            this.richTextBoxAvailableCommands.Text = "";
            // 
            // textBoxResults
            // 
            this.textBoxResults.Font = new System.Drawing.Font("Cascadia Code SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxResults.Location = new System.Drawing.Point(572, 12);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ReadOnly = true;
            this.textBoxResults.Size = new System.Drawing.Size(396, 390);
            this.textBoxResults.TabIndex = 1;
            // 
            // TestingBtn
            // 
            this.TestingBtn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TestingBtn.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestingBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TestingBtn.Location = new System.Drawing.Point(571, 659);
            this.TestingBtn.Name = "TestingBtn";
            this.TestingBtn.Size = new System.Drawing.Size(195, 32);
            this.TestingBtn.TabIndex = 2;
            this.TestingBtn.Text = "Start Continuous Dictation";
            this.TestingBtn.UseVisualStyleBackColor = false;
            this.TestingBtn.Visible = false;
            this.TestingBtn.Click += new System.EventHandler(this.TestingBtn_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(572, 417);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(392, 20);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(572, 444);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 18);
            this.labelStatus.TabIndex = 4;
            // 
            // checkBoxUppercase
            // 
            this.checkBoxUppercase.AutoSize = true;
            this.checkBoxUppercase.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBoxUppercase.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUppercase.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxUppercase.Location = new System.Drawing.Point(571, 465);
            this.checkBoxUppercase.Name = "checkBoxUppercase";
            this.checkBoxUppercase.Size = new System.Drawing.Size(101, 22);
            this.checkBoxUppercase.TabIndex = 5;
            this.checkBoxUppercase.Text = "UPPERCASE";
            this.checkBoxUppercase.UseVisualStyleBackColor = false;
            this.checkBoxUppercase.Click += new System.EventHandler(this.checkBoxUppercase_Click);
            // 
            // checkBoxLowercase
            // 
            this.checkBoxLowercase.AutoSize = true;
            this.checkBoxLowercase.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBoxLowercase.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxLowercase.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxLowercase.Location = new System.Drawing.Point(678, 465);
            this.checkBoxLowercase.Name = "checkBoxLowercase";
            this.checkBoxLowercase.Size = new System.Drawing.Size(88, 22);
            this.checkBoxLowercase.TabIndex = 6;
            this.checkBoxLowercase.Text = "lowercase";
            this.checkBoxLowercase.UseVisualStyleBackColor = false;
            this.checkBoxLowercase.Click += new System.EventHandler(this.checkBoxLowercase_Click);
            // 
            // checkBoxRemovePunctuation
            // 
            this.checkBoxRemovePunctuation.AutoSize = true;
            this.checkBoxRemovePunctuation.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBoxRemovePunctuation.Checked = true;
            this.checkBoxRemovePunctuation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRemovePunctuation.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRemovePunctuation.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxRemovePunctuation.Location = new System.Drawing.Point(772, 465);
            this.checkBoxRemovePunctuation.Name = "checkBoxRemovePunctuation";
            this.checkBoxRemovePunctuation.Size = new System.Drawing.Size(145, 22);
            this.checkBoxRemovePunctuation.TabIndex = 7;
            this.checkBoxRemovePunctuation.Text = "Remove Punctuation";
            this.checkBoxRemovePunctuation.UseVisualStyleBackColor = false;
            // 
            // checkBoxTreatAsCommand
            // 
            this.checkBoxTreatAsCommand.AutoSize = true;
            this.checkBoxTreatAsCommand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBoxTreatAsCommand.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxTreatAsCommand.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxTreatAsCommand.Location = new System.Drawing.Point(571, 493);
            this.checkBoxTreatAsCommand.Name = "checkBoxTreatAsCommand";
            this.checkBoxTreatAsCommand.Size = new System.Drawing.Size(137, 22);
            this.checkBoxTreatAsCommand.TabIndex = 8;
            this.checkBoxTreatAsCommand.Text = "Treat as Command";
            this.checkBoxTreatAsCommand.UseVisualStyleBackColor = false;
            // 
            // checkBoxConvertWordsToSymbols
            // 
            this.checkBoxConvertWordsToSymbols.AutoSize = true;
            this.checkBoxConvertWordsToSymbols.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.checkBoxConvertWordsToSymbols.Checked = true;
            this.checkBoxConvertWordsToSymbols.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxConvertWordsToSymbols.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxConvertWordsToSymbols.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxConvertWordsToSymbols.Location = new System.Drawing.Point(714, 493);
            this.checkBoxConvertWordsToSymbols.Name = "checkBoxConvertWordsToSymbols";
            this.checkBoxConvertWordsToSymbols.Size = new System.Drawing.Size(187, 22);
            this.checkBoxConvertWordsToSymbols.TabIndex = 9;
            this.checkBoxConvertWordsToSymbols.Text = "Convert Words to Symbols";
            this.checkBoxConvertWordsToSymbols.UseVisualStyleBackColor = false;
            // 
            // AvailableCommandsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 782);
            this.Controls.Add(this.checkBoxConvertWordsToSymbols);
            this.Controls.Add(this.checkBoxTreatAsCommand);
            this.Controls.Add(this.checkBoxRemovePunctuation);
            this.Controls.Add(this.checkBoxLowercase);
            this.Controls.Add(this.checkBoxUppercase);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.TestingBtn);
            this.Controls.Add(this.textBoxResults);
            this.Controls.Add(this.richTextBoxAvailableCommands);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AvailableCommandsForm";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Available Commands";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AvailableCommandsForm_FormClosing);
            this.Load += new System.EventHandler(this.AvailableCommandsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBoxAvailableCommands;
		private System.Windows.Forms.TextBox textBoxResults;
		private System.Windows.Forms.Button TestingBtn;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.CheckBox checkBoxUppercase;
        private System.Windows.Forms.CheckBox checkBoxLowercase;
        private System.Windows.Forms.CheckBox checkBoxRemovePunctuation;
        private System.Windows.Forms.CheckBox checkBoxTreatAsCommand;
        private System.Windows.Forms.CheckBox checkBoxConvertWordsToSymbols;
    }
}

