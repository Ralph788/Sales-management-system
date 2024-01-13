using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSTuto
{
    public partial class Billings : Form
    {
       
        public Billings()
        {
            InitializeComponent();
            DisplayProducts();
            GetCustomer();
           // GetDate();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            Logins l = new Logins();
            l.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ralph\OneDrive\Desktop\PointOfSale\PointOfSale\POSTuto\POSDb.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayProducts()
        {
            Con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetCustName()
        {
            Con.Open();
            string Query = "select * from CustomerTbl where CustId=" + CustIdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close();
        }
        private void SearchProducts()
        {
            Con.Open();
            string Query = "Select * from ProductTbl where PName='"+SearchTb.Text+"'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            SearchProducts();
            SearchTb.Text = "";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            DisplayProducts();
            SearchTb.Text = "";
        }
        private void GetCustomer()
            {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustId from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(rdr);
            CustIdCb.ValueMember = "CustId";
            CustIdCb.DataSource = dt;
            Con.Close();
            }
        private void GetDate()
        {

            //DateTime now = DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            DateLbl.Text = ""+ DateTime.Now.ToString("MM/dd/yyyy HH:mm");
        }
        private void Reset()
        {
            Pname = "";
            QtyTb.Text = "";
            Key = 0;
        }
        private void UpdateQty()
        {
            int newQty = PStock - Convert.ToInt32(QtyTb.Text);
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("Update ProductTbl set PQty=@PQ where PId=@PKey", Con);
               
                cmd.Parameters.AddWithValue("@PQ", newQty);
                cmd.Parameters.AddWithValue("@PKey", Key);

                cmd.ExecuteNonQuery();
              //  MessageBox.Show("Product Updated");
                Con.Close();
                DisplayProducts();
              //  Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        int Key = 0;
        string Pname;
        int Pprice, PStock;
        int n = 1,total = 0;
        private void AddToBill_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select A Product");
            }else if(QtyTb.Text == "")
            {
                MessageBox.Show("Enter The Quantity");

            }else if(Convert.ToInt32(QtyTb.Text) > PStock)
            {
                MessageBox.Show("No Enough Stock");
            }
            else
            {
                int Subtotal = Convert.ToInt32(QtyTb.Text) * Pprice;
                total = total + Subtotal;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n;
                newRow.Cells[1].Value = Pname;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = Pprice;
                newRow.Cells[4].Value = Subtotal;
                BillDGV.Rows.Add(newRow);
                n++;
                SubTotalTb.Text = "" + total;
               // double VAT = (Convert.ToDouble(VATTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                TotTaxTb.Text = "" + VATTb.Text;
                GrdTotalTb.Text = "" + (Convert.ToInt32(SubTotalTb.Text) + Convert.ToDouble(TotTaxTb.Text));

               // double Disc = (Convert.ToDouble(DiscountTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                DiscTotTb.Text = "0" ;
               
                Reset();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
        private void VATTb_KeyUp(object sender, KeyEventArgs e)
        {
            if(VATTb.Text == "")
            {

            }else if(SubTotalTb.Text == "")
            {
                MessageBox.Show("Add Products To Cart");
                VATTb.Text = "";
            }else
            {
                try
                {
                    MessageBox.Show(VATTb.Text);
                    double VAT = (Convert.ToDouble(VATTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                    TotTaxTb.Text = "" + VAT;
                    GrdTotalTb.Text = ""+(Convert.ToInt32(SubTotalTb.Text) + Convert.ToDouble(TotTaxTb.Text));
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
           
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (DiscountTb.Text == "")
            {

            }
            else if (SubTotalTb.Text == "")
            {
                MessageBox.Show("Add Products To Cart");
                DiscountTb.Text = "";
            }
            else
            {
                try
                {
                    double Disc = (Convert.ToDouble(DiscountTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                    DiscTotTb.Text = "" + Disc;
                    GrdTotalTb.Text = "" + (Convert.ToInt32(SubTotalTb.Text) + Convert.ToDouble(TotTaxTb.Text)-Convert.ToDouble(DiscTotTb.Text));
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void InsertBill()
        {
            if (CustIdCb.SelectedIndex == -1 || PaymentMtdCb.SelectedIndex == -1 || GrdTotalTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
                

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(BDate,CustId,CustName,PMethod,Amt)values(@BD,@CI,@CN,@PM,@Am)", Con);
                    cmd.Parameters.AddWithValue("@BD", BDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@PM", PaymentMtdCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Am", Convert.ToDouble(GrdTotalTb.Text));

                    cmd.ExecuteNonQuery();
                    flag = 1;
                   // MessageBox.Show("Bill Saved");
                    Con.Close();
                   // Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int flag = 0;
        Bitmap memoryImage;
        int prodid, prodqty, prodprice, tottal, pos = 60;

        private void Billings_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            DiscountTb.Text = "";
            DiscTotTb.Text = "";
            VATTb.Text = "";
            TotTaxTb.Text = "";
            GrdTotalTb.Text = "";

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            GetDate();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            ViewBills Obj = new ViewBills();
            Obj.Show();
        }

		[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

		private void PrintBtn_Click(object sender, EventArgs e)
		{
            if (CustIdCb.SelectedIndex == -1 || PaymentMtdCb.SelectedIndex == -1 || GrdTotalTb.Text == "" || CustNameTb.Text=="")
            {
                MessageBox.Show("Missing Information");


            }
            else
            {
                InsertBill();
                PrintForm printFrm = new PrintForm();
                this.Hide();
                
                printFrm.label6.Text = BillDGV.SelectedRows[0].Cells[1].Value.ToString();
                printFrm.label2.Text = DateLbl.Text;
                printFrm.label5.Text = CustNameTb.Text;
                //qty = QtyTb.Text;
                printFrm.label8.Text = SubTotalTb.Text;
                printFrm.label10.Text = BillDGV.SelectedRows[0].Cells[2].Value.ToString();
                printFrm.label4.Text = PaymentMtdCb.SelectedItem.ToString();
                printFrm.label9.Text = GrdTotalTb.Text;
                printFrm.Show();
            }
        }

        private void QtyTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void DateLbl_Click(object sender, EventArgs e)
        {

        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void SubTotalTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void GrdTotalTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
            e.Graphics.DrawImage(memoryImage, 0, 0);
		}

		private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BillDGV.ReadOnly = true;
        }
       // public static string qty;
         

		private void EditBtn_Click(object sender, EventArgs e)
		{
			InsertBill();
            int height=BillDGV.Height;
            BillDGV.Height = BillDGV.RowCount * BillDGV.RowTemplate.Height * 2;
            memoryImage = new Bitmap(BillDGV.Width, BillDGV.Height);
            BillDGV.DrawToBitmap(memoryImage, new Rectangle(0, 0, BillDGV.Width, BillDGV.Height));
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
            BillDGV.Height = height;
			PrintForm printFrm = new PrintForm();
			this.Hide();
            printFrm.Show();
            //foreach (DataGridViewRow rows in BillDGV.Rows)
            //{
            //             if (rows.Cells==null)
            //             {
            //                 break;
            //             }
            //             else
            //             {
            //int rowindex = BillDGV.CurrentCell.RowIndex;
            //int columnindex = BillDGV.CurrentCell.ColumnIndex;
            //// BillDGV.Rows[rowindex].Cells[columnindex].Value.ToString();
            //MessageBox.Show(BillDGV.Rows[rowindex].Cells[columnindex].Value.ToString());
            //printFrm.label4.Text = BillDGV.SelectedRows[0].Cells[0].ToString();
            //printFrm.label6.Text = BillDGV.SelectedRows[0].Cells[1].Value.ToString();
            //printFrm.label2.Text = BDate.Value.ToLongDateString();
            ////qty = QtyTb.Text;
            //printFrm.label8.Text = SubTotalTb.Text;
            //printFrm.label10.Text = BillDGV.SelectedRows[0].Cells[2].Value.ToString();
            //printFrm.label12.Text = GrdTotalTb.Text;
            ////}
            //}
            printFrm.Show();

		}
		private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Print the contents.
            e.Graphics.DrawImage(memoryImage, 0, 0);
           
        }

        private void CustIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ProductsDGV.ReadOnly = true;
            Pname = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
          //  PCatCb.SelectedItem = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
           Pprice = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PStock = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[4].Value.ToString());
            if (Pname == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[0].Value.ToString());

            }
           
        }

        //New Print Function

        //private void printDocument1_BeginPrint(object sender,
        //                               System.Drawing.Printing.PrintEventArgs e)
        //{
        //    // create a demo title for the page header:
        //    printDocument1.DocumentName = " Order Summary "
        //                                  + DateTime.Now.ToShortDateString();
        //    // initial values for a print job:
        //    nextPageToPrint = 0;
        //    linesOnPage = 0;
        //    maxLinesOnPage = 30;
        //    linesPrinted = 0;
        //}

    }
}
