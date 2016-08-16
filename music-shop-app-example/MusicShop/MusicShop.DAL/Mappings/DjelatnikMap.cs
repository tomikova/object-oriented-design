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

    public class DjelatnikOsobaMap : JoinedSubclassMapping<Djelatnik>
    {
        public DjelatnikOsobaMap()
        {
            Key(m => m.Column("osoba_id"));
            ManyToOne<Zanimanje>(x => x.Zanimanje);
            Bag<Racun>(x => x.Racun, cp => { }, cr => cr.OneToMany(x => x.Class(typeof(Racun))));
        }
    }
}
