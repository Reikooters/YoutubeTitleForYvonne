namespace YoutubeTitleForYvonne
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.btnFindChromeWindows = new System.Windows.Forms.Button();
            this.lstYouTubeWindows = new System.Windows.Forms.ListBox();
            this.btnSelectChromeWindow = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.bgwFindYouTubeWindows = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentlyPlaying = new System.Windows.Forms.Label();
            this.tmrUpdateCurrentlyPlaying = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bgwUpdateCurrentlyPlaying = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFindChromeWindows
            // 
            this.btnFindChromeWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindChromeWindows.Location = new System.Drawing.Point(274, 35);
            this.btnFindChromeWindows.Name = "btnFindChromeWindows";
            this.btnFindChromeWindows.Size = new System.Drawing.Size(734, 37);
            this.btnFindChromeWindows.TabIndex = 0;
            this.btnFindChromeWindows.Text = "1. Find non-minimized Chrome windows with YouTube tabs";
            this.btnFindChromeWindows.UseVisualStyleBackColor = true;
            this.btnFindChromeWindows.Click += new System.EventHandler(this.btnFindYouTubeWindows_Click);
            // 
            // lstYouTubeWindows
            // 
            this.lstYouTubeWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstYouTubeWindows.FormattingEnabled = true;
            this.lstYouTubeWindows.ItemHeight = 18;
            this.lstYouTubeWindows.Location = new System.Drawing.Point(275, 78);
            this.lstYouTubeWindows.Name = "lstYouTubeWindows";
            this.lstYouTubeWindows.Size = new System.Drawing.Size(732, 130);
            this.lstYouTubeWindows.TabIndex = 1;
            // 
            // btnSelectChromeWindow
            // 
            this.btnSelectChromeWindow.Enabled = false;
            this.btnSelectChromeWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectChromeWindow.Location = new System.Drawing.Point(274, 214);
            this.btnSelectChromeWindow.Name = "btnSelectChromeWindow";
            this.btnSelectChromeWindow.Size = new System.Drawing.Size(734, 37);
            this.btnSelectChromeWindow.TabIndex = 2;
            this.btnSelectChromeWindow.Text = "2. Select window from list above";
            this.btnSelectChromeWindow.UseVisualStyleBackColor = true;
            this.btnSelectChromeWindow.Click += new System.EventHandler(this.btnSelectChromeWindow_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Enabled = false;
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartStop.Location = new System.Drawing.Point(274, 256);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(734, 37);
            this.btnStartStop.TabIndex = 3;
            this.btnStartStop.Text = "3. Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // bgwFindYouTubeWindows
            // 
            this.bgwFindYouTubeWindows.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwFindYouTubeWindows_DoWork);
            this.bgwFindYouTubeWindows.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwFindYouTubeWindows_RunWorkerCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(274, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Currently Playing:";
            // 
            // lblCurrentlyPlaying
            // 
            this.lblCurrentlyPlaying.AutoSize = true;
            this.lblCurrentlyPlaying.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlyPlaying.Location = new System.Drawing.Point(420, 301);
            this.lblCurrentlyPlaying.Name = "lblCurrentlyPlaying";
            this.lblCurrentlyPlaying.Size = new System.Drawing.Size(63, 18);
            this.lblCurrentlyPlaying.TabIndex = 5;
            this.lblCurrentlyPlaying.Text = "Stopped";
            // 
            // tmrUpdateCurrentlyPlaying
            // 
            this.tmrUpdateCurrentlyPlaying.Interval = 2000;
            this.tmrUpdateCurrentlyPlaying.Tick += new System.EventHandler(this.tmrUpdateCurrentlyPlaying_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(998, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Open YouTube tab in Google Chrome first. YouTube song title will be saved to \"now" +
    "playing.txt\" in the application folder. Left-most tab in window is used.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // bgwUpdateCurrentlyPlaying
            // 
            this.bgwUpdateCurrentlyPlaying.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwUpdateCurrentlyPlaying_DoWork);
            this.bgwUpdateCurrentlyPlaying.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwUpdateCurrentlyPlaying_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 300);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(256, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 8;
            this.progressBar.Value = 100;
            this.progressBar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 331);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCurrentlyPlaying);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnSelectChromeWindow);
            this.Controls.Add(this.lstYouTubeWindows);
            this.Controls.Add(this.btnFindChromeWindows);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::YoutubeTitleForYvonne.Properties.Resources.appicon;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YouTube Title For Yvonne by Shane 2019-12-30";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFindChromeWindows;
        private System.Windows.Forms.ListBox lstYouTubeWindows;
        private System.Windows.Forms.Button btnSelectChromeWindow;
        private System.Windows.Forms.Button btnStartStop;
        private System.ComponentModel.BackgroundWorker bgwFindYouTubeWindows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentlyPlaying;
        private System.Windows.Forms.Timer tmrUpdateCurrentlyPlaying;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.ComponentModel.BackgroundWorker bgwUpdateCurrentlyPlaying;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

