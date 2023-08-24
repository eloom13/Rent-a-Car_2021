using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Rent_a_Car
{
    public partial class Customer : Form
    {
   
        private OleDbConnection konekcija = new OleDbConnection();
        int b;

        public Customer()
        {
            InitializeComponent();
            konekcija.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/Elmir/Desktop/Nitro.mdb; Persist Security Info = False";
        }

        private void Customer_Load(object sender, EventArgs e)
        {
 
            konekcija.Open();
            OleDbCommand komanda = new OleDbCommand();

            komanda.Connection = konekcija;
            komanda.CommandType = CommandType.Text;
            komanda.CommandText = ("Select * From customers");

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = komanda;
            da.Fill(ds);
            customerdata.DataSource = ds.Tables[0];
            konekcija.Close();

        }

        private void backbtn1_Click(object sender, EventArgs e)
        {

            b = customerdata.RowCount - 1;
            Class1.customer = b.ToString();
            MainForm mf = new MainForm();
            mf.Show();
            this.Close();
        }

        private void addbtn1_Click(object sender, EventArgs e)
        {
      
            if (customer_id.Text == "")
            {
                idlbl.Visible = true;
            }

            if (customer_id.Text != "")
            {
                idlbl.Visible = false;
            }

            if (customer_fname.Text == "")
            {
                fnamelbl.Visible = true;
            }

            if (customer_fname.Text != "")
            {
                fnamelbl.Visible = false;
            }

            if (customer_lname.Text == "")
            {
                lnamelbl.Visible = true;
            }

            if (customer_lname.Text != "")
            {
                lnamelbl.Visible = false;
            }

            if (customer_address.Text == "")
            {
                addresslbl.Visible = true;
            }

            if (customer_address.Text != "")
            {
                addresslbl.Visible = false;
            }

            if (customer_phone.Text == "")
            {
                phonelbl.Visible = true;
            }

            if (customer_phone.Text != "")
            {
                phonelbl.Visible = false;
            }

            if (customer_id.Text != "" && customer_fname.Text != "" && customer_lname.Text != "" && customer_address.Text != "" && customer_phone.Text != "")
            {
                konekcija.Open();
                OleDbCommand komanda = konekcija.CreateCommand();
                komanda.CommandType = CommandType.Text;
                komanda.CommandText = ("Insert into customers values('" + customer_id.Text.ToUpper() + "', '" + customer_fname.Text.ToUpper() + "',  '" + customer_lname.Text.ToUpper() + "', '" + customer_address.Text.ToUpper() + "', '" + customer_phone.Text + "')");
                komanda.ExecuteNonQuery();

                OleDbCommand citanje = konekcija.CreateCommand();
                citanje.CommandType = CommandType.Text;
                citanje.CommandText = ("Select * From customers");

                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = citanje;
                da.Fill(ds);
                customerdata.DataSource = ds.Tables[0];
                konekcija.Close();

                customer_id.Clear();
                customer_fname.Clear();
                customer_lname.Clear();
                customer_address.Clear();
                customer_phone.Clear();
            }
           



        }

        private void deletebtn1_Click(object sender, EventArgs e)
        {
            if (customer_id.Text == "")
            {
                idlbl.Visible = true;
            }

            else
            {

                idlbl.Visible = false;

                konekcija.Open();

                OleDbCommand komanda = new OleDbCommand();
                komanda.Connection = konekcija;
                komanda.CommandType = CommandType.Text;

                komanda.CommandText = "Delete ID,first_name,last_name,address,phone From customers Where ID =" + customer_id.Text + "";
                komanda.ExecuteNonQuery();

                konekcija.Close();

                OleDbCommand citanje = konekcija.CreateCommand();
                citanje.CommandType = CommandType.Text;
                citanje.CommandText = ("Select * From customers");

                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = citanje;
                da.Fill(ds);
                customerdata.DataSource = ds.Tables[0];
                konekcija.Close();

                customer_id.Clear();
                customer_fname.Clear();
                customer_lname.Clear();
                customer_address.Clear();
                customer_phone.Clear();
            }
        }

        private void editbtn1_Click(object sender, EventArgs e)
        {
            konekcija.Open();
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandType = CommandType.Text;

            komanda.CommandText = "Delete ID,first_name,last_name,address,phone From customers Where ID =" + customer_id.Text + "";
            komanda.ExecuteNonQuery();


            OleDbCommand komanda2 = new OleDbCommand();
            komanda2.Connection = konekcija;
            komanda2.CommandType = CommandType.Text;

            komanda2.Parameters.AddWithValue("@id", customer_id.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@first_name", customer_fname.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@last_name", customer_lname.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@address", customer_address.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@phone", customer_phone.Text.ToUpper());

            komanda2.CommandText = ("Insert into customers values(@id,@first_name,@last_name,@address,@phone)");
            komanda2.ExecuteNonQuery();


            OleDbCommand citanje = konekcija.CreateCommand();
            citanje.CommandType = CommandType.Text;
            citanje.CommandText = ("Select * From customers");

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = citanje;
            da.Fill(ds);
            customerdata.DataSource = ds.Tables[0];

            konekcija.Close();

            customer_id.Clear();
            customer_fname.Clear();
            customer_lname.Clear();
            customer_address.Clear();
            customer_phone.Clear();
        }

        private void customerdata_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            customer_id.Text = customerdata.SelectedRows[0].Cells[0].Value.ToString();
            customer_fname.Text = customerdata.SelectedRows[0].Cells[1].Value.ToString();
            customer_lname.Text = customerdata.SelectedRows[0].Cells[2].Value.ToString();
            customer_address.Text = customerdata.SelectedRows[0].Cells[3].Value.ToString();
            customer_phone.Text = customerdata.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (customer_fname.Text == "")
            {
                fnamelbl.Visible = true;
            }


            if (customer_fname.Text != "")
            {
                fnamelbl.Visible = false;
                int count;
                konekcija.Open();
                OleDbCommand komanda = konekcija.CreateCommand();
                komanda.CommandType = CommandType.Text;
                komanda.Parameters.AddWithValue("@first_name", customer_fname.Text);
                komanda.Parameters.AddWithValue("@last_name", customer_lname.Text);
                komanda.CommandText = ("Select * from customers where first_name = @first_name");
                komanda.ExecuteNonQuery();

                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(komanda);
                da.Fill(dt);
                count = Convert.ToInt16(dt.Rows.Count.ToString());
                customerdata.DataSource = dt;
                konekcija.Close();

                if (count == 0)
                {
                    MessageBox.Show("Customer does not exist!");
                }
        }

       

        
    }

        
 }

}
