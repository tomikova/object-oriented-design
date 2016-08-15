using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using EvenTime.Controllers;
using EvenTime.DomainModel;
using EvenTime.BaseLib;

namespace EvenTime
{
    public partial class CustomerForm : Form, ICustomerForm
    {
        ReservationController _refResController = ReservationController.getInstance();
        int _kol;
        Event _event;

        public CustomerForm(int inKol, Event inEvent)
        {
            InitializeComponent();
            this._kol = inKol;
            this._event = inEvent;
        }

        public void CloseCustomerForm()
        {
            ActiveForm.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            _refResController.ButtonOKClick(this, textBoxName.Text, textBoxSurname.Text, textBoxPhone.Text, _kol, _event);
        }
    }
}
