using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace EvenTime.DomainModel
{
    static class NHibernateService
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                   .Database(SQLiteConfiguration.Standard.UsingFile("EvenTime.db"))
                   .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Bill>())
                   .ExposeConfiguration(cfg => new SchemaUpdate(cfg)
                   .Execute(false, true)).BuildSessionFactory();
        }
    }


}
