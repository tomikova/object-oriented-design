using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicShop.DAL;

namespace MusicShop.BLL
{
    public class Kosarica
    {
        private static Kosarica _instance = null;
        private Djelatnik trenutniDjelatnik = null;
        private Kupac trenutniKupac = null;
        private IList<StavkaKupovina> _listaKupovinaStavki;
        private IList<StavkaPosudba> _listaPosudbaStavki;

        public Kosarica()
        {
            this._listaKupovinaStavki = new List<StavkaKupovina>();
            this._listaPosudbaStavki = new List<StavkaPosudba>();
        }

        public static Kosarica getInstance()
        {
            if (_instance == null)
                _instance = new Kosarica();
            return _instance;
        }

        public Djelatnik Djelatnik
        {
            get { return this.trenutniDjelatnik; }
            set { this.trenutniDjelatnik = value; }
        }

        public Kupac Kupac
        {
            get { return this.trenutniKupac; }
            set { this.trenutniKupac = value; }
        }

        public IList<StavkaKupovina> KupovinaStavke
        {
            get { return this._listaKupovinaStavki; }
            set { this._listaKupovinaStavki = value; }
        }

        public IList<StavkaPosudba> PosudbaStavke
        {
            get { return this._listaPosudbaStavki; }
            set { this._listaPosudbaStavki = value; }
        }

        public void DodajStavku(Stavka stavka)
        {
            if (stavka is StavkaKupovina)
                this._listaKupovinaStavki.Add((StavkaKupovina)stavka);
            else if (stavka is StavkaPosudba)
                this._listaPosudbaStavki.Add((StavkaPosudba)stavka);
           
        }

        public void ObrisiStavku(Stavka stavka)
        {
            if (stavka is StavkaKupovina)
                this._listaKupovinaStavki.Remove((StavkaKupovina)stavka);
            else if (stavka is StavkaPosudba)
                this._listaPosudbaStavki.Remove((StavkaPosudba)stavka);
        }
    }
}
