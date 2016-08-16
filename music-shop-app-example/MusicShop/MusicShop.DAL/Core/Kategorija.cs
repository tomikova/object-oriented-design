using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;


namespace MusicShop.DAL
{
    public class Kategorija
    {

        #region Constructor
        public Kategorija()
        {
            Proizvod = new List<Proizvod>();
        }
        #endregion

        public virtual int Id { get; set; }
        public virtual string Naziv { get; set; }
        public virtual IList<Proizvod> Proizvod { get; set; }
    }
}
