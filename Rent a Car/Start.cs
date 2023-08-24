using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rent_a_Car
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        int startpoint = 0; 

        private void Start_Load(object sender, EventArgs e)
        {
            timer1.Start(); // na samom početku korišten je timer kako bi Progress Bar mogao raditi
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint = startpoint + 3; // sa ovim djelom koda upravljamo brzinom Progress Bara
            ProgressBar1.Value = startpoint; // Dodjeljujemo vrijednost Progress Bara vrijednosti varijable startpoint, koja je 0. 

            if (ProgressBar1.Value == 100)  // kada vrijednost Progress Bara dostigne 100%, prestaje sa radom.
            {
                ProgressBar1.Value = 0;
                timer1.Stop();

                Login log = new Login(); // nakon loadanja Progress Bara, ovim djelom koda ćemo prikazati sljedeću Login Formu i izaći iz trenutne.
                this.Hide();
                log.Show();
            }
        }

        private void exitbtn1_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Klikom na dugme Exit izlazimo iz aplikacije.
        }
    }
}
