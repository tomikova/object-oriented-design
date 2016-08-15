using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Cryptography;
using EvenTime.BaseLib;
using EvenTime.DomainModel;

namespace EvenTime.Controllers
{
    public class BillController
    {
        private static BillController _instance = null;

        public static BillController getInstance()
        {
            if (_instance == null)
                _instance = new BillController();

            return _instance;
        }

        public void AddNewBill(string inCode, int inAmount, DateTime inTime, Event inEvent)
        {

            BillRepository.getInstance().addBill(new Bill(inCode, inAmount, inTime, inEvent));

            List<Bill> list = GetBillList();

            //NotifyObservers(list);
        }

        public List<Bill> GetBillList()
        {
            List<Bill> retList = new List<Bill>();

            for (int i = 0; i < BillRepository.getInstance().Count(); i++)
            {
                Bill refBill = BillRepository.getInstance().getBillByIndex(i);
                retList.Add(refBill);
            }

            return retList;
        }

        public void ButtonKupnjaClick(IBuyForm BuyFormView, DataGridView inDatagridView, decimal inKol)
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
                if (kol == 0)
                {
                    MessageBox.Show("Odaberite količinu karata");
                }
                else if (ev.Quantity < kol)
                {
                    MessageBox.Show("Nema dovoljno karata");
                }
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


                    AddNewBill(md5string, kol, time, ev);
                    EventRepository.getInstance().RemoveTickets(kol, ev.Name);

                    List<Event> list = EventController.getInstance().GetEventList();
                    EventController.getInstance().NotifyObservers(list);

                    MessageBox.Show("Uspješna kupovina!\n\nRačun:\n\nŠifra: " + md5string + "\nDogađaj: " + ev.Name + "\nKoličina: " + kol.ToString());
                }
            }
            else
            {
                MessageBox.Show("Nije odabran niti jedan događaj!");
            }
        }
    }
}
