using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHSchemaTest
{
    public class Parent
    {
        public Parent()
        {
            child = new List<Child>();
        }

        #region Properties

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DOB { get; set; }

        public virtual IList<Child> child { get; set; }

        #endregion
    }

    public class ParentMap : ClassMapping<Parent>
    {
        public ParentMap()
        {
            Id<int>(x => x.Id);
            Property<string>(x => x.Name);
            Property<DateTime?>(x => x.DOB);
            Bag<Child>(x => x.child, cp => { }, cr => cr.OneToMany(x => x.Class(typeof(Child))));
        }
    }
}
