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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AddProducts Obj = new AddProducts();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            AddSuppliers Obj = new AddSuppliers();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            AddCustomers Obj = new AddCustomers();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ViewProducts Obj = new ViewProducts();
            Obj.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ViewSuppliers Obj = new ViewSuppliers();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            ViewCustomers Obj = new ViewCustomers();
            Obj.Show();
            this.Hide();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            Logins Obj = new Logins();
            Obj.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

		private void panel4_Paint(object sender, PaintEventArgs e)
		{

		}

		private void pictureBox9_Click(object sender, EventArgs e)
		{
            ViewSuppliers vs = new ViewSuppliers();
            this.Hide();
            vs.Show();
		}

		private void pictureBox10_Click(object sender, EventArgs e)
		{
            ViewCustomers vc = new ViewCustomers();
            this.Hide();
            vc.Show();
		}

		private void pictureBox11_Click(object sender, EventArgs e)
		{
            ViewProducts vp = new ViewProducts();
            this.Hide();
            vp.Show();
		}

		private void pictureBox12_Click(object sender, EventArgs e)
		{
            Billings b = new Billings();
            this.Hide();
            b.Show();
		}

		private void panel4_MouseClick(object sender, MouseEventArgs e)
		{
            ViewSuppliers vp = new ViewSuppliers();
            this.Hide();
            vp.Show();
        }

		private void panel5_MouseClick(object sender, MouseEventArgs e)
		{
            ViewCustomers vc = new ViewCustomers();
            this.Hide();
            vc.Show();
        }

		private void panel7_MouseClick(object sender, MouseEventArgs e)
		{
            Billings b = new Billings();
            this.Hide();
            b.Show();
        }

		private void panel6_MouseClick(object sender, MouseEventArgs e)
		{
            ViewProducts vp = new ViewProducts();
            this.Hide();
            vp.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
