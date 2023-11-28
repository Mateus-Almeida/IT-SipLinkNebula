namespace IT_SipProcess
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_ultimoserial = new System.Windows.Forms.Label();
            this.lbl_status = new System.Windows.Forms.Label();
            this.TimerLog = new System.Windows.Forms.Timer(this.components);
            this.DGV_Info = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Info)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 63);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.DGV_Info);
            this.panel2.Controls.Add(this.lbl_ultimoserial);
            this.panel2.Controls.Add(this.lbl_status);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(844, 428);
            this.panel2.TabIndex = 1;
            // 
            // lbl_ultimoserial
            // 
            this.lbl_ultimoserial.BackColor = System.Drawing.Color.White;
            this.lbl_ultimoserial.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.lbl_ultimoserial.ForeColor = System.Drawing.Color.Black;
            this.lbl_ultimoserial.Location = new System.Drawing.Point(4, 57);
            this.lbl_ultimoserial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_ultimoserial.Name = "lbl_ultimoserial";
            this.lbl_ultimoserial.Size = new System.Drawing.Size(836, 69);
            this.lbl_ultimoserial.TabIndex = 2;
            this.lbl_ultimoserial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_status
            // 
            this.lbl_status.BackColor = System.Drawing.Color.White;
            this.lbl_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.lbl_status.ForeColor = System.Drawing.Color.Black;
            this.lbl_status.Location = new System.Drawing.Point(4, 3);
            this.lbl_status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(836, 54);
            this.lbl_status.TabIndex = 0;
            this.lbl_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimerLog
            // 
            this.TimerLog.Tick += new System.EventHandler(this.TimerLog_Tick);
            // 
            // DGV_Info
            // 
            this.DGV_Info.AllowUserToAddRows = false;
            this.DGV_Info.AllowUserToDeleteRows = false;
            this.DGV_Info.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGV_Info.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Info.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Info.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.DGV_Info.Location = new System.Drawing.Point(0, 129);
            this.DGV_Info.Name = "DGV_Info";
            this.DGV_Info.ReadOnly = true;
            this.DGV_Info.RowHeadersWidth = 51;
            this.DGV_Info.RowTemplate.Height = 24;
            this.DGV_Info.Size = new System.Drawing.Size(844, 299);
            this.DGV_Info.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "CMES Status";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "CSV Info";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(844, 491);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "IT-Sip 2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Info)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Timer TimerLog;
        private System.Windows.Forms.Label lbl_ultimoserial;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.DataGridView DGV_Info;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}

