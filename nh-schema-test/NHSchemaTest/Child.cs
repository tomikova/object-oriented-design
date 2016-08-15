using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NHSchemaTest
{
    public class Child
    {
        public Child() { }

        #region Properties

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DOB { get; set; }

        public virtual Parent parent { get; set; }

        #endregion
    }

    public class ChildMap : ClassMapping<Child>
    {
        public ChildMap()
        {
            Id<int>(x => x.Id);
            Property<string>(x => x.Name);
            Property<DateTime?>(x => x.DOB);
            ManyToOne<Parent>(x => x.parent);
        }
    }
}
