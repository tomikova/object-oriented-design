using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvenTime.DomainModel;
using EvenTime.BaseLib;

namespace EvenTime.Controllers
{
    public class MainController
    {
        private static MainController _instance = null;

        public static MainController getInstance()
        {
            if (_instance == null)
                _instance = new MainController();

            return _instance;
        }

        public void ButtonUnosClick(IMainMenuForm MainMenuView)
        {
            MainMenuView.OpenLoginForm();
        }

        public void ButtonLoginClick(ILogInForm LogInFormView,string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                LogInFormView.OpenAdminForm();
            }
            else
            {
                MessageBox.Show("Pogrešni login podaci!");
            }
        }

        public void ButtonLokacijaClick(IAdminForm AdminFormView)
        {
            AdminFormView.OpenLocationForm();
        }

        public void ButtonEventClick(IAdminForm AdminFormView)
        {
            AdminFormView.OpenEventForm();
        }

        public void ButtonKupnjaClick(IMainMenuForm MainMenuView)
        {
            MainMenuView.OpenBuyForm();
        }

        public void ButtonRezervacijaClick(IMainMenuForm MainFormView)
        {
            MainFormView.OpenReservationForm();
        }

        public void ButtonIzvjesceClick(IMainMenuForm MainFormView)
        {
            MainFormView.OpenReportForm();
        }

        public void SetReportInitialParam(IReportForm ReportView, ref DataGridView inDataGrid, ref ComboBox inComboBox)
        {

            inComboBox.Items.Add("događaju");
            inComboBox.Items.Add("lokaciji");
            inComboBox.Items.Add("vremenu");
            inComboBox.SelectedItem = inComboBox.Items[0];

            inDataGrid.Rows.Clear();

            List<Bill> bills = BillController.getInstance().GetBillList();
            Dictionary<string, float> eventProfit = new Dictionary<string, float>();
            Dictionary<string, int> soldTickets = new Dictionary<string, int>();
            foreach (Bill bill in bills)
            {
                if (!eventProfit.ContainsKey(bill.PaidEvent.Name))
                {
                    eventProfit.Add(bill.PaidEvent.Name, 0);
                }

                if (!soldTickets.ContainsKey(bill.PaidEvent.Name))
                {
                    soldTickets.Add(bill.PaidEvent.Name, 0);
                }

                eventProfit[bill.PaidEvent.Name] += bill.Value();
                soldTickets[bill.PaidEvent.Name] += bill.TicketAmount;
            }

            int count;

            foreach (KeyValuePair<string, float> entry in eventProfit)
            {
                count = inDataGrid.Rows.Add();
                inDataGrid.Rows[count].Cells[0].Value = entry.Key;
                inDataGrid.Rows[count].Cells[1].Value = entry.Value.ToString() + " kn";
                inDataGrid.Rows[count].Cells[2].Value = soldTickets[entry.Key].ToString();
            }
            inDataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void GenerateReport(IReportForm ReportView, ref DataGridView inDataGrid, string inText)
        {
            inDataGrid.Rows.Clear();

            List<Bill> bills = BillController.getInstance().GetBillList();
            int count;

            if (inText == "događaju")
            {
                Dictionary<string, float> eventProfit = new Dictionary<string, float>();
                Dictionary<string, int> soldTickets = new Dictionary<string, int>();
                foreach (Bill bill in bills)
                {
                    if (!eventProfit.ContainsKey(bill.PaidEvent.Name))
                    {
                        eventProfit.Add(bill.PaidEvent.Name, 0);
                    }

                    if (!soldTickets.ContainsKey(bill.PaidEvent.Name))
                    {
                        soldTickets.Add(bill.PaidEvent.Name, 0);
                    }

                    eventProfit[bill.PaidEvent.Name] += bill.Value();
                    soldTickets[bill.PaidEvent.Name] += bill.TicketAmount;
                }

                foreach (KeyValuePair<string, float> entry in eventProfit)
                {
                    count = inDataGrid.Rows.Add();
                    inDataGrid.Rows[count].Cells[0].Value = entry.Key;
                    inDataGrid.Rows[count].Cells[1].Value = entry.Value.ToString() + " kn";
                    inDataGrid.Rows[count].Cells[2].Value = soldTickets[entry.Key].ToString();
                }
            }
            else if (inText == "lokaciji")
            {
                Dictionary<string, float> locationProfit = new Dictionary<string, float>();
                Dictionary<string, int> soldTickets = new Dictionary<string, int>();
                foreach (Bill bill in bills)
                {
                    if (!locationProfit.ContainsKey(bill.PaidEvent.Location.Name))
                    {
                        locationProfit.Add(bill.PaidEvent.Location.Name, 0);
                    }

                    if (!soldTickets.ContainsKey(bill.PaidEvent.Location.Name))
                    {
                        soldTickets.Add(bill.PaidEvent.Location.Name, 0);
                    }

                    locationProfit[bill.PaidEvent.Location.Name] += bill.Value();
                    soldTickets[bill.PaidEvent.Location.Name] += bill.TicketAmount;
                }

                foreach (KeyValuePair<string, float> entry in locationProfit)
                {
                    count = inDataGrid.Rows.Add();
                    inDataGrid.Rows[count].Cells[0].Value = entry.Key;
                    inDataGrid.Rows[count].Cells[1].Value = entry.Value.ToString() + " kn";
                    inDataGrid.Rows[count].Cells[2].Value = soldTickets[entry.Key].ToString();
                }
            }
            else if (inText == "vremenu")
            {
                Dictionary<string, float> timeProfit = new Dictionary<string, float>();
                Dictionary<string, int> soldTickets = new Dictionary<string, int>();

                timeProfit.Add("0-6", 0);
                timeProfit.Add("6-12", 0);
                timeProfit.Add("12-18", 0);
                timeProfit.Add("18-24", 0);
                soldTickets.Add("0-6", 0);
                soldTickets.Add("6-12", 0);
                soldTickets.Add("12-18", 0);
                soldTickets.Add("18-24", 0);

                foreach (Bill bill in bills)
                {
                    if (bill.PayTime.Hour >= 0 && bill.PayTime.Hour < 6)
                    {
                        timeProfit["0-6"] += bill.Value();
                        soldTickets["0-6"] += bill.TicketAmount;
                    }
                    else if (bill.PayTime.Hour >= 6 && bill.PayTime.Hour < 12)
                    {
                        timeProfit["6-12"] += bill.Value();
                        soldTickets["6-12"] += bill.TicketAmount;
                    }
                    else if (bill.PayTime.Hour >= 12 && bill.PayTime.Hour < 18)
                    {
                        timeProfit["12-18"] += bill.Value();
                        soldTickets["12-18"] += bill.TicketAmount;
                    }
                    else
                    {
                        timeProfit["18-24"] += bill.Value();
                        soldTickets["18-24"] += bill.TicketAmount;
                    }
                }

                foreach (KeyValuePair<string, float> entry in timeProfit)
                {
                    count = inDataGrid.Rows.Add();
                    inDataGrid.Rows[count].Cells[0].Value = entry.Key + " h";
                    inDataGrid.Rows[count].Cells[1].Value = entry.Value.ToString() + " kn";
                    inDataGrid.Rows[count].Cells[2].Value = soldTickets[entry.Key].ToString();
                }
                
            }

            inDataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void ButtonCategoryClick(IAdminForm AdminFormView)
        {
            AdminFormView.OpenCategoryForm();
        }

        public void Initialize()
        {
            
        }
    }
}
