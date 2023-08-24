using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace Rent_a_Car
{

    public partial class Employee : Form
    {
        private OleDbConnection konekcija = new OleDbConnection();
        int a;

        public Employee()
        {
            InitializeComponent();
            konekcija.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/Elmir/Desktop/Nitro.mdb; Persist Security Info = False";
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            konekcija.Open();
            OleDbCommand komanda = new OleDbCommand();

            komanda.Connection = konekcija;
            komanda.CommandType = CommandType.Text;
            komanda.CommandText = ("Select * From employees");

            DataSet ds = new DataSet();
            OleDbDataAdapter da = new OleDbDataAdapter();
            da.SelectCommand = komanda;
            da.Fill(ds);
            employeedata.DataSource = ds.Tables[0];
            konekcija.Close();

          
        }

        private void addbtn1_Click(object sender, EventArgs e)
        {
       

            if (employee_id.Text == "")
            {
                idlbl.Visible = true;
            }

            if (employee_id.Text != "")
            {
                idlbl.Visible = false;
            }

            if (employee_fname.Text == "")
            {
                fnamelbl.Visible = true;
            }

            if (employee_fname.Text != "")
            {
                fnamelbl.Visible = false;
            }

            if (employee_lname.Text == "")
            {
                lnamelbl.Visible = true;
            }

            if (employee_lname.Text != "")
            {
                lnamelbl.Visible = false;
            }

            if (employee_age.Text == "")
            {
                agelbl.Visible = true;
            }

            if (employee_age.Text != "")
            {
                agelbl.Visible = false;
            }

            if (employee_password.Text == "")
            {
                pwlbl.Visible = true;
            }

            if (employee_password.Text != "")
            {
                pwlbl.Visible = false;
            }

            if (employee_id.Text != "" && employee_fname.Text != "" && employee_lname.Text != "" && employee_age.Text != "" && employee_password.Text != "")
            {
                konekcija.Open();
                OleDbCommand komanda = konekcija.CreateCommand();
                komanda.CommandType = CommandType.Text;
                komanda.CommandText = ("Insert into employees values('" + employee_id.Text.ToUpper() + "', '" + employee_fname.Text.ToUpper() + "',  '" + employee_lname.Text.ToUpper() + "', '" + employee_age.Text.ToUpper() + "', '" + employee_password.Text + "')");
                komanda.ExecuteNonQuery();

                OleDbCommand citanje = konekcija.CreateCommand();
                citanje.CommandType = CommandType.Text;
                citanje.CommandText = ("Select * From employees");

                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = citanje;
                da.Fill(ds);
                employeedata.DataSource = ds.Tables[0];
                konekcija.Close();

                employee_id.Clear();
                employee_fname.Clear();
                employee_lname.Clear();
                employee_age.Clear();
                employee_password.Clear();
            }


        }

        private void deletebtn1_Click(object sender, EventArgs e)
        {
       
            if (employee_id.Text == "")
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

                komanda.CommandText = "Delete ID,first_name,last_name,age,password From employees Where ID =" + employee_id.Text + "";
                komanda.ExecuteNonQuery();

                konekcija.Close();

                OleDbCommand citanje = konekcija.CreateCommand();
                citanje.CommandType = CommandType.Text;
                citanje.CommandText = ("Select * From employees");

                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = citanje;
                da.Fill(ds);
                employeedata.DataSource = ds.Tables[0];
                konekcija.Close();

                employee_id.Clear();
                employee_fname.Clear();
                employee_lname.Clear();
                employee_age.Clear();
                employee_password.Clear();
            }

        }

        private void backbtn1_Click(object sender, EventArgs e)
        {
            a = employeedata.RowCount - 1;
            Class1.employee = a.ToString();
            MainForm mf = new MainForm();
            mf.Show();
            this.Close();
        }


        private void editbtn1_Click(object sender, EventArgs e)
        {
            
                konekcija.Open();
                OleDbCommand komanda = new OleDbCommand();
                komanda.Connection = konekcija;
                komanda.CommandType = CommandType.Text;

                komanda.CommandText = "Delete ID,first_name,last_name,age,password From employees Where ID =" + employee_id.Text + "";
                komanda.ExecuteNonQuery();


                OleDbCommand komanda2 = new OleDbCommand();
                komanda2.Connection = konekcija;
                komanda2.CommandType = CommandType.Text;

                komanda2.Parameters.AddWithValue("@id", employee_id.Text.ToUpper());
                komanda2.Parameters.AddWithValue("@first_name", employee_fname.Text.ToUpper());
                komanda2.Parameters.AddWithValue("@last_name", employee_lname.Text.ToUpper());
                komanda2.Parameters.AddWithValue("@age", employee_age.Text.ToUpper());
                komanda2.Parameters.AddWithValue("@password", employee_password.Text.ToUpper());

                komanda2.CommandText = ("Insert into employees values(@id,@first_name,@last_name,@age,@password)");
                komanda2.ExecuteNonQuery();


                OleDbCommand citanje = konekcija.CreateCommand();
                citanje.CommandType = CommandType.Text;
                citanje.CommandText = ("Select * From employees");

                DataSet ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = citanje;
                da.Fill(ds);
                employeedata.DataSource = ds.Tables[0];

                konekcija.Close();
                
                employee_id.Clear();
                employee_fname.Clear();
                employee_lname.Clear();
                employee_age.Clear();
                employee_password.Clear();
            
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (employee_fname.Text == "")
            {
                fnamelbl.Visible = true;
            }


            if (employee_fname.Text != "")
            {
                fnamelbl.Visible = false;
                int count;
                konekcija.Open();
                OleDbCommand komanda = konekcija.CreateCommand();
                komanda.CommandType = CommandType.Text;
                komanda.Parameters.AddWithValue("@first_name", employee_fname.Text);
                komanda.Parameters.AddWithValue("@last_name", employee_lname.Text);
                komanda.CommandText = ("Select * from employees where first_name = @first_name");
                komanda.ExecuteNonQuery();

                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(komanda);
                da.Fill(dt);
                count = Convert.ToInt16(dt.Rows.Count.ToString());
                employeedata.DataSource = dt;
                konekcija.Close();

                if (count == 0)
                {
                    MessageBox.Show("Employee does not exist!");
                }
            }
        }

        private void employeedata_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            employee_id.Text = employeedata.SelectedRows[0].Cells[0].Value.ToString();
            employee_fname.Text = employeedata.SelectedRows[0].Cells[1].Value.ToString();
            employee_lname.Text = employeedata.SelectedRows[0].Cells[2].Value.ToString();
            employee_age.Text = employeedata.SelectedRows[0].Cells[3].Value.ToString();
            employee_password.Text = employeedata.SelectedRows[0].Cells[4].Value.ToString();
        }

        
       

            

        
      

     
    }
}
