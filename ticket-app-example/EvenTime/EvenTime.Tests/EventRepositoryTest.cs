using System;
using EvenTime.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EvenTime.Tests
{
    [TestClass]
    public class EventRepository_Test
    {
        [TestMethod]
        public void Test_CreatingOneEvent()
        {
            EventRepository evRep = EventRepository.getInstance();

            Category cat = new Category("Sport");

            Location loc = new Location("Poljud", "neka adresa", "neki opis");

            Event ev = new Event("Hajduk-Inter", "utakmica", 200, 10000, DateTime.Now.AddDays(5), cat, loc);

            evRep.addEvent(ev);

            Assert.AreEqual(ev, evRep.getEventByIndex(0));
        }

        [TestInitialize]
        public void ReInitializeEventRepository()
        {
            System.Reflection.FieldInfo fi = typeof(EventRepository).GetField("_instance",
                                                                                System.Reflection.BindingFlags.Static |
                                                                                System.Reflection.BindingFlags.NonPublic);

            Assert.IsNotNull(fi);

            fi.SetValue(null, null);
        }

        [TestMethod]
        public void Test_CreatingTwoEvents()
        {
            EventRepository evRep = EventRepository.getInstance();

            Category cat = new Category("Sport");
            Location loc1 = new Location("Poljud", "neka adresa", "neki opis");
            Location loc2 = new Location("Maksimir", "neka adresa", "neki opis");

            Event ev1 = new Event("Hajduk-Inter", "utakmica", 200, 10000, DateTime.Now.AddDays(5), cat, loc1);
            Event ev2 = new Event("Dinamo- Real Madrid", "utakmica", 500, 10000, DateTime.Now.AddDays(5), cat, loc2);

            evRep.addEvent(ev1);
            evRep.addEvent(ev2);

            Assert.AreEqual(2, evRep.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(EventAlreadyExists))]
        public void Test_AddingSameEventReferenceTwice()
        {
            EventRepository evRep = EventRepository.getInstance();

            Category cat = new Category("Sport");
            Location loc = new Location("Poljud", "neka adresa", "neki opis");
            Event ev = new Event("Hajduk-Inter", "utakmica", 200, 10000, DateTime.Now.AddDays(5), cat, loc);

            evRep.addEvent(ev);
            evRep.addEvent(ev);
        }

        [TestMethod]
        public void Test_getEventByName()
        {
            EventRepository evRep = EventRepository.getInstance();

            string evName = "Hajduk-Inter";

            Category cat = new Category("Sport");
            Location loc = new Location("Poljud", "neka adresa", "neki opis");
            Event ev = new Event(evName, "utakmica", 200, 10000, DateTime.Now.AddDays(5), cat, loc);

            evRep.addEvent(ev);

            Event ev2 = evRep.getEventByName(evName);

            Assert.AreEqual(ev, ev2);
        }

        [TestMethod]
        [ExpectedException(typeof(EventDoesntExist))]
        public void Test_getEventByName_EvDoesntExist()
        {
            EventRepository evRep = EventRepository.getInstance();

            Event ev = evRep.getEventByName("nepostojeći događaj");
        }

        [TestMethod]
        public void Test_addSomeTickets()
        {
            EventRepository evRep = EventRepository.getInstance();

            string evName = "Hajduk-Inter";

            Category cat = new Category("Sport");
            Location loc = new Location("Poljud", "neka adresa", "neki opis");
            Event ev = new Event(evName, "utakmica", 200, 10000, DateTime.Now.AddDays(5), cat, loc);

            evRep.addEvent(ev);

            evRep.AddTickets(1000, evName);

            Event ev2 = evRep.getEventByName(evName);

           Assert.AreEqual(11000,ev2.Quantity);
        }
    }
}
