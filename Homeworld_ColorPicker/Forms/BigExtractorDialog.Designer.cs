namespace Homeworld_ColorPicker.Forms
{
    partial class BigExtractorDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BigExtractorDialog));
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.startDoneButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.deleteFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // outputTextBox
            // 
            this.outputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(36)))), ((int)(((byte)(86)))));
            this.outputTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.outputTextBox.ForeColor = System.Drawing.Color.White;
            this.outputTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(999, 253);
            this.outputTextBox.TabIndex = 0;
            // 
            // startDoneButton
            // 
            this.startDoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startDoneButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startDoneButton.Location = new System.Drawing.Point(896, 13);
            this.startDoneButton.Name = "startDoneButton";
            this.startDoneButton.Size = new System.Drawing.Size(91, 27);
            this.startDoneButton.TabIndex = 1;
            this.startDoneButton.Text = "Start";
            this.startDoneButton.UseVisualStyleBackColor = true;
            this.startDoneButton.Click += new System.EventHandler(this.StartDonePressed);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(783, 13);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(91, 27);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelPressed);
            // 
            // deleteFilesCheckBox
            // 
            this.deleteFilesCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteFilesCheckBox.AutoSize = true;
            this.deleteFilesCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.deleteFilesCheckBox.Checked = true;
            this.deleteFilesCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteFilesCheckBox.Location = new System.Drawing.Point(590, 18);
            this.deleteFilesCheckBox.Name = "deleteFilesCheckBox";
            this.deleteFilesCheckBox.Size = new System.Drawing.Size(156, 19);
            this.deleteFilesCheckBox.TabIndex = 3;
            this.deleteFilesCheckBox.Text = "Keep necessary files only";
            this.deleteFilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.deleteFilesCheckBox);
            this.panel1.Controls.Add(this.startDoneButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 259);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(999, 52);
            this.panel1.TabIndex = 4;
            // 
            // BigExtractorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 311);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.outputTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(447, 200);
            this.Name = "BigExtractorDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BigExtractorDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Cleanup);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox outputTextBox;
        private Button startDoneButton;
        private Button cancelButton;
        private CheckBox deleteFilesCheckBox;
        private Panel panel1;
    }
}