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
    public partial class CategoryForm : Form, ICategoryForm
    {

        EventController _refEventController = EventController.getInstance();

        public CategoryForm()
        {
            InitializeComponent();
        }

        public void CloseCatForm()
        {
            ActiveForm.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _refEventController.ButtonCatOKClick(this, textBoxName.Text);
        }
    }
}
