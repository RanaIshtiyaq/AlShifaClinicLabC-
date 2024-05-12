using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Al_shifa_clinic_lab
{
    public partial class ReportingOp : Form
    {
        public ReportingOp()
        {
            InitializeComponent();
        }
        public void show()
        {
            try
            {
                queryClass.selectds("Select Invoice_id,Patient_id,Time_out,Doctor_name,Test_value,Specimen,Test_name,Normal_value from invoice ORDER BY Invoice_id DESC");
                dgvReporting.DataSource = queryClass.ds.Tables[0];

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            queryClass.selectds("Select Invoice_id,Patient_id,Time_out,Doctor_name,Test_value,Specimen,Test_name,Normal_value from invoice where Test_name LIKE '%" + tbSearch.Text + "%'OR Invoice_id LIKE '%" + tbSearch.Text + "%' ORDER BY Invoice_id DESC");
            dgvReporting.DataSource = queryClass.ds.Tables[0];
        }

        private void ReportingOp_Load(object sender, EventArgs e)
        {
            show();
        }

        private void dgvReporting_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                queryClass.id = Convert.ToInt32(dgvReporting.Rows[e.RowIndex].Cells[0].Value);
                tbTestVal.Text = dgvReporting.Rows[e.RowIndex].Cells[4].Value.ToString();
                lbTestName.Text = dgvReporting.Rows[e.RowIndex].Cells[6].Value.ToString(); ;

            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lbTestName.Text) || string.IsNullOrEmpty(tbTestVal.Text))
                {
                    MessageBox.Show("Please fill fields");
                }
                else
                {
                    DialogResult iExit = MessageBox.Show("Are you sure, You want to UPDATE record", "Want Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (iExit == DialogResult.Yes)
                    {
                        string qry = string.Format("update invoice set Time_out='" + DateTime.Now + "', Test_value='" + tbTestVal.Text + "' where Invoice_id=@i");
                        queryClass.update(qry);
                        show();
                        tbTestVal.Text = lbTestName.Text = "";
                        MessageBox.Show("Updated successful");
                    }
                }
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        
        }
        string allId = "";

        private void dgvReporting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                queryClass.id = Convert.ToInt32(dgvReporting.Rows[e.RowIndex].Cells[0].Value);
                allId = dgvReporting.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }
        private DataSet1 GetData()
        {
            queryClass.con.Close();
            queryClass.con.Open();
            OleDbCommand cmd = new OleDbCommand("Select Patient_name,Age,Phone,Specimen,Doctor_name,Invoice_id,Test_name,Test_value,Normal_value from patient, invoice where patient.Patient_id= @i AND invoice.Invoice_id= @in", queryClass.con);
            cmd.Parameters.AddWithValue("@i", allId);
            cmd.Parameters.AddWithValue("@in", queryClass.id);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "DataTable1");
            return ds;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet1 ds = GetData(); queryClass.con.Close();
                ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(datasource);
                this.reportViewer1.RefreshReport();
                reportViewer1.Visible = true;
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
 
        }
      
    }
}
