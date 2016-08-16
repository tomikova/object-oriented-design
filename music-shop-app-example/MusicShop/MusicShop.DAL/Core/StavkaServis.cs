using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace MusicShop.DAL
{
    public class StavkaServis : Stavka
    {

        #region Constructor
        public StavkaServis()
        {
        }
        #endregion

        public virtual DateTime DatumZavrsetka { get; set; }
        public virtual string Opis { get; set; }
        public virtual string Napomena { get; set; }
    }
}
