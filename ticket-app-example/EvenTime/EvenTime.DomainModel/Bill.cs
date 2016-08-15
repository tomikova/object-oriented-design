using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.DomainModel
{
    public class Bill
    {
        private int _id;
        private string _billCode;
        private int _ticketAmount;
        private DateTime _payTime;
        private Event _paidEvent;

        public Bill()
        {

        }

        public Bill(string inBillCode, int inAmount, DateTime inPayTime, Event inEvent)
        {
            this._billCode = inBillCode;
            this._ticketAmount = inAmount;
            this._payTime = inPayTime;
            this._paidEvent = inEvent;
        }

        public virtual int Id { get; protected set; }

        public virtual string Code
        {
            get { return _billCode; }
            set { _billCode = value; }
        }

        public virtual int TicketAmount
        {
            get { return _ticketAmount; }
            set { _ticketAmount = value; }
        }

        public virtual DateTime PayTime
        {
            get { return _payTime; }
            set { _payTime = value; }
        }

        public virtual Event PaidEvent
        {
            get { return _paidEvent; }
            set { _paidEvent = value; }
        }

        public virtual float Value()
        {
            return _paidEvent.Price * _ticketAmount;
        }
    }
}