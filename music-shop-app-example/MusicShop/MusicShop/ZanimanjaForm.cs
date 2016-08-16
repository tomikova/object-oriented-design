using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MusicShop.DAL;

namespace MusicShop
{
    public partial class ZanimanjaForm : Form
    {
        private ZaposleniciForm _zaposleniciFrm;

        public ZanimanjaForm(ZaposleniciForm form)
        {
            InitializeComponent();
            this._zaposleniciFrm = form;
        }

        private void ZanimanjaForm_Load(object sender, EventArgs e)
        {
            UpdateDisplay(false);
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            IList<Zanimanje> zanimanja = MusicShop.DAL.ZanimanjeRepository.LoadZanimanjaFromDatabase();
            zanimanjaBindingSource.DataSource = zanimanja.ToList();
        }

        private void UpdateDisplay(bool editMode)
        {
            bindingNavigator1.MoveFirstItem.Enabled = bindingNavigator1.MovePreviousItem.Enabled
                                                       = bindingNavigatorMoveFirstItem.Enabled
                                                       = bindingNavigator1.MoveNextItem.Enabled
                                                       = bindingNavigator1.MoveLastItem.Enabled
                                                       = bindingNavigator1.PositionItem.Enabled
                                                       = bindingNavigatorAddNewItem.Enabled
                                                       = bindingNavigatorDeleteItem.Enabled
                                                       = toolStripButton1.Enabled
                                                       = !editMode;
            toolStripButton2.Enabled = toolStripButton3.Enabled = editMode;

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            New(true);
            newEntry = true;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UpdateDisplay(true);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Save();
        }


        bool newEntry = false;

        private void New(bool addedAutomatically = false)
        {
            if (!addedAutomatically)
            {
                zanimanjaBindingSource.AddNew();
            }
            UpdateDisplay(true);
        }

        private void Cancel()
        {
            int position = 0;

            if (!newEntry)
            {
                position = zanimanjaBindingSource.Position;
            }
            UpdateDisplay(false);
            RefreshScreen();
            if (!newEntry)
            {
                zanimanjaBindingSource.Position = position;
            }
            else
            {
                newEntry = false;
            }
        }

        private void Delete()
        {
            DialogResult pick = MessageBox.Show("Želite li obrisati zapis?", "Brisanje zapisa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pick == DialogResult.Yes)
            {
                Zanimanje forDelete = (Zanimanje)zanimanjaBindingSource.Current;
                int position = zanimanjaBindingSource.Position;
                MusicShop.DAL.ZanimanjeRepository.Delete(forDelete);
                RefreshScreen();
            }
        }

        private void Save()
        {
            dataGridView1.EndEdit();
            zanimanjaBindingSource.EndEdit();

            int position = zanimanjaBindingSource.Position;

            if (!newEntry)
            {
                Zanimanje zanimanje = (Zanimanje)zanimanjaBindingSource.Current;
                MusicShop.DAL.ZanimanjeRepository.Update(zanimanje);
            }
            else
            {
                Zanimanje zanimanje = (Zanimanje)zanimanjaBindingSource.Current;
                MusicShop.DAL.ZanimanjeRepository.Add(zanimanje);
                newEntry = false;
            }
            RefreshScreen();
            UpdateDisplay(false);
            zanimanjaBindingSource.Position = position;
        }

        private void ZanimanjaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _zaposleniciFrm.Setup();
        }

    }
}
