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
    public partial class MainForm : Form, IMainMenuForm
    {
        private Controllers.EventController _refEventController = EventController.getInstance();
        private Controllers.ReservationController _refResController = ReservationController.getInstance();
        private Controllers.BillController _refBillController = BillController.getInstance();
        private Controllers.LocationController _refLocationController = LocationController.getInstance();
        private Controllers.MainController _refMainController = MainController.getInstance();

        public MainForm()
        {
            InitializeComponent();

            _refMainController.Initialize();
        }

        public void OpenLoginForm()
        {
            LogInForm newFrm = new LogInForm();
            newFrm.Show();
        }

        public void OpenBuyForm()
        {
            BuyForm newBuyfrm = new BuyForm();
            newBuyfrm.Show();
        }

        public void OpenReportForm()
        {
            ReportForm newRepfrm = new ReportForm();
            newRepfrm.Show();
        }

        public void OpenReservationForm()
        {
            ReservationForm newResfrm = new ReservationForm();
            newResfrm.Show();
        }

        private void btnUnos_Click(object sender, EventArgs e)
        {
            _refMainController.ButtonUnosClick(this);
        }
 

        private void btnKupnja_Click(object sender, EventArgs e)
        {
            _refMainController.ButtonKupnjaClick(this);
        }

        private void btnRezervacija_Click(object sender, EventArgs e)
        {
            _refMainController.ButtonRezervacijaClick(this);
        }

        private void btnIzvjesce_Click(object sender, EventArgs e)
        {
            _refMainController.ButtonIzvjesceClick(this);
        }
    }
}
