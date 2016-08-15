using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using System.Text.RegularExpressions;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace EvenTime.DomainModel
{
    public class LocationRepository
    {
        private static LocationRepository _instance = null;

        private IList<Location> _listLocations;

        private static ISessionFactory sessionFactory = EvenTime.DomainModel.NHibernateService.CreateSessionFactory();
       
        public static LocationRepository getInstance()
        {
            if (_instance == null)
                _instance = new LocationRepository();

            return _instance;
        }

        private void LoadLocationsFromDatabase()
        {
            try
            {
                using (var session2 = sessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Location");
                    session2.Flush();
                    _listLocations = upit.List<Location>();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }        
          }
            
        

        public int Count()
        {
            LoadLocationsFromDatabase();

            return _listLocations.Count;
        }

        public void addLocation(Location inLoc)
        {
            LoadLocationsFromDatabase();

            foreach (Location loc in _listLocations)
                if (loc.Name == inLoc.Name)
                    throw new LocationAlreadyExists();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inLoc);
                    transaction.Commit();
                }
            }
            
            LoadLocationsFromDatabase();
        }

        public Location getLocationByName(string inLocName)
        {
            LoadLocationsFromDatabase();

            foreach (Location loc in _listLocations)
                if (loc.Name == inLocName)
                    return loc;

            throw new LocationDoesntExist();
        }

        public Location getLocationByIndex(int Index)
        {
            LoadLocationsFromDatabase();

            if (0 <= Index && Index < Count())
                return _listLocations[Index];
            else
                throw new LocationDoesntExist();
        }

        public bool locationExists(string inLocName)
        {
            LoadLocationsFromDatabase();

            return getLocationByName(inLocName) != null;
        }
    }
}
