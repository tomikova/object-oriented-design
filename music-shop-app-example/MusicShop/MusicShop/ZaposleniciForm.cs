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
    public partial class ZaposleniciForm : Form
    {
        IList<Djelatnik> _djelatnici = null;

        public ZaposleniciForm(IList<Djelatnik> djelatnici = null)
        {
            InitializeComponent();
            this._djelatnici = djelatnici;
        }

        private void ZaposleniciForm_Load(object sender, EventArgs e)
        {
            UpdateDisplay(false);
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            IList<Djelatnik> djelatnici;
            if (_djelatnici != null)
                djelatnici = _djelatnici;
            else
                djelatnici = MusicShop.DAL.DjelatnikRepository.LoadUsersFromDatabase();

            IList<Zanimanje> zanimanja = MusicShop.DAL.ZanimanjeRepository.LoadZanimanjaFromDatabase();
            zaposleniciBindingSource.DataSource = djelatnici.ToList();
            ZanimanjeBindingSource.DataSource = zanimanja.ToList();
            Setup();
        }

        private void UpdateDisplay(bool editMode)
        {
            zaposleniciBindingNavigator.MoveFirstItem.Enabled = zaposleniciBindingNavigator.MovePreviousItem.Enabled
                                                       = bindingNavigatorMoveFirstItem.Enabled
                                                       = zaposleniciBindingNavigator.MoveNextItem.Enabled
                                                       = zaposleniciBindingNavigator.MoveLastItem.Enabled
                                                       = zaposleniciBindingNavigator.PositionItem.Enabled
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
            if (zaposleniciBindingSource.Count > 0)
            {
                UpdateDisplay(true);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int position = 0;

            if (!newEntry)
            {
                position = zaposleniciBindingSource.Position;
            }
            UpdateDisplay(false);
            RefreshScreen();
            if (!newEntry)
            {
                zaposleniciBindingSource.Position = position;
            }
            else
            {
                newEntry = false;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int position = zaposleniciBindingSource.Position;

            if (!newEntry)
            {
                Djelatnik djelatnik = (Djelatnik)zaposleniciBindingSource.Current;
                djelatnik.Ime = textBox1.Text;
                djelatnik.Prezime = textBox2.Text;
                djelatnik.Adresa = textBox3.Text;
                djelatnik.Zanimanje = MusicShop.DAL.ZanimanjeRepository.GetSpecific(((Zanimanje)comboBox1.SelectedItem).Id);
                MusicShop.DAL.DjelatnikRepository.Update(djelatnik);
            }
            else
            {
                Djelatnik djelatnik = new Djelatnik();
                djelatnik.Ime = textBox1.Text;
                djelatnik.Prezime = textBox2.Text;
                djelatnik.Adresa = textBox3.Text;
                djelatnik.Zanimanje = MusicShop.DAL.ZanimanjeRepository.GetSpecific(((Zanimanje)comboBox1.SelectedItem).Id);
                MusicShop.DAL.DjelatnikRepository.Add(djelatnik);
                newEntry = false;
            }
            RefreshScreen();
            UpdateDisplay(false);
            zaposleniciBindingSource.Position = position;
        }

        private void New(bool addedAutomatically = false)
        {
            if (!addedAutomatically)
            {
                zaposleniciBindingSource.AddNew();
            }
            UpdateDisplay(true);
        }

        private void Delete()
        {
            DialogResult pick = MessageBox.Show("Želite li obrisati zapis?", "Brisanje zapisa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pick == DialogResult.Yes)
            {
                Djelatnik forDelete = (Djelatnik)zaposleniciBindingSource.Current;
                int position = zaposleniciBindingSource.Position;
                MusicShop.DAL.DjelatnikRepository.Delete(forDelete);
                //if (korisniciBindingSource.Count >= position)
                //{
                //    korisniciBindingSource.Position = position;
                //}
                //else if (korisniciBindingSource.Count != 0)
                //{
                //    korisniciBindingSource.Position = position - 1;
                //}
                RefreshScreen();
            }
        }

        private void zaposleniciBindingNavigator_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            Setup();
            if ((Djelatnik)zaposleniciBindingSource.Current != null)
            {
                IList<Racun> racuni = RacunRepository.RacuniSearch(((Djelatnik)zaposleniciBindingSource.Current).Id.ToString(), "Djelatnik");
                racuniBindingSource.DataSource = racuni.ToList();
            }
        }

        internal void Setup()
        {
            button2.Enabled = false;
            button2.BackColor = SystemColors.Control;
            IList<Zanimanje> zanimanja = MusicShop.DAL.ZanimanjeRepository.LoadZanimanjaFromDatabase();
            ZanimanjeBindingSource.DataSource = zanimanja.ToList();
            IList<Djelatnik> djelatnici;
            if (_djelatnici != null)
                djelatnici = _djelatnici;
            else
                djelatnici = MusicShop.DAL.DjelatnikRepository.LoadUsersFromDatabase();
            if (djelatnici.Count != 0 && djelatnici.Count == zaposleniciBindingSource.Count)
            {
                Djelatnik dj = (Djelatnik)zaposleniciBindingSource.Current;
                Zanimanje zanim = MusicShop.DAL.ZanimanjeRepository.GetSpecific(dj.Zanimanje.Id);
                comboBox1.SelectedIndex = comboBox1.FindStringExact(zanim.Naziv);
                if (zanim.Naziv == "Blagajnik")
                {
                    button2.Enabled = true;
                    if (Kosarica.getInstance().Djelatnik != null && ((Djelatnik)zaposleniciBindingSource.Current)!= null)
                    {
                        if (Kosarica.getInstance().Djelatnik.Id == ((Djelatnik)zaposleniciBindingSource.Current).Id)
                        {
                            button2.BackColor = Color.Green;
                        }
                        else
                        {
                            button2.BackColor = SystemColors.Control;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZanimanjaForm zanimanjeFrm = new ZanimanjaForm(this);
            zanimanjeFrm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kosarica.getInstance().Djelatnik = (Djelatnik)zaposleniciBindingSource.Current;
            button2.BackColor = Color.Green;
        }

    }
}
