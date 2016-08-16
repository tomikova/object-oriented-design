using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;

namespace MusicShop.DAL
{
    public class VrstaPlacanja
    {

        #region Constructor
        public VrstaPlacanja()
        {
        }
        #endregion

        public virtual int Id { get; set; }
        public virtual string Naziv { get; set; }
        public virtual float Popust { get; set; }
    }
}
