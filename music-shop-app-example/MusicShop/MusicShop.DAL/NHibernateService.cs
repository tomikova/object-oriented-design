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

namespace MusicShop.DAL
{
    public class NHibernateService
    {
        protected static Configuration NHConfiguration;
        protected static ISessionFactory SessionFactory;

        public static void Setup()
        {
            NHConfiguration = ConfigureNHibernate();
            HbmMapping mapping = GetMappings();
            NHConfiguration.AddDeserializedMapping(mapping, "MusicShopDB");
            SchemaMetadataUpdater.QuoteTableAndColumns(NHConfiguration);
            SessionFactory =  NHConfiguration.BuildSessionFactory();
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

                string ConString = @"Data Source=.\SQLExpress;Integrated Security=True;User Instance=True;AttachDBFilename=|DataDirectory|MusicShopDB.mdf";
                //db.ConnectionString = "Data Source=TOMISLAV-PC\\SQLEXPRESS;Initial Catalog=MusicShopDB;Integrated Security=True";
                db.ConnectionString = ConString;
                db.Timeout = 10;

                db.LogSqlInConsole = true;
                db.AutoCommentSql = true;
            });

            return configure;
        }

        protected static HbmMapping GetMappings()
        {
            ModelMapper mapper = new ModelMapper();

            mapper.AddMapping<DjelatnikOsobaMap>();
            mapper.AddMapping<KategorijaMap>();
            mapper.AddMapping<KupacOsobaMap>();
            mapper.AddMapping<OsobaMap>();
            mapper.AddMapping<ProizvodMap>();
            mapper.AddMapping<RacunMap>();
            mapper.AddMapping<StavkaMap>();
            mapper.AddMapping<StavkaKupovinaMap>();
            mapper.AddMapping<StavkaPosudbaMap>();
            mapper.AddMapping<StavkaServisMap>();
            mapper.AddMapping<VrstaPlacanjaMap>();
            mapper.AddMapping<ZanimanjeMap>();

            HbmMapping mapping = mapper.CompileMappingFor(new[] { typeof(Djelatnik), typeof(Kategorija), typeof(Kupac), typeof(Osoba), typeof(Proizvod),
                                  typeof(Racun), typeof(Stavka), typeof(StavkaKupovina), typeof(StavkaPosudba), typeof(StavkaServis), typeof(VrstaPlacanja), typeof(Zanimanje) });

            return mapping;
        }

        public static void CreateDatabaseSchema()
        {
            new SchemaExport(NHConfiguration).Drop(false, true);
            new SchemaExport(NHConfiguration).Create(false, true);
        }

        public static bool ValidateSchema()
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

        public static ISessionFactory GetSessionFactory()
        {
            return SessionFactory;
        }
    }

}
