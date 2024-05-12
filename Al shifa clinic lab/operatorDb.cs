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
    public partial class operatorDb : Form
    {
        public operatorDb()
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
            OpPatient pat = new OpPatient();
            showForm(pat);
        }

        private void operatorDb_Load(object sender, EventArgs e)
        {
          lbPendRep.Text= Properties.Settings.Default.PendingRep;
          lbTotDoc.Text = Properties.Settings.Default.TotalDoc;
          lbTotPat.Text = Properties.Settings.Default.TotalPatient;
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            InvoiceOp invOP = new InvoiceOp();
            showForm(invOP);
            
        }

        private void btnRepMen_Click(object sender, EventArgs e)
        {
            ReportingOp rop = new ReportingOp();
            showForm(rop);
        }

        private void BtnDb_Click(object sender, EventArgs e)
        {
            pnDash.Controls.Clear();
            pnDash.Controls.Add(pnDbControls);
            operatorDb_Load(null,null);
        }
    }
}
