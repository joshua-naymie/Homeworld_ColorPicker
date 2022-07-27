namespace Homeworld_ColorPicker
{
    partial class DirectoryDialog
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectoryDialog));
            this.OKButton = new System.Windows.Forms.Button();
            this.rootDirInput = new System.Windows.Forms.TextBox();
            this.rootDirLabel = new System.Windows.Forms.Label();
            this.homeworldFoundLabel = new System.Windows.Forms.Label();
            this.profileLabel = new System.Windows.Forms.Label();
            this.profileComboBox = new System.Windows.Forms.ComboBox();
            this.noProfilesLabel = new System.Windows.Forms.Label();
            this.toolkitFoundLabel = new System.Windows.Forms.Label();
            this.toolkitDirLabel = new System.Windows.Forms.Label();
            this.toolkitDirInput = new System.Windows.Forms.TextBox();
            this.dirDescriptionToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gameComboBox = new System.Windows.Forms.ComboBox();
            this.gameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Enabled = false;
            this.OKButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OKButton.Location = new System.Drawing.Point(617, 269);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(92, 35);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // rootDirInput
            // 
            this.rootDirInput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.rootDirInput.Location = new System.Drawing.Point(183, 56);
            this.rootDirInput.Name = "rootDirInput";
            this.rootDirInput.Size = new System.Drawing.Size(526, 25);
            this.rootDirInput.TabIndex = 1;
            this.dirDescriptionToolTip.SetToolTip(this.rootDirInput, "The root directory of the Homeworld instance. Should end in Homeworld2Classic or " +
        "HomeworldRM");
            this.rootDirInput.TextChanged += new System.EventHandler(this.RootDirInputChanged);
            // 
            // rootDirLabel
            // 
            this.rootDirLabel.AutoSize = true;
            this.rootDirLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.rootDirLabel.Location = new System.Drawing.Point(64, 59);
            this.rootDirLabel.Name = "rootDirLabel";
            this.rootDirLabel.Size = new System.Drawing.Size(113, 19);
            this.rootDirLabel.TabIndex = 2;
            this.rootDirLabel.Text = "Root Directory:";
            this.dirDescriptionToolTip.SetToolTip(this.rootDirLabel, "The root directory of the Homeworld instance. Should end in Homeworld2Classic or " +
        "HomeworldRM");
            // 
            // homeworldFoundLabel
            // 
            this.homeworldFoundLabel.AutoSize = true;
            this.homeworldFoundLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.homeworldFoundLabel.Location = new System.Drawing.Point(183, 86);
            this.homeworldFoundLabel.Name = "homeworldFoundLabel";
            this.homeworldFoundLabel.Size = new System.Drawing.Size(0, 19);
            this.homeworldFoundLabel.TabIndex = 3;
            // 
            // profileLabel
            // 
            this.profileLabel.AutoSize = true;
            this.profileLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.profileLabel.Location = new System.Drawing.Point(385, 211);
            this.profileLabel.Name = "profileLabel";
            this.profileLabel.Size = new System.Drawing.Size(50, 19);
            this.profileLabel.TabIndex = 4;
            this.profileLabel.Text = "Profile:";
            // 
            // profileComboBox
            // 
            this.profileComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.profileComboBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.profileComboBox.FormattingEnabled = true;
            this.profileComboBox.Location = new System.Drawing.Point(441, 208);
            this.profileComboBox.Name = "profileComboBox";
            this.profileComboBox.Size = new System.Drawing.Size(224, 25);
            this.profileComboBox.TabIndex = 5;
            // 
            // noProfilesLabel
            // 
            this.noProfilesLabel.AutoSize = true;
            this.noProfilesLabel.ForeColor = System.Drawing.Color.Red;
            this.noProfilesLabel.Location = new System.Drawing.Point(441, 236);
            this.noProfilesLabel.Name = "noProfilesLabel";
            this.noProfilesLabel.Size = new System.Drawing.Size(268, 15);
            this.noProfilesLabel.TabIndex = 6;
            this.noProfilesLabel.Text = "No profiles found. Run Homeworld to create one.";
            this.noProfilesLabel.Visible = false;
            // 
            // toolkitFoundLabel
            // 
            this.toolkitFoundLabel.AutoSize = true;
            this.toolkitFoundLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolkitFoundLabel.Location = new System.Drawing.Point(183, 151);
            this.toolkitFoundLabel.Name = "toolkitFoundLabel";
            this.toolkitFoundLabel.Size = new System.Drawing.Size(0, 19);
            this.toolkitFoundLabel.TabIndex = 9;
            // 
            // toolkitDirLabel
            // 
            this.toolkitDirLabel.AutoSize = true;
            this.toolkitDirLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.toolkitDirLabel.Location = new System.Drawing.Point(50, 124);
            this.toolkitDirLabel.Name = "toolkitDirLabel";
            this.toolkitDirLabel.Size = new System.Drawing.Size(127, 19);
            this.toolkitDirLabel.TabIndex = 8;
            this.toolkitDirLabel.Text = "Toolkit Directory:";
            this.dirDescriptionToolTip.SetToolTip(this.toolkitDirLabel, "The root directory of the Gearbox Toolkit. Should end in GBXTools");
            // 
            // toolkitDirInput
            // 
            this.toolkitDirInput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolkitDirInput.Location = new System.Drawing.Point(183, 121);
            this.toolkitDirInput.Name = "toolkitDirInput";
            this.toolkitDirInput.Size = new System.Drawing.Size(526, 25);
            this.toolkitDirInput.TabIndex = 7;
            this.dirDescriptionToolTip.SetToolTip(this.toolkitDirInput, "The root directory of the Gearbox Toolkit. Should end in GBXTools");
            this.toolkitDirInput.TextChanged += new System.EventHandler(this.ToolkitDirInputChanged);
            // 
            // gameComboBox
            // 
            this.gameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gameComboBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gameComboBox.FormattingEnabled = true;
            this.gameComboBox.Location = new System.Drawing.Point(183, 208);
            this.gameComboBox.Name = "gameComboBox";
            this.gameComboBox.Size = new System.Drawing.Size(106, 25);
            this.gameComboBox.TabIndex = 10;
            // 
            // gameLabel
            // 
            this.gameLabel.AutoSize = true;
            this.gameLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gameLabel.Location = new System.Drawing.Point(129, 211);
            this.gameLabel.Name = "gameLabel";
            this.gameLabel.Size = new System.Drawing.Size(48, 19);
            this.gameLabel.TabIndex = 11;
            this.gameLabel.Text = "Game:";
            // 
            // DirectoryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 337);
            this.Controls.Add(this.gameLabel);
            this.Controls.Add(this.gameComboBox);
            this.Controls.Add(this.toolkitFoundLabel);
            this.Controls.Add(this.toolkitDirLabel);
            this.Controls.Add(this.toolkitDirInput);
            this.Controls.Add(this.noProfilesLabel);
            this.Controls.Add(this.profileComboBox);
            this.Controls.Add(this.profileLabel);
            this.Controls.Add(this.homeworldFoundLabel);
            this.Controls.Add(this.rootDirLabel);
            this.Controls.Add(this.rootDirInput);
            this.Controls.Add(this.OKButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DirectoryDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Directories";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button OKButton;
        private TextBox rootDirInput;
        private Label rootDirLabel;
        private Label homeworldFoundLabel;
        private Label profileLabel;
        private ComboBox profileComboBox;
        private Label noProfilesLabel;
        private Label toolkitFoundLabel;
        private Label toolkitDirLabel;
        private TextBox toolkitDirInput;
        private ToolTip dirDescriptionToolTip;
        private ComboBox gameComboBox;
        private Label gameLabel;
    }
}