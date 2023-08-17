using System;
using System.Configuration;
using System.Globalization;
using System.Windows.Forms;

namespace YoutubeTitleForYvonne
{
    public partial class frmOptions : Form
    {
        public string ChromeLanguage { get; set; } = "";
        public decimal RefreshInterval { get; set; }
        public string OutputFilename { get; set; } = "";
        public string TextSeparatorType { get; set; } = "";
        public string TextSeparator { get; set; } = "";
        public decimal MinimumTextLength { get; set; }

        public frmOptions()
        {
            InitializeComponent();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            // Get Chrome Language
            ChromeLanguage = ConfigurationManager.AppSettings.Get("ChromeLanguage");

            if (string.IsNullOrEmpty(ChromeLanguage))
            {
                // Try to determine the language based on the user's Windows language
                CultureInfo ci = CultureInfo.InstalledUICulture;

                string foundLanguage;

                if (frmMain.languages.TryGetValue(ci.TwoLetterISOLanguageName, out foundLanguage))
                {
                    ChromeLanguage = foundLanguage;
                }
                else if (frmMain.languages.TryGetValue(ci.Name, out foundLanguage))
                {
                    ChromeLanguage = foundLanguage;
                }
                else
                {
                    ChromeLanguage = frmMain.DefaultChromeLanguage;
                }
            }

            cmbLanguage.SelectedItem = ChromeLanguage;

            // Get refresh interval
            if (int.TryParse(ConfigurationManager.AppSettings.Get("RefreshInterval"), out int refreshInterval))
            {
                RefreshInterval = refreshInterval;
            }
            else
            {
                RefreshInterval = frmMain.DefaultRefreshInterval;
            }

            numRefreshInterval.Value = RefreshInterval;

            // Get output filename
            OutputFilename = ConfigurationManager.AppSettings.Get("OutputFilename");

            if (string.IsNullOrEmpty(OutputFilename))
            {
                OutputFilename = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\nowplaying.txt";
            }

            txtOutputFilename.Text = OutputFilename;

            // Get text separator type
            TextSeparatorType = ConfigurationManager.AppSettings.Get("TextSeparatorType");

            if (string.IsNullOrEmpty(TextSeparatorType))
            {
                TextSeparatorType = frmMain.DefaultTextSeparatorType;
            }

            cmbTextSeparator.SelectedItem = TextSeparatorType;

            // Enable/Disable text input based on TextSeparatorType value
            if (TextSeparatorType == "None")
            {
                txtTextSeparator.Enabled = false;
                numMinimumTextLength.Enabled = false;
            }
            else if (TextSeparatorType == "Space Padding")
            {
                txtTextSeparator.Enabled = false;
                numMinimumTextLength.Enabled = true;
            }
            else if (TextSeparatorType == "Custom")
            {
                txtTextSeparator.Enabled = true;
                numMinimumTextLength.Enabled = false;
            }

            // Get minimum text length
            if (int.TryParse(ConfigurationManager.AppSettings.Get("MinimumTextLength"), out int parsedMinimumTextLength))
            {
                MinimumTextLength = parsedMinimumTextLength;
            }
            else
            {
                MinimumTextLength = frmMain.DefaultMinimumTextLength;
            }

            numMinimumTextLength.Value = MinimumTextLength;

            // Get text separator
            TextSeparator = ConfigurationManager.AppSettings.Get("TextSeparator");

            if (TextSeparator == null)
            {
                TextSeparator = frmMain.DefaultTextSeparator;
            }

            txtTextSeparator.Text = TextSeparator;
        }

        private void numRefreshInterval_ValueChanged(object sender, EventArgs e)
        {
            RefreshInterval = numRefreshInterval.Value;
        }

        private void btnOutputFileBrowse_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt";
            saveFileDialog.Title = "Select file to save YouTube title to";
            saveFileDialog.FileName = OutputFilename;
            saveFileDialog.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog.FileName != "")
            {
                OutputFilename = saveFileDialog.FileName;
                txtOutputFilename.Text = saveFileDialog.FileName;
            }
        }

        private void txtOutputFilename_TextChanged(object sender, EventArgs e)
        {
            OutputFilename = txtOutputFilename.Text;

            if (string.IsNullOrEmpty(OutputFilename))
            {
                OutputFilename = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\nowplaying.txt";
                //txtOutputFilename.Text = OutputFilename;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOutputFilename.Text))
            {
                txtOutputFilename.Text = OutputFilename;
                MessageBox.Show("The output file will default to:" + Environment.NewLine + OutputFilename, "Default Output Filename", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Check output file is writable
            try
            {
                System.IO.File.WriteAllText(OutputFilename, "");
            }
            catch
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show("The selected output file/folder:" + Environment.NewLine + OutputFilename + Environment.NewLine + "is not writeable. Please try again.", "Invalid Output Filename", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnklblProjectUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigate to a URL.
            System.Diagnostics.Process.Start(lnklblProjectUrl.Text);
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChromeLanguage = cmbLanguage.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(ChromeLanguage))
            {
                // Try to determine the language based on the user's Windows language
                CultureInfo ci = CultureInfo.InstalledUICulture;

                string foundLanguage;

                if (frmMain.languages.TryGetValue(ci.TwoLetterISOLanguageName, out foundLanguage))
                {
                    ChromeLanguage = foundLanguage;
                }
                else if (frmMain.languages.TryGetValue(ci.Name, out foundLanguage))
                {
                    ChromeLanguage = foundLanguage;
                }
                else
                {
                    ChromeLanguage = frmMain.DefaultChromeLanguage;
                }

                //cmbLanguage.SelectedItem = ChromeLanguage;
            }
        }

        private void txtTextSeparator_TextChanged(object sender, EventArgs e)
        {
            TextSeparator = txtTextSeparator.Text;
        }

        private void numMinimumTextLength_ValueChanged(object sender, EventArgs e)
        {
            MinimumTextLength = numMinimumTextLength.Value;
        }

        private void cmbTextSeparator_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextSeparatorType = cmbTextSeparator.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(TextSeparatorType))
            {
                TextSeparatorType = frmMain.DefaultTextSeparatorType;
            }

            if (TextSeparatorType == "None")
            {
                txtTextSeparator.Enabled = false;
                numMinimumTextLength.Enabled = false;
            }
            else if (TextSeparatorType == "Space Padding")
            {
                txtTextSeparator.Enabled = false;
                numMinimumTextLength.Enabled = true;
            }
            else if (TextSeparatorType == "Custom")
            {
                txtTextSeparator.Enabled = true;
                numMinimumTextLength.Enabled = false;
            }
        }
    }
}
