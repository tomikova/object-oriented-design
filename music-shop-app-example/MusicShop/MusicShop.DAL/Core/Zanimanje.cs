using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace MusicShop.DAL
{
    public class Zanimanje
    {

        #region Constructor
        public Zanimanje()
        {
            Djelatnik = new List<Djelatnik>();
        }
        #endregion

        public virtual int Id { get; set; }
        public virtual string Naziv { get; set; }
        public virtual string Opis { get; set; }
        public virtual IList<Djelatnik> Djelatnik { get; set; }
    }
}
