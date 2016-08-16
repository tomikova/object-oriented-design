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
    public class ProizvodMap : ClassMapping<Proizvod>
    {
        public ProizvodMap()
        {
            Id<int>(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Naziv);
            Property(x => x.Opis);
            Property(x => x.Cijena);
            Property(x => x.Kolicina);
            ManyToOne<Kategorija>(x => x.Kategorija);
        }
    }
}
