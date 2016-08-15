namespace EvenTime
{
    partial class AdminForm
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
            this.btnLokacija = new System.Windows.Forms.Button();
            this.btnDogadjaj = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLokacija
            // 
            this.btnLokacija.Location = new System.Drawing.Point(108, 24);
            this.btnLokacija.Name = "btnLokacija";
            this.btnLokacija.Size = new System.Drawing.Size(124, 63);
            this.btnLokacija.TabIndex = 0;
            this.btnLokacija.Text = "Unos lokacije";
            this.btnLokacija.UseVisualStyleBackColor = true;
            this.btnLokacija.Click += new System.EventHandler(this.btnLokacija_Click);
            // 
            // btnDogadjaj
            // 
            this.btnDogadjaj.Location = new System.Drawing.Point(108, 106);
            this.btnDogadjaj.Name = "btnDogadjaj";
            this.btnDogadjaj.Size = new System.Drawing.Size(124, 63);
            this.btnDogadjaj.TabIndex = 1;
            this.btnDogadjaj.Text = "Unos događaja";
            this.btnDogadjaj.UseVisualStyleBackColor = true;
            this.btnDogadjaj.Click += new System.EventHandler(this.btnDogadjaj_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(108, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 63);
            this.button1.TabIndex = 2;
            this.button1.Text = "Unos kategorije";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 283);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDogadjaj);
            this.Controls.Add(this.btnLokacija);
            this.Name = "AdminForm";
            this.Text = "Control Panel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLokacija;
        private System.Windows.Forms.Button btnDogadjaj;
        private System.Windows.Forms.Button button1;
    }
}