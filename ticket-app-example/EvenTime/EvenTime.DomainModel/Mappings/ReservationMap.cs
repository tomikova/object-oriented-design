using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvenTime.DomainModel;
using FluentNHibernate.Mapping;

namespace EvenTime.DomainModel.Mappings
{
    public class ReservationMap : ClassMap<Reservation>
    {
        public ReservationMap()
        {
            Id(x => x.Id);
            Map(x => x.Code);
            Map(x => x.TicketAmount);
            Map(x => x.ResTime);
            Map(x => x.CustomerName);
            Map(x => x.CustomerSurname);
            Map(x => x.CustomerPhone);
            References(x => x.ResEvent).Not.LazyLoad();
        }
    }
}