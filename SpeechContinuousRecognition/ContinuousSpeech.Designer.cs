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
      checkBoxUppercase = new CheckBox();
      checkBoxLowercase = new CheckBox();
      textBoxResultsLocal = new TextBox();
      labelStatus = new Label();
      checkBoxTreatAsCommand = new CheckBox();
      checkBoxTreatAsCommandFirst = new CheckBox();
      checkBoxRemovePunctuation = new CheckBox();
      buttonStop = new Button();
      buttonStart = new Button();
      buttonCloseApplication = new Button();
      labelCurrentProcess = new Label();
      buttonRestartDragon = new Button();
      buttonDatabaseCommands = new Button();
      textBoxKey = new TextBox();
      label2 = new Label();
      label3 = new Label();
      textBoxRegion = new TextBox();
      buttonRandomCommand = new Button();
      checkBoxIncludeSpace = new CheckBox();
      buttonStartWithoutToggle = new Button();
      buttonStopNoToggle = new Button();
      label1 = new Label();
      buttonPrograms = new Button();
      SuspendLayout();
      // 
      // checkBoxUppercase
      // 
      checkBoxUppercase.AutoSize = true;
      checkBoxUppercase.BackColor = Color.FromArgb(64, 0, 64);
      checkBoxUppercase.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
      checkBoxUppercase.ForeColor = SystemColors.Control;
      checkBoxUppercase.Location = new Point(19, 35);
      checkBoxUppercase.Name = "checkBoxUppercase";
      checkBoxUppercase.Size = new Size(137, 29);
      checkBoxUppercase.TabIndex = 0;
      checkBoxUppercase.Text = "UPPERCASE";
      checkBoxUppercase.UseVisualStyleBackColor = false;
      checkBoxUppercase.Click += checkBoxUppercase_Click;
      // 
      // checkBoxLowercase
      // 
      checkBoxLowercase.AutoSize = true;
      checkBoxLowercase.BackColor = Color.FromArgb(64, 0, 64);
      checkBoxLowercase.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      checkBoxLowercase.ForeColor = SystemColors.Control;
      checkBoxLowercase.Location = new Point(19, 70);
      checkBoxLowercase.Name = "checkBoxLowercase";
      checkBoxLowercase.Size = new Size(105, 25);
      checkBoxLowercase.TabIndex = 1;
      checkBoxLowercase.Text = "lowercase";
      checkBoxLowercase.UseVisualStyleBackColor = false;
      checkBoxLowercase.Click += checkBoxLowercase_Click;
      // 
      // textBoxResultsLocal
      // 
      textBoxResultsLocal.BackColor = Color.FromArgb(64, 0, 64);
      textBoxResultsLocal.Dock = DockStyle.Right;
      textBoxResultsLocal.Font = new Font("Cascadia Code SemiBold", 12F, FontStyle.Bold, GraphicsUnit.Point);
      textBoxResultsLocal.ForeColor = SystemColors.Control;
      textBoxResultsLocal.Location = new Point(415, 0);
      textBoxResultsLocal.Multiline = true;
      textBoxResultsLocal.Name = "textBoxResultsLocal";
      textBoxResultsLocal.Size = new Size(695, 679);
      textBoxResultsLocal.TabIndex = 2;
      // 
      // labelStatus
      // 
      labelStatus.AutoSize = true;
      labelStatus.BackColor = Color.FromArgb(64, 0, 64);
      labelStatus.FlatStyle = FlatStyle.Popup;
      labelStatus.Font = new Font("Britannic Bold", 15.75F, FontStyle.Italic, GraphicsUnit.Point);
      labelStatus.ForeColor = Color.Red;
      labelStatus.Location = new Point(19, 169);
      labelStatus.Name = "labelStatus";
      labelStatus.Size = new Size(98, 23);
      labelStatus.TabIndex = 3;
      labelStatus.Text = "STOPPED";
      labelStatus.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // checkBoxTreatAsCommand
      // 
      checkBoxTreatAsCommand.AutoSize = true;
      checkBoxTreatAsCommand.BackColor = Color.FromArgb(64, 0, 64);
      checkBoxTreatAsCommand.Enabled = false;
      checkBoxTreatAsCommand.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      checkBoxTreatAsCommand.ForeColor = SystemColors.Control;
      checkBoxTreatAsCommand.Location = new Point(161, 36);
      checkBoxTreatAsCommand.Name = "checkBoxTreatAsCommand";
      checkBoxTreatAsCommand.Size = new Size(170, 25);
      checkBoxTreatAsCommand.TabIndex = 4;
      checkBoxTreatAsCommand.Text = "Treat as Command";
      checkBoxTreatAsCommand.UseVisualStyleBackColor = false;
      checkBoxTreatAsCommand.Visible = false;
      // 
      // checkBoxTreatAsCommandFirst
      // 
      checkBoxTreatAsCommandFirst.AutoSize = true;
      checkBoxTreatAsCommandFirst.BackColor = Color.FromArgb(64, 0, 64);
      checkBoxTreatAsCommandFirst.Checked = true;
      checkBoxTreatAsCommandFirst.CheckState = CheckState.Checked;
      checkBoxTreatAsCommandFirst.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      checkBoxTreatAsCommandFirst.ForeColor = SystemColors.Control;
      checkBoxTreatAsCommandFirst.Location = new Point(19, 101);
      checkBoxTreatAsCommandFirst.Name = "checkBoxTreatAsCommandFirst";
      checkBoxTreatAsCommandFirst.Size = new Size(206, 25);
      checkBoxTreatAsCommandFirst.TabIndex = 5;
      checkBoxTreatAsCommandFirst.Text = "Treat as Command First";
      checkBoxTreatAsCommandFirst.UseVisualStyleBackColor = false;
      // 
      // checkBoxRemovePunctuation
      // 
      checkBoxRemovePunctuation.AutoSize = true;
      checkBoxRemovePunctuation.BackColor = Color.FromArgb(64, 0, 64);
      checkBoxRemovePunctuation.Checked = true;
      checkBoxRemovePunctuation.CheckState = CheckState.Checked;
      checkBoxRemovePunctuation.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      checkBoxRemovePunctuation.ForeColor = SystemColors.Control;
      checkBoxRemovePunctuation.Location = new Point(19, 132);
      checkBoxRemovePunctuation.Name = "checkBoxRemovePunctuation";
      checkBoxRemovePunctuation.Size = new Size(189, 25);
      checkBoxRemovePunctuation.TabIndex = 6;
      checkBoxRemovePunctuation.Text = "Remove Punctuation";
      checkBoxRemovePunctuation.UseVisualStyleBackColor = false;
      // 
      // buttonStop
      // 
      buttonStop.BackColor = Color.FromArgb(64, 0, 64);
      buttonStop.FlatStyle = FlatStyle.Flat;
      buttonStop.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      buttonStop.ForeColor = SystemColors.Control;
      buttonStop.Location = new Point(20, 243);
      buttonStop.Name = "buttonStop";
      buttonStop.Size = new Size(190, 36);
      buttonStop.TabIndex = 7;
      buttonStop.Text = "Stop && Toggle";
      buttonStop.UseVisualStyleBackColor = false;
      buttonStop.Click += buttonStop_Click;
      // 
      // buttonStart
      // 
      buttonStart.BackColor = Color.FromArgb(64, 0, 64);
      buttonStart.FlatStyle = FlatStyle.Flat;
      buttonStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      buttonStart.ForeColor = SystemColors.Control;
      buttonStart.Location = new Point(20, 200);
      buttonStart.Name = "buttonStart";
      buttonStart.Size = new Size(190, 36);
      buttonStart.TabIndex = 8;
      buttonStart.Text = "Start && Toggle";
      buttonStart.UseVisualStyleBackColor = false;
      buttonStart.Click += buttonStart_Click;
      // 
      // buttonCloseApplication
      // 
      buttonCloseApplication.BackColor = Color.FromArgb(64, 0, 64);
      buttonCloseApplication.FlatStyle = FlatStyle.Flat;
      buttonCloseApplication.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      buttonCloseApplication.ForeColor = SystemColors.Control;
      buttonCloseApplication.Location = new Point(20, 286);
      buttonCloseApplication.Name = "buttonCloseApplication";
      buttonCloseApplication.Size = new Size(386, 36);
      buttonCloseApplication.TabIndex = 9;
      buttonCloseApplication.Text = "Close Application";
      buttonCloseApplication.UseVisualStyleBackColor = false;
      buttonCloseApplication.Click += buttonCloseApplication_Click;
      // 
      // labelCurrentProcess
      // 
      labelCurrentProcess.AutoSize = true;
      labelCurrentProcess.BackColor = Color.FromArgb(64, 0, 64);
      labelCurrentProcess.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
      labelCurrentProcess.ForeColor = SystemColors.Control;
      labelCurrentProcess.Location = new Point(19, 10);
      labelCurrentProcess.Name = "labelCurrentProcess";
      labelCurrentProcess.Size = new Size(100, 17);
      labelCurrentProcess.TabIndex = 11;
      labelCurrentProcess.Text = "CurrentProcess";
      // 
      // buttonRestartDragon
      // 
      buttonRestartDragon.BackColor = Color.FromArgb(64, 0, 64);
      buttonRestartDragon.FlatStyle = FlatStyle.Flat;
      buttonRestartDragon.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      buttonRestartDragon.ForeColor = SystemColors.Control;
      buttonRestartDragon.Location = new Point(20, 476);
      buttonRestartDragon.Name = "buttonRestartDragon";
      buttonRestartDragon.Size = new Size(386, 36);
      buttonRestartDragon.TabIndex = 12;
      buttonRestartDragon.Text = "Restart Dragon";
      buttonRestartDragon.UseVisualStyleBackColor = false;
      buttonRestartDragon.Click += buttonRestartDragon_Click;
      // 
      // buttonDatabaseCommands
      // 
      buttonDatabaseCommands.BackColor = Color.FromArgb(64, 0, 64);
      buttonDatabaseCommands.FlatStyle = FlatStyle.Flat;
      buttonDatabaseCommands.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      buttonDatabaseCommands.ForeColor = SystemColors.Control;
      buttonDatabaseCommands.Location = new Point(20, 329);
      buttonDatabaseCommands.Name = "buttonDatabaseCommands";
      buttonDatabaseCommands.Size = new Size(190, 36);
      buttonDatabaseCommands.TabIndex = 15;
      buttonDatabaseCommands.Text = "Database Commands";
      buttonDatabaseCommands.UseVisualStyleBackColor = false;
      buttonDatabaseCommands.Click += buttonDatabaseCommands_Click;
      // 
      // textBoxKey
      // 
      textBoxKey.Location = new Point(20, 394);
      textBoxKey.Name = "textBoxKey";
      textBoxKey.PasswordChar = 'x';
      textBoxKey.Size = new Size(386, 23);
      textBoxKey.TabIndex = 16;
      textBoxKey.Text = "123";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(20, 372);
      label2.Name = "label2";
      label2.Size = new Size(120, 15);
      label2.TabIndex = 17;
      label2.Text = "Azure application key";
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(20, 424);
      label3.Name = "label3";
      label3.Size = new Size(77, 15);
      label3.TabIndex = 19;
      label3.Text = "Azure Region";
      // 
      // textBoxRegion
      // 
      textBoxRegion.Location = new Point(20, 446);
      textBoxRegion.Name = "textBoxRegion";
      textBoxRegion.Size = new Size(386, 23);
      textBoxRegion.TabIndex = 18;
      textBoxRegion.Text = "UKSouth";
      // 
      // buttonRandomCommand
      // 
      buttonRandomCommand.BackColor = Color.FromArgb(64, 0, 64);
      buttonRandomCommand.FlatStyle = FlatStyle.Flat;
      buttonRandomCommand.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
      buttonRandomCommand.ForeColor = Color.BurlyWood;
      buttonRandomCommand.Location = new Point(20, 519);
      buttonRandomCommand.Name = "buttonRandomCommand";
      buttonRandomCommand.Size = new Size(386, 98);
      buttonRandomCommand.TabIndex = 21;
      buttonRandomCommand.Text = "Command Tip";
      buttonRandomCommand.UseVisualStyleBackColor = false;
      buttonRandomCommand.Click += buttonRandomCommand_Click;
      // 
      // checkBoxIncludeSpace
      // 
      checkBoxIncludeSpace.AutoSize = true;
      checkBoxIncludeSpace.BackColor = Color.FromArgb(64, 0, 64);
      checkBoxIncludeSpace.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      checkBoxIncludeSpace.ForeColor = SystemColors.Control;
      checkBoxIncludeSpace.Location = new Point(161, 70);
      checkBoxIncludeSpace.Name = "checkBoxIncludeSpace";
      checkBoxIncludeSpace.Size = new Size(135, 25);
      checkBoxIncludeSpace.TabIndex = 22;
      checkBoxIncludeSpace.Text = "Include Space";
      checkBoxIncludeSpace.UseVisualStyleBackColor = false;
      // 
      // buttonStartWithoutToggle
      // 
      buttonStartWithoutToggle.BackColor = Color.FromArgb(64, 0, 64);
      buttonStartWithoutToggle.FlatStyle = FlatStyle.Flat;
      buttonStartWithoutToggle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      buttonStartWithoutToggle.ForeColor = SystemColors.Control;
      buttonStartWithoutToggle.Location = new Point(219, 200);
      buttonStartWithoutToggle.Name = "buttonStartWithoutToggle";
      buttonStartWithoutToggle.Size = new Size(190, 36);
      buttonStartWithoutToggle.TabIndex = 23;
      buttonStartWithoutToggle.Text = "Start";
      buttonStartWithoutToggle.UseVisualStyleBackColor = false;
      buttonStartWithoutToggle.Click += buttonStartWithoutToggle_Click;
      // 
      // buttonStopNoToggle
      // 
      buttonStopNoToggle.BackColor = Color.FromArgb(64, 0, 64);
      buttonStopNoToggle.FlatStyle = FlatStyle.Flat;
      buttonStopNoToggle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      buttonStopNoToggle.ForeColor = SystemColors.Control;
      buttonStopNoToggle.Location = new Point(219, 243);
      buttonStopNoToggle.Name = "buttonStopNoToggle";
      buttonStopNoToggle.Size = new Size(190, 36);
      buttonStopNoToggle.TabIndex = 24;
      buttonStopNoToggle.Text = "Stop";
      buttonStopNoToggle.UseVisualStyleBackColor = false;
      buttonStopNoToggle.Click += buttonStopNoToggle_Click;
      // 
      // label1
      // 
      label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
      label1.Location = new Point(20, 624);
      label1.Name = "label1";
      label1.Size = new Size(386, 46);
      label1.TabIndex = 25;
      label1.Text = "Camel, Variable/Pascal, Period, Underscore, Hyphenate, Title, Upper, All Caps, Lower Gap, Lower/Trim, Kebab\r\n";
      // 
      // buttonPrograms
      // 
      buttonPrograms.BackColor = Color.FromArgb(64, 0, 64);
      buttonPrograms.FlatStyle = FlatStyle.Flat;
      buttonPrograms.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
      buttonPrograms.ForeColor = SystemColors.Control;
      buttonPrograms.Location = new Point(218, 329);
      buttonPrograms.Name = "buttonPrograms";
      buttonPrograms.Size = new Size(190, 36);
      buttonPrograms.TabIndex = 26;
      buttonPrograms.Text = "Programs";
      buttonPrograms.UseVisualStyleBackColor = false;
      buttonPrograms.Click += buttonPrograms_Click;
      // 
      // ContinuousSpeech
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.FromArgb(64, 0, 64);
      ClientSize = new Size(1110, 679);
      Controls.Add(buttonPrograms);
      Controls.Add(label1);
      Controls.Add(buttonStopNoToggle);
      Controls.Add(buttonStartWithoutToggle);
      Controls.Add(checkBoxIncludeSpace);
      Controls.Add(buttonRandomCommand);
      Controls.Add(label3);
      Controls.Add(textBoxRegion);
      Controls.Add(label2);
      Controls.Add(textBoxKey);
      Controls.Add(buttonDatabaseCommands);
      Controls.Add(buttonRestartDragon);
      Controls.Add(labelCurrentProcess);
      Controls.Add(buttonCloseApplication);
      Controls.Add(buttonStart);
      Controls.Add(buttonStop);
      Controls.Add(checkBoxRemovePunctuation);
      Controls.Add(checkBoxTreatAsCommandFirst);
      Controls.Add(checkBoxTreatAsCommand);
      Controls.Add(labelStatus);
      Controls.Add(textBoxResultsLocal);
      Controls.Add(checkBoxLowercase);
      Controls.Add(checkBoxUppercase);
      ForeColor = SystemColors.ControlLight;
      Icon = (Icon)resources.GetObject("$this.Icon");
      Name = "ContinuousSpeech";
      Opacity = 0.8D;
      StartPosition = FormStartPosition.Manual;
      Text = "Coding by Voice";
      FormClosing += ContinuousSpeech_FormClosing;
      Load += ContinuousSpeech_Load;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private CheckBox checkBoxUppercase;
    private CheckBox checkBoxLowercase;
    private TextBox textBoxResultsLocal;
    private Label labelStatus;
    private CheckBox checkBoxTreatAsCommand;
    private CheckBox checkBoxTreatAsCommandFirst;
    private CheckBox checkBoxRemovePunctuation;
    private Button buttonStop;
    private Button buttonStart;
    private Button buttonCloseApplication;
    private Label labelCurrentProcess;
    private Button buttonRestartDragon;
    private Button buttonDatabaseCommands;
    private TextBox textBoxKey;
    private Label label2;
    private Label label3;
    private TextBox textBoxRegion;
    private Button buttonRandomCommand;
    private CheckBox checkBoxIncludeSpace;
    private Button buttonStartWithoutToggle;
    private Button buttonStopNoToggle;
    private Label label1;
    private Button buttonPrograms;
  }
}