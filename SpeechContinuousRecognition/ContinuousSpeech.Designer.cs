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
            this.SuspendLayout();
            // 
            // checkBoxUppercase
            // 
            this.checkBoxUppercase.AutoSize = true;
            this.checkBoxUppercase.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxUppercase.Location = new System.Drawing.Point(68, 32);
            this.checkBoxUppercase.Name = "checkBoxUppercase";
            this.checkBoxUppercase.Size = new System.Drawing.Size(137, 29);
            this.checkBoxUppercase.TabIndex = 0;
            this.checkBoxUppercase.Text = "UPPERCASE";
            this.checkBoxUppercase.UseVisualStyleBackColor = true;
            // 
            // checkBoxLowercase
            // 
            this.checkBoxLowercase.AutoSize = true;
            this.checkBoxLowercase.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxLowercase.Location = new System.Drawing.Point(68, 67);
            this.checkBoxLowercase.Name = "checkBoxLowercase";
            this.checkBoxLowercase.Size = new System.Drawing.Size(105, 25);
            this.checkBoxLowercase.TabIndex = 1;
            this.checkBoxLowercase.Text = "lowercase";
            this.checkBoxLowercase.UseVisualStyleBackColor = true;
            // 
            // textBoxResultsLocal
            // 
            this.textBoxResultsLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResultsLocal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxResultsLocal.Location = new System.Drawing.Point(407, 39);
            this.textBoxResultsLocal.Multiline = true;
            this.textBoxResultsLocal.Name = "textBoxResultsLocal";
            this.textBoxResultsLocal.Size = new System.Drawing.Size(535, 583);
            this.textBoxResultsLocal.TabIndex = 2;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.labelStatus.Location = new System.Drawing.Point(77, 227);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 21);
            this.labelStatus.TabIndex = 3;
            // 
            // checkBoxTreatAsCommand
            // 
            this.checkBoxTreatAsCommand.AutoSize = true;
            this.checkBoxTreatAsCommand.Enabled = false;
            this.checkBoxTreatAsCommand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxTreatAsCommand.Location = new System.Drawing.Point(222, 39);
            this.checkBoxTreatAsCommand.Name = "checkBoxTreatAsCommand";
            this.checkBoxTreatAsCommand.Size = new System.Drawing.Size(170, 25);
            this.checkBoxTreatAsCommand.TabIndex = 4;
            this.checkBoxTreatAsCommand.Text = "Treat as Command";
            this.checkBoxTreatAsCommand.UseVisualStyleBackColor = true;
            this.checkBoxTreatAsCommand.Visible = false;
            // 
            // checkBoxConvertWordsToSymbols
            // 
            this.checkBoxConvertWordsToSymbols.AutoSize = true;
            this.checkBoxConvertWordsToSymbols.Checked = true;
            this.checkBoxConvertWordsToSymbols.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxConvertWordsToSymbols.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxConvertWordsToSymbols.Location = new System.Drawing.Point(68, 98);
            this.checkBoxConvertWordsToSymbols.Name = "checkBoxConvertWordsToSymbols";
            this.checkBoxConvertWordsToSymbols.Size = new System.Drawing.Size(231, 25);
            this.checkBoxConvertWordsToSymbols.TabIndex = 5;
            this.checkBoxConvertWordsToSymbols.Text = "Convert Words to Symbols";
            this.checkBoxConvertWordsToSymbols.UseVisualStyleBackColor = true;
            // 
            // checkBoxRemovePunctuation
            // 
            this.checkBoxRemovePunctuation.AutoSize = true;
            this.checkBoxRemovePunctuation.Checked = true;
            this.checkBoxRemovePunctuation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRemovePunctuation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRemovePunctuation.Location = new System.Drawing.Point(68, 129);
            this.checkBoxRemovePunctuation.Name = "checkBoxRemovePunctuation";
            this.checkBoxRemovePunctuation.Size = new System.Drawing.Size(189, 25);
            this.checkBoxRemovePunctuation.TabIndex = 6;
            this.checkBoxRemovePunctuation.Text = "Remove Punctuation";
            this.checkBoxRemovePunctuation.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonStop.Location = new System.Drawing.Point(79, 303);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(156, 36);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonStart.Location = new System.Drawing.Point(79, 261);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(156, 36);
            this.buttonStart.TabIndex = 8;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonCloseApplication
            // 
            this.buttonCloseApplication.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonCloseApplication.Location = new System.Drawing.Point(78, 345);
            this.buttonCloseApplication.Name = "buttonCloseApplication";
            this.buttonCloseApplication.Size = new System.Drawing.Size(156, 36);
            this.buttonCloseApplication.TabIndex = 9;
            this.buttonCloseApplication.Text = "Close Application";
            this.buttonCloseApplication.UseVisualStyleBackColor = true;
            this.buttonCloseApplication.Click += new System.EventHandler(this.buttonCloseApplication_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(79, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 36);
            this.button1.TabIndex = 10;
            this.button1.Text = "Testing";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ContinuousSpeech
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 644);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ContinuousSpeech";
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
    }
}