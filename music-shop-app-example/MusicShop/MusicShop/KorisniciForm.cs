using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MusicShop.DAL;
using MusicShop.BLL;

namespace MusicShop
{
    public partial class KorisniciForm : Form
    {
        IList<Kupac> _kupci = null;

        public KorisniciForm(IList<Kupac> kupci = null)
        {
            InitializeComponent();
            this._kupci = kupci;
        }

        private void KorisniciForm_Load(object sender, EventArgs e)
        {
            UpdateDisplay(false);
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            IList<Kupac> kupci;
            if (_kupci != null)
                kupci = _kupci;
            else
                kupci = MusicShop.DAL.KupacRepository.LoadUsersFromDatabase();
            korisniciBindingSource.DataSource = kupci.ToList();
        }

        private void UpdateDisplay(bool editMode)
        {
            korisniciBindingNavigator.MoveFirstItem.Enabled = korisniciBindingNavigator.MovePreviousItem.Enabled
                                                       = bindingNavigatorMoveFirstItem.Enabled
                                                       = korisniciBindingNavigator.MoveNextItem.Enabled
                                                       = korisniciBindingNavigator.MoveLastItem.Enabled
                                                       = korisniciBindingNavigator.PositionItem.Enabled
                                                       = bindingNavigatorAddNewItem.Enabled
                                                       = bindingNavigatorDeleteItem.Enabled
                                                       = toolStripButton1.Enabled
                                                       = !editMode;
            toolStripButton2.Enabled = toolStripButton3.Enabled = editMode;

            foreach (Control childControl in groupBox1.Controls)
            {
                TextBox textBoxControl = childControl as TextBox;
                if (textBoxControl != null)
                {
                    ((TextBox)childControl).ReadOnly = !editMode;
                }
            }
        }

        bool newEntry = false;

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
            if (korisniciBindingSource.Count > 0)
            {
                UpdateDisplay(true);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int position = 0;

            if (!newEntry)
            {
                position = korisniciBindingSource.Position;
            }
            UpdateDisplay(false);
            RefreshScreen();
            if (!newEntry)
            {
                korisniciBindingSource.Position = position;
            }
            else
            {
                newEntry = false;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int position = korisniciBindingSource.Position;

            if (!newEntry)
            {
                Kupac kupac = (Kupac)korisniciBindingSource.Current;
                kupac.Ime = textBox1.Text;
                kupac.Prezime = textBox2.Text;
                kupac.Adresa = textBox3.Text;
                MusicShop.DAL.KupacRepository.Update(kupac);
            }
            else
            {
                Kupac kupac = new Kupac();
                kupac.Ime = textBox1.Text;
                kupac.Prezime = textBox2.Text;
                kupac.Adresa = textBox3.Text;
                MusicShop.DAL.KupacRepository.Add(kupac);
                newEntry = false;
            }
            RefreshScreen();
            UpdateDisplay(false);
            korisniciBindingSource.Position = position;
        }


        private void New(bool addedAutomatically = false)
        {
            if (!addedAutomatically)
            {
                korisniciBindingSource.AddNew();
            }
            UpdateDisplay(true);
        }

        private void Delete()
        {
            DialogResult pick = MessageBox.Show("Želite li obrisati zapis?", "Brisanje zapisa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pick == DialogResult.Yes)
            {
                Kupac forDelete = (Kupac)korisniciBindingSource.Current;
                int position = korisniciBindingSource.Position;
                MusicShop.DAL.KupacRepository.Delete(forDelete);
                RefreshScreen();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kosarica.getInstance().Kupac = (Kupac)korisniciBindingSource.Current;
            button1.BackColor = Color.Green;
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            button1.BackColor = SystemColors.Control;
            if (Kosarica.getInstance().Kupac != null && ((Kupac)korisniciBindingSource.Current)!= null)
            {
                if (Kosarica.getInstance().Kupac.Id == ((Kupac)korisniciBindingSource.Current).Id)
                {
                    button1.BackColor = Color.Green;
                }
                else
                {
                    button1.BackColor = SystemColors.Control;
                }
            }
            if ((Kupac)korisniciBindingSource.Current != null)
            {
                IList<Racun> racuni = RacunRepository.RacuniSearch(((Kupac)korisniciBindingSource.Current).Id.ToString(), "Kupac");
                racuniBindingSource.DataSource = racuni.ToList();
            }
        }
    }
}
