using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace MusicShop.DAL
{
    public class Proizvod
    {

        #region Constructor
        public Proizvod()
        {
        }
        #endregion

        public virtual int Id { get; set; }
        public virtual string Naziv { get; set; }
        public virtual string Opis { get; set; }
        public virtual float Cijena { get; set; }
        public virtual int Kolicina { get; set; }
        public virtual Kategorija Kategorija { get; set; }
    }
}
