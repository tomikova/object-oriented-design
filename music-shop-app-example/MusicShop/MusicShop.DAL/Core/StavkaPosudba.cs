using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace MusicShop.DAL
{
    public class StavkaPosudba : Stavka
    {

        #region Constructor
        public StavkaPosudba()
        {
        }
        #endregion

        public virtual DateTime DatumVracanja { get; set; }
        public virtual Proizvod Proizvod { get; set; }
    }
}
