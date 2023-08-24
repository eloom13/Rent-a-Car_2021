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
    public partial class Dashboard : Form
    {
        private OleDbConnection konekcija = new OleDbConnection();

        public Dashboard()
        {
            InitializeComponent();
            konekcija.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:/Users/Elmir/Desktop/Nitro.mdb; Persist Security Info = False";
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm();
            mf.Show();
            this.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(Class1.employee);
            label2.Text = Convert.ToString(Class1.customer);
            label3.Text = Convert.ToString(Class1.car);
        }

        private void exitbtn3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       

        
    }
}
