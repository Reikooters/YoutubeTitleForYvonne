namespace YoutubeTitleForYvonne
{
    partial class frmOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this.numRefreshInterval = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputFilename = new System.Windows.Forms.TextBox();
            this.btnOutputFileBrowse = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.lnklblProjectUrl = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.grpTextSeparator = new System.Windows.Forms.GroupBox();
            this.lblMinimumTextLengthHelperText = new System.Windows.Forms.Label();
            this.numMinimumTextLength = new System.Windows.Forms.NumericUpDown();
            this.lblMinimumTextLength = new System.Windows.Forms.Label();
            this.lblTextSeparatorType = new System.Windows.Forms.Label();
            this.cmbTextSeparator = new System.Windows.Forms.ComboBox();
            this.lblTextSeparatorHelperText = new System.Windows.Forms.Label();
            this.txtTextSeparator = new System.Windows.Forms.TextBox();
            this.lblTextSeparator = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshInterval)).BeginInit();
            this.grpTextSeparator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinimumTextLength)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Refresh Interval (secs)";
            // 
            // numRefreshInterval
            // 
            this.numRefreshInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numRefreshInterval.Location = new System.Drawing.Point(174, 53);
            this.numRefreshInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numRefreshInterval.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numRefreshInterval.Name = "numRefreshInterval";
            this.numRefreshInterval.Size = new System.Drawing.Size(120, 24);
            this.numRefreshInterval.TabIndex = 1;
            this.numRefreshInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numRefreshInterval.ValueChanged += new System.EventHandler(this.numRefreshInterval_ValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(700, 328);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 37);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(574, 328);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 37);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Output Filename";
            // 
            // txtOutputFilename
            // 
            this.txtOutputFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputFilename.Location = new System.Drawing.Point(174, 91);
            this.txtOutputFilename.MaxLength = 255;
            this.txtOutputFilename.Name = "txtOutputFilename";
            this.txtOutputFilename.Size = new System.Drawing.Size(609, 24);
            this.txtOutputFilename.TabIndex = 2;
            this.txtOutputFilename.TextChanged += new System.EventHandler(this.txtOutputFilename_TextChanged);
            // 
            // btnOutputFileBrowse
            // 
            this.btnOutputFileBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutputFileBrowse.Location = new System.Drawing.Point(789, 90);
            this.btnOutputFileBrowse.Name = "btnOutputFileBrowse";
            this.btnOutputFileBrowse.Size = new System.Drawing.Size(30, 26);
            this.btnOutputFileBrowse.TabIndex = 3;
            this.btnOutputFileBrowse.Text = "...";
            this.btnOutputFileBrowse.UseVisualStyleBackColor = true;
            this.btnOutputFileBrowse.Click += new System.EventHandler(this.btnOutputFileBrowse_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.OverwritePrompt = false;
            // 
            // lnklblProjectUrl
            // 
            this.lnklblProjectUrl.AutoSize = true;
            this.lnklblProjectUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnklblProjectUrl.Location = new System.Drawing.Point(172, 301);
            this.lnklblProjectUrl.Name = "lnklblProjectUrl";
            this.lnklblProjectUrl.Size = new System.Drawing.Size(359, 18);
            this.lnklblProjectUrl.TabIndex = 7;
            this.lnklblProjectUrl.TabStop = true;
            this.lnklblProjectUrl.Text = "https://github.com/Reikooters/YoutubeTitleForYvonne";
            this.lnklblProjectUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblProjectUrl_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 301);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Project URL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Chrome Language";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Items.AddRange(new object[] {
            "Afrikaans",
            "Amharic",
            "Arabic",
            "Bengali",
            "Bulgarian",
            "Catalan",
            "Chinese (China)",
            "Chinese (Taiwan)",
            "Croatian",
            "Czech",
            "Danish",
            "Dutch",
            "English",
            "Estonian",
            "Filipino",
            "Finnish",
            "French",
            "German",
            "Greek",
            "Gujarati",
            "Hebrew",
            "Hindi",
            "Hungarian",
            "Indonesian",
            "Italian",
            "Japanese",
            "Kannada",
            "Korean",
            "Latvian",
            "Lithuanian",
            "Malay",
            "Malayalam",
            "Marathi",
            "Norwegian Bokmål",
            "Persian",
            "Polish",
            "Portuguese (Brazil)",
            "Portuguese (Portugal)",
            "Romanian",
            "Russian",
            "Serbian",
            "Slovak",
            "Slovenian",
            "Spanish",
            "Swahili",
            "Swedish",
            "Tamil",
            "Telugu",
            "Thai",
            "Turkish",
            "Ukrainian",
            "Urdu",
            "Vietnamese"});
            this.cmbLanguage.Location = new System.Drawing.Point(174, 13);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(295, 26);
            this.cmbLanguage.TabIndex = 0;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // grpTextSeparator
            // 
            this.grpTextSeparator.Controls.Add(this.lblMinimumTextLengthHelperText);
            this.grpTextSeparator.Controls.Add(this.numMinimumTextLength);
            this.grpTextSeparator.Controls.Add(this.lblMinimumTextLength);
            this.grpTextSeparator.Controls.Add(this.lblTextSeparatorType);
            this.grpTextSeparator.Controls.Add(this.cmbTextSeparator);
            this.grpTextSeparator.Controls.Add(this.lblTextSeparatorHelperText);
            this.grpTextSeparator.Controls.Add(this.txtTextSeparator);
            this.grpTextSeparator.Controls.Add(this.lblTextSeparator);
            this.grpTextSeparator.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTextSeparator.Location = new System.Drawing.Point(16, 129);
            this.grpTextSeparator.Name = "grpTextSeparator";
            this.grpTextSeparator.Size = new System.Drawing.Size(803, 161);
            this.grpTextSeparator.TabIndex = 13;
            this.grpTextSeparator.TabStop = false;
            this.grpTextSeparator.Text = "Text Separator";
            // 
            // lblMinimumTextLengthHelperText
            // 
            this.lblMinimumTextLengthHelperText.AutoSize = true;
            this.lblMinimumTextLengthHelperText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimumTextLengthHelperText.Location = new System.Drawing.Point(156, 82);
            this.lblMinimumTextLengthHelperText.Name = "lblMinimumTextLengthHelperText";
            this.lblMinimumTextLengthHelperText.Size = new System.Drawing.Size(613, 16);
            this.lblMinimumTextLengthHelperText.TabIndex = 18;
            this.lblMinimumTextLengthHelperText.Text = "When using Space Padding, spaces will be added to the end of the output to reach " +
    "the minimum length.";
            // 
            // numMinimumTextLength
            // 
            this.numMinimumTextLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numMinimumTextLength.Location = new System.Drawing.Point(159, 55);
            this.numMinimumTextLength.Name = "numMinimumTextLength";
            this.numMinimumTextLength.Size = new System.Drawing.Size(120, 24);
            this.numMinimumTextLength.TabIndex = 16;
            this.numMinimumTextLength.ValueChanged += new System.EventHandler(this.numMinimumTextLength_ValueChanged);
            // 
            // lblMinimumTextLength
            // 
            this.lblMinimumTextLength.AutoSize = true;
            this.lblMinimumTextLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimumTextLength.Location = new System.Drawing.Point(9, 58);
            this.lblMinimumTextLength.Name = "lblMinimumTextLength";
            this.lblMinimumTextLength.Size = new System.Drawing.Size(117, 18);
            this.lblMinimumTextLength.TabIndex = 17;
            this.lblMinimumTextLength.Text = "Minimum Length";
            // 
            // lblTextSeparatorType
            // 
            this.lblTextSeparatorType.AutoSize = true;
            this.lblTextSeparatorType.Location = new System.Drawing.Point(9, 28);
            this.lblTextSeparatorType.Name = "lblTextSeparatorType";
            this.lblTextSeparatorType.Size = new System.Drawing.Size(40, 18);
            this.lblTextSeparatorType.TabIndex = 15;
            this.lblTextSeparatorType.Text = "Type";
            // 
            // cmbTextSeparator
            // 
            this.cmbTextSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTextSeparator.FormattingEnabled = true;
            this.cmbTextSeparator.Items.AddRange(new object[] {
            "None",
            "Space Padding",
            "Custom"});
            this.cmbTextSeparator.Location = new System.Drawing.Point(159, 25);
            this.cmbTextSeparator.Name = "cmbTextSeparator";
            this.cmbTextSeparator.Size = new System.Drawing.Size(296, 26);
            this.cmbTextSeparator.TabIndex = 14;
            this.cmbTextSeparator.SelectedIndexChanged += new System.EventHandler(this.cmbTextSeparator_SelectedIndexChanged);
            // 
            // lblTextSeparatorHelperText
            // 
            this.lblTextSeparatorHelperText.AutoSize = true;
            this.lblTextSeparatorHelperText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextSeparatorHelperText.Location = new System.Drawing.Point(156, 133);
            this.lblTextSeparatorHelperText.Name = "lblTextSeparatorHelperText";
            this.lblTextSeparatorHelperText.Size = new System.Drawing.Size(593, 16);
            this.lblTextSeparatorHelperText.TabIndex = 13;
            this.lblTextSeparatorHelperText.Text = "Custom separator will be added to the end of the output, so it will appear in the" +
    " middle of looped text.";
            // 
            // txtTextSeparator
            // 
            this.txtTextSeparator.Enabled = false;
            this.txtTextSeparator.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTextSeparator.Location = new System.Drawing.Point(159, 106);
            this.txtTextSeparator.Name = "txtTextSeparator";
            this.txtTextSeparator.Size = new System.Drawing.Size(630, 24);
            this.txtTextSeparator.TabIndex = 11;
            this.txtTextSeparator.TextChanged += new System.EventHandler(this.txtTextSeparator_TextChanged);
            // 
            // lblTextSeparator
            // 
            this.lblTextSeparator.AutoSize = true;
            this.lblTextSeparator.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextSeparator.Location = new System.Drawing.Point(9, 108);
            this.lblTextSeparator.Name = "lblTextSeparator";
            this.lblTextSeparator.Size = new System.Drawing.Size(130, 18);
            this.lblTextSeparator.TabIndex = 12;
            this.lblTextSeparator.Text = "Custom Separator";
            // 
            // frmOptions
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(832, 377);
            this.Controls.Add(this.grpTextSeparator);
            this.Controls.Add(this.cmbLanguage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lnklblProjectUrl);
            this.Controls.Add(this.btnOutputFileBrowse);
            this.Controls.Add(this.txtOutputFilename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numRefreshInterval);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshInterval)).EndInit();
            this.grpTextSeparator.ResumeLayout(false);
            this.grpTextSeparator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMinimumTextLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRefreshInterval;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputFilename;
        private System.Windows.Forms.Button btnOutputFileBrowse;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.LinkLabel lnklblProjectUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.GroupBox grpTextSeparator;
        private System.Windows.Forms.TextBox txtTextSeparator;
        private System.Windows.Forms.Label lblTextSeparator;
        private System.Windows.Forms.Label lblTextSeparatorHelperText;
        private System.Windows.Forms.ComboBox cmbTextSeparator;
        private System.Windows.Forms.Label lblTextSeparatorType;
        private System.Windows.Forms.NumericUpDown numMinimumTextLength;
        private System.Windows.Forms.Label lblMinimumTextLength;
        private System.Windows.Forms.Label lblMinimumTextLengthHelperText;
    }
}