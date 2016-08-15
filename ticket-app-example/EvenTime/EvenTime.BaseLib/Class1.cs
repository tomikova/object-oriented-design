using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.BaseLib
{
    public interface IEvenTimeObserver
    {
        void update(Object param);
    }
}
