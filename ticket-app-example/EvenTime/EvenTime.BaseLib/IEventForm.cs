using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.BaseLib
{
    public interface IEventForm
    {
        void SetDateTimeFormat(string format);

        void SetLocations(Object param);

        void SetCategories(Object param);

        void CloseEventForm();
    }
}
