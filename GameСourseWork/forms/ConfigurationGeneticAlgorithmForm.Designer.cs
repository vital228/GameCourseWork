namespace GameСourseWork.forms
{
    partial class ConfigurationGeneticAlgorithmForm
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
            this.numericUpDownGeneration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBoxStrategy = new System.Windows.Forms.GroupBox();
            this.labelStrategy = new System.Windows.Forms.Label();
            this.numericUpDownStrategy = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownPopulations = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGeneration)).BeginInit();
            this.groupBoxStrategy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrategy)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPopulations)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownGeneration
            // 
            this.numericUpDownGeneration.Location = new System.Drawing.Point(231, 12);
            this.numericUpDownGeneration.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownGeneration.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownGeneration.Name = "numericUpDownGeneration";
            this.numericUpDownGeneration.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownGeneration.TabIndex = 0;
            this.numericUpDownGeneration.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(40, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество поколений:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(419, 23);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(130, 49);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Добавить\r\nстратегию";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(46, 377);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(169, 47);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Начать";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
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
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBoxStrategy);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(40, 77);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(526, 294);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(40, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Количество популяции:";
            // 
            // numericUpDownPopulations
            // 
            this.numericUpDownPopulations.Location = new System.Drawing.Point(231, 41);
            this.numericUpDownPopulations.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownPopulations.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownPopulations.Name = "numericUpDownPopulations";
            this.numericUpDownPopulations.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownPopulations.TabIndex = 6;
            this.numericUpDownPopulations.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // ConfigurationGeneticAlgorithmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 450);
            this.Controls.Add(this.numericUpDownPopulations);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownGeneration);
            this.Name = "ConfigurationGeneticAlgorithmForm";
            this.Text = "ConfigurationGeneticAlgorithmForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGeneration)).EndInit();
            this.groupBoxStrategy.ResumeLayout(false);
            this.groupBoxStrategy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStrategy)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPopulations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownGeneration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBoxStrategy;
        private System.Windows.Forms.Label labelStrategy;
        private System.Windows.Forms.NumericUpDown numericUpDownStrategy;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownPopulations;
    }
}