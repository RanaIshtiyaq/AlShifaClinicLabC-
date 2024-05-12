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
    public partial class OpPatient : Form
    {
        public OpPatient()
        {
            InitializeComponent();
        }
        public void show()
        {
            try
            {
                queryClass.selectds("Select * from patient");
                Properties.Settings.Default.TotalPatient = queryClass.ds.Tables[0].Rows.Count.ToString();
                dgvPatient.DataSource = queryClass.ds.Tables[0];
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
       
        private void OpPatient_Load(object sender, EventArgs e)
        {
            show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
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
                    show();
                    queryClass.clrearC(this);
                    MessageBox.Show("One record added");
                }

            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        
        private void dgvPatient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                queryClass.id = Convert.ToInt32(dgvPatient.Rows[e.RowIndex].Cells[0].Value.ToString());

            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void dgvPatient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tbPatName.Text = dgvPatient.Rows[e.RowIndex].Cells[1].Value.ToString();
                cbGender.Text = dgvPatient.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbPhone.Text = dgvPatient.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbAge.Text = dgvPatient.Rows[e.RowIndex].Cells[4].Value.ToString();
                tbAdr.Text = dgvPatient.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            queryClass.clrearC(this);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                    DialogResult iExit = MessageBox.Show("Are you sure, You want to UPDATE record", "Want Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (iExit == DialogResult.Yes)
                    {
                        string qry = string.Format("update patient set Patient_name='" + tbPatName.Text + "', Gender='" + cbGender.Text + "',Phone='" + tbPhone.Text + "',Age='" + tbAge.Text
                            + "',Address='" + tbAdr.Text + "' where Patient_id=@i");
                        queryClass.update(qry);
                        show();
                        queryClass.clrearC(this);
                        MessageBox.Show("Updated successful");
                    }
                }
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            queryClass.selectds("SELECT * FROM patient where Patient_name LIKE '%" + tbSearch.Text + "%' OR Patient_id LIKE '%" + tbSearch.Text + "%'");
            dgvPatient.DataSource = queryClass.ds.Tables[0];
        }
    }
}