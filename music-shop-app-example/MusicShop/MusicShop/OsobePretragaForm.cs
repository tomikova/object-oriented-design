using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MusicShop.DAL;

namespace MusicShop
{
    public partial class OsobePretragaForm : Form
    {
        public OsobePretragaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string what = comboBox2.Text;
            string value = textBox1.Text;
            string type = comboBox1.Text;
            if (type == "Kupac")
            {
                IList<Kupac> kupci = MusicShop.DAL.KupacRepository.KupacSearch(what, value);
                if (kupci.Count == 0)
                {
                    MessageBox.Show("Nema rezultata pretrage!");
                }
                else
                {
                    KorisniciForm korisFrm = new KorisniciForm(kupci);
                    ActiveForm.Close();
                    korisFrm.Show();
                }
            }
            else
            {
                IList<Djelatnik> djelatnici = MusicShop.DAL.DjelatnikRepository.DjelatnikSearch(what, value);
                if (djelatnici.Count == 0)
                {
                    MessageBox.Show("Nema rezultata pretrage!");
                }
                else
                {
                    ZaposleniciForm zapFrm = new ZaposleniciForm(djelatnici);
                    ActiveForm.Close();
                    zapFrm.Show();
                }
            }
        }
    }
}
