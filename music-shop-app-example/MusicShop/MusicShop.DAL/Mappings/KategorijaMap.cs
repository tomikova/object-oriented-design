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
    public class KategorijaMap : ClassMapping<Kategorija>
    {
        public KategorijaMap()
        {
            Id<int>(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Naziv);
            Bag<Proizvod>(x => x.Proizvod, cp => { }, cr => cr.OneToMany(x => x.Class(typeof(Proizvod))));
        }
    }
}
