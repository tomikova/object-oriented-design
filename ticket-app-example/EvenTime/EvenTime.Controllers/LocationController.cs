using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvenTime.BaseLib;
using EvenTime.DomainModel;

namespace EvenTime.Controllers
{
    public class LocationController
    {

        private static LocationController _instance = null;

        public static LocationController getInstance()
        {
            if (_instance == null)
                _instance = new LocationController();

            return _instance;
        }

        public void AddNewLocation(string inName, string inAddress, string inDescription)
        {
                LocationRepository.getInstance().addLocation(new Location(inName, inAddress, inDescription));

                List<Location> list = GetLocationList();
        }

        public List<Location> GetLocationList()
        {
            List<Location> retList = new List<Location>();

            for (int i = 0; i < LocationRepository.getInstance().Count(); i++)
            {
                Location refLoc = LocationRepository.getInstance().getLocationByIndex(i);
                retList.Add(refLoc);
            }

            return retList;
        }

        public void ButtonOKClick(ILocationForm LocFormView, string name, string address, string desc)
        {
            try
            {
                if (name == "")
                {
                    MessageBox.Show("Upišite naziv!");
                }
                else if (address == "")
                {
                    MessageBox.Show("Upišite adresu!");
                }
                else
                {
                    AddNewLocation(name, address, desc);
                    LocFormView.CloseLocationForm();
                }
            }
            catch (LocationAlreadyExists ex)
            {
                MessageBox.Show("Lokacija s ovim nazivom već postoji!");
            }
        }

        public void SetEventFormParameters(IEventForm EventFormView)
        {
            EventFormView.SetLocations(GetLocationList());
        }
    }
}
