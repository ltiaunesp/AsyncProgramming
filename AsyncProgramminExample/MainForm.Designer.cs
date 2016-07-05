namespace AsyncProgramminExample
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
            this.asyncButton = new System.Windows.Forms.Button();
            this.cont2Label = new System.Windows.Forms.Label();
            this.cont1Label = new System.Windows.Forms.Label();
            this.syncButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // asyncButton
            // 
            this.asyncButton.Location = new System.Drawing.Point(12, 63);
            this.asyncButton.Name = "asyncButton";
            this.asyncButton.Size = new System.Drawing.Size(133, 23);
            this.asyncButton.TabIndex = 1;
            this.asyncButton.Text = "Start Async";
            this.asyncButton.UseVisualStyleBackColor = true;
            this.asyncButton.Click += new System.EventHandler(this.asyncButton_Click);
            // 
            // cont2Label
            // 
            this.cont2Label.AutoSize = true;
            this.cont2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cont2Label.Location = new System.Drawing.Point(208, 9);
            this.cont2Label.Name = "cont2Label";
            this.cont2Label.Size = new System.Drawing.Size(24, 26);
            this.cont2Label.TabIndex = 3;
            this.cont2Label.Text = "?";
            // 
            // cont1Label
            // 
            this.cont1Label.AutoSize = true;
            this.cont1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cont1Label.Location = new System.Drawing.Point(63, 9);
            this.cont1Label.Name = "cont1Label";
            this.cont1Label.Size = new System.Drawing.Size(24, 26);
            this.cont1Label.TabIndex = 4;
            this.cont1Label.Text = "?";
            // 
            // syncButton
            // 
            this.syncButton.Location = new System.Drawing.Point(151, 63);
            this.syncButton.Name = "syncButton";
            this.syncButton.Size = new System.Drawing.Size(133, 23);
            this.syncButton.TabIndex = 2;
            this.syncButton.Text = "Start Sync";
            this.syncButton.UseVisualStyleBackColor = true;
            this.syncButton.Click += new System.EventHandler(this.syncButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(298, 97);
            this.Controls.Add(this.cont1Label);
            this.Controls.Add(this.cont2Label);
            this.Controls.Add(this.syncButton);
            this.Controls.Add(this.asyncButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Async Programming Example";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button asyncButton;
        private System.Windows.Forms.Button syncButton;
        private System.Windows.Forms.Label cont2Label;
        private System.Windows.Forms.Label cont1Label;
    }
}

