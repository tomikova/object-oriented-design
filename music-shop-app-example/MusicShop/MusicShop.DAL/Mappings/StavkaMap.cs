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
    public class StavkaMap : ClassMapping<Stavka>
    {
        public StavkaMap()
        {
            Id<int>(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Kolicina);
            Property(x => x.Cijena);
            Property(x => x.Datum);
            ManyToOne<Racun>(x => x.Racun);
        }
    }
}
