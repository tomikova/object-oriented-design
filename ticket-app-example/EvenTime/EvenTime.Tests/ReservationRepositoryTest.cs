using System;
using EvenTime.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EvenTime.Tests
{
    [TestClass]
    public class ReservationRepository_Test
    {
        [TestMethod]
        public void Test_CreatingReservation()
        {
            ReservationRepository resRep = ReservationRepository.getInstance();

            Category cat = new Category("Sport");
            Location loc = new Location("Poljud", "neka adresa", "neki opis");
            Event ev = new Event("Hajduk-Inter", "utakmica", 200, 10000, DateTime.Now.AddDays(5), cat, loc);

            Reservation res = new Reservation("abcdefgh", 10, DateTime.Now, ev, "Ivo", "Ivić", "0123456789");

            resRep.addReservation(res);

            Assert.AreEqual(res, resRep.getReservationByCode("abcdefgh"));
        }


        [TestMethod]
        public void Test_ReservationValue()
        {
            ReservationRepository resRep = ReservationRepository.getInstance();

            Category cat = new Category("Sport");
            Location loc = new Location("Poljud", "neka adresa", "neki opis");
            Event ev = new Event("Hajduk-Inter", "utakmica", 200, 10000, DateTime.Now.AddDays(5), cat, loc);

            Reservation res = new Reservation("1234567", 10, DateTime.Now, ev, "Ivo", "Ivić", "0123456789");

            resRep.addReservation(res);

            float value = resRep.getReservationValue("1234567");

            Assert.AreEqual(2000, value);
        }
    }
}
