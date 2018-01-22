namespace MediaControl
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
            if ( disposing && (components != null) )
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
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.ButtonsPressedLabel = new System.Windows.Forms.Label();
            this.ButtonsPressedTextBox = new System.Windows.Forms.TextBox();
            this.GamepadsLabel = new System.Windows.Forms.Label();
            this.GamepadsComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Interval = 33;
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // ButtonsPressedLabel
            // 
            this.ButtonsPressedLabel.AutoSize = true;
            this.ButtonsPressedLabel.Location = new System.Drawing.Point(9, 55);
            this.ButtonsPressedLabel.Name = "ButtonsPressedLabel";
            this.ButtonsPressedLabel.Size = new System.Drawing.Size(84, 13);
            this.ButtonsPressedLabel.TabIndex = 0;
            this.ButtonsPressedLabel.Text = "Pressed Buttons";
            // 
            // ButtonsPressedTextBox
            // 
            this.ButtonsPressedTextBox.Location = new System.Drawing.Point(12, 71);
            this.ButtonsPressedTextBox.Name = "ButtonsPressedTextBox";
            this.ButtonsPressedTextBox.Size = new System.Drawing.Size(259, 20);
            this.ButtonsPressedTextBox.TabIndex = 1;
            // 
            // GamepadsLabel
            // 
            this.GamepadsLabel.AutoSize = true;
            this.GamepadsLabel.Location = new System.Drawing.Point(9, 9);
            this.GamepadsLabel.Name = "GamepadsLabel";
            this.GamepadsLabel.Size = new System.Drawing.Size(100, 13);
            this.GamepadsLabel.TabIndex = 7;
            this.GamepadsLabel.Text = "Gamepad Selection";
            // 
            // GamepadsComboBox
            // 
            this.GamepadsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GamepadsComboBox.Location = new System.Drawing.Point(12, 25);
            this.GamepadsComboBox.Name = "GamepadsComboBox";
            this.GamepadsComboBox.Size = new System.Drawing.Size(538, 21);
            this.GamepadsComboBox.TabIndex = 8;
            this.GamepadsComboBox.SelectionChangeCommitted += new System.EventHandler(this.GamepadsComboBox_SelectionChangeCommitted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 104);
            this.Controls.Add(this.GamepadsComboBox);
            this.Controls.Add(this.GamepadsLabel);
            this.Controls.Add(this.ButtonsPressedTextBox);
            this.Controls.Add(this.ButtonsPressedLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.Label ButtonsPressedLabel;
        private System.Windows.Forms.TextBox ButtonsPressedTextBox;
        private System.Windows.Forms.Label GamepadsLabel;
        private System.Windows.Forms.ComboBox GamepadsComboBox;
    }
}

