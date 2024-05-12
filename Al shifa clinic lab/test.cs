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
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }
        
        public void show()
        {
            try
            {
                queryClass.selectds("Select * from test");
                dgvTest.DataSource = queryClass.ds.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void test_Load(object sender, EventArgs e)
        {
            show();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbTestName.Text) ||  string.IsNullOrEmpty(tbNormal_value.Text)
                          || string.IsNullOrEmpty(tbPrice.Text))
                {
                    MessageBox.Show("Please fill fields");
                }
                else
                {
                    string qry = string.Format("insert into test(Test_name,Normal_value,Price,Description)values('" + tbTestName.Text + "','" + tbNormal_value.Text
                + "','" + tbPrice.Text + "','" + tbDesc.Text + "' )");
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            queryClass.clrearC(this);
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
                        string qry = string.Format("delete from test where Test_id=@i");
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
                if (string.IsNullOrEmpty(tbTestName.Text) ||  string.IsNullOrEmpty(tbNormal_value.Text)
                        || string.IsNullOrEmpty(tbPrice.Text) || string.IsNullOrEmpty(tbDesc.Text))
                {
                    MessageBox.Show("Please fill fields");
                }
                else
                {
                    DialogResult iExit = MessageBox.Show("Are you sure, You want to UPDATE record", "Want Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (iExit == DialogResult.Yes)
                    {
                        string qry = string.Format("update test set Test_name='" + tbTestName.Text + "',Normal_value='" + tbNormal_value.Text + "',Price='" + tbPrice.Text
                            + "',Description='" + tbDesc.Text + "' where Test_id=@i");
                        queryClass.update(qry);
                        show();
                        queryClass.clrearC(this);
                        MessageBox.Show("Updated successful");
                    }
                }
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void dgvTest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                queryClass.id = Convert.ToInt32(dgvTest.Rows[e.RowIndex].Cells[0].Value.ToString());

            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void dgvTest_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tbTestName.Text = dgvTest.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbNormal_value.Text = dgvTest.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbPrice.Text = dgvTest.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbDesc.Text = dgvTest.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            catch (Exception x) { MessageBox.Show(x.Message); }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            queryClass.selectds("SELECT * FROM test where Test_name LIKE '%" + tbSearch.Text + "%'");
            dgvTest.DataSource = queryClass.ds.Tables[0];
        }
    }
}
