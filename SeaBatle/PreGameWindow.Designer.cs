namespace SeaBatle {
    partial class PreGameWindow {
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
            this.veryBigShipCheckBox = new System.Windows.Forms.CheckBox();
            this.bigShipCheckBox = new System.Windows.Forms.CheckBox();
            this.middleShipCheckBox = new System.Windows.Forms.CheckBox();
            this.smallShipCheckBox = new System.Windows.Forms.CheckBox();
            this.resumeButton = new System.Windows.Forms.Button();
            this.autoLocation = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // veryBigShipCheckBox
            // 
            this.veryBigShipCheckBox.AutoSize = true;
            this.veryBigShipCheckBox.Location = new System.Drawing.Point(704, 286);
            this.veryBigShipCheckBox.Name = "veryBigShipCheckBox";
            this.veryBigShipCheckBox.Size = new System.Drawing.Size(168, 20);
            this.veryBigShipCheckBox.TabIndex = 1;
            this.veryBigShipCheckBox.Text = "4 палубний корабель";
            this.veryBigShipCheckBox.UseVisualStyleBackColor = true;
            this.veryBigShipCheckBox.CheckedChanged += new System.EventHandler(this.veryBigShipCheckBox_CheckedChanged);
            // 
            // bigShipCheckBox
            // 
            this.bigShipCheckBox.AutoSize = true;
            this.bigShipCheckBox.Location = new System.Drawing.Point(704, 313);
            this.bigShipCheckBox.Name = "bigShipCheckBox";
            this.bigShipCheckBox.Size = new System.Drawing.Size(168, 20);
            this.bigShipCheckBox.TabIndex = 2;
            this.bigShipCheckBox.Text = "3 палубний корабель";
            this.bigShipCheckBox.UseVisualStyleBackColor = true;
            this.bigShipCheckBox.CheckedChanged += new System.EventHandler(this.bigShipCheckBox_CheckedChanged);
            // 
            // middleShipCheckBox
            // 
            this.middleShipCheckBox.AutoSize = true;
            this.middleShipCheckBox.Location = new System.Drawing.Point(704, 340);
            this.middleShipCheckBox.Name = "middleShipCheckBox";
            this.middleShipCheckBox.Size = new System.Drawing.Size(168, 20);
            this.middleShipCheckBox.TabIndex = 3;
            this.middleShipCheckBox.Text = "2 палубний корабель";
            this.middleShipCheckBox.UseVisualStyleBackColor = true;
            this.middleShipCheckBox.CheckedChanged += new System.EventHandler(this.middleShipCheckBox_CheckedChanged);
            // 
            // smallShipCheckBox
            // 
            this.smallShipCheckBox.AutoSize = true;
            this.smallShipCheckBox.Location = new System.Drawing.Point(704, 367);
            this.smallShipCheckBox.Name = "smallShipCheckBox";
            this.smallShipCheckBox.Size = new System.Drawing.Size(168, 20);
            this.smallShipCheckBox.TabIndex = 4;
            this.smallShipCheckBox.Text = "1 палубний корабель";
            this.smallShipCheckBox.UseVisualStyleBackColor = true;
            this.smallShipCheckBox.CheckedChanged += new System.EventHandler(this.smallShipCheckBox_CheckedChanged);
            // 
            // resumeButton
            // 
            this.resumeButton.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resumeButton.Location = new System.Drawing.Point(400, 620);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(200, 40);
            this.resumeButton.TabIndex = 5;
            this.resumeButton.Text = "Продовжити";
            this.resumeButton.UseMnemonic = false;
            this.resumeButton.UseVisualStyleBackColor = true;
            this.resumeButton.Click += new System.EventHandler(this.resumeButton_Click);
            // 
            // autoLocation
            // 
            this.autoLocation.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.autoLocation.Location = new System.Drawing.Point(630, 461);
            this.autoLocation.Name = "autoLocation";
            this.autoLocation.Size = new System.Drawing.Size(99, 38);
            this.autoLocation.TabIndex = 6;
            this.autoLocation.Text = "Авто";
            this.autoLocation.UseVisualStyleBackColor = true;
            this.autoLocation.Click += new System.EventHandler(this.autoLocation_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(786, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "зберегти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PreGameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(982, 703);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.autoLocation);
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.smallShipCheckBox);
            this.Controls.Add(this.middleShipCheckBox);
            this.Controls.Add(this.bigShipCheckBox);
            this.Controls.Add(this.veryBigShipCheckBox);
            this.Name = "PreGameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Морський бій";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox veryBigShipCheckBox;
        private System.Windows.Forms.CheckBox bigShipCheckBox;
        private System.Windows.Forms.CheckBox middleShipCheckBox;
        private System.Windows.Forms.CheckBox smallShipCheckBox;
        private System.Windows.Forms.Button resumeButton;
        private System.Windows.Forms.Button autoLocation;
        private System.Windows.Forms.Button button1;
    }
}