using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;


namespace MusicShop.DAL
{
    public class StavkaKupovina : Stavka
    {

        #region Constructor
        public StavkaKupovina()
        {
        }
        #endregion

        public virtual float Popust { get; set; }
        public virtual Proizvod Proizvod { get; set; }
    }
}
