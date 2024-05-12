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
    public partial class InvoiceOp : Form
    {
        public InvoiceOp()
        {
            InitializeComponent();
        }
        public void ManageControls(bool a)
        {
            lbDate.Visible = dateTimeEntry.Visible = lbDocName.Visible = tbDrName.Visible = pbDocName.Visible = a;
        }

        private void pbPatname_Click(object sender, EventArgs e)
        {
            try
            {

                label6.Visible = dgvPatName.Visible = true; tbPatName.Focus();
                queryClass.selectds("Select * from patient where Patient_name LIKE '%" + tbPatName.Text + "%' ORDER BY Patient_id DESC");
                dgvPatName.DataSource = queryClass.ds.Tables[0];
                if (dgvPatName.RowCount < 1)
                {
                    dgvDocName.Visible = label6.Visible = dgvPatName.Visible = false; pnlPatForm.Visible = true;
                    ManageControls(false);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void dgvPatName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tbPatId.Text = dgvPatName.Rows[e.RowIndex].Cells[0].Value.ToString();
                tbPatName.Text = dgvPatName.Rows[e.RowIndex].Cells[1].Value.ToString();
                label6.Visible = dgvPatName.Visible = pnlPatForm.Visible = false;
                ManageControls(true);
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void pbDocName_Click(object sender, EventArgs e)
        {
            try
            {
                dgvDocName.Visible = true; tbDrName.Focus();
                queryClass.selectds("Select Doctor_name from doctor");
                dgvDocName.DataSource = queryClass.ds.Tables[0];
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void dgvDocName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tbDrName.Text = dgvDocName.Rows[e.RowIndex].Cells[0].Value.ToString();
                dgvDocName.Visible = false;
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            queryClass.clrearC(this);
        }

        private void pbTestSeach_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTestShow.Visible == false)
                {
                    dgvTestShow.Visible = true;
                    queryClass.selectds("Select Test_name, Price,Normal_value from test where Test_name LIKE '%" + tbSearchTest.Text + "%' OR Test_id LIKE '%" + tbSearchTest.Text + "%'");
                    dgvTestShow.DataSource = queryClass.ds.Tables[0];
                }
                else
                {
                    dgvTestShow.Visible = false;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        int total, TestCost, pos = 70;

        private void tbSearchTest_TextChanged(object sender, EventArgs e)
        {
            pbTestSeach_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 185, 340);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        string TestName;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Al_shifa Lab", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(33));
            e.Graphics.DrawString("Name: " + tbPatName.Text, new Font("Century Gothic", 9, FontStyle.Bold), Brushes.Black, new Point(15, 19));
            e.Graphics.DrawString("ID       Test            Cost", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(18, 40));
            foreach (DataGridViewRow row in dgvTestList.Rows)
            {
                TestName = "       " + row.Cells["Test_name"].Value;
                TestCost = Convert.ToInt32(row.Cells["Price"].Value);

                e.Graphics.DrawString("" + TestName, new Font("Century Gothic", 9, FontStyle.Bold), Brushes.Blue, new Point(40, pos));
                e.Graphics.DrawString("" + TestCost, new Font("Century Gothic", 9, FontStyle.Bold), Brushes.Blue, new Point(140, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Total: RS " + total, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(33, pos + 10));

            dgvTestList.Refresh();
            pos = 100;
        }

        private void InvoiceOp_Click(object sender, EventArgs e)
        {
            if (dgvPatName.Visible == true)
            {
                label6.Visible = dgvPatName.Visible = false;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            label6.Visible = dgvPatName.Visible = false;
        }

        private void btnInserPatient_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbPatName.Text) || string.IsNullOrEmpty(cbGender.Text) || string.IsNullOrEmpty(tbPhone.Text)
                          || string.IsNullOrEmpty(tbAge.Text) || string.IsNullOrEmpty(tbAdr.Text))
                {
                    MessageBox.Show("Please fill fields");
                }
                else
                {
                    string qry = string.Format("insert into patient(Patient_name,Gender,Phone,Age,Address)values('" + tbPatName.Text + "','" + cbGender.Text
                + "','" + tbPhone.Text + "','" + tbAge.Text + "','" + tbAdr.Text + "' )");
                    queryClass.insert(qry);
                    queryClass.clrearC(this);
                    lbDate.Visible = dateTimeEntry.Visible = lbDocName.Visible = tbDrName.Visible = pbDocName.Visible = true;
                    pnlPatForm.Visible = false;
                    MessageBox.Show("One record added");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbTestNames.Text) || string.IsNullOrEmpty(tbPatId.Text) || string.IsNullOrEmpty(tbPatName.Text)
                          || string.IsNullOrEmpty(tbDrName.Text) || string.IsNullOrEmpty(dateTimeEntry.Text)
                          || string.IsNullOrEmpty(tbTotal.Text) || string.IsNullOrEmpty(cmbSpecimen.Text))
                {
                    MessageBox.Show("Please fill fields");
                }
                else
                {
                    string qry = string.Format("insert into invoice(Patient_id,Time_in,Doctor_name,Total,Specimen,Test_name,Normal_value)values('" + tbPatId.Text + "','" + dateTimeEntry.Value
                + "','" + tbDrName.Text + "','" + total + "','" + cmbSpecimen.SelectedItem + "','" + tbTestNames.Text + "','" + tbNormalVal.Text + "' )");
                    queryClass.insert(qry);
                    queryClass.clrearC(this);
                    show();
                    label6.Visible = dgvPatName.Visible = pnlPatForm.Visible = false;
                    ManageControls(true);
                    dgvTestList.Rows.Clear();
                    MessageBox.Show("One record added");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        public void show()
        {
            try
            {
                queryClass.selectds("Select Invoice_id,Patient_id,Time_in,Doctor_name,Total,Specimen,Test_name,Normal_value from invoice ORDER BY Invoice_id DESC");
                dgvInvoice.DataSource = queryClass.ds.Tables[0];

                queryClass.selectds("Select Test_value from invoice where Test_value='" + 0 + "'");
                Properties.Settings.Default.PendingRep = queryClass.ds.Tables[0].Rows.Count.ToString();
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void InvoiceOp_Load(object sender, EventArgs e)
        {
            show();
        }

        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                queryClass.id = Convert.ToInt32(dgvInvoice.Rows[e.RowIndex].Cells[0].Value.ToString());

            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbTestNames.Text) || string.IsNullOrEmpty(tbPatId.Text) || string.IsNullOrEmpty(tbPatName.Text)
                          || string.IsNullOrEmpty(tbDrName.Text) || string.IsNullOrEmpty(dateTimeEntry.Text)
                          || string.IsNullOrEmpty(tbTotal.Text) || string.IsNullOrEmpty(cmbSpecimen.Text))
                {
                    MessageBox.Show("Please fill fields");
                }
                else
                {
                    DialogResult iExit = MessageBox.Show("Are you sure, You want to UPDATE record", "Want Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (iExit == DialogResult.Yes)
                    {
                        string qry = string.Format("update invoice set Patient_id='" + tbPatId.Text + "', Time_in='" + dateTimeEntry.Value + "',Doctor_name='" + tbDrName.Text + "',Total='" + tbTotal.Text
                            + "',Test_name='" + tbTestNames.Text + "',Specimen='" + cmbSpecimen.Text + "' where Invoice_id=@i");
                        queryClass.update(qry);
                        show();
                        queryClass.clrearC(this);
                        dgvPatName.Visible = false;
                        dgvTestList.Rows.Clear();
                        MessageBox.Show("Updated successful");
                    }
                }
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        
        }

        private void dgvTestShow_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow newrow = new DataGridViewRow();
                newrow.CreateCells(dgvTestList);
                tbTestNames.Text = tbTestNames.Text + "," + Convert.ToString(newrow.Cells[0].Value = dgvTestShow.Rows[e.RowIndex].Cells[0].Value);
                tbNormalVal.Text = tbNormalVal.Text + "," + Convert.ToString(newrow.Cells[2].Value = dgvTestShow.Rows[e.RowIndex].Cells[2].Value);
                total = total + Convert.ToInt32(newrow.Cells[1].Value = dgvTestShow.Rows[e.RowIndex].Cells[1].Value.ToString());
                dgvTestList.Rows.Add(newrow);
                tbSearchTest.Text = "";
                dgvTestShow.Visible = false;
                tbTotal.Text = total.ToString();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void dgvInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvTestList.Rows.Clear();
                tbPatId.Text = Convert.ToString(queryClass.id = Convert.ToInt32(dgvInvoice.Rows[e.RowIndex].Cells[1].Value));
                queryClass.GetData("Select Patient_name from patient where Patient_id= @i");
                DataRow r = queryClass.d.Tables[0].Rows[0];
                tbPatName.Text = r.Field<String>(0);
                label6.Visible = dgvPatName.Visible = pnlPatForm.Visible = false;
                ManageControls(true);
                string testName;
                tbTestNames.Text = dgvInvoice.Rows[e.RowIndex].Cells[6].Value.ToString();
                string s = tbTestNames.Text;
                string[] vlaues = s.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < vlaues.Length; i++)
                {
                    testName = vlaues[i].Trim();
                    queryClass.GetData("Select Test_name, Price,Normal_value from test where Test_name= '" + testName + "'");
                    for (int j = 0; j < queryClass.d.Tables[0].Rows.Count; j++)
                    {
                        DataRow rw = queryClass.d.Tables[0].Rows[j];
                        DataGridViewRow newrow = new DataGridViewRow();
                        newrow.CreateCells(dgvTestList);
                        newrow.Cells[0].Value = rw.Field<String>(0);
                        newrow.Cells[1].Value = rw.Field<Int32>(1);
                        newrow.Cells[2].Value = rw.Field<String>(2);
                        dgvTestList.Rows.Add(newrow);
                    }
                }
                dateTimeEntry.Value = Convert.ToDateTime(dgvInvoice.Rows[e.RowIndex].Cells[2].Value);
                tbDrName.Text = dgvInvoice.Rows[e.RowIndex].Cells[3].Value.ToString();
                total = Convert.ToInt32(tbTotal.Text = dgvInvoice.Rows[e.RowIndex].Cells[4].Value.ToString());

                cmbSpecimen.Text = dgvInvoice.Rows[e.RowIndex].Cells[5].Value.ToString();
                queryClass.id = Convert.ToInt32(dgvInvoice.Rows[e.RowIndex].Cells[0].Value);


            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            queryClass.selectds("Select Invoice_id,Patient_id,Time_in,Doctor_name,Total,Specimen,Test_name,Normal_value from invoice where Test_name LIKE '%" + tbSearch.Text + "%' OR Invoice_id LIKE '%" + tbSearch.Text + "%'");
            dgvInvoice.DataSource = queryClass.ds.Tables[0];
        }

        private void InvoiceOp_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

    }
}
