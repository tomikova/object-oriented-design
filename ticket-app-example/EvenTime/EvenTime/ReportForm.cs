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
    public partial class ReportForm : Form, IReportForm
    {
        Controllers.MainController _refMainController = MainController.getInstance();

        public ReportForm()
        {
            InitializeComponent();

            _refMainController.SetReportInitialParam(this, ref dataGridViewReport, ref comboBoxReport);
        }

        private void comboBoxReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            _refMainController.GenerateReport(this, ref dataGridViewReport, comboBoxReport.Text);
        }
    }
}
