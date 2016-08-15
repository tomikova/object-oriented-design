using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvenTime.BaseLib;
using EvenTime.DomainModel;

namespace EvenTime.Controllers
{
    public class EventController : Subject
    {

        private static EventController _instance = null;

        public static EventController getInstance()
        {
            if (_instance == null)
                _instance = new EventController();

            return _instance;
        }

        public void AddNewEvent(string inName, string inDescription, float inPrice, int inQuantity, DateTime inTime, Category inCategory, Location inLocation)
        {      
                EventRepository.getInstance().addEvent(new Event(inName, inDescription, inPrice, inQuantity, inTime, inCategory, inLocation));

                List<Event> list = GetEventList();

                NotifyObservers(list);      
        }

        public List<Event> GetEventList()
        {
            List<Event> retList = new List<Event>();

            for (int i = 0; i < EventRepository.getInstance().Count(); i++)
            {
                Event refEvent = EventRepository.getInstance().getEventByIndex(i);
                if (refEvent.Quantity > 0)
                {
                    retList.Add(refEvent);
                }
            }

            return retList;
        }

        public void AddNewCategory(string inCategoryName)
        {
                CategoryRepository.getInstance().addCategory(new Category(inCategoryName));

                List<Category> list = GetCategoryList();
        }

        public List<Category> GetCategoryList()
        {
            List<Category> retList = new List<Category>();

            for (int i = 0; i < CategoryRepository.getInstance().Count(); i++)
            {
                Category refCategory = CategoryRepository.getInstance().getCategoryByIndex(i);
                retList.Add(refCategory);
            }

            return retList;
        }

        public void SetEventFormParameters(IEventForm EventFormView)
        {
            EventFormView.SetDateTimeFormat("MMMM dd, yyyy HH:mm:ss");
            EventFormView.SetCategories(GetCategoryList());
        }

        public void ButtonOKClick(IEventForm EventFormView, string name, string price, string desc, decimal kol, DateTime time, string categoryName, string locationName)
        {
            try
            {
                float pr = Convert.ToSingle(price);
                if (name == "")
                    MessageBox.Show("Upišite naziv!");
                else if (categoryName == "")
                    MessageBox.Show("Odaberite kategoriju!");
                else if (locationName == "")
                    MessageBox.Show("Odaberite lokaciju!");
                else if (kol <= 0)
                    MessageBox.Show("Upišite broj karata!");
                else
                {
                    Category cat = CategoryRepository.getInstance().getCategoryByName(categoryName);
                    Location loc = LocationRepository.getInstance().getLocationByName(locationName);
                    AddNewEvent(name, desc, pr, Convert.ToInt32(kol), time, cat, loc);
                    EventFormView.CloseEventForm();
                }
            }
            catch (EventAlreadyExists ex)
            {
                MessageBox.Show("Događaj s ovim nazivom već postoji!");
            }
            catch
            {
                MessageBox.Show("Upišite ispravnu cijenu!");
            }

        }

        public void SetBuyFormParameters(IBuyForm BuyFormView, ref DataGridView inDataGrid)
        {
            Attach((IEvenTimeObserver)BuyFormView);

            DataGridViewCheckBoxColumn CheckBox1 = new DataGridViewCheckBoxColumn();
            CheckBox1.HeaderText = "Odabir";
            CheckBox1.Name = "CheckBoxPass1";
            CheckBox1.FlatStyle = FlatStyle.Standard;
            CheckBox1.ThreeState = false;
            CheckBox1.CellTemplate.Style.BackColor = System.Drawing.Color.LightBlue;
            inDataGrid.Columns.Insert(0, CheckBox1);

            List<Event> listEvent = GetEventList();
            SetEventList(listEvent, ref inDataGrid);
        }

        public void SetEventList(List<Event> inList, ref DataGridView inDataGrid)
        {
            inDataGrid.Rows.Clear();

            int count;

            foreach (Event ev in inList)
            {
                count = inDataGrid.Rows.Add();
                inDataGrid.Rows[count].Cells[1].Value = ev.Name;
                inDataGrid.Rows[count].Cells[2].Value = ev.Description;
                inDataGrid.Rows[count].Cells[3].Value = ev.Date.ToString();
                inDataGrid.Rows[count].Cells[4].Value = ev.Category.CategoryName;
                inDataGrid.Rows[count].Cells[5].Value = ev.Location.Name;
                inDataGrid.Rows[count].Cells[6].Value = ev.Price.ToString();
                inDataGrid.Rows[count].Cells[7].Value = ev.Quantity.ToString();
            }

            inDataGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        public void ButtonCatOKClick(ICategoryForm CatFormView, string inName)
        {
            try
            {
                AddNewCategory(inName);
                CatFormView.CloseCatForm();
            }
            catch (CategoryAlreadyExists ex)
            {
                MessageBox.Show("Kategorija već postoji!");
            }
        }
    }
}
