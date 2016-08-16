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
    public partial class IzvjesceForm : Form
    {

        IList<Racun> _racuni;
        IList<StavkaKupovinaInfo> _kupovina;
        IList<StavkaPosudbaInfo> _posudba;
        float zarada = 0;


        public IzvjesceForm()
        {
            InitializeComponent();
            this._racuni = null;
            this._kupovina = null;
            this._posudba = null;
        }

        private void IzvjesceForm_Load(object sender, EventArgs e)
        {
            RefreshBills(DateTime.Today);
        }

        private void RefreshBills(DateTime time)
        {
            zarada = 0;
            _racuni = RacunRepository.GetSpecific(time).ToList();
            foreach (Racun r in _racuni)
            {
                zarada += r.Cijena;
            }
            if (_racuni.Count == 0)
            {
                _racuni = null;
            }
            racuniBindingSource.DataSource = _racuni;
            label2.Text = zarada.ToString();
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            if (_racuni != null)
            {
                string id = ((Racun)racuniBindingSource.Current).Id.ToString();
                IList<Stavka> lista = StavkaRepository.StavkaSearch(id);
                List<StavkaKupovinaInfo> list1 = new List<StavkaKupovinaInfo>();
                List<StavkaPosudbaInfo> list2 = new List<StavkaPosudbaInfo>();
                foreach (Stavka s in lista)
                {
                    if (s is StavkaKupovina)
                    {
                        StavkaKupovina stavka = (StavkaKupovina)s;
                        IList<Proizvod> plista = ProizvodRepository.ProizvodSearch("Id", ((StavkaKupovina)s).Proizvod.Id.ToString());
                        StavkaKupovinaInfo info = new StavkaKupovinaInfo();
                        info.Cijena = stavka.Cijena;
                        info.Datum = stavka.Datum;
                        info.Kolicina = s.Kolicina;
                        info.Popust = 0;
                        info.Proizvod = plista[0].Naziv;
                        list1.Add(info);
                    }
                    if (s is StavkaPosudba)
                    {
                        StavkaPosudba stavka = (StavkaPosudba)s;
                        IList<Proizvod> plista = ProizvodRepository.ProizvodSearch("Id", ((StavkaPosudba)s).Proizvod.Id.ToString());
                        StavkaPosudbaInfo info = new StavkaPosudbaInfo();
                        info.Cijena = stavka.Cijena;
                        info.Datum = stavka.Datum;
                        info.DatumVracanja = stavka.DatumVracanja;
                        info.Kolicina = stavka.Kolicina;
                        info.Proizvod = plista[0].Naziv;
                        list2.Add(info);
                    }
                }
                _kupovina = list1;
                _posudba = list2;
                kupovinaBindingSource.DataSource = _kupovina;
                posudbaBindingSource.DataSource = _posudba;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshBills(dateTimePicker1.Value);
            _kupovina = null;
            _posudba = null;
            kupovinaBindingSource.DataSource = _kupovina;
            posudbaBindingSource.DataSource = _posudba;
        }
    }

}
