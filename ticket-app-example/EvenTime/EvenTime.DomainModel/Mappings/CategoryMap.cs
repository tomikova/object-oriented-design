using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EvenTime.DomainModel;
using FluentNHibernate.Mapping;

namespace EvenTime.DomainModel.Mappings
{
    class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id);
            Map(x => x.CategoryName);
        }
    }
}
