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
    public class EventRepository
    {
        private static EventRepository _instance = null;

        private IList<Event> _listEvents;

        private static ISessionFactory sessionFactory = EvenTime.DomainModel.NHibernateService.CreateSessionFactory();

        public static EventRepository getInstance()
        {
            if (_instance == null)
                _instance = new EventRepository();

            return _instance;
        }

        private void LoadEventsFromDatabase()
        {
            try
            {
                using (var session2 = sessionFactory.OpenSession())
                {
                    IQuery upit = session2.CreateQuery("FROM Event");
                    session2.Flush();
                    _listEvents = upit.List<Event>();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }        
          }

        public int Count()
        {
            LoadEventsFromDatabase();

            return _listEvents.Count;
        }

        public void addEvent(Event inEvent)
        {
            LoadEventsFromDatabase();

            foreach (Event ev in _listEvents)
                if (ev.Name == inEvent.Name)
                    throw new EventAlreadyExists();

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(inEvent);
                    transaction.Commit();
                }
            }

            LoadEventsFromDatabase();
        }

        public Event getEventByName(string inEventName)
        {
            LoadEventsFromDatabase();

            foreach (Event ev in _listEvents)
                if (ev.Name == inEventName)
                    return ev;

            throw new EventDoesntExist();
        }

        public Event getEventByIndex(int Index)
        {
            LoadEventsFromDatabase();

            if (0 <= Index && Index < Count())
                return _listEvents[Index];
            else
                throw new EventDoesntExist();
        }

        public void RemoveTickets(int inAmount, string inEventName)
        {
            LoadEventsFromDatabase();

            Event ev = getEventByName(inEventName);
            ev.Quantity -= inAmount;

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(ev);
                    transaction.Commit();
                }
            }

            LoadEventsFromDatabase();
        }

        public void AddTickets(int inAmount, string inEventName)
        {
            LoadEventsFromDatabase();

            Event ev = getEventByName(inEventName);
            ev.Quantity += inAmount;

            using (ISession session = sessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(ev);
                    transaction.Commit();
                }
            }

            LoadEventsFromDatabase();
        }

        public bool eventExists(string inEventName)
        {
            LoadEventsFromDatabase();

            return getEventByName(inEventName) != null;
        }
    }
}
