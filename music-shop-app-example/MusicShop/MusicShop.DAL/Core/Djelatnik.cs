using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;


namespace MusicShop.DAL
{
    public  class Djelatnik : Osoba
    {

        #region Constructor
        public Djelatnik()
        {
            Racun = new List<Racun>();
        }
        #endregion

        public virtual Zanimanje Zanimanje { get; set; }
        public virtual IList<Racun> Racun { get; set; }
    }
}
