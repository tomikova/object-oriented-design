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
    public partial class ProizvodiPretragaForm : Form
    {
        public ProizvodiPretragaForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string what = comboBox1.Text;
            string value = textBox1.Text;
            IList<Proizvod> proizvodi = MusicShop.DAL.ProizvodRepository.ProizvodSearch(what, value);
            if (proizvodi.Count == 0)
            {
                MessageBox.Show("Nema rezultata pretrage!");
            }
            else
            {
                ProizvodiForm proizFrm = new ProizvodiForm(proizvodi);
                ActiveForm.Close();
                proizFrm.Show();
            }
        }
    }
}
