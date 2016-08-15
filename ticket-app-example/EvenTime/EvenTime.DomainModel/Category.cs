using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.DomainModel
{
    public class Category
    {
        private int _id;
        private string _CategoryName;

        public Category()
        {

        }

        public Category(string inName)
        {
            CategoryName = inName;
        }
        public virtual string CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value; }
        }

        public virtual int Id { get; protected set; }

    }
}
