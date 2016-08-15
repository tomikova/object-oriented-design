using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using EvenTime.BaseLib;
using EvenTime.DomainModel;

namespace EvenTime.Controllers
{
    public class ReservationController : Subject
    {

        private static ReservationController _instance = null;

        public static ReservationController getInstance()
        {
            if (_instance == null)
                _instance = new ReservationController();

            return _instance;
        }

        public void AddNewReservation(string inResCode, int inAmount, DateTime inResTime, Event inEvent, string inCusName, string inCusSurname, string inCusPhone)
        {

            ReservationRepository.getInstance().addReservation(new Reservation(inResCode, inAmount, inResTime, inEvent, inCusName, inCusSurname, inCusPhone));

            List<Reservation> list = GetReservationList();

            NotifyObservers(list);
        }

        public void RemoveReservation(string inKod)
        {
            Reservation res = ReservationRepository.getInstance().getReservationByCode(inKod);
            ReservationRepository.getInstance().removeReservation(res);

            List<Reservation> list = GetReservationList();

            NotifyObservers(list);
        }

        public List<Reservation> GetReservationList()
        {
            List<Reservation> retList = new List<Reservation>();

            for (int i = 0; i < ReservationRepository.getInstance().Count(); i++)
            {
                Reservation refReservation = ReservationRepository.getInstance().getReservationByIndex(i);
                retList.Add(refReservation);
            }

            return retList;
        }

        public void AddNewPaidReservation(Reservation inRes, Bill inBill)
        {

            PaidReservationRepository.getInstance().addPaidReservation(new PaidReservation(inRes.Code, inRes.TicketAmount, inRes.ResTime, inRes.ResEvent, inRes.CustomerName, inRes.CustomerSurname, inRes.CustomerPhone, inBill));

            List<PaidReservation> list = GetPaidReservationList();

        }

        public List<PaidReservation> GetPaidReservationList()
        {
            List<PaidReservation> retList = new List<PaidReservation>();

            for (int i = 0; i < PaidReservationRepository.getInstance().Count(); i++)
            {
                PaidReservation refPaidReservation = PaidReservationRepository.getInstance().getPaidReservationByIndex(i);
                retList.Add(refPaidReservation);
            }

            return retList;
        }

        public void ButtonRezervacijaClick(IBuyForm BuyFormView, ref DataGridView inDatagridView, decimal inKol)
        {
            int index = 0;
            bool pom = false;
            foreach (DataGridViewRow row in inDatagridView.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;

                if (Convert.ToBoolean(chk.Value) == true)
                {
                    pom = true;
                    break;
                }
                index++;
            }
            if (pom)
            {
                Event ev = EventRepository.getInstance().getEventByIndex(index);
                int kol = Convert.ToInt32(inKol);

                if(DateTime.Compare(ev.Date, DateTime.Now.AddDays(2)) < 0)
                {
                    MessageBox.Show("Za ovaj događaj moguća je samo kupnja!");
                }
                else if (kol == 0)
                {
                    MessageBox.Show("Odaberite količinu karata");
                }
                else if (ev.Quantity < kol)
                {
                    MessageBox.Show("Nema dovoljno karata");
                }
                else
                {
                    BuyFormView.OpenCustomerForm(kol, ev);
                }
            }
            else
            {
                MessageBox.Show("Nije odabran niti jedan događaj!");
            }
        }

        public void ButtonOKClick(ICustomerForm CustomerFormView, string inName, string inSurname, string inPhone, int inKol, Event inEvent)
        {
            if (inName == "")
                MessageBox.Show("Upišite ime!");
            else if (inSurname == "")
                MessageBox.Show("Upišite prezime!");
            else if (inPhone == "")
                MessageBox.Show("Upišite broj telefona!");
            else
            {

                DateTime time = DateTime.Now;
                string str = time.ToString();
                byte[] buffer = new byte[str.Length];
                int i = 0;
                foreach (char c in str.ToCharArray())
                {
                    buffer[i] = (byte)c;
                    i++;
                }
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                string md5string = BitConverter.ToString(md5.ComputeHash(buffer)).Replace("-", "");
                md5string = md5string.Substring(10, 8);

                AddNewReservation(md5string, inKol, time, inEvent, inName, inSurname, inPhone);
                EventRepository.getInstance().RemoveTickets(inKol, inEvent.Name);

                List<Event> list = EventController.getInstance().GetEventList();
                EventController.getInstance().NotifyObservers(list);

                MessageBox.Show("Uspješna rezervacija!\n\nRezervacija:\n\nŠifra: " + md5string + "\nDogađaj: " + inEvent.Name + "\nKoličina: " + inKol.ToString());

                CustomerFormView.CloseCustomerForm();
            }
        }

