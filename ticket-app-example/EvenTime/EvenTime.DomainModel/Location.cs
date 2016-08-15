using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.DomainModel
{
    public class Location
    {
        private int _id;

        private string _locationName;

        private string _locationAddress;

        private string _locationDescription;

        public Location()
        {

        }

        public Location(string inName, string inAddress, string inDescription)
        {
            Name = inName;
            Address = inAddress;
            Description = inDescription;
        }

        public virtual int Id { get; protected set; }

        public virtual string Name
        {
            get { return _locationName; }
            set { _locationName = value; }
        }

        public virtual string Address
        {
            get { return _locationAddress; }
            set { _locationAddress = value; }
        }

        public virtual string Description
        {
            get { return _locationDescription; }
            set { _locationDescription = value; }
        }
    }
}
