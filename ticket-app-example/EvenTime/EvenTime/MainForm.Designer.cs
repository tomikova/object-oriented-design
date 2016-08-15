namespace EvenTime
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnKupnja = new System.Windows.Forms.Button();
            this.btnRezervacija = new System.Windows.Forms.Button();
            this.btnUnos = new System.Windows.Forms.Button();
            this.btnIzvjesce = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKupnja
            // 
            this.btnKupnja.Location = new System.Drawing.Point(53, 69);
            this.btnKupnja.Name = "btnKupnja";
            this.btnKupnja.Size = new System.Drawing.Size(164, 71);
            this.btnKupnja.TabIndex = 1;
            this.btnKupnja.Text = "Događanja";
            this.btnKupnja.UseVisualStyleBackColor = true;
            this.btnKupnja.Click += new System.EventHandler(this.btnKupnja_Click);
            // 
            // btnRezervacija
            // 
            this.btnRezervacija.Location = new System.Drawing.Point(251, 69);
            this.btnRezervacija.Name = "btnRezervacija";
            this.btnRezervacija.Size = new System.Drawing.Size(164, 71);
            this.btnRezervacija.TabIndex = 2;
            this.btnRezervacija.Text = "Rezervacije";
            this.btnRezervacija.UseVisualStyleBackColor = true;
            this.btnRezervacija.Click += new System.EventHandler(this.btnRezervacija_Click);
            // 
            // btnUnos
            // 
            this.btnUnos.Location = new System.Drawing.Point(53, 172);
            this.btnUnos.Name = "btnUnos";
            this.btnUnos.Size = new System.Drawing.Size(164, 66);
            this.btnUnos.TabIndex = 3;
            this.btnUnos.Text = "Unos podataka";
            this.btnUnos.UseVisualStyleBackColor = true;
            this.btnUnos.Click += new System.EventHandler(this.btnUnos_Click);
            // 
            // btnIzvjesce
            // 
            this.btnIzvjesce.Location = new System.Drawing.Point(251, 172);
            this.btnIzvjesce.Name = "btnIzvjesce";
            this.btnIzvjesce.Size = new System.Drawing.Size(164, 66);
            this.btnIzvjesce.TabIndex = 4;
            this.btnIzvjesce.Text = "Izvješće";
            this.btnIzvjesce.UseVisualStyleBackColor = true;
            this.btnIzvjesce.Click += new System.EventHandler(this.btnIzvjesce_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 341);
            this.Controls.Add(this.btnIzvjesce);
            this.Controls.Add(this.btnUnos);
            this.Controls.Add(this.btnRezervacija);
            this.Controls.Add(this.btnKupnja);
            this.Name = "MainForm";
            this.Text = "EvenTimeApp";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKupnja;
        private System.Windows.Forms.Button btnRezervacija;
        private System.Windows.Forms.Button btnUnos;
        private System.Windows.Forms.Button btnIzvjesce;
    }
}

