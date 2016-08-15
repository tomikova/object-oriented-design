using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvenTime.BaseLib
{
    public abstract class Subject
    {
        List<IEvenTimeObserver> _listObservers = new List<IEvenTimeObserver>();

        public void Attach(IEvenTimeObserver obs)
        {
            _listObservers.Add(obs);
        }
        public void Delete(IEvenTimeObserver obs)
        {
            _listObservers.Remove(obs);
        }
        public void NotifyObservers(Object inObj)
        {
            foreach (IEvenTimeObserver obs in _listObservers)
                obs.update(inObj);
        }
    }
}
