using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvenTime.DomainModel;
using FluentNHibernate.Mapping;
namespace EvenTime.DomainModel.Mappings
{
    public class LocationMap : ClassMap<Location>
    {
        public LocationMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Address);
        }
    }
}
