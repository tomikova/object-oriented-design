using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.DomainModel
{
    public class Event
    {
        private int _id;

        private DateTime _eventTime;

        private string _eventName;

        private string _eventDescription;

        private float _eventPrice;

        private int _eventQuantity;

        private Category _eventCategory;

        private Location _eventLocation;

        public Event()
        {

        }

        public Event(string inName, string inDesc, float inPrice, int inQuantity, DateTime inTime, Category inCategory, Location inLocation)
        {
            Name = inName;
            Description = inDesc;
            Price = inPrice;
            Quantity = inQuantity;
            Category = inCategory;
            Location = inLocation;

            _eventTime = inTime;
        }

        public virtual int Id { get; protected set; }

        public virtual string Name
        {
            get { return _eventName; }
            set { _eventName = value; }
        }

        public virtual string Description
        {
            get { return _eventDescription; }
            set { _eventDescription = value; }
        }

        public virtual float Price
        {
            get { return _eventPrice; }
            set { _eventPrice = value; }
        }

        public virtual int Quantity
        {
            get { return _eventQuantity; }
            set { _eventQuantity = value; }
        }

        public virtual Category Category
        {
            get { return _eventCategory; }
            set { _eventCategory = value; }
        }

        public virtual Location Location
        {
            get { return _eventLocation; }
            set { _eventLocation = value; }
        }

        public virtual DateTime Date
        {
            get { return _eventTime; }
            set { _eventTime = value; }
        }
    }
}
