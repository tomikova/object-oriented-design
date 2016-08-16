using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace MusicShop.DAL
{
    public class Stavka
    {

        #region Constructor
        public Stavka()
        {
        }
        #endregion

        public virtual int Id { get; set; }
        public virtual int Kolicina { get; set; }
        public virtual float Cijena { get; set; }
        public virtual DateTime Datum { get; set; }
        public virtual Racun Racun { get; set; }
      
    }
}
