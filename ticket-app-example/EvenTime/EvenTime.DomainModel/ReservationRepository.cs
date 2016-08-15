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
    public class ReservationRepository
    {
        private static ReservationRepository _instance = null;

        private IList<Reservation> _listReservations;

        private static ISessionFactory sessionFactory = EvenTime.DomainModel.NHibernateService.CreateSessionFactory();
        
        public static ReservationRepository getInstance()
        {
            if (_instance == null)
                _instance = new ReservationRepository();

            return _instance;
        }

        private void LoadReservationsFromDatabase()
        {
            try
            {
                using (var session2 = sessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Reservation");
                    session2.Flush();
                    _listReservations = upit.List<Reservation>();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            } 
            
        }

        public int Count()
        {
            LoadReservationsFromDatabase();

            return _listReservations.Count;
        }

        public void addReservation(Reservation inReservation)
        {
            LoadReservationsFromDatabase();

            foreach (Reservation res in _listReservations)
                if (res.Code == inReservation.Code)
                    throw new ReservationAlreadyExists();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inReservation);
                    transaction.Commit();
                }
            }

            LoadReservationsFromDatabase();
        }

        public void removeReservation(Reservation inReservation)
        {
            LoadReservationsFromDatabase();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(inReservation);
                    transaction.Commit();
                }
            }

            LoadReservationsFromDatabase();
        }

        public Reservation getReservationByCode(string inReservationCode)
        {
            LoadReservationsFromDatabase();

            foreach (Reservation res in _listReservations)
                if (res.Code == inReservationCode)
                    return res;

            throw new ReservationDoesntExist();
        }

        public Reservation getReservationByIndex(int Index)
        {
            LoadReservationsFromDatabase();

            if (0 <= Index && Index < Count())
                return _listReservations[Index];
            else
                throw new ReservationDoesntExist();
        }

        public bool reservationExists(string inReservationCode)
        {
            LoadReservationsFromDatabase();

            return getReservationByCode(inReservationCode) != null;
        }

        public float getReservationValue(string inReservationCode)
        {
            LoadReservationsFromDatabase();

            Reservation res = getReservationByCode(inReservationCode);
            return res.TicketAmount * res.ResEvent.Price;
        }
    }
}

