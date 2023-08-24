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
    public partial class Car : Form
    {
        private OleDbConnection konekcija = new OleDbConnection();
        int c;
        public Car()
        {
            InitializeComponent();
            konekcija.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/Elmir/Desktop/Nitro.mdb; Persist Security Info = False";
        }

        private void Car_Load(object sender, EventArgs e)
        {
            konekcija.Open();
            OleDbCommand komanda = new OleDbCommand();

            komanda.Connection = konekcija;
            komanda.CommandType = CommandType.Text;
            komanda.CommandText = ("Select * From cars");

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = komanda;
            da.Fill(ds);
            cardata.DataSource = ds.Tables[0];
            konekcija.Close();
        }

        private void backbtn1_Click(object sender, EventArgs e)
        {
            c = cardata.RowCount - 1;
            Class1.car = c.ToString();
            MainForm mf = new MainForm();
            mf.Show();
            this.Close();
        }

        private void addbtn1_Click(object sender, EventArgs e)
        {
            if (car_id.Text == "")
            {
                idlbl.Visible = true;
            }

            if (car_id.Text != "")
            {
                idlbl.Visible = false;
            }

            if (car_regno.Text == "")
            {
                regnolbl.Visible = true;
            }

            if (car_regno.Text != "")
            {
                regnolbl.Visible = false;
            }

            if (car_brand.Text == "")
            {
                brandlbl.Visible = true;
            }

            if (car_brand.Text != "")
            {
                brandlbl.Visible = false;
            }

            if (car_model.Text == "")
            {
                modellbl.Visible = true;
            }

            if (car_model.Text != "")
            {
                modellbl.Visible = false;
            }

            if (car_color.Text == "")
            {
                colorlbl.Visible = true;
            }

            if (car_color.Text != "")
            {
                colorlbl.Visible = false;
            }

            if (car_price.Text == "")
            {
                pricelbl.Visible = true;
            }

            if (car_price.Text != "")
            {
                pricelbl.Visible = false;
            }

            if (car_available.Text == "")
            {
                availablelbl.Visible = true;
            }

            if (car_available.Text != "")
            {
                availablelbl.Visible = false;
            }

            if (car_id.Text != "" && car_regno.Text != "" && car_brand.Text != "" && car_model.Text != "" && car_color.Text != "" && car_price.Text != "" && car_available.Text != "")
            {
                konekcija.Open();
                OleDbCommand komanda = konekcija.CreateCommand();
                komanda.CommandType = CommandType.Text;
                komanda.CommandText = ("Insert into cars values('" + car_id.Text.ToUpper() + "', '" + car_regno.Text.ToUpper() + "',  '" + car_brand.Text.ToUpper() + "', '" + car_model.Text.ToUpper() + "', '" + car_color.Text.ToUpper() + "', '" + car_price.Text.ToUpper() + "', '" + car_available.Text.ToUpper() + "')");
                komanda.ExecuteNonQuery();

                OleDbCommand citanje = konekcija.CreateCommand();
                citanje.CommandType = CommandType.Text;
                citanje.CommandText = ("Select * From cars");

                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = citanje;
                da.Fill(ds);
                cardata.DataSource = ds.Tables[0];
                konekcija.Close();

                car_id.Clear();
                car_regno.Clear();
                car_brand.Clear();
                car_model.Clear();
                car_color.Clear();
                car_price.Clear();
             
            }    
        }

        private void deletebtn1_Click(object sender, EventArgs e)
        {
            if (car_id.Text == "")
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

                komanda.CommandText = "Delete ID,registration_number,brand,model,color,price,available From cars Where ID =" + car_id.Text + "";
                komanda.ExecuteNonQuery();

                konekcija.Close();

                OleDbCommand citanje = konekcija.CreateCommand();
                citanje.CommandType = CommandType.Text;
                citanje.CommandText = ("Select * From cars");

                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = citanje;
                da.Fill(ds);
                cardata.DataSource = ds.Tables[0];
                konekcija.Close();

                car_id.Clear();
                car_regno.Clear();
                car_brand.Clear();
                car_model.Clear();
                car_color.Clear();
                car_price.Clear();
            }
        }

        private void cardata_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            car_id.Text = cardata.SelectedRows[0].Cells[0].Value.ToString();
            car_regno.Text = cardata.SelectedRows[0].Cells[1].Value.ToString();
            car_brand.Text = cardata.SelectedRows[0].Cells[2].Value.ToString();
            car_model.Text = cardata.SelectedRows[0].Cells[3].Value.ToString();
            car_color.Text = cardata.SelectedRows[0].Cells[4].Value.ToString();
            car_price.Text = cardata.SelectedRows[0].Cells[5].Value.ToString();
            car_available.Text = cardata.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void editbtn1_Click(object sender, EventArgs e)
        {
            konekcija.Open();
            OleDbCommand komanda = new OleDbCommand();
            komanda.Connection = konekcija;
            komanda.CommandType = CommandType.Text;

            komanda.CommandText = "Delete ID,registration_number,brand,model,color,price,available From cars Where ID =" + car_id.Text + "";
            komanda.ExecuteNonQuery();


            OleDbCommand komanda2 = new OleDbCommand();
            komanda2.Connection = konekcija;
            komanda2.CommandType = CommandType.Text;

            komanda2.Parameters.AddWithValue("@id", car_id.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@regno", car_regno.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@brand", car_brand.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@model", car_model.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@color", car_color.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@price", car_price.Text.ToUpper());
            komanda2.Parameters.AddWithValue("@available", car_available.Text.ToUpper());

            komanda2.CommandText = ("Insert into cars values(@id,@regno,@brand,@model,@color,@price,@available)");
            komanda2.ExecuteNonQuery();


            OleDbCommand citanje = konekcija.CreateCommand();
            citanje.CommandType = CommandType.Text;
            citanje.CommandText = ("Select * From cars");

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = citanje;
            da.Fill(ds);
            cardata.DataSource = ds.Tables[0];

            konekcija.Close();

            car_id.Clear();
            car_regno.Clear();
            car_brand.Clear();
            car_model.Clear();
            car_color.Clear();
            car_price.Clear();
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
             if (car_model.Text == "")
            {
                modellbl.Visible = true;
            }


            if (car_model.Text != "")
            {
                modellbl.Visible = false;
                int count;
                konekcija.Open();
                OleDbCommand komanda = konekcija.CreateCommand();
                komanda.CommandType = CommandType.Text;
                komanda.Parameters.AddWithValue("@car_model", car_model.Text);
            
                komanda.CommandText = ("Select * from cars where model = @car_model");
                komanda.ExecuteNonQuery();

                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(komanda);
                da.Fill(dt);
                count = Convert.ToInt16(dt.Rows.Count.ToString());
                cardata.DataSource = dt;
                konekcija.Close();

                if (count == 0)
                {
                    MessageBox.Show("Car does not exist!");
                }
        }

        

       
        
    }
}
}