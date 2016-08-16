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
    public class ZanimanjeMap : ClassMapping<Zanimanje>
    {
        public ZanimanjeMap()
        {
            Id<int>(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Naziv);
            Property(x => x.Opis);
            Bag<Djelatnik>(x => x.Djelatnik, cp => { }, cr => cr.OneToMany(x => x.Class(typeof(Djelatnik))));
        }
    }
}
