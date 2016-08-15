using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.DomainModel
{
    public class PaidReservation : Reservation
    {

        private Bill _resBill;

        public PaidReservation(string inResCode, int inAmount, DateTime inResTime, Event inEvent, string inCusName, string inCusSurname, string inCusPhone)
            : base (inResCode, inAmount, inResTime, inEvent, inCusName, inCusSurname, inCusPhone)
        {
            _resBill = null;
        }

        public PaidReservation(string inResCode, int inAmount, DateTime inResTime, Event inEvent, string inCusName, string inCusSurname, string inCusPhone, Bill inBill)
            : base(inResCode, inAmount, inResTime, inEvent, inCusName, inCusSurname, inCusPhone)
        {
            _resBill = inBill;
        }

        public Bill ResBill
        {
            get { return _resBill; }
        }

    }
}
