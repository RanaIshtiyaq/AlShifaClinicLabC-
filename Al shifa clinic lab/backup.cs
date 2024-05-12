using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;

namespace Al_shifa_clinic_lab
{
    public partial class backup : Form
    {
        public backup()
        {
            InitializeComponent();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (tbPath.Text == "")
            {
                MessageBox.Show("Please select location", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Backup();
            }
        }
        private void Backup()
        {
            try
            {
                string dbFilename = "alShifaDb.accdb";
                string CurrentDbPath = Path.Combine(Environment.CurrentDirectory, dbFilename);
                string backTimeStamp = Path.GetFileNameWithoutExtension(dbFilename);
                string destFileName = dbFilename;
                string PathBackup = tbPath.Text;
                destFileName = Path.Combine(PathBackup, destFileName);
                File.Copy(CurrentDbPath, destFileName, true);
                MessageBox.Show("Backup successful");
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbPath.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
        }
    }
}
