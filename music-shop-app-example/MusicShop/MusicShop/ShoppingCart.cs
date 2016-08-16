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
    public partial class ShoppingCart : Form
    {

        float total = 0;

        public ShoppingCart()
        {
            InitializeComponent();
        }

        private void ShoppingCart_Load(object sender, EventArgs e)
        {
            Setup();
        }

        private void Setup()
        {
            KupovinaBindingSource.DataSource = Kosarica.getInstance().KupovinaStavke.ToList();
            PosudbaBindingSource.DataSource = Kosarica.getInstance().PosudbaStavke.ToList();
            PlacanjeBindingSource.DataSource = PlacanjeRepository.LoadPlacanjeFromDatabase().ToList();
            Kupac k = Kosarica.getInstance().Kupac;
            Djelatnik d = Kosarica.getInstance().Djelatnik;
            label4.Text = k.Id.ToString();
            label6.Text = k.Ime;
            label7.Text = k.Prezime;
            label5.Text = d.Id.ToString();
            label8.Text = d.Ime;
            label9.Text = d.Prezime;
            IzracunajCijenu();
        }

        private void IzracunajCijenu()
        {
            foreach (StavkaKupovina s in Kosarica.getInstance().KupovinaStavke)
            {
                total += s.Cijena;
            }
            foreach (StavkaPosudba s in Kosarica.getInstance().PosudbaStavke)
            {
                total += s.Cijena;
            }
            label2.Text = total.ToString() + " kn";
        }

        private void Reset()
        {
            Kosarica.getInstance().KupovinaStavke.Clear();
            Kosarica.getInstance().PosudbaStavke.Clear();
            Setup();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Kosarica.getInstance().KupovinaStavke.Count != 0 || Kosarica.getInstance().PosudbaStavke.Count != 0)
            {
                Racun racun = new Racun();
                racun.Cijena = total;
                racun.Datum = DateTime.Now;
                racun.Djelatnik = Kosarica.getInstance().Djelatnik;
                racun.Kupac = Kosarica.getInstance().Kupac;
                foreach (StavkaKupovina s in Kosarica.getInstance().KupovinaStavke)
                {
                    racun.Stavke.Add(s);
                    StavkaRepository.Add(s);
                }
                foreach (StavkaPosudba s in Kosarica.getInstance().PosudbaStavke)
                {
                    racun.Stavke.Add(s);
                    StavkaRepository.Add(s);
                }
                RacunRepository.Add(racun);
                MessageBox.Show("Transakcija uspješno provedena!");
                Reset();
            }
            else
            {
                MessageBox.Show("Košarica je prazna");
            }
        }

        private void button2_Click(object sender, EventArgs e)
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

            Reset();
        }
    }
}
