namespace SpeechContinuousRecognition
{
    partial class ContinuousSpeech
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContinuousSpeech));
            this.checkBoxUppercase = new System.Windows.Forms.CheckBox();
            this.checkBoxLowercase = new System.Windows.Forms.CheckBox();
            this.textBoxResultsLocal = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxTreatAsCommand = new System.Windows.Forms.CheckBox();
            this.checkBoxConvertWordsToSymbols = new System.Windows.Forms.CheckBox();
            this.checkBoxRemovePunctuation = new System.Windows.Forms.CheckBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCloseApplication = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.labelCurrentProcess = new System.Windows.Forms.Label();
            this.buttonRestartDragon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonVoiceAdministration = new System.Windows.Forms.Button();
            this.buttonDatabaseCommands = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxUppercase
            // 
            this.checkBoxUppercase.AutoSize = true;
            this.checkBoxUppercase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.checkBoxUppercase.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxUppercase.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxUppercase.Location = new System.Drawing.Point(68, 32);
            this.checkBoxUppercase.Name = "checkBoxUppercase";
            this.checkBoxUppercase.Size = new System.Drawing.Size(137, 29);
            this.checkBoxUppercase.TabIndex = 0;
            this.checkBoxUppercase.Text = "UPPERCASE";
            this.checkBoxUppercase.UseVisualStyleBackColor = false;
            this.checkBoxUppercase.Click += new System.EventHandler(this.checkBoxUppercase_Click);
            // 
            // checkBoxLowercase
            // 
            this.checkBoxLowercase.AutoSize = true;
            this.checkBoxLowercase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.checkBoxLowercase.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxLowercase.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxLowercase.Location = new System.Drawing.Point(68, 67);
            this.checkBoxLowercase.Name = "checkBoxLowercase";
            this.checkBoxLowercase.Size = new System.Drawing.Size(105, 25);
            this.checkBoxLowercase.TabIndex = 1;
            this.checkBoxLowercase.Text = "lowercase";
            this.checkBoxLowercase.UseVisualStyleBackColor = false;
            this.checkBoxLowercase.Click += new System.EventHandler(this.checkBoxLowercase_Click);
            // 
            // textBoxResultsLocal
            // 
            this.textBoxResultsLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResultsLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.textBoxResultsLocal.Font = new System.Drawing.Font("Cascadia Code SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxResultsLocal.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxResultsLocal.Location = new System.Drawing.Point(407, 7);
            this.textBoxResultsLocal.Multiline = true;
            this.textBoxResultsLocal.Name = "textBoxResultsLocal";
            this.textBoxResultsLocal.Size = new System.Drawing.Size(555, 628);
            this.textBoxResultsLocal.TabIndex = 2;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.labelStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelStatus.Font = new System.Drawing.Font("Britannic Bold", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(77, 166);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(147, 23);
            this.labelStatus.TabIndex = 3;
            this.labelStatus.Text = "Current Status";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxTreatAsCommand
            // 
            this.checkBoxTreatAsCommand.AutoSize = true;
            this.checkBoxTreatAsCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.checkBoxTreatAsCommand.Enabled = false;
            this.checkBoxTreatAsCommand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxTreatAsCommand.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxTreatAsCommand.Location = new System.Drawing.Point(222, 39);
            this.checkBoxTreatAsCommand.Name = "checkBoxTreatAsCommand";
            this.checkBoxTreatAsCommand.Size = new System.Drawing.Size(170, 25);
            this.checkBoxTreatAsCommand.TabIndex = 4;
            this.checkBoxTreatAsCommand.Text = "Treat as Command";
            this.checkBoxTreatAsCommand.UseVisualStyleBackColor = false;
            this.checkBoxTreatAsCommand.Visible = false;
            // 
            // checkBoxConvertWordsToSymbols
            // 
            this.checkBoxConvertWordsToSymbols.AutoSize = true;
            this.checkBoxConvertWordsToSymbols.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.checkBoxConvertWordsToSymbols.Checked = true;
            this.checkBoxConvertWordsToSymbols.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxConvertWordsToSymbols.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxConvertWordsToSymbols.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxConvertWordsToSymbols.Location = new System.Drawing.Point(68, 98);
            this.checkBoxConvertWordsToSymbols.Name = "checkBoxConvertWordsToSymbols";
            this.checkBoxConvertWordsToSymbols.Size = new System.Drawing.Size(231, 25);
            this.checkBoxConvertWordsToSymbols.TabIndex = 5;
            this.checkBoxConvertWordsToSymbols.Text = "Convert Words to Symbols";
            this.checkBoxConvertWordsToSymbols.UseVisualStyleBackColor = false;
            // 
            // checkBoxRemovePunctuation
            // 
            this.checkBoxRemovePunctuation.AutoSize = true;
            this.checkBoxRemovePunctuation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.checkBoxRemovePunctuation.Checked = true;
            this.checkBoxRemovePunctuation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRemovePunctuation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRemovePunctuation.ForeColor = System.Drawing.SystemColors.Control;
            this.checkBoxRemovePunctuation.Location = new System.Drawing.Point(68, 129);
            this.checkBoxRemovePunctuation.Name = "checkBoxRemovePunctuation";
            this.checkBoxRemovePunctuation.Size = new System.Drawing.Size(189, 25);
            this.checkBoxRemovePunctuation.TabIndex = 6;
            this.checkBoxRemovePunctuation.Text = "Remove Punctuation";
            this.checkBoxRemovePunctuation.UseVisualStyleBackColor = false;
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonStop.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonStop.Location = new System.Drawing.Point(79, 303);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(199, 36);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonStart.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonStart.Location = new System.Drawing.Point(79, 261);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(199, 36);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonCloseApplication
            // 
            this.buttonCloseApplication.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonCloseApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCloseApplication.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonCloseApplication.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCloseApplication.Location = new System.Drawing.Point(78, 345);
            this.buttonCloseApplication.Name = "buttonCloseApplication";
            this.buttonCloseApplication.Size = new System.Drawing.Size(199, 36);
            this.buttonCloseApplication.TabIndex = 9;
            this.buttonCloseApplication.Text = "Close Application";
            this.buttonCloseApplication.UseVisualStyleBackColor = false;
            this.buttonCloseApplication.Click += new System.EventHandler(this.buttonCloseApplication_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(77, 515);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 36);
            this.button1.TabIndex = 10;
            this.button1.Text = "Testing";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // labelCurrentProcess
            // 
            this.labelCurrentProcess.AutoSize = true;
            this.labelCurrentProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.labelCurrentProcess.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCurrentProcess.ForeColor = System.Drawing.SystemColors.Control;
            this.labelCurrentProcess.Location = new System.Drawing.Point(171, 7);
            this.labelCurrentProcess.Name = "labelCurrentProcess";
            this.labelCurrentProcess.Size = new System.Drawing.Size(100, 17);
            this.labelCurrentProcess.TabIndex = 11;
            this.labelCurrentProcess.Text = "CurrentProcess";
            // 
            // buttonRestartDragon
            // 
            this.buttonRestartDragon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonRestartDragon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRestartDragon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonRestartDragon.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonRestartDragon.Location = new System.Drawing.Point(78, 557);
            this.buttonRestartDragon.Name = "buttonRestartDragon";
            this.buttonRestartDragon.Size = new System.Drawing.Size(199, 36);
            this.buttonRestartDragon.TabIndex = 12;
            this.buttonRestartDragon.Text = "Restart Dragon";
            this.buttonRestartDragon.UseVisualStyleBackColor = false;
            this.buttonRestartDragon.Click += new System.EventHandler(this.buttonRestartDragon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(70, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Current Process";
            // 
            // buttonVoiceAdministration
            // 
            this.buttonVoiceAdministration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonVoiceAdministration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonVoiceAdministration.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonVoiceAdministration.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonVoiceAdministration.Location = new System.Drawing.Point(79, 387);
            this.buttonVoiceAdministration.Name = "buttonVoiceAdministration";
            this.buttonVoiceAdministration.Size = new System.Drawing.Size(199, 36);
            this.buttonVoiceAdministration.TabIndex = 14;
            this.buttonVoiceAdministration.Text = "Voice Administration";
            this.buttonVoiceAdministration.UseVisualStyleBackColor = false;
            this.buttonVoiceAdministration.Click += new System.EventHandler(this.buttonVoiceAdministration_Click);
            // 
            // buttonDatabaseCommands
            // 
            this.buttonDatabaseCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.buttonDatabaseCommands.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDatabaseCommands.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonDatabaseCommands.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonDatabaseCommands.Location = new System.Drawing.Point(79, 429);
            this.buttonDatabaseCommands.Name = "buttonDatabaseCommands";
            this.buttonDatabaseCommands.Size = new System.Drawing.Size(199, 36);
            this.buttonDatabaseCommands.TabIndex = 15;
            this.buttonDatabaseCommands.Text = "Database Commands";
            this.buttonDatabaseCommands.UseVisualStyleBackColor = false;
            this.buttonDatabaseCommands.Click += new System.EventHandler(this.buttonDatabaseCommands_Click);
            // 
            // ContinuousSpeech
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(968, 644);
            this.Controls.Add(this.buttonDatabaseCommands);
            this.Controls.Add(this.buttonVoiceAdministration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRestartDragon);
            this.Controls.Add(this.labelCurrentProcess);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonCloseApplication);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.checkBoxRemovePunctuation);
            this.Controls.Add(this.checkBoxConvertWordsToSymbols);
            this.Controls.Add(this.checkBoxTreatAsCommand);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.textBoxResultsLocal);
            this.Controls.Add(this.checkBoxLowercase);
            this.Controls.Add(this.checkBoxUppercase);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContinuousSpeech";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContinuousSpeech_FormClosing);
            this.Load += new System.EventHandler(this.ContinuousSpeech_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox checkBoxUppercase;
        private CheckBox checkBoxLowercase;
        private TextBox textBoxResultsLocal;
        private Label labelStatus;
        private CheckBox checkBoxTreatAsCommand;
        private CheckBox checkBoxConvertWordsToSymbols;
        private CheckBox checkBoxRemovePunctuation;
        private Button buttonStop;
        private Button buttonStart;
        private Button buttonCloseApplication;
        private Button button1;
        private Label labelCurrentProcess;
        private Button buttonRestartDragon;
        private Label label1;
        private Button buttonVoiceAdministration;
		private Button buttonDatabaseCommands;
	}
}