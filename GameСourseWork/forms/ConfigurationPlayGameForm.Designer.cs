namespace GameСourseWork
{
    partial class ConfigurationPlayGameForm
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
            this.comboBoxPlayer1 = new System.Windows.Forms.ComboBox();
            this.comboBoxPlayer2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxBoard = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxStrategy1 = new System.Windows.Forms.GroupBox();
            this.labelStrategy = new System.Windows.Forms.Label();
            this.numericUpDownStrategy = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxStrategy1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrategy)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxPlayer1
            // 
            this.comboBoxPlayer1.FormattingEnabled = true;
            this.comboBoxPlayer1.Location = new System.Drawing.Point(45, 57);
            this.comboBoxPlayer1.Name = "comboBoxPlayer1";
            this.comboBoxPlayer1.Size = new System.Drawing.Size(121, 24);
            this.comboBoxPlayer1.TabIndex = 0;
            this.comboBoxPlayer1.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlayer1_SelectedIndexChanged);
            // 
            // comboBoxPlayer2
            // 
            this.comboBoxPlayer2.FormattingEnabled = true;
            this.comboBoxPlayer2.Location = new System.Drawing.Point(45, 136);
            this.comboBoxPlayer2.Name = "comboBoxPlayer2";
            this.comboBoxPlayer2.Size = new System.Drawing.Size(121, 24);
            this.comboBoxPlayer2.TabIndex = 1;
            this.comboBoxPlayer2.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlayer2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Игрок 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Игрок 2";
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(45, 262);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(121, 23);
            this.buttonStartGame.TabIndex = 4;
            this.buttonStartGame.Text = "Начать игру";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.buttonStartGame_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Тип поля";
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
            this.comboBoxBoard.Location = new System.Drawing.Point(45, 207);
            this.comboBoxBoard.Name = "comboBoxBoard";
            this.comboBoxBoard.Size = new System.Drawing.Size(121, 24);
            this.comboBoxBoard.TabIndex = 6;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBoxStrategy1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(202, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(382, 315);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // groupBoxStrategy1
            // 
            this.groupBoxStrategy1.AutoSize = true;
            this.groupBoxStrategy1.Controls.Add(this.labelStrategy);
            this.groupBoxStrategy1.Controls.Add(this.numericUpDownStrategy);
            this.groupBoxStrategy1.Location = new System.Drawing.Point(3, 3);
            this.groupBoxStrategy1.Name = "groupBoxStrategy1";
            this.groupBoxStrategy1.Size = new System.Drawing.Size(125, 65);
            this.groupBoxStrategy1.TabIndex = 0;
            this.groupBoxStrategy1.TabStop = false;
            this.groupBoxStrategy1.Text = "Стратегия";
            this.groupBoxStrategy1.Visible = false;
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
            // ConfigurationPlayGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 331);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.comboBoxBoard);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxPlayer2);
            this.Controls.Add(this.comboBoxPlayer1);
            this.Name = "ConfigurationPlayGameForm";
            this.Text = "ConfigurationPlayGameForm";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBoxStrategy1.ResumeLayout(false);
            this.groupBoxStrategy1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrategy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPlayer1;
        private System.Windows.Forms.ComboBox comboBoxPlayer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxBoard;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBoxStrategy1;
        private System.Windows.Forms.Label labelStrategy;
        private System.Windows.Forms.NumericUpDown numericUpDownStrategy;
    }
}