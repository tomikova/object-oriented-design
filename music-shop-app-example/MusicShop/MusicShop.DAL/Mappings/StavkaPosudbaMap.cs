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
    public class StavkaPosudbaMap : JoinedSubclassMapping<StavkaPosudba>
    {
        public StavkaPosudbaMap()
        {
            Key(m => m.Column("stavka_id"));
            Property(x => x.DatumVracanja);
            ManyToOne<Proizvod>(x => x.Proizvod);
        }
    }
}
