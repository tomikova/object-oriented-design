using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvenTime.DomainModel;
using FluentNHibernate.Mapping;

namespace EvenTime.DomainModel.Mappings
{
    public class BillMap : ClassMap<Bill>
    {
        public BillMap()
        {
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.TicketAmount);
            Map(x => x.PayTime);
            References(x => x.PaidEvent).Not.LazyLoad();
        }
    }
}
