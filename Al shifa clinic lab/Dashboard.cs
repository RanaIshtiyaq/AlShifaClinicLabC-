using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Al_shifa_clinic_lab
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        public void showForm(Form f)
        {
            pnDash.Controls.Clear();
            f.TopLevel = false;
            pnDash.Controls.Add(f);
            f.Dock = DockStyle.Fill;
            f.Show();
        }

      

        private void btnPatient_Click(object sender, EventArgs e)
        {
            patient pat = new patient();
            showForm(pat);
        }

        private void btnDashord_Click(object sender, EventArgs e)
        {
            pnDash.Controls.Clear();
            pnDash.Controls.Add(pnDbControls);
           Dashboard_Load(null, null);
            
        }

        private void btnDoctor_Click(object sender, EventArgs e)
        {
            doctor doc = new doctor();
            showForm(doc);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            test tst = new test();
            showForm(tst);
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            invoice inv = new invoice();
            showForm(inv);
        }


        private void btnRepMen_Click(object sender, EventArgs e)
        {
            Reporting rep = new Reporting();
            showForm(rep);
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            backup bk = new backup();
            showForm(bk);
        }
        
        private void Dashboard_Load(object sender, EventArgs e)
        {
            lbPendRep.Text = Properties.Settings.Default.PendingRep;
            lbTotDoc.Text = Properties.Settings.Default.TotalDoc;
            lbTotPat.Text = Properties.Settings.Default.TotalPatient;
            lbIncome.Text = Properties.Settings.Default.Income;
           
        }

    }
}
