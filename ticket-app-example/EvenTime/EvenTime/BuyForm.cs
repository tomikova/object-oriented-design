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
    public partial class BuyForm : Form, IEvenTimeObserver, IBuyForm
    {
        private EventController _refEventController = EventController.getInstance();
        private BillController _refBillController = BillController.getInstance();
        private ReservationController _refResController = ReservationController.getInstance();

        public BuyForm()
        {
            InitializeComponent();

            _refEventController.SetBuyFormParameters(this, ref dataGridViewEvents);
        }

        public void update(Object inObj)
        {
            List<Event> inList = inObj as List<Event>;

            _refEventController.SetEventList(inList, ref dataGridViewEvents);
        }

        public void OpenCustomerForm(int inKol, Object ev)
        {
            CustomerForm newCustFrm = new CustomerForm(inKol, ev as Event);
            newCustFrm.Show();
        }

        private void btnKupnja_Click(object sender, EventArgs e)
        {
            _refBillController.ButtonKupnjaClick(this, dataGridViewEvents, numericUpDownKol.Value);
        }

        private void btnRezervacija_Click(object sender, EventArgs e)
        {
            _refResController.ButtonRezervacijaClick(this, ref dataGridViewEvents, numericUpDownKol.Value);
        }

        private void BuyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _refEventController.Delete(this);
        }


    }
}
