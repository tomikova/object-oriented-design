using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MusicShop.DAL;
using MusicShop.BLL;

namespace MusicShop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            MusicShop.DAL.NHibernateService.Setup();
            //MusicShop.BLL.NHibernateService.CreateDatabaseSchema();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KorisniciForm korFrm = new KorisniciForm();
            korFrm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ZaposleniciForm zapFrm = new ZaposleniciForm();
            zapFrm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OsobePretragaForm osobePretragaFrm = new OsobePretragaForm();
            osobePretragaFrm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProizvodiForm proizvodiFrm = new ProizvodiForm();
            proizvodiFrm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ProizvodiPretragaForm proizPretragaFrm = new ProizvodiPretragaForm();
            proizPretragaFrm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OsobePretragaForm osobSrFrm = new OsobePretragaForm();
            osobSrFrm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Kosarica.getInstance().Kupac == null)
            {
                MessageBox.Show("Korisnik nije prijavljen");
            }
            else if (Kosarica.getInstance().Djelatnik == null)
            {
                MessageBox.Show("Djelatnik nije prijavljen");
            }
            else
            {
                ShoppingCart skFrm = new ShoppingCart();
                skFrm.ShowDialog();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            foreach (StavkaKupovina k in Kosarica.getInstance().KupovinaStavke)
            {
                Proizvod n = ProizvodRepository.ProizvodSearch("Id", k.Proizvod.Id.ToString())[0];
                n.Kolicina += k.Kolicina;
                ProizvodRepository.Update(n);
            }


            foreach (StavkaPosudba k in Kosarica.getInstance().PosudbaStavke)
            {
                Proizvod n = ProizvodRepository.ProizvodSearch("Id", k.Proizvod.Id.ToString())[0];
                n.Kolicina += k.Kolicina;
                ProizvodRepository.Update(n);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            IzvjesceForm frm = new IzvjesceForm();
            frm.ShowDialog();
        }
    }
}
