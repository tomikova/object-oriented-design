using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EvenTime.Controllers;
using EvenTime.BaseLib;

namespace EvenTime
{
    public partial class LogInForm : Form, ILogInForm
    {
        private Controllers.MainController _refMainController = MainController.getInstance();

        public LogInForm()
        {
            InitializeComponent();
        }

        public void OpenAdminForm()
        {
            ActiveForm.Close();
            AdminForm newAdminFrm = new AdminForm();
            newAdminFrm.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _refMainController.ButtonLoginClick(this, textBoxUsername.Text, textBoxPassword.Text);
        }
    }
}
