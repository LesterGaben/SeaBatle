namespace SeaBatle {
    partial class Game {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gameRestartButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameRestartButton
            // 
            this.gameRestartButton.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gameRestartButton.Location = new System.Drawing.Point(686, 640);
            this.gameRestartButton.Name = "gameRestartButton";
            this.gameRestartButton.Size = new System.Drawing.Size(231, 39);
            this.gameRestartButton.TabIndex = 0;
            this.gameRestartButton.Text = "Почати заново гру";
            this.gameRestartButton.UseVisualStyleBackColor = true;
            this.gameRestartButton.Click += new System.EventHandler(this.gameRestartButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(1078, 640);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(236, 33);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Зберегти прогрес гри";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1482, 703);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.gameRestartButton);
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Морський бій";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button gameRestartButton;
        private System.Windows.Forms.Button saveButton;
    }
}