        public void SetReservationFormParameters(IReservationForm ResFormView, ref DataGridView inDataGrid)
        {
            Attach((IEvenTimeObserver)ResFormView);
            List<Reservation> listReservation = GetReservationList();
            SetReservationList(listReservation, ref inDataGrid);
        }

        public void SetReservationList(List<Reservation> inList, ref DataGridView inDataGrid)
        {
            inDataGrid.Rows.Clear();

            int count;

            foreach (Reservation res in inList)
            {
                count = inDataGrid.Rows.Add();
                inDataGrid.Rows[count].Cells[0].Value = res.Code;
                inDataGrid.Rows[count].Cells[1].Value = res.TicketAmount.ToString();
                inDataGrid.Rows[count].Cells[2].Value = res.ResTime.ToString();
                inDataGrid.Rows[count].Cells[3].Value = res.ResEvent.Name;
                inDataGrid.Rows[count].Cells[4].Value = res.CustomerName;
                inDataGrid.Rows[count].Cells[5].Value = res.CustomerSurname;
                inDataGrid.Rows[count].Cells[6].Value = res.CustomerPhone;
            }

            inDataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void ButtonResOKClick(IReservationForm ResFormView, string inKod)
        {
            try
            {
                if (ReservationRepository.getInstance().reservationExists(inKod))
                {
                    Reservation res = ReservationRepository.getInstance().getReservationByCode(inKod);

                    RemoveReservation(inKod);

                    DateTime time = DateTime.Now;
                    string str = time.ToString();
                    byte[] buffer = new byte[str.Length];
                    int i = 0;
                    foreach (char c in str.ToCharArray())
                    {
                        buffer[i] = (byte)c;
                        i++;
                    }
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    string md5string = BitConverter.ToString(md5.ComputeHash(buffer)).Replace("-", "");

                    BillController.getInstance().AddNewBill(md5string, res.TicketAmount, time, res.ResEvent);

                    Bill bill = BillRepository.getInstance().getBillByCode(md5string);

                    AddNewPaidReservation(res, bill);

                    MessageBox.Show("Uspješna kupovina!\n\nRačun:\n\nŠifra: " + bill.Code + "\nDogađaj: " + bill.PaidEvent.Name + "\nKoličina: " + bill.TicketAmount.ToString());
                }
            }
            catch (ReservationDoesntExist ex)
            {
                MessageBox.Show("Rezervacija s upidanim kodom ne postoji!");
            }

        }

        public void ButtonResCancelClick(IReservationForm ResFormView, string inKod)
        {
            try
            {
                if (ReservationRepository.getInstance().reservationExists(inKod))
                {
                    Reservation res = ReservationRepository.getInstance().getReservationByCode(inKod);

                    RemoveReservation(inKod);

                    EventRepository.getInstance().AddTickets(res.TicketAmount, res.ResEvent.Name);
                    List<Event> list = EventController.getInstance().GetEventList();
                    EventController.getInstance().NotifyObservers(list);

                    MessageBox.Show("Rezervacija " + inKod + " je otkazana!");
                }
            }
            catch (ReservationDoesntExist ex)
            {
                MessageBox.Show("Rezervacija s upidanim kodom ne postoji!");
            }
        }

        public void CheckReservations()
        {
            List<Reservation> listRes = GetReservationList();
            int pom;
            foreach (Reservation res in listRes)
            {
                pom = DateTime.Compare(res.ResEvent.Date, res.ResTime.AddDays(2));
                if (pom < 0)
                {
                    ReservationRepository.getInstance().removeReservation(res);

                    EventRepository.getInstance().AddTickets(res.TicketAmount, res.ResEvent.Name);
                    List<Event> list = EventController.getInstance().GetEventList();
                    EventController.getInstance().NotifyObservers(list);
                }
            }
        }
    }
}
