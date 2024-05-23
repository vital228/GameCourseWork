namespace GameСourseWork
{
    partial class StartMenuForm
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
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.buttonNewCup = new System.Windows.Forms.Button();
            this.buttonLookGame = new System.Windows.Forms.Button();
            this.buttonNash = new System.Windows.Forms.Button();
            this.buttonGenetic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Location = new System.Drawing.Point(75, 60);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(150, 50);
            this.buttonNewGame.TabIndex = 0;
            this.buttonNewGame.Text = "Новая игра";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // buttonNewCup
            // 
            this.buttonNewCup.Location = new System.Drawing.Point(75, 137);
            this.buttonNewCup.Name = "buttonNewCup";
            this.buttonNewCup.Size = new System.Drawing.Size(150, 50);
            this.buttonNewCup.TabIndex = 1;
            this.buttonNewCup.Text = "Новый турнир";
            this.buttonNewCup.UseVisualStyleBackColor = true;
            this.buttonNewCup.Click += new System.EventHandler(this.buttonNewCup_Click);
            // 
            // buttonLookGame
            // 
            this.buttonLookGame.Location = new System.Drawing.Point(75, 208);
            this.buttonLookGame.Name = "buttonLookGame";
            this.buttonLookGame.Size = new System.Drawing.Size(150, 50);
            this.buttonLookGame.TabIndex = 2;
            this.buttonLookGame.Text = "Просмотреть игру";
            this.buttonLookGame.UseVisualStyleBackColor = true;
            this.buttonLookGame.Click += new System.EventHandler(this.buttonLookGame_Click);
            // 
            // buttonNash
            // 
            this.buttonNash.Location = new System.Drawing.Point(75, 287);
            this.buttonNash.Name = "buttonNash";
            this.buttonNash.Size = new System.Drawing.Size(150, 48);
            this.buttonNash.TabIndex = 3;
            this.buttonNash.Text = "Равновесие Нэша";
            this.buttonNash.UseVisualStyleBackColor = true;
            this.buttonNash.Click += new System.EventHandler(this.buttonNash_Click);
            // 
            // buttonGenetic
            // 
            this.buttonGenetic.Location = new System.Drawing.Point(75, 360);
            this.buttonGenetic.Name = "buttonGenetic";
            this.buttonGenetic.Size = new System.Drawing.Size(150, 48);
            this.buttonGenetic.TabIndex = 4;
            this.buttonGenetic.Text = "Генетический алгоритм";
            this.buttonGenetic.UseVisualStyleBackColor = true;
            this.buttonGenetic.Click += new System.EventHandler(this.buttonGenetic_Click);
            // 
            // StartMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 499);
            this.Controls.Add(this.buttonGenetic);
            this.Controls.Add(this.buttonNash);
            this.Controls.Add(this.buttonLookGame);
            this.Controls.Add(this.buttonNewCup);
            this.Controls.Add(this.buttonNewGame);
            this.Name = "StartMenuForm";
            this.Text = "StartMenuForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Button buttonNewCup;
        private System.Windows.Forms.Button buttonLookGame;
        private System.Windows.Forms.Button buttonNash;
        private System.Windows.Forms.Button buttonGenetic;
    }
}