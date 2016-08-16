using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace MusicShop.DAL
{
    public class Racun
    {

        #region Constructor
        public Racun()
        {
            Stavke = new List<Stavka>();
        }
        #endregion

        public virtual int Id { get; set; }
        public virtual float Cijena { get; set; }
        public virtual string Opis { get; set; }
        public virtual DateTime Datum { get; set; }
        public virtual Djelatnik Djelatnik { get; set; }
        public virtual Kupac Kupac { get; set; }
        public virtual VrstaPlacanja Placanje { get; set; }
        public virtual IList<Stavka> Stavke { get; set; }
    }
}
