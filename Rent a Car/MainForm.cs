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
    public partial class MainForm : Form
    {
        private OleDbConnection konekcija = new OleDbConnection();
        public MainForm()
        {
            InitializeComponent();
            konekcija.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/Elmir/Desktop/Nitro.mdb; Persist Security Info = False";
        }

        private void employeebtn_Click(object sender, EventArgs e)
        {
            Employee em = new Employee();
            em.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbltimer.Text = DateTime.Now.ToString("hh:mm:ss tt");
            datelbl.Text = DateTime.Now.ToString("ddddddd, MMMMMMMMM, yyyy");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            konekcija.Open();
            OleDbCommand komanda = konekcija.CreateCommand();
            komanda.CommandType = CommandType.Text;
            komanda.CommandText = ("Select * From employees");

            OleDbDataReader reader = null;
            reader = komanda.ExecuteReader();
            while (reader.Read())
            {
                label1.Text = Convert.ToString(reader["first_name"].ToString());
                label2.Text = Convert.ToString(reader["last_name"].ToString());
               
            }
            reader.Close();
            konekcija.Close();
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void customerbtn_Click(object sender, EventArgs e)
        {
            Customer cs = new Customer();
            cs.Show();
            this.Close();
        }

        private void carbtn_Click(object sender, EventArgs e)
        {
            Car cr = new Car();
            cr.Show();
            this.Hide();
        }

        private void rentalbtn_Click(object sender, EventArgs e)
        {
            Rental rent = new Rental();
            rent.Show();
            this.Hide();
        }

        private void dashboardbtn_Click(object sender, EventArgs e)
        {
            Dashboard ds = new Dashboard();
            ds.Show();
            this.Close();
        }

        

       

        

       

        

       

       

       

  
        

    }
}
