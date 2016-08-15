using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.DomainModel
{
    public class PaidReservationRepository
    {
        private static PaidReservationRepository _instance = null;

        List<PaidReservation> _listPaidReservations = new List<PaidReservation>();

        public static PaidReservationRepository getInstance()
        {
            if (_instance == null)
                _instance = new PaidReservationRepository();

            return _instance;
        }

        public int Count()
        {
            return _listPaidReservations.Count;
        }

        public void addPaidReservation(PaidReservation inPaidReservation)
        {
            foreach (PaidReservation res in _listPaidReservations)
                if (res.Code == inPaidReservation.Code)
                    throw new PaidReservationAlreadyExists();

            _listPaidReservations.Add(inPaidReservation);
        }

        public PaidReservation getPaidReservationByCode(string inPaidReservationCode)
        {
            foreach (PaidReservation res in _listPaidReservations)
                if (res.Code == inPaidReservationCode)
                    return res;

            throw new PaidReservationDoesntExist();
        }

        public PaidReservation getPaidReservationByIndex(int Index)
        {
            if (0 <= Index && Index < Count())
                return _listPaidReservations[Index];
            else
                throw new PaidReservationDoesntExist();
        }

        public bool paidReservationExists(string inPaidReservationCode)
        {
            return getPaidReservationByCode(inPaidReservationCode) != null;
        }
    }
}
