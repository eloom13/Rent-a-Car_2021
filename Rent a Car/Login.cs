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
    public partial class Login : Form
    {
        private OleDbConnection konekcija = new OleDbConnection();

        public Login()
        {
            InitializeComponent();

            konekcija.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/Elmir/Desktop/Nitro.mdb; Persist Security Info = False";

        }
         
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                konekcija.Open();
                konekcija.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
          
            if (user_id.Text == "" && user_password.Text == "")
            {
                label1.Visible = true;
                label2.Visible = true;
            }

            else if (user_id.Text == "" && user_password.Text != null)
            {
                label1.Visible = true;
                label2.Visible = false;
            }

            else  if (user_password.Text == "" && user_id.Text !=null)
            {
                label1.Visible = false;
                label2.Visible = true;
            }

            else  if (user_id.Text != null && user_password.Text !=null )
            {
                label1.Visible = false;
                label2.Visible = false;

                konekcija.Open();
                OleDbCommand komanda = new OleDbCommand();

                komanda.Parameters.AddWithValue("@id", user_id.Text);
                komanda.Parameters.AddWithValue("@password", user_password.Text);

                komanda.Connection = konekcija;
                komanda.CommandType = CommandType.Text;
                komanda.CommandText = ("Select employees.ID, employees.password From employees Where ID = @id And password = @password ");

                OleDbDataReader citac = komanda.ExecuteReader();

                int count = 0;
                while (citac.Read())
                {
                    count = count + 1;
                }

                if (count == 1)
                {
                    MainForm mainform = new MainForm();
                    mainform.Show();
                }

                if(count == 0)
                {
                    MessageBox.Show("Incorrect ID or Passowrd!", "Incorrect Login!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



                konekcija.Close();
            }
      
        }

        private void clearbtn1_Click(object sender, EventArgs e)
        {
            user_id.Clear();
            user_password.Clear();
        }

        private void exitbtn2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
