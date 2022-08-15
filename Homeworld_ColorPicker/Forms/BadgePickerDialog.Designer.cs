namespace Homeworld_ColorPicker.Forms
{
    partial class BadgePickerDialog
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.backgroundTrackBar = new System.Windows.Forms.TrackBar();
            this.badgesPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.OKButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundTrackBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.backgroundTrackBar);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(584, 124);
            this.topPanel.TabIndex = 2;
            // 
            // backgroundTrackBar
            // 
            this.backgroundTrackBar.LargeChange = 2;
            this.backgroundTrackBar.Location = new System.Drawing.Point(295, 76);
            this.backgroundTrackBar.Maximum = 15;
            this.backgroundTrackBar.Name = "backgroundTrackBar";
            this.backgroundTrackBar.Size = new System.Drawing.Size(286, 45);
            this.backgroundTrackBar.TabIndex = 0;
            this.backgroundTrackBar.TabStop = false;
            this.backgroundTrackBar.Value = 7;
            this.backgroundTrackBar.ValueChanged += new System.EventHandler(this.SetBadgeBackground);
            // 
            // badgesPanel
            // 
            this.badgesPanel.AutoScroll = true;
            this.badgesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.badgesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.badgesPanel.Location = new System.Drawing.Point(0, 124);
            this.badgesPanel.Name = "badgesPanel";
            this.badgesPanel.Size = new System.Drawing.Size(584, 497);
            this.badgesPanel.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 621);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 57);
            this.panel1.TabIndex = 16;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(364, 14);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(101, 31);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(471, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // BadgePickerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(584, 678);
            this.Controls.Add(this.badgesPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BadgePickerDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BadgePickerDialog";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundTrackBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel topPanel;
        private Panel badgesPanel;
        private TrackBar backgroundTrackBar;
        private Panel panel1;
        private Button OKButton;
        private Button button1;
    }
}