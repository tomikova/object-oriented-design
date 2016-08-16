using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace MusicShop.DAL
{
    public class Kupac : Osoba
    {
        #region Constructor
        public Kupac()
        {
            Racun = new List<Racun>();
        }
        #endregion

        public virtual IList<Racun> Racun { get; set; }
    }
}
