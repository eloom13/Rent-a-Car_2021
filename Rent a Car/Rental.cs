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
    public partial class Rental : Form
    {
        Double a, b;
        int c;
        private OleDbConnection konekcija = new OleDbConnection();

        public Rental()
        {
            InitializeComponent();
            konekcija.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/Elmir/Desktop/Nitro.mdb; Persist Security Info = False";
        }

        private void Rental_Load(object sender, EventArgs e)
        {

            konekcija.Open();

            /* CAR RENTAL COMBBOX */
            OleDbCommand car_rental = new OleDbCommand();
            car_rental.Connection = konekcija;
            car_rental.CommandType = CommandType.Text;
            car_rental.CommandText = ("Select * From cars");
            OleDbDataAdapter acarrental = new OleDbDataAdapter(car_rental);
            DataSet dcarrental = new DataSet();
            acarrental.Fill(dcarrental);
            for (int i = 0; i < dcarrental.Tables[0].Rows.Count; i++)
            {
                rental_car.Items.Add(dcarrental.Tables[0].Rows[i][0] + "|" + dcarrental.Tables[0].Rows[i][1] + "|" + dcarrental.Tables[0].Rows[i][3] + "|" + dcarrental.Tables[0].Rows[i][6]);
            }

            /* CUSTOMER RENTAL COMBBOX */
            OleDbCommand customer_rental = new OleDbCommand();
            customer_rental.Connection = konekcija;
            customer_rental.CommandType = CommandType.Text;
            customer_rental.CommandText = ("Select * From customers");
            OleDbDataAdapter acustomerrental = new OleDbDataAdapter(customer_rental);
            DataSet dcustomerrental = new DataSet();
            acustomerrental.Fill(dcustomerrental);
            for (int i = 0; i < dcustomerrental.Tables[0].Rows.Count; i++)
            {
                rental_customer.Items.Add(dcustomerrental.Tables[0].Rows[i][0] + " " + dcustomerrental.Tables[0].Rows[i][1] + " " + dcustomerrental.Tables[0].Rows[i][2] + ", " + dcustomerrental.Tables[0].Rows[i][3]);
            }


            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandType = CommandType.Text;
            komanda.CommandText = ("Select * From rental");

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = komanda;
            da.Fill(ds);
            rentaldata.DataSource = ds.Tables[0];

            konekcija.Close();

        }

        private void addbtn1_Click(object sender, EventArgs e)
        {
            DateTime dt1 = rental_date.Value;
            DateTime dt2 = rental_return.Value;
            a = Convert.ToDouble((rental_return.Value - rental_date.Value).TotalDays.ToString());
            

            konekcija.Open();
            OleDbCommand komanda2 = konekcija.CreateCommand();
            komanda2.CommandType = CommandType.Text;
            komanda2.Parameters.AddWithValue("@id", rental_car.Text.Substring(0, 1));
            komanda2.CommandText = ("Select * From cars Where ID =@id");

            OleDbDataReader reader = null;
            reader = komanda2.ExecuteReader();
            while (reader.Read())
            {
                b = Convert.ToDouble(reader["price"].ToString());
                c = Convert.ToInt32(a * b);
                DialogResult dr = MessageBox.Show("Vaš račun za rentanje iznosi: " + c.ToString() + Environment.NewLine + "Potvrdite rentanje.", "Predračun", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes)
                {
                    OleDbCommand komanda = konekcija.CreateCommand();
                    komanda.CommandType = CommandType.Text;
                    komanda.CommandText = ("Insert into rental values('" + rental_id.Text.ToUpper() + "', '" + rental_car.Text.ToUpper() + "',  '" + rental_customer.Text.ToUpper() + "', '" + rental_date.Text.ToUpper() + "', '" + rental_return.Text.ToUpper() + "', '" + c + "' )");
                    komanda.ExecuteNonQuery();

                    OleDbCommand komanda3 = konekcija.CreateCommand();
                    komanda3.Parameters.AddWithValue("@id", rental_car.Text.Substring(0, 1));
                    komanda3.CommandType = CommandType.Text;
                    komanda3.CommandText = ("Update cars set available = 'NO' Where ID = @id");
                    komanda3.ExecuteNonQuery();
                }
            }
            reader.Close();
            


            OleDbCommand citanje = konekcija.CreateCommand();
            citanje.CommandType = CommandType.Text;
            citanje.CommandText = ("Select * From rental");

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = citanje;
            da.Fill(ds);
            rentaldata.DataSource = ds.Tables[0];
            konekcija.Close();
        }

        private void editbtn1_Click(object sender, EventArgs e)
        {
            konekcija.Open();
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandType = CommandType.Text;

            komanda.CommandText = "Delete ID,car,customer,rental_date,return_date,bill From rental Where ID =" + rental_id.Text + "";
            komanda.ExecuteNonQuery();


            OleDbCommand komanda2 = new OleDbCommand();
            komanda2.Connection = konekcija;
            komanda2.CommandType = CommandType.Text;

            komanda2.Parameters.AddWithValue("@id", rental_id.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@car", rental_car.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@customer", rental_customer.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@rentaldate", rental_date.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@returndate", rental_return.Text.ToUpper());
           

            komanda2.CommandText = ("Insert into rental values(@id,@car,@customer,@rental_date,@rental_return)");
            komanda2.ExecuteNonQuery();


            OleDbCommand citanje = konekcija.CreateCommand();
            citanje.CommandType = CommandType.Text;
            citanje.CommandText = ("Select * From rental");

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = citanje;
            da.Fill(ds);
            rentaldata.DataSource = ds.Tables[0];

            konekcija.Close();

            rental_id.Clear();
            
        }

        private void rentaldata_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            rental_id.Text = rentaldata.SelectedRows[0].Cells[0].Value.ToString();
            rental_car.Text = rentaldata.SelectedRows[0].Cells[1].Value.ToString();
            rental_customer.Text = rentaldata.SelectedRows[0].Cells[2].Value.ToString();
            rental_date.Text = rentaldata.SelectedRows[0].Cells[3].Value.ToString();
            rental_return.Text = rentaldata.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void backbtn1_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Hide();
        }

        private void deletebtn1_Click(object sender, EventArgs e)
        {
            if (rental_id.Text == "")
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

                komanda.CommandText = "Delete ID,car,customer,rental_date,return_date,bill From rental Where ID =" + rental_id.Text + "";
                komanda.ExecuteNonQuery();

                konekcija.Close();

                OleDbCommand citanje = konekcija.CreateCommand();
                citanje.CommandType = CommandType.Text;
                citanje.CommandText = ("Select * From rental");

                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = citanje;
                da.Fill(ds);
                rentaldata.DataSource = ds.Tables[0];
                konekcija.Close();

                rental_id.Clear();
            }
        }

        

       

        

       

       

        

       
    }
}

