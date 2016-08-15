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
    public partial class AdminForm : Form, IAdminForm
    {
        private Controllers.MainController _refMainController = MainController.getInstance();

        public AdminForm()
        {
            InitializeComponent();
        }

        public void OpenLocationForm()
        {
            LocationForm newLocFrm = new LocationForm();
            newLocFrm.Show();
        }

        public void OpenEventForm()
        {
            EventForm newEventFrm = new EventForm();
            newEventFrm.Show();
        }

        public void OpenCategoryForm()
        {
            CategoryForm newCatfrm = new CategoryForm();
            newCatfrm.Show();
        }

        private void btnLokacija_Click(object sender, EventArgs e)
        {
            _refMainController.ButtonLokacijaClick(this);
        }


        private void btnDogadjaj_Click(object sender, EventArgs e)
        {
            _refMainController.ButtonEventClick(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _refMainController.ButtonCategoryClick(this);
        }
    }
}
