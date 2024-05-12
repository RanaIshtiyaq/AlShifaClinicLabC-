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
    public partial class doctor : Form
    {
        public doctor()
        {
            InitializeComponent();
        }
        public void show()
        {
            try
            {
                queryClass.selectds("Select * from doctor");
                Properties.Settings.Default.TotalDoc = queryClass.ds.Tables[0].Rows.Count.ToString();
                dgvDoctor.DataSource = queryClass.ds.Tables[0];
                Properties.Settings.Default.Save();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void doctor_Load(object sender, EventArgs e)
        {
            show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbDocName.Text) || string.IsNullOrEmpty(tbAdr.Text) || string.IsNullOrEmpty(tbPhone.Text)
                          || string.IsNullOrEmpty(tbComm.Text) || string.IsNullOrEmpty(tbDesig.Text))
                {
                    MessageBox.Show("Please fill fields");
                }
                else
                {
                    string qry = string.Format("insert into doctor(Doctor_name,Phone,Address,Designation,Commission)values('" + tbDocName.Text + "','" + tbPhone.Text
                + "','" + tbAdr.Text + "','" + tbDesig.Text + "','" + tbComm.Text + "' )");
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

        private void dgvDoctor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                queryClass.id = Convert.ToInt32(dgvDoctor.Rows[e.RowIndex].Cells[0].Value.ToString());

            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void dgvDoctor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             try
            {
                tbDocName.Text = dgvDoctor.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbDesig.Text = dgvDoctor.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbPhone.Text = dgvDoctor.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbComm.Text = dgvDoctor.Rows[e.RowIndex].Cells[4].Value.ToString();
                tbAdr.Text = dgvDoctor.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (queryClass.id == 0) { MessageBox.Show("Click on record for delete"); }
                else
                {
                    DialogResult iExit = MessageBox.Show("Are you sure, You want to DELETE record", "Want Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (iExit == DialogResult.Yes)
                    {
                        string qry = string.Format("delete from doctor where Doctor_id=@i");
                        queryClass.Delete(qry);
                        show();
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbDocName.Text) || string.IsNullOrEmpty(tbPhone.Text) || string.IsNullOrEmpty(tbDesig.Text)
                        || string.IsNullOrEmpty(tbComm.Text) || string.IsNullOrEmpty(tbAdr.Text))
                {
                    MessageBox.Show("Please fill fields");
                }
                else
                {
                    DialogResult iExit = MessageBox.Show("Are you sure, You want to UPDATE record", "Want Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (iExit == DialogResult.Yes)
                    {
                        string qry = string.Format("update doctor set Doctor_name='" + tbDocName.Text + "', Phone='" + tbPhone.Text + "',Designation='" + tbDesig.Text + "',Commission='" + tbComm.Text
                            + "',Address='" + tbAdr.Text + "' where Doctor_id=@i");
                        queryClass.update(qry);
                        show();
                        queryClass.clrearC(this);
                        MessageBox.Show("Updated successful");
                    }
                }
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            queryClass.clrearC(this);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            queryClass.selectds("SELECT * FROM doctor where Doctor_name LIKE '%" + tbSearch.Text + "%'");
            dgvDoctor.DataSource = queryClass.ds.Tables[0];

        }
    }
}
