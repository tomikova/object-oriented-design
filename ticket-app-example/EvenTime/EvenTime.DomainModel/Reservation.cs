using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.DomainModel
{
    public class Reservation
    {
        private int _id;
        private string _resCode;
        private string _custumerName;
        private string _custumerSurname;
        private string _custumerPhone;
        private int _ticketAmount;
        private DateTime _resTime;
        private Event _resEvent;

        public Reservation()
        {

        }

        public Reservation(string inResCode, int inAmount, DateTime inResTime, Event inEvent, string inCusName, string inCusSurname, string inCusPhone)
        {
            this._resCode = inResCode;
            this._ticketAmount = inAmount;
            this._resTime = inResTime;
            this._resEvent = inEvent;

            CustomerName = inCusName;
            CustomerSurname = inCusSurname;
            CustomerPhone = inCusPhone;
        }

        public virtual int Id { get; protected set; }

        public virtual string Code
        {
            get { return _resCode; }
            set { _resCode = value; }
        }

        public virtual int TicketAmount
        {
            get { return _ticketAmount; }
            set { _ticketAmount = value; }
        }

        public virtual DateTime ResTime
        {
            get { return _resTime; }
            set { _resTime = value; }
        }

        public virtual Event ResEvent
        {
            get { return _resEvent; }
            set { _resEvent = value; }
        }

        public virtual string CustomerName
        {
            get { return _custumerName; }
            set { _custumerName = value; }
        }

        public virtual string CustomerSurname
        {
            get { return _custumerSurname; }
            set { _custumerSurname = value; }
        }

        public virtual string CustomerPhone
        {
            get { return _custumerPhone; }
            set { _custumerPhone = value; }
        }

        public virtual float Value()
        {
            return _resEvent.Price * _ticketAmount;
        }
    }
}

