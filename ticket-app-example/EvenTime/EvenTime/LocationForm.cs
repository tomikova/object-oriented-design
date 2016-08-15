using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvenTime.BaseLib;
using EvenTime.Controllers;

namespace EvenTime
{
    public partial class LocationForm : Form, ILocationForm
    {

        Controllers.LocationController _refLocController = LocationController.getInstance();

        public LocationForm()
        {
            InitializeComponent();
        }

        public void CloseLocationForm()
        {
            ActiveForm.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _refLocController.ButtonOKClick(this, textBoxNaziv.Text, textBoxAdresa.Text, textBoxOpis.Text);
        }
    }
}
