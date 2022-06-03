namespace ImportNfe_SAP
{
    partial class DownLoadNfe
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbt451F = new System.Windows.Forms.RadioButton();
            this.rbt451T = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConectar = new System.Windows.Forms.Button();
            this.lbRodape = new System.Windows.Forms.Label();
            this.timerCount = new System.Windows.Forms.Timer(this.components);
            this.lbStatus = new System.Windows.Forms.Label();
            this.pictureBoxCarregando = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbAviso = new System.Windows.Forms.Label();
            this.lbtime = new System.Windows.Forms.Label();
            this.btnParar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCarregando)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbt451F);
            this.groupBox1.Controls.Add(this.rbt451T);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(95, 77);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PLANTA";
            // 
            // rbt451F
            // 
            this.rbt451F.AutoSize = true;
            this.rbt451F.Location = new System.Drawing.Point(6, 19);
            this.rbt451F.Name = "rbt451F";
            this.rbt451F.Size = new System.Drawing.Size(49, 17);
            this.rbt451F.TabIndex = 3;
            this.rbt451F.TabStop = true;
            this.rbt451F.Text = "451F";
            this.rbt451F.UseVisualStyleBackColor = true;
            // 
            // rbt451T
            // 
            this.rbt451T.AutoSize = true;
            this.rbt451T.Location = new System.Drawing.Point(6, 42);
            this.rbt451T.Name = "rbt451T";
            this.rbt451T.Size = new System.Drawing.Size(50, 17);
            this.rbt451T.TabIndex = 4;
            this.rbt451T.TabStop = true;
            this.rbt451T.Text = "451T";
            this.rbt451T.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(540, 139);
            this.dataGridView1.TabIndex = 7;
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(247, 28);
            this.txtDescricao.MaximumSize = new System.Drawing.Size(130, 80);
            this.txtDescricao.MaxLength = 10;
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(130, 41);
            this.txtDescricao.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(247, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "DATA - YYYY-MM-DD";
            // 
            // btnConectar
            // 
            this.btnConectar.BackColor = System.Drawing.Color.Silver;
            this.btnConectar.Location = new System.Drawing.Point(383, 28);
            this.btnConectar.Name = "btnConectar";
            this.btnConectar.Size = new System.Drawing.Size(154, 41);
            this.btnConectar.TabIndex = 14;
            this.btnConectar.Text = "Conectar";
            this.btnConectar.UseVisualStyleBackColor = false;
            this.btnConectar.Click += new System.EventHandler(this.btnConectar_Click);
            // 
            // lbRodape
            // 
            this.lbRodape.AutoSize = true;
            this.lbRodape.BackColor = System.Drawing.Color.Transparent;
            this.lbRodape.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRodape.ForeColor = System.Drawing.Color.Black;
            this.lbRodape.Location = new System.Drawing.Point(-4, 241);
            this.lbRodape.Name = "lbRodape";
            this.lbRodape.Size = new System.Drawing.Size(51, 20);
            this.lbRodape.TabIndex = 15;
            this.lbRodape.Text = "label1";
            // 
            // timerCount
            // 
            this.timerCount.Interval = 1000;
            this.timerCount.Tick += new System.EventHandler(this.timerCount_Tick);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbStatus.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbStatus.Location = new System.Drawing.Point(101, 12);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(133, 13);
            this.lbStatus.TabIndex = 16;
            this.lbStatus.Text = "CONECTANDO AO SAP...";
            // 
            // pictureBoxCarregando
            // 
            this.pictureBoxCarregando.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxCarregando.Image = global::ImportNfe_SAP.Properties.Resources.carregando;
            this.pictureBoxCarregando.Location = new System.Drawing.Point(113, 28);
            this.pictureBoxCarregando.Name = "pictureBoxCarregando";
            this.pictureBoxCarregando.Size = new System.Drawing.Size(62, 21);
            this.pictureBoxCarregando.TabIndex = 17;
            this.pictureBoxCarregando.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnParar);
            this.panel1.Controls.Add(this.lbAviso);
            this.panel1.Controls.Add(this.lbtime);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.pictureBoxCarregando);
            this.panel1.Controls.Add(this.txtDescricao);
            this.panel1.Controls.Add(this.lbStatus);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.lbRodape);
            this.panel1.Controls.Add(this.btnConectar);
            this.panel1.Location = new System.Drawing.Point(2, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 269);
            this.panel1.TabIndex = 18;
            // 
            // lbAviso
            // 
            this.lbAviso.AutoSize = true;
            this.lbAviso.BackColor = System.Drawing.Color.Transparent;
            this.lbAviso.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAviso.ForeColor = System.Drawing.Color.Red;
            this.lbAviso.Location = new System.Drawing.Point(3, 83);
            this.lbAviso.Name = "lbAviso";
            this.lbAviso.Size = new System.Drawing.Size(26, 13);
            this.lbAviso.TabIndex = 19;
            this.lbAviso.Text = "msg";
            // 
            // lbtime
            // 
            this.lbtime.AutoSize = true;
            this.lbtime.BackColor = System.Drawing.Color.Transparent;
            this.lbtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtime.Location = new System.Drawing.Point(101, 52);
            this.lbtime.Name = "lbtime";
            this.lbtime.Size = new System.Drawing.Size(27, 17);
            this.lbtime.TabIndex = 18;
            this.lbtime.Text = "0:s";
            // 
            // btnParar
            // 
            this.btnParar.BackColor = System.Drawing.Color.Red;
            this.btnParar.Location = new System.Drawing.Point(418, 70);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(101, 26);
            this.btnParar.TabIndex = 20;
            this.btnParar.Text = "Parar";
            this.btnParar.UseVisualStyleBackColor = false;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // DownLoadNfe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ImportNfe_SAP.Properties.Resources.foxconn_logo_iloveimg_resized;
            this.ClientSize = new System.Drawing.Size(544, 347);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DownLoadNfe";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Nfe - SAP  V.1.0.3";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCarregando)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbt451F;
        private System.Windows.Forms.RadioButton rbt451T;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConectar;
        private System.Windows.Forms.Label lbRodape;
        private System.Windows.Forms.Timer timerCount;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.PictureBox pictureBoxCarregando;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbtime;
        private System.Windows.Forms.Label lbAviso;
        private System.Windows.Forms.Button btnParar;
    }
}

