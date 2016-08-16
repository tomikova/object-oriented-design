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
using NHibernate.Criterion;
using NHibernate.Transform;


namespace MusicShop.DAL
{
    public class KupacRepository
    {

        protected static ISessionFactory SessionFactory = MusicShop.DAL.NHibernateService.GetSessionFactory();

        public static IList<Kupac> LoadUsersFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Kupac");
                    session2.Flush();
                    return upit.List<Kupac>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Delete(Kupac forDelete)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(forDelete);
                    transaction.Commit();
                }
            }
        }

        public static void Add(Kupac inKupac)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inKupac);
                    transaction.Commit();
                }
            }
        }

        public static void Update(Kupac inKupac)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(inKupac);
                    transaction.Commit();
                }
            }
        }

        public static IList<Kupac> KupacSearch(string what, string value)
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Kupac WHERE "+what+" like '"+value+"%'");
                    session2.Flush();
                    return upit.List<Kupac>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public class DjelatnikRepository
    {
        protected static ISessionFactory SessionFactory = MusicShop.DAL.NHibernateService.GetSessionFactory();

        public static IList<Djelatnik> LoadUsersFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Djelatnik");
                    session2.Flush();
                    return upit.List<Djelatnik>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Delete(Djelatnik forDelete)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(forDelete);
                    transaction.Commit();
                }
            }
        }

        public static void Add(Djelatnik inDjelatnik)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inDjelatnik);
                    transaction.Commit();
                }
            }
        }

        public static void Update(Djelatnik inDjelatnik)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(inDjelatnik);
                    transaction.Commit();
                }
            }
        }

        public static IList<Djelatnik> DjelatnikSearch(string what, string value)
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Djelatnik WHERE " + what + " like '" + value + "%'");
                    session2.Flush();
                    return upit.List<Djelatnik>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

    public class ZanimanjeRepository
    {
        protected static ISessionFactory SessionFactory = MusicShop.DAL.NHibernateService.GetSessionFactory();

        public static IList<Zanimanje> LoadZanimanjaFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Zanimanje");
                    session2.Flush();
                    return upit.List<Zanimanje>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Delete(Zanimanje forDelete)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(forDelete);
                    transaction.Commit();
                }
            }
        }

        public static void Add(Zanimanje inZanimanje)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inZanimanje);
                    transaction.Commit();
                }
            }
        }

        public static void Update(Zanimanje inZanimanje)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(inZanimanje);
                    transaction.Commit();
                }
            }
        }

        public static Zanimanje GetSpecific(int Id)
        {
            using (var session2 = SessionFactory.OpenSession())
            {
                ISQLQuery query = session2.CreateSQLQuery("SELECT * FROM Zanimanje WHERE Id = "+Id.ToString());
                Zanimanje p = query.SetResultTransformer(Transformers.AliasToBean<Zanimanje>()).UniqueResult<Zanimanje>();
                return p;
            }

        }
    
    }

    public class ProizvodRepository
    {
        protected static ISessionFactory SessionFactory = MusicShop.DAL.NHibernateService.GetSessionFactory();

        public static IList<Proizvod> LoadProizvodFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Proizvod");
                    session2.Flush();
                    return upit.List<Proizvod>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Delete(Proizvod forDelete)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(forDelete);
                    transaction.Commit();
                }
            }
        }

        public static void Add(Proizvod inProizvod)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inProizvod);
                    transaction.Commit();
                }
            }
        }

        public static void Update(Proizvod inProizvod)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(inProizvod);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        //public static Proizvod GetSpecific(int Id)
        //{
        //    using (var session2 = SessionFactory.OpenSession())
        //    {
        //        ISQLQuery query = session2.CreateSQLQuery("SELECT * FROM Proizvod WHERE Id = " + Id.ToString());
        //        Proizvod p = query.SetResultTransformer(Transformers.AliasToBean<Proizvod>()).UniqueResult<Proizvod>();
        //        return p;
        //    }

        //}

        public static IList<Proizvod> ProizvodSearch(string what, string value)
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Proizvod WHERE "+what+" like '"+value+"%'");
                    session2.Flush();
                    return upit.List<Proizvod>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static IList<Proizvod> ProizvodByCategory(string id)
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    ICriteria crit = session2.CreateCriteria<Proizvod>()
                        .CreateAlias("Kategorija", "k")
                        .Add(Expression.Eq("k.Id", Convert.ToInt32(id)));
                    IList<Proizvod> results = crit.List<Proizvod>();
                    return results;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }


    public class KategorijaRepository
    {
        protected static ISessionFactory SessionFactory = MusicShop.DAL.NHibernateService.GetSessionFactory();

        public static IList<Kategorija> LoadKategorijaFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Kategorija");
                    session2.Flush();
                    return upit.List<Kategorija>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Delete(Kategorija forDelete)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(forDelete);
                    transaction.Commit();
                }
            }
        }

        public static void Add(Kategorija inKategorija)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inKategorija);
                    transaction.Commit();
                }
            }
        }

        public static void Update(Kategorija inKategorija)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(inKategorija);
                    transaction.Commit();
                }
            }
        }

        public static Kategorija GetSpecific(int Id)
        {
            using (var session2 = SessionFactory.OpenSession())
            {
                ISQLQuery query = session2.CreateSQLQuery("SELECT * FROM Kategorija WHERE Id = " + Id.ToString());
                Kategorija k = query.SetResultTransformer(Transformers.AliasToBean<Kategorija>()).UniqueResult<Kategorija>();
                return k;
            }

        }

    }

    public class PlacanjeRepository
    {
        protected static ISessionFactory SessionFactory = MusicShop.DAL.NHibernateService.GetSessionFactory();

        public static IList<VrstaPlacanja> LoadPlacanjeFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM VrstaPlacanja");
                    session2.Flush();
                    return upit.List<VrstaPlacanja>();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Add(VrstaPlacanja inVrsta)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inVrsta);
                    transaction.Commit();
                }
            }
        }

    }



    public class RacunRepository
    {
        protected static ISessionFactory SessionFactory = MusicShop.DAL.NHibernateService.GetSessionFactory();

        public static IList<Racun> LoadRacunFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Racun");
                    session2.Flush();
                    return upit.List<Racun>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static IList<Racun> GetSpecific(DateTime date)
        {
            using (var session2 = SessionFactory.OpenSession())
            {       
                return session2.CreateCriteria<Racun>()
               .Add(Restrictions.Le("Datum",date))
               .AddOrder(Order.Asc("Datum"))
               .List<Racun>();

            }

        }

        public static void Delete(Racun forDelete)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(forDelete);
                    transaction.Commit();
                }
            }
        }

        public static void Add(Racun inRacun)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inRacun);
                    transaction.Commit();
                }
            }
        }

        public static IList<Racun> RacuniSearch(string id, string type)
        {
            try
            {
                using (var session2 = SessionFactory.OpenSession())
                {
                    ICriteria crit = session2.CreateCriteria<Racun>()
                        .CreateAlias(type, "k")
                        .Add(Expression.Eq("k.Id", Convert.ToInt32(id)));
                    IList<Racun> results = crit.List<Racun>();
                    return results;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }


    public class StavkaRepository
    {
        protected static ISessionFactory SessionFactory = MusicShop.DAL.NHibernateService.GetSessionFactory();

        public static void Add(object inStavka)
        {
            using (ISession session = SessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inStavka);
                    transaction.Commit();
                }
            }
        }

        public static IList<StavkaKupovina> LoadKStavkeFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM StavkaKupovina");
                    session2.Flush();
                    return upit.List<StavkaKupovina>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static IList<StavkaPosudba> LoadPStavkeFromDatabase()
        {
            try
            {

                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM StavkaPosudba");
                    session2.Flush();
                    return upit.List<StavkaPosudba>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static IList<Stavka> StavkaSearch(string id)
        {
            try
            {
                using (var session2 = SessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Stavka WHERE Racun = " + id);
                    session2.Flush();
                    return upit.List<Stavka>();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }

}
