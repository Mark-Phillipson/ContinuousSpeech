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
            if (disposing && (components2 != null))
            {
                components2.Dispose();
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
            panel1 = new Panel();
            textBoxResultsLocal = new TextBox();
            label1 = new Label();
            buttonRandomCommand = new Button();
            buttonRestartDragon = new Button();
            label3 = new Label();
            textBoxRegion = new TextBox();
            label2 = new Label();
            textBoxKey = new TextBox();
            buttonPrograms = new Button();
            buttonDatabaseCommands = new Button();
            buttonCloseApplication = new Button();
            buttonStopNoToggle = new Button();
            buttonStop = new Button();
            buttonStart = new Button();
            checkBoxRemovePunctuation = new CheckBox();
            checkBoxTreatAsCommandFirst = new CheckBox();
            labelStatus = new Label();
            checkBoxIncludeSpace = new CheckBox();
            labelCurrentProcess = new Label();
            checkBoxTreatAsCommand = new CheckBox();
            checkBoxLowercase = new CheckBox();
            checkBoxUppercase = new CheckBox();
            buttonStartWithoutToggle = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonStartWithoutToggle);
            panel1.Controls.Add(checkBoxIncludeSpace);
            panel1.Controls.Add(labelCurrentProcess);
            panel1.Controls.Add(checkBoxTreatAsCommand);
            panel1.Controls.Add(checkBoxLowercase);
            panel1.Controls.Add(checkBoxUppercase);
            panel1.Controls.Add(checkBoxRemovePunctuation);
            panel1.Controls.Add(checkBoxTreatAsCommandFirst);
            panel1.Controls.Add(labelStatus);
            panel1.Controls.Add(buttonStart);
            panel1.Controls.Add(buttonStopNoToggle);
            panel1.Controls.Add(buttonStop);
            panel1.Controls.Add(buttonCloseApplication);
            panel1.Controls.Add(buttonPrograms);
            panel1.Controls.Add(buttonDatabaseCommands);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBoxRegion);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBoxKey);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(buttonRandomCommand);
            panel1.Controls.Add(buttonRestartDragon);
            panel1.Controls.Add(textBoxResultsLocal);
            panel1.Location = new Point(7, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(851, 742);
            panel1.TabIndex = 27;
            // 
            // textBoxResultsLocal
            // 
            textBoxResultsLocal.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            textBoxResultsLocal.BackColor = Color.FromArgb(64, 0, 64);
            textBoxResultsLocal.Font = new Font("Cascadia Code SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxResultsLocal.ForeColor = SystemColors.Control;
            textBoxResultsLocal.Location = new Point(444, 3);
            textBoxResultsLocal.Multiline = true;
            textBoxResultsLocal.Name = "textBoxResultsLocal";
            textBoxResultsLocal.Size = new Size(556, 738);
            textBoxResultsLocal.TabIndex = 3;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(14, 630);
            label1.Name = "label1";
            label1.Size = new Size(386, 45);
            label1.TabIndex = 28;
            label1.Text = "Camel, Variable/Pascal, Period, Underscore, Hyphenate, Title, Upper, All Caps, Lower Gap, Lower/Trim, Kebab\r\n";
            // 
            // buttonRandomCommand
            // 
            buttonRandomCommand.Anchor = AnchorStyles.Left;
            buttonRandomCommand.BackColor = Color.FromArgb(64, 0, 64);
            buttonRandomCommand.FlatStyle = FlatStyle.Flat;
            buttonRandomCommand.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            buttonRandomCommand.ForeColor = Color.BurlyWood;
            buttonRandomCommand.Location = new Point(14, 525);
            buttonRandomCommand.Name = "buttonRandomCommand";
            buttonRandomCommand.Size = new Size(386, 97);
            buttonRandomCommand.TabIndex = 27;
            buttonRandomCommand.Text = "Command Tip";
            buttonRandomCommand.UseVisualStyleBackColor = false;
            buttonRandomCommand.Click += buttonRandomCommand_Click;
            // 
            // buttonRestartDragon
            // 
            buttonRestartDragon.Anchor = AnchorStyles.Left;
            buttonRestartDragon.BackColor = Color.FromArgb(64, 0, 64);
            buttonRestartDragon.FlatStyle = FlatStyle.Flat;
            buttonRestartDragon.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonRestartDragon.ForeColor = SystemColors.Control;
            buttonRestartDragon.Location = new Point(14, 482);
            buttonRestartDragon.Name = "buttonRestartDragon";
            buttonRestartDragon.Size = new Size(386, 35);
            buttonRestartDragon.TabIndex = 26;
            buttonRestartDragon.Text = "Restart Dragon";
            buttonRestartDragon.UseVisualStyleBackColor = false;
            buttonRestartDragon.Click += buttonRestartDragon_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(14, 427);
            label3.Name = "label3";
            label3.Size = new Size(77, 15);
            label3.TabIndex = 32;
            label3.Text = "Azure Region";
            // 
            // textBoxRegion
            // 
            textBoxRegion.Anchor = AnchorStyles.Left;
            textBoxRegion.Location = new Point(14, 449);
            textBoxRegion.Name = "textBoxRegion";
            textBoxRegion.Size = new Size(386, 23);
            textBoxRegion.TabIndex = 31;
            textBoxRegion.Text = "UKSouth";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(14, 375);
            label2.Name = "label2";
            label2.Size = new Size(120, 15);
            label2.TabIndex = 30;
            label2.Text = "Azure application key";
            // 
            // textBoxKey
            // 
            textBoxKey.Anchor = AnchorStyles.Left;
            textBoxKey.Location = new Point(14, 397);
            textBoxKey.Name = "textBoxKey";
            textBoxKey.PasswordChar = 'x';
            textBoxKey.Size = new Size(386, 23);
            textBoxKey.TabIndex = 29;
            textBoxKey.Text = "123";
            // 
            // buttonPrograms
            // 
            buttonPrograms.Anchor = AnchorStyles.Left;
            buttonPrograms.BackColor = Color.FromArgb(64, 0, 64);
            buttonPrograms.FlatStyle = FlatStyle.Flat;
            buttonPrograms.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonPrograms.ForeColor = SystemColors.Control;
            buttonPrograms.Location = new Point(212, 336);
            buttonPrograms.Name = "buttonPrograms";
            buttonPrograms.Size = new Size(190, 36);
            buttonPrograms.TabIndex = 34;
            buttonPrograms.Text = "Programs";
            buttonPrograms.UseVisualStyleBackColor = false;
            buttonPrograms.Click += buttonPrograms_Click;
            // 
            // buttonDatabaseCommands
            // 
            buttonDatabaseCommands.Anchor = AnchorStyles.Left;
            buttonDatabaseCommands.BackColor = Color.FromArgb(64, 0, 64);
            buttonDatabaseCommands.FlatStyle = FlatStyle.Flat;
            buttonDatabaseCommands.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDatabaseCommands.ForeColor = SystemColors.Control;
            buttonDatabaseCommands.Location = new Point(14, 336);
            buttonDatabaseCommands.Name = "buttonDatabaseCommands";
            buttonDatabaseCommands.Size = new Size(190, 36);
            buttonDatabaseCommands.TabIndex = 33;
            buttonDatabaseCommands.Text = "Database Commands";
            buttonDatabaseCommands.UseVisualStyleBackColor = false;
            buttonDatabaseCommands.Click += buttonDatabaseCommands_Click;
            // 
            // buttonCloseApplication
            // 
            buttonCloseApplication.Anchor = AnchorStyles.Left;
            buttonCloseApplication.BackColor = Color.FromArgb(64, 0, 64);
            buttonCloseApplication.FlatStyle = FlatStyle.Flat;
            buttonCloseApplication.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonCloseApplication.ForeColor = SystemColors.Control;
            buttonCloseApplication.Location = new Point(14, 289);
            buttonCloseApplication.Name = "buttonCloseApplication";
            buttonCloseApplication.Size = new Size(386, 36);
            buttonCloseApplication.TabIndex = 35;
            buttonCloseApplication.Text = "Close Application";
            buttonCloseApplication.UseVisualStyleBackColor = false;
            buttonCloseApplication.Click += buttonCloseApplication_Click;
            // 
            // buttonStopNoToggle
            // 
            buttonStopNoToggle.Anchor = AnchorStyles.Left;
            buttonStopNoToggle.BackColor = Color.FromArgb(64, 0, 64);
            buttonStopNoToggle.FlatStyle = FlatStyle.Flat;
            buttonStopNoToggle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStopNoToggle.ForeColor = SystemColors.Control;
            buttonStopNoToggle.Location = new Point(212, 247);
            buttonStopNoToggle.Name = "buttonStopNoToggle";
            buttonStopNoToggle.Size = new Size(190, 36);
            buttonStopNoToggle.TabIndex = 37;
            buttonStopNoToggle.Text = "Stop";
            buttonStopNoToggle.UseVisualStyleBackColor = false;
            buttonStopNoToggle.Click += buttonStopNoToggle_Click;
            // 
            // buttonStop
            // 
            buttonStop.Anchor = AnchorStyles.Left;
            buttonStop.BackColor = Color.FromArgb(64, 0, 64);
            buttonStop.FlatStyle = FlatStyle.Flat;
            buttonStop.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStop.ForeColor = SystemColors.Control;
            buttonStop.Location = new Point(13, 247);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(190, 36);
            buttonStop.TabIndex = 36;
            buttonStop.Text = "Stop && Toggle";
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // buttonStart
            // 
            buttonStart.Anchor = AnchorStyles.Left;
            buttonStart.BackColor = Color.FromArgb(64, 0, 64);
            buttonStart.FlatStyle = FlatStyle.Flat;
            buttonStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStart.ForeColor = SystemColors.Control;
            buttonStart.Location = new Point(13, 205);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(190, 36);
            buttonStart.TabIndex = 38;
            buttonStart.Text = "Start && Toggle";
            buttonStart.UseVisualStyleBackColor = false;
            buttonStart.Click += buttonStart_Click;
            // 
            // checkBoxRemovePunctuation
            // 
            checkBoxRemovePunctuation.Anchor = AnchorStyles.Left;
            checkBoxRemovePunctuation.AutoSize = true;
            checkBoxRemovePunctuation.BackColor = Color.FromArgb(64, 0, 64);
            checkBoxRemovePunctuation.Checked = true;
            checkBoxRemovePunctuation.CheckState = CheckState.Checked;
            checkBoxRemovePunctuation.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBoxRemovePunctuation.ForeColor = SystemColors.Control;
            checkBoxRemovePunctuation.Location = new Point(13, 133);
            checkBoxRemovePunctuation.Name = "checkBoxRemovePunctuation";
            checkBoxRemovePunctuation.Size = new Size(189, 25);
            checkBoxRemovePunctuation.TabIndex = 41;
            checkBoxRemovePunctuation.Text = "Remove Punctuation";
            checkBoxRemovePunctuation.UseVisualStyleBackColor = false;
            // 
            // checkBoxTreatAsCommandFirst
            // 
            checkBoxTreatAsCommandFirst.Anchor = AnchorStyles.Left;
            checkBoxTreatAsCommandFirst.AutoSize = true;
            checkBoxTreatAsCommandFirst.BackColor = Color.FromArgb(64, 0, 64);
            checkBoxTreatAsCommandFirst.Checked = true;
            checkBoxTreatAsCommandFirst.CheckState = CheckState.Checked;
            checkBoxTreatAsCommandFirst.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBoxTreatAsCommandFirst.ForeColor = SystemColors.Control;
            checkBoxTreatAsCommandFirst.Location = new Point(13, 102);
            checkBoxTreatAsCommandFirst.Name = "checkBoxTreatAsCommandFirst";
            checkBoxTreatAsCommandFirst.Size = new Size(206, 25);
            checkBoxTreatAsCommandFirst.TabIndex = 40;
            checkBoxTreatAsCommandFirst.Text = "Treat as Command First";
            checkBoxTreatAsCommandFirst.UseVisualStyleBackColor = false;
            // 
            // labelStatus
            // 
            labelStatus.Anchor = AnchorStyles.Left;
            labelStatus.AutoSize = true;
            labelStatus.BackColor = Color.FromArgb(64, 0, 64);
            labelStatus.FlatStyle = FlatStyle.Popup;
            labelStatus.Font = new Font("Britannic Bold", 15.75F, FontStyle.Italic, GraphicsUnit.Point);
            labelStatus.ForeColor = Color.Red;
            labelStatus.Location = new Point(13, 170);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(98, 23);
            labelStatus.TabIndex = 39;
            labelStatus.Text = "STOPPED";
            labelStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // checkBoxIncludeSpace
            // 
            checkBoxIncludeSpace.Anchor = AnchorStyles.Left;
            checkBoxIncludeSpace.AutoSize = true;
            checkBoxIncludeSpace.BackColor = Color.FromArgb(64, 0, 64);
            checkBoxIncludeSpace.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBoxIncludeSpace.ForeColor = SystemColors.Control;
            checkBoxIncludeSpace.Location = new Point(155, 68);
            checkBoxIncludeSpace.Name = "checkBoxIncludeSpace";
            checkBoxIncludeSpace.Size = new Size(135, 25);
            checkBoxIncludeSpace.TabIndex = 46;
            checkBoxIncludeSpace.Text = "Include Space";
            checkBoxIncludeSpace.UseVisualStyleBackColor = false;
            // 
            // labelCurrentProcess
            // 
            labelCurrentProcess.Anchor = AnchorStyles.Left;
            labelCurrentProcess.AutoSize = true;
            labelCurrentProcess.BackColor = Color.FromArgb(64, 0, 64);
            labelCurrentProcess.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            labelCurrentProcess.ForeColor = SystemColors.Control;
            labelCurrentProcess.Location = new Point(13, 8);
            labelCurrentProcess.Name = "labelCurrentProcess";
            labelCurrentProcess.Size = new Size(100, 17);
            labelCurrentProcess.TabIndex = 45;
            labelCurrentProcess.Text = "CurrentProcess";
            // 
            // checkBoxTreatAsCommand
            // 
            checkBoxTreatAsCommand.Anchor = AnchorStyles.Left;
            checkBoxTreatAsCommand.AutoSize = true;
            checkBoxTreatAsCommand.BackColor = Color.FromArgb(64, 0, 64);
            checkBoxTreatAsCommand.Enabled = false;
            checkBoxTreatAsCommand.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBoxTreatAsCommand.ForeColor = SystemColors.Control;
            checkBoxTreatAsCommand.Location = new Point(155, 34);
            checkBoxTreatAsCommand.Name = "checkBoxTreatAsCommand";
            checkBoxTreatAsCommand.Size = new Size(170, 25);
            checkBoxTreatAsCommand.TabIndex = 44;
            checkBoxTreatAsCommand.Text = "Treat as Command";
            checkBoxTreatAsCommand.UseVisualStyleBackColor = false;
            checkBoxTreatAsCommand.Visible = false;
            // 
            // checkBoxLowercase
            // 
            checkBoxLowercase.Anchor = AnchorStyles.Left;
            checkBoxLowercase.AutoSize = true;
            checkBoxLowercase.BackColor = Color.FromArgb(64, 0, 64);
            checkBoxLowercase.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            checkBoxLowercase.ForeColor = SystemColors.Control;
            checkBoxLowercase.Location = new Point(13, 68);
            checkBoxLowercase.Name = "checkBoxLowercase";
            checkBoxLowercase.Size = new Size(105, 25);
            checkBoxLowercase.TabIndex = 43;
            checkBoxLowercase.Text = "lowercase";
            checkBoxLowercase.UseVisualStyleBackColor = false;
            checkBoxLowercase.Click += checkBoxLowercase_Click;
            // 
            // checkBoxUppercase
            // 
            checkBoxUppercase.Anchor = AnchorStyles.Left;
            checkBoxUppercase.AutoSize = true;
            checkBoxUppercase.BackColor = Color.FromArgb(64, 0, 64);
            checkBoxUppercase.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            checkBoxUppercase.ForeColor = SystemColors.Control;
            checkBoxUppercase.Location = new Point(13, 33);
            checkBoxUppercase.Name = "checkBoxUppercase";
            checkBoxUppercase.Size = new Size(137, 29);
            checkBoxUppercase.TabIndex = 42;
            checkBoxUppercase.Text = "UPPERCASE";
            checkBoxUppercase.UseVisualStyleBackColor = false;
            checkBoxUppercase.Click += checkBoxUppercase_Click;
            // 
            // buttonStartWithoutToggle
            // 
            buttonStartWithoutToggle.Anchor = AnchorStyles.Left;
            buttonStartWithoutToggle.BackColor = Color.FromArgb(64, 0, 64);
            buttonStartWithoutToggle.FlatStyle = FlatStyle.Flat;
            buttonStartWithoutToggle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStartWithoutToggle.ForeColor = SystemColors.Control;
            buttonStartWithoutToggle.Location = new Point(212, 205);
            buttonStartWithoutToggle.Name = "buttonStartWithoutToggle";
            buttonStartWithoutToggle.Size = new Size(190, 36);
            buttonStartWithoutToggle.TabIndex = 47;
            buttonStartWithoutToggle.Text = "Start";
            buttonStartWithoutToggle.UseVisualStyleBackColor = false;
            buttonStartWithoutToggle.Click += buttonStartWithoutToggle_Click;
            // 
            // ContinuousSpeech
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 0, 64);
            ClientSize = new Size(1022, 760);
            Controls.Add(panel1);
            ForeColor = SystemColors.ControlLight;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ContinuousSpeech";
            Opacity = 0.8D;
            StartPosition = FormStartPosition.Manual;
            Text = "Coding by Voice";
            FormClosing += ContinuousSpeech_FormClosing;
            Load += ContinuousSpeech_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Label label1;
        private Button buttonRandomCommand;
        private Button buttonRestartDragon;
        private TextBox textBoxResultsLocal;
        private Button buttonStart;
        private Button buttonStopNoToggle;
        private Button buttonStop;
        private Button buttonCloseApplication;
        private Button buttonPrograms;
        private Button buttonDatabaseCommands;
        private Label label3;
        private TextBox textBoxRegion;
        private Label label2;
        private TextBox textBoxKey;
        private Button buttonStartWithoutToggle;
        private CheckBox checkBoxIncludeSpace;
        private Label labelCurrentProcess;
        private CheckBox checkBoxTreatAsCommand;
        private CheckBox checkBoxLowercase;
        private CheckBox checkBoxUppercase;
        private CheckBox checkBoxRemovePunctuation;
        private CheckBox checkBoxTreatAsCommandFirst;
        private Label labelStatus;
    }
}