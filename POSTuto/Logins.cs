using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSTuto
{
    public partial class Logins : Form
    {
        public Logins()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings Obj = new Billings();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(UnameTb.Text == "" || PasswordTb.Text == "")
            {
               MessageBox.Show("Enter UserName and Password");
                //MessageBox.Show("Enter UserName and Password");
            }else if(UnameTb.Text == "Admin" && PasswordTb.Text == "Password")
            {
                MainMenu Obj = new MainMenu();
                Obj.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Wrong UserName Or Password");
                //MessageBox.Show("Wrong UserName Or Password");
            }
        }
        private void PasswordTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                bunifuThinButton21_Click(sender, e);
            }
        }

        private void PasswordTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue==13)
            {
                bunifuThinButton21_Click(sender, e);
            }
        }
    }
}
