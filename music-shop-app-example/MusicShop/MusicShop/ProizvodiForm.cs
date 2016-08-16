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
    public partial class ProizvodiForm : Form
    {
        IList<Proizvod> _proizvodi = null;

        public ProizvodiForm(IList<Proizvod> proizvodi = null)
        {
            InitializeComponent();
            this._proizvodi = proizvodi;
        }

        private void SetBindings(IList<Kategorija> kategorije)
        {
            kategorijaBindingSource2.DataSource = kategorije.ToList();
            toolStripComboBox1.ComboBox.DataSource = kategorijaBindingSource2;
            toolStripComboBox1.ComboBox.DisplayMember = "Naziv";
            toolStripComboBox1.ComboBox.ValueMember = "Id";
        }

        private void ProizvodiForm_Load(object sender, EventArgs e)
        {
            UpdateDisplay(false);
            RefreshScreen();
        }

        private void RefreshScreen()
        {
            IList<Proizvod> proizvodi;
            if (_proizvodi != null)
                proizvodi = _proizvodi;
            else
                proizvodi = MusicShop.DAL.ProizvodRepository.LoadProizvodFromDatabase();
            proizvodiBindingSource.DataSource = proizvodi.ToList();
            IList<Kategorija> kategorije = MusicShop.DAL.KategorijaRepository.LoadKategorijaFromDatabase();
            kategorijaBindingSource.DataSource = kategorije.ToList();
            SetBindings(kategorije);
            Setup();
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
            if (proizvodiBindingSource.Count > 0)
            {
                UpdateDisplay(true);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            int position = 0;

            if (!newEntry)
            {
                position = proizvodiBindingSource.Position;
            }
            UpdateDisplay(false);
            RefreshScreen();
            if (!newEntry)
            {
                proizvodiBindingSource.Position = position;
            }
            else
            {
                newEntry = false;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int position = proizvodiBindingSource.Position;
            bool pom = false;
            if (this._proizvodi != null)
                pom = true;

            if (!newEntry)
            {
                try
                {
                    Proizvod proizvod = (Proizvod)proizvodiBindingSource.Current;
                    proizvod.Naziv = textBox3.Text;
                    proizvod.Kolicina = Convert.ToInt32(textBox2.Text);
                    proizvod.Cijena = Convert.ToSingle(textBox1.Text);
                    proizvod.Opis = textBox4.Text;
                    proizvod.Kategorija = MusicShop.DAL.KategorijaRepository.GetSpecific(((Kategorija)comboBox1.SelectedItem).Id);
                    MusicShop.DAL.ProizvodRepository.Update(proizvod);
                }
                catch
                {
                    MessageBox.Show("Upišite ispravnu cijenu ili količinu");
                }
            }
            else
            {
                Proizvod proizvod = (Proizvod)proizvodiBindingSource.Current;
                proizvod.Kategorija = MusicShop.DAL.KategorijaRepository.GetSpecific(((Kategorija)comboBox1.SelectedItem).Id);
                MusicShop.DAL.ProizvodRepository.Add(proizvod);
                newEntry = false;
            }
            this._proizvodi = null;
            RefreshScreen();
            UpdateDisplay(false);
            if (!pom)
            {
                proizvodiBindingSource.Position = position;
            }
        }

        private void New(bool addedAutomatically = false)
        {
            if (!addedAutomatically)
            {
                proizvodiBindingSource.AddNew();
            }
            UpdateDisplay(true);
        }

        private void Delete()
        {
            DialogResult pick = MessageBox.Show("Želite li obrisati zapis?", "Brisanje zapisa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pick == DialogResult.Yes)
            {
                Proizvod forDelete = (Proizvod)proizvodiBindingSource.Current;
                MusicShop.DAL.ProizvodRepository.Delete(forDelete);
                this._proizvodi = null;
                RefreshScreen();
            }
        }

        private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        {
            Setup();
        }

        internal void Setup()
        {
            IList<Kategorija> kategorije = MusicShop.DAL.KategorijaRepository.LoadKategorijaFromDatabase();
            kategorijaBindingSource.DataSource = kategorije.ToList();
            SetBindings(kategorije);
            IList<Proizvod> proizvodi;
            if (_proizvodi != null)
                proizvodi = _proizvodi;
            else
                proizvodi = MusicShop.DAL.ProizvodRepository.LoadProizvodFromDatabase();

            if (proizvodi.Count != 0 && proizvodi.Count == proizvodiBindingSource.Count)
            {
                Proizvod p = (Proizvod)proizvodiBindingSource.Current;
                Kategorija kat = MusicShop.DAL.KategorijaRepository.GetSpecific(p.Kategorija.Id);
                comboBox1.SelectedIndex = comboBox1.FindStringExact(kat.Naziv);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KategorijeForm katFrm = new KategorijeForm(this);
            katFrm.ShowDialog();
        }

        private void toolStripComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                _proizvodi = MusicShop.DAL.ProizvodRepository.ProizvodByCategory(toolStripComboBox1.ComboBox.SelectedValue.ToString());
            }
            catch { }
            RefreshScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Kosarica.getInstance().Kupac == null)
            {
                MessageBox.Show("Nije odabran korisnik!");
                OsobePretragaForm frm = new OsobePretragaForm();
                frm.ShowDialog();
            }
            else
            {
                int kolicina = Convert.ToInt32(numericUpDown1.Value);
                if (kolicina > 0)
                {
                    Proizvod p = (Proizvod)proizvodiBindingSource.Current;
                    if (kolicina > p.Kolicina)
                    {
                        MessageBox.Show("Pogrešna količina");
                    }
                    else
                    {
                        StavkaKupovina stavka = new StavkaKupovina();
                        stavka.Kolicina = kolicina;
                        stavka.Cijena = p.Cijena * kolicina;
                        stavka.Datum = DateTime.Now;
                        stavka.Popust = 0;
                        stavka.Proizvod = p;
                        p.Kolicina = p.Kolicina - kolicina;
                        Kosarica.getInstance().DodajStavku(stavka);
                        MusicShop.DAL.ProizvodRepository.Update(p);
                        int position = proizvodiBindingSource.Position;
                        RefreshScreen();
                        proizvodiBindingSource.Position = position;
                        MessageBox.Show("Uspješno dodano u košaricu!");
                    }
                }
                else
                {
                    MessageBox.Show("Nije odabrana količina!");
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Kosarica.getInstance().Kupac == null)
            {
                MessageBox.Show("Nije odabran korisnik!");
                OsobePretragaForm frm = new OsobePretragaForm();
                frm.ShowDialog();
            }
            else
            {
                int brDana = Convert.ToInt32(numericUpDown2.Value);
                if (brDana > 0)
                {
                    Proizvod p = (Proizvod)proizvodiBindingSource.Current;
                    if (p.Kolicina == 0)
                    {
                        MessageBox.Show("Proizvod nije dostupan");
                    }
                    else
                    {
                        StavkaPosudba stavka = new StavkaPosudba();
                        stavka.Kolicina = 1;
                        stavka.Cijena = (p.Cijena / 40) * brDana;
                        stavka.Datum = DateTime.Now;
                        stavka.DatumVracanja = DateTime.Now.AddDays(brDana);
                        stavka.Proizvod = p;
                        p.Kolicina = p.Kolicina - 1;
                        Kosarica.getInstance().DodajStavku(stavka);
                        MusicShop.DAL.ProizvodRepository.Update(p);
                        int position = proizvodiBindingSource.Position;
                        RefreshScreen();
                        proizvodiBindingSource.Position = position;
                        MessageBox.Show("Uspješno dodano u košaricu!");
                    }
                }
                else
                {
                    MessageBox.Show("Nije odabrano trajanje posudbe!");
                }
            }
        }

    }
}
