using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvenTime.Controllers;
using EvenTime.DomainModel;
using EvenTime.BaseLib;

namespace EvenTime
{
    public partial class EventForm : Form, IEventForm
    {

        Controllers.LocationController _refLocController = LocationController.getInstance();
        Controllers.EventController _refEventController = EventController.getInstance();

        public EventForm()
        {
            InitializeComponent();

            _refEventController.SetEventFormParameters(this);
            _refLocController.SetEventFormParameters(this);
        }

        public void SetDateTimeFormat(string format)
        {
            dateTimePicker1.CustomFormat = format;
        }

        public void SetLocations(Object inList)
        {
            foreach (Location loc in inList as List<Location>)
            {
                comboBoxLoc.Items.Add(loc.Name);
            }
        }

        public void SetCategories(Object inList)
        {
            foreach (Category cat in inList as List<Category>)
            {
                comboBoxCategory.Items.Add(cat.CategoryName);
            }
        }

        public void CloseEventForm()
        {
            ActiveForm.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _refEventController.ButtonOKClick(this, textBoxNaziv.Text, textBoxCijena.Text, textBoxOpis.Text, numericUpDown1.Value, dateTimePicker1.Value, comboBoxCategory.Text, comboBoxLoc.Text);
        }


    }
}
