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
    public class StavkaKupovinaMap : JoinedSubclassMapping<StavkaKupovina>
    {
        public StavkaKupovinaMap()
        {
            Key(m => m.Column("stavka_id"));
            Property(x => x.Popust);
            ManyToOne<Proizvod>(x => x.Proizvod);
        }
    }
}
