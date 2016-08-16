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
    public class RacunMap : ClassMapping<Racun>
    {
        public RacunMap()
        {
            Id<int>(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Cijena);
            Property(x => x.Opis);
            Property(x => x.Datum);
            ManyToOne<Djelatnik>(x => x.Djelatnik);
            ManyToOne<Kupac>(x => x.Kupac);
            ManyToOne<VrstaPlacanja>(x => x.Placanje);
            Bag<Stavka>(x => x.Stavke, cp => { }, cr => cr.OneToMany(x => x.Class(typeof(Stavka))));
        }
    }
}
