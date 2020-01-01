using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoutubeTitleForYvonne
{
    public partial class frmOptions : Form
    {
        public decimal RefreshInterval { get; set; }
        public string OutputFilename { get; set; } = "";

        public frmOptions()
        {
            InitializeComponent();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            // Get refresh interval
            if (int.TryParse(ConfigurationManager.AppSettings.Get("RefreshInterval"), out int refreshInterval))
            {
                numRefreshInterval.Value = refreshInterval;
            }
            else
            {
                numRefreshInterval.Value = frmMain.DefaultRefreshInterval;
            }

            RefreshInterval = numRefreshInterval.Value;

            // Get output filename
            OutputFilename = ConfigurationManager.AppSettings.Get("OutputFilename");

            if (String.IsNullOrEmpty(OutputFilename))
            {
                OutputFilename = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\nowplaying.txt";
            }

            txtOutputFilename.Text = OutputFilename;
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

            if (String.IsNullOrEmpty(OutputFilename))
            {
                OutputFilename = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + @"\nowplaying.txt";
                //txtOutputFilename.Text = OutputFilename;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Check output file is writable
            PermissionSet permissionSet = new PermissionSet(PermissionState.None);

            if (String.IsNullOrEmpty(txtOutputFilename.Text))
            {
                txtOutputFilename.Text = OutputFilename;
                MessageBox.Show("The output file will default to:" + Environment.NewLine + OutputFilename, "Default Output Filename", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
    }
}
