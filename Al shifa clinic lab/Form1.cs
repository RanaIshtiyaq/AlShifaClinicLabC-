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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string qry = string.Format("select * from admin where userName='" + tbUserName.Text + "' AND pasword='" + tbPasword.Text + "' ");
            queryClass.con.Open();
            OleDbCommand cmd = new OleDbCommand(qry, queryClass.con);
            OleDbDataReader rd = cmd.ExecuteReader();
            if (rd.Read() )
            {
                if(tbUserName.Text=="admin"){
                Dashboard db = new Dashboard();
                db.Show();
                
                this.Hide();}
                else if (tbUserName.Text == "operator")
                {
                    operatorDb db = new operatorDb();
                    db.Show(); queryClass.con.Close();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show("User name or password not match");
            }

            queryClass.con.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
