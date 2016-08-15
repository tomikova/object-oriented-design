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
    public partial class ReservationForm : Form, IEvenTimeObserver, IReservationForm
    {
        private ReservationController _refResController = ReservationController.getInstance();

        public ReservationForm()
        {
            InitializeComponent();

            _refResController.CheckReservations();
            _refResController.SetReservationFormParameters(this, ref dataGridViewReservations);
        }

        public void update(Object inObj)
        {
            List<Reservation> inList = inObj as List<Reservation>;

            _refResController.SetReservationList(inList, ref dataGridViewReservations);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _refResController.ButtonResOKClick(this, textBoxKod.Text);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _refResController.ButtonResCancelClick(this, textBoxKod.Text);
        }

        private void ReservationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _refResController.Delete(this);
        }
    }
}
