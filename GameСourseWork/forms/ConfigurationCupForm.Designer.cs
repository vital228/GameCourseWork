namespace GameСourseWork
{
    partial class ConfigurationCupForm
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonStartCup = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxBoard = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxStrategy = new System.Windows.Forms.GroupBox();
            this.labelStrategy = new System.Windows.Forms.Label();
            this.numericUpDownStrategy = new System.Windows.Forms.NumericUpDown();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxStrategy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrategy)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(152, 73);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество кругов:";
            // 
            // buttonStartCup
            // 
            this.buttonStartCup.Location = new System.Drawing.Point(12, 191);
            this.buttonStartCup.Name = "buttonStartCup";
            this.buttonStartCup.Size = new System.Drawing.Size(123, 42);
            this.buttonStartCup.TabIndex = 2;
            this.buttonStartCup.Text = "Старт";
            this.buttonStartCup.UseVisualStyleBackColor = true;
            this.buttonStartCup.Click += new System.EventHandler(this.buttonStartCup_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Название турнира:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(152, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 22);
            this.textBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Тип поля:";
            // 
            // comboBoxBoard
            // 
            this.comboBoxBoard.FormattingEnabled = true;
            this.comboBoxBoard.Items.AddRange(new object[] {
            "Любое",
            "Большая",
            "Маленькое",
            "Единичное",
            "Центр",
            "Край",
            "Около игроков",
            "Далеко от игроков",
            "Каньон"});
            this.comboBoxBoard.Location = new System.Drawing.Point(152, 101);
            this.comboBoxBoard.Name = "comboBoxBoard";
            this.comboBoxBoard.Size = new System.Drawing.Size(121, 24);
            this.comboBoxBoard.TabIndex = 7;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBoxStrategy);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(279, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 390);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // groupBoxStrategy
            // 
            this.groupBoxStrategy.AutoSize = true;
            this.groupBoxStrategy.Controls.Add(this.labelStrategy);
            this.groupBoxStrategy.Controls.Add(this.numericUpDownStrategy);
            this.groupBoxStrategy.Location = new System.Drawing.Point(3, 3);
            this.groupBoxStrategy.Name = "groupBoxStrategy";
            this.groupBoxStrategy.Size = new System.Drawing.Size(125, 65);
            this.groupBoxStrategy.TabIndex = 0;
            this.groupBoxStrategy.TabStop = false;
            this.groupBoxStrategy.Text = "Стратегия";
            this.groupBoxStrategy.Visible = false;
            // 
            // labelStrategy
            // 
            this.labelStrategy.AutoSize = true;
            this.labelStrategy.Location = new System.Drawing.Point(7, 24);
            this.labelStrategy.MaximumSize = new System.Drawing.Size(120, 0);
            this.labelStrategy.Name = "labelStrategy";
            this.labelStrategy.Size = new System.Drawing.Size(67, 16);
            this.labelStrategy.TabIndex = 1;
            this.labelStrategy.Text = "Статегия";
            // 
            // numericUpDownStrategy
            // 
            this.numericUpDownStrategy.Location = new System.Drawing.Point(80, 22);
            this.numericUpDownStrategy.Name = "numericUpDownStrategy";
            this.numericUpDownStrategy.Size = new System.Drawing.Size(39, 22);
            this.numericUpDownStrategy.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(141, 144);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(131, 42);
            this.buttonAdd.TabIndex = 9;
            this.buttonAdd.Text = "Добавить игрока";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(12, 143);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(123, 42);
            this.buttonDownload.TabIndex = 10;
            this.buttonDownload.Text = "Загрузить игроков";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // ConfigurationCupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 428);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.comboBoxBoard);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonStartCup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Name = "ConfigurationCupForm";
            this.Text = "ConfigurationCupForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBoxStrategy.ResumeLayout(false);
            this.groupBoxStrategy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrategy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonStartCup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxBoard;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxStrategy;
        private System.Windows.Forms.Label labelStrategy;
        private System.Windows.Forms.NumericUpDown numericUpDownStrategy;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDownload;
    }
}