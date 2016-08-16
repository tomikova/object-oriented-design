using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Validation;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MusicShop.DAL
{
    public class VrstaPlacanjaMap : ClassMapping<VrstaPlacanja>
    {
        public VrstaPlacanjaMap()
        {
            Id<int>(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Naziv);
            Property(x => x.Popust);
        }
    }
}
