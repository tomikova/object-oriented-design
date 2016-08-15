using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.DomainModel
{
    [Serializable]
    public class EvenTimeBaseException : Exception
    {
    }

    [Serializable]
    public class BillAlreadyExists : EvenTimeBaseException
    {

    }

    [Serializable]
    public class BillDoesntExist : EvenTimeBaseException
    {

    }

    [Serializable]
    public class CategoryAlreadyExists : EvenTimeBaseException
    {

    }

    [Serializable]
    public class CategoryDoesntExist : EvenTimeBaseException
    {

    }

    [Serializable]
    public class EventAlreadyExists : EvenTimeBaseException
    {

    }

    [Serializable]
    public class EventDoesntExist : EvenTimeBaseException
    {

    }

    [Serializable]
    public class LocationAlreadyExists : EvenTimeBaseException
    {

    }

    [Serializable]
    public class LocationDoesntExist : EvenTimeBaseException
    {

    }

    [Serializable]
    public class PaidReservationAlreadyExists : EvenTimeBaseException
    {

    }

    [Serializable]
    public class PaidReservationDoesntExist : EvenTimeBaseException
    {

    }

    [Serializable]
    public class ReservationAlreadyExists : EvenTimeBaseException
    {

    }

    [Serializable]
    public class ReservationDoesntExist : EvenTimeBaseException
    {

    }
}
