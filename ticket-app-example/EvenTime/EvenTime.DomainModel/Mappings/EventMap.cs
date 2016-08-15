using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvenTime.DomainModel;
using FluentNHibernate.Mapping;

namespace EvenTime.DomainModel.Mappings
{
    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Price);
            Map(x => x.Quantity);
            Map(x => x.Date);
            References(x => x.Category).Not.LazyLoad();
            References(x => x.Location).Not.LazyLoad();
        }
    }
}