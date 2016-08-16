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
    public partial class KategorijeForm : Form
    {

        private ProizvodiForm _proizFrm;

        public KategorijeForm(ProizvodiForm form)
        {
            InitializeComponent();
            this._proizFrm = form;
        }

        private void KategorijeForm_Load(object sender, EventArgs e)
        {
            UpdateDisplay(false);
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            IList<  Kategorija> kategorije = MusicShop.DAL.KategorijaRepository.LoadKategorijaFromDatabase();
            kategorijeBindingSource.DataSource = kategorije.ToList();
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
                kategorijeBindingSource.AddNew();
            }
            UpdateDisplay(true);
        }

        private void Cancel()
        {
            int position = 0;

            if (!newEntry)
            {
                position = kategorijeBindingSource.Position;
            }
            UpdateDisplay(false);
            RefreshScreen();
            if (!newEntry)
            {
                kategorijeBindingSource.Position = position;
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
                Kategorija forDelete = (Kategorija)kategorijeBindingSource.Current;
                MusicShop.DAL.KategorijaRepository.Delete(forDelete);
                RefreshScreen();
            }
        }

        private void Save()
        {
            dataGridView1.EndEdit();
            kategorijeBindingSource.EndEdit();

            int position = kategorijeBindingSource.Position;

            if (!newEntry)
            {
                Kategorija kategorija = (Kategorija)kategorijeBindingSource.Current;
                MusicShop.DAL.KategorijaRepository.Update(kategorija);
            }
            else
            {
                Kategorija kategorija = (Kategorija)kategorijeBindingSource.Current;
                MusicShop.DAL.KategorijaRepository.Add(kategorija);
                newEntry = false;
            }
            RefreshScreen();
            UpdateDisplay(false);
            kategorijeBindingSource.Position = position;
        }

        private void KategorijeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _proizFrm.Setup();
        }

    }
}
