using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

namespace NHSchemaTest
{
    public class Program
    {
        protected static Configuration NHConfiguration;
        protected static ISessionFactory SessionFactory;
        protected static List<Parent> Parents;
        protected static List<Child> Children;

        static void Main(string[] args)
        {
            Setup();
            CreateDatabaseSchema();
            if (ValidateSchema())
            {
                InsertData();

                using (ISession session = SessionFactory.OpenSession())
                using (ITransaction transaction = session.BeginTransaction())
                {
                    Parent parent = session.Get<Parent>(1);

                    foreach (Child child in parent.child)
                    {
                        Console.WriteLine(parent.Name + " is the parent of " + child.Name);
                        Console.WriteLine(child.Name + " is the child of " + child.parent.Name);
                    }
                }
            }
            else
            {
                Console.WriteLine("Schema validation failed.");
            }

            Console.ReadLine();
        }

        public static void Setup()
        {
            NHConfiguration = ConfigureNHibernate();
            HbmMapping mapping = GetMappings();
            NHConfiguration.AddDeserializedMapping(mapping, "NHSchemaTest");
            SchemaMetadataUpdater.QuoteTableAndColumns(NHConfiguration);
            SessionFactory = NHConfiguration.BuildSessionFactory();
        }

        private static Configuration ConfigureNHibernate()
        {
            var configure = new Configuration();
            configure.SessionFactoryName("BuildIt");

            configure.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2008Dialect>();
                db.Driver<SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.IsolationLevel = IsolationLevel.ReadCommitted;
                db.ConnectionString = "Data Source=PERKINS-W7;Initial Catalog=NHSchemaTest;Integrated Security=True";
                db.Timeout = 10;

                // enabled for testing
                db.LogFormatedSql = true;
                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;
            });

            return configure;
        }

        protected static HbmMapping GetMappings()
        {
            //There is a dynamic way to do this, but for simplicity I chose to hard code
            ModelMapper mapper = new ModelMapper();

            mapper.AddMapping<ChildMap>();
            mapper.AddMapping<ParentMap>();

            HbmMapping mapping = mapper.CompileMappingFor(new[] { typeof(Parent), typeof(Child) });

            return mapping;
        }

        protected static void CreateDatabaseSchema()
        {
            //This will drop your database, be careful here not to run in production, 
            //unless you want to drop your database
            new SchemaExport(NHConfiguration).Drop(false, true);
            new SchemaExport(NHConfiguration).Create(false, true);
        }

        protected static bool ValidateSchema()
        {
            try
            {
                SchemaValidator schemaValidator = new SchemaValidator(NHConfiguration);
                schemaValidator.Validate();
                return true;
            }
            catch (HibernateException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        protected static void InsertData()
        {
            //using session per database transaction here, no caching happens as 1st level cache
            //has scope of the session
            using (ISession session = SessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                //
                Child child1 = new Child { Id = 1, Name = "Child1", DOB = DateTime.Now.AddYears(10) };
                Child child2 = new Child { Id = 2, Name = "Child2", DOB = DateTime.Now.AddYears(11) };
                Child child3 = new Child { Id = 3, Name = "Child3", DOB = DateTime.Now.AddYears(12) };
                Child child4 = new Child { Id = 4, Name = "Child4", DOB = DateTime.Now.AddYears(13) };

                Parent parent1 = new Parent { Id = 1, Name = "Parent1", DOB = DateTime.Now.AddYears(-13) };
                Parent parent2 = new Parent { Id = 2, Name = "Parent2", DOB = DateTime.Now.AddYears(-12) };
                Parent parent3 = new Parent { Id = 3, Name = "Parent3", DOB = DateTime.Now.AddYears(-11) };
                Parent parent4 = new Parent { Id = 4, Name = "Parent4", DOB = DateTime.Now.AddYears(-10) };

                parent1.child.Add(child1);
                parent1.child.Add(child2);

                parent3.child.Add(child3);
                parent3.child.Add(child4);

                Parents = new List<Parent> { parent1, parent2, parent3, parent4 };
                Children = new List<Child> { child1, child2, child3, child4 };

                foreach (Parent parent in Parents)
                {
                    session.SaveOrUpdate(parent);
                }

                foreach (Child child in Children)
                {
                    session.SaveOrUpdate(child);
                }

                transaction.Commit();
            }
        }
    }
}
