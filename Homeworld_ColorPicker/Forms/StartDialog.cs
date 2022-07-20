namespace Homeworld_ColorPicker
{
    using Forms;

    /// <summary>
    /// The different versions of Homeworld that can be detected.
    /// </summary>
    public enum HomeworldVersion
    {
        HW1,
        HW2,
        HWR,
        NONE
    }

    public partial class StartDialog : Form
    {
        // CONSTANTS
        //----------------------------------------
        private const
        string DIR_DOCUMENTS_COLORPICKER = @"\Homeworld ColorPicker",
               DIR_DEFAULT_PATH = @"C:\Program Files (x86)\Steam\steamapps\common\Homeworld\HomeworldRM",
               DIR_PROFILE_PATH = @"\Bin\Profiles",

               FILE_CONFIG = @"\config",
               FILE_HW1_EXE_PATH = @"\exe\Homeworld.exe",
               FILE_HW2_EXE_PATH = @"\Bin\Release\Homeworld2.exe",
               FILE_HWR_EXE_PATH = @"\Bin\Release\HomeworldRM.exe",

               TEXT_HW1_FOUND = "Homeworld 1 Found - Not Supported!",
               TEXT_HW2_FOUND = "Homeworld 2 Found",
               TEXT_HWR_FOUND = "Homeworld Remastered Found",
               TEXT_HW_NOT_FOUND = "Homeworld Not Found!";

        // INSTANCE VARIABLES
        //----------------------------------------

        private
        Color HW_NOT_FOUND_COLOR = System.Drawing.Color.Red,
              HW_FOUND_COLOR = System.Drawing.Color.Black;

        /// <summary>
        /// The path of the current directory entered by the user.
        /// </summary>
        private
        string currentDirectory = DIR_DEFAULT_PATH;

        /// <summary>
        /// The version of Homeworld found at the current root directory.
        /// </summary>
        private
        HomeworldVersion version = HomeworldVersion.NONE;

        private
        bool validRootDir = false,
             validProfile = false;

        // CONSTRUCTOR
        //----------------------------------------

        public StartDialog()
        {
            InitializeComponent();
            noProfilesLabel.Hide();
            OKButton.Enabled = false;

            string configPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DIR_DOCUMENTS_COLORPICKER + FILE_CONFIG;

            rootDirInput.Text = Util.checkPathExists(configPath) ? ReadConfigPath(configPath)
                                                                 : DIR_DEFAULT_PATH;
        }

        // READ CONFIG FILE
        //----------------------------------------

        /// <summary>
        /// Reads the users config file in the users My Documents directory. If the config file is more than 1 line, returns the default path.
        /// </summary>
        /// <param name="path">The file path of the config file</param>
        /// <returns>The root directory path saved in the config file or the default path if config file does not exist or is invalid.</returns>
        private string ReadConfigPath(string path)
        {
            string[] configText = System.IO.File.ReadAllLines(path);

            return configText.Length == 1 ? configText[0] : DIR_DEFAULT_PATH;
        }

        // EVENTS
        //----------------------------------------

        /// <summary>
        /// Called when the OK button is pressed.
        /// Writes currentDirectory to the config file in the `My Documents` directory.
        /// Closes this form.
        /// </summary>
        /// <param name="sender">The object that calls the event</param>
        /// <param name="e">The event</param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DIR_DOCUMENTS_COLORPICKER;

            if (!Util.checkPathExists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += FILE_CONFIG;

            File.WriteAllTextAsync(path, currentDirectory);

            this.Close();
        }

        //----------------------------------------

        /// <summary>
        /// Called when rootDirInput text is changed.
        /// Validates the root directory input by the user and gets updated profiles.
        /// </summary>
        /// <param name="sender">The object that calls the event</param>
        /// <param name="e">The event</param>
        private void CheckRootInput(object sender, EventArgs e)
        {
            ValidateRootDir();
            UpdateProfilesComboBox();


            OKButton.Enabled = validRootDir && validProfile;
        }

        // PATH VALIDATION
        //----------------------------------------

        /// <summary>
        /// Checks if the path input by the user points to a valid Homeworld root directory. Updates labels.
        /// </summary>
        private void ValidateRootDir()
        {
            validRootDir = false;

            noProfilesLabel.Hide();
            currentDirectory = rootDirInput.Text.TrimEnd('\\');

            if (!Util.checkPathExists(rootDirInput.Text))
            {
                SetHomeworldNotFound();
                version = HomeworldVersion.NONE;
            }

            else
            {
                // check for version specific Homeworld .exe's
                if (Util.checkPathExists(rootDirInput.Text + FILE_HW1_EXE_PATH))
                {
                    SetHomeworldFound(TEXT_HW1_FOUND);
                    version = HomeworldVersion.HW1;

                    // HW1 not supported
                    //validRootDir = true;
                }
                else if (Util.checkPathExists(rootDirInput.Text + FILE_HW2_EXE_PATH))
                {
                    SetHomeworldFound(TEXT_HW2_FOUND);
                    version = HomeworldVersion.HW2;
                    validRootDir = true;
                }
                else if (Util.checkPathExists(rootDirInput.Text + FILE_HWR_EXE_PATH))
                {
                    SetHomeworldFound(TEXT_HWR_FOUND);
                    version = HomeworldVersion.HWR;
                    validRootDir = true;
                }
                else
                {
                    SetHomeworldNotFound();
                    version = HomeworldVersion.NONE;
                }
            }
        }

        //----------------------------------------

        /// <summary>
        /// Gets all profiles at the current root directory and updates the profile ComboBox.
        /// </summary>
        private void UpdateProfilesComboBox()
        {
            validProfile = false;
            profileComboBox.Items.Clear();

            switch (version)
            {
                case HomeworldVersion.HW1:
                    profileComboBox.Enabled = false;

                    // no proiles in HW1
                    validProfile = true;
                    break;

                case HomeworldVersion.HW2:
                    profileComboBox.Enabled = true;
                    
                    profileComboBox.Items.AddRange(GetAllProfiles());
                    break;

                case HomeworldVersion.HWR:
                    profileComboBox.Enabled = true;
                    profileComboBox.Items.AddRange(GetAllProfiles());
                    break;

                case HomeworldVersion.NONE:
                    profileComboBox.Enabled = false;
                    break;
            }

            if(profileComboBox.Items.Count > 0)
            {
                profileComboBox.SelectedIndex = 0;
            }
            else
            {
                profileComboBox.Text = "";
                profileComboBox.Enabled = false;
            }
        }

        //--------------------

        /// <summary>
        /// Gets all directory names within the profile directory of the current valid root directory.
        /// </summary>
        /// <returns>All profiles in the current valid root directory</returns>
        private string[] GetAllProfiles()
        {
            string profilesPath = rootDirInput.Text + DIR_PROFILE_PATH;
            
            if (Util.checkPathExists(profilesPath))
            {
                string[] paths = Directory.GetDirectories(profilesPath),
                         profiles = new string[paths.Length];

                // remove paths except directory names
                int i = 0;
                foreach (string path in paths)
                {
                    string[] temp = path.Split('\\');

                    profiles[i++] = temp[temp.Length - 1];
                }

                // if no profiles found
                if (profiles.Length == 0)
                {
                    noProfilesLabel.Show();
                }
                else
                {
                    validProfile = true;
                }

                return profiles;
            }

            // if 'profile' directory doesnt exist
            noProfilesLabel.Show();

            return new string[0];
        }

        // HOMEWORLD FOUND LABEL
        //----------------------------------------

        /// <summary>
        /// Sets the homeworldFoundLabel text and the font color to black.
        /// </summary>
        /// <param name="labelText">The text to set the homeworldFoundLabel to</param>
        private void SetHomeworldFound(string labelText)
        {
            homeworldFoundLabel.Text = labelText;
            homeworldFoundLabel.ForeColor = HW_FOUND_COLOR;
        }

        //--------------------

        /// <summary>
        /// Sets the homeworldFoundLabel text to inform the user homeworl root directory has not been found and sets the font color to red.
        /// </summary>
        private void SetHomeworldNotFound()
        {
            homeworldFoundLabel.Text = TEXT_HW_NOT_FOUND;
            homeworldFoundLabel.ForeColor = HW_NOT_FOUND_COLOR;
        }

        // ACCESSORS
        //----------------------------------------

        /// <summary>
        /// Gets the current root directory input by the user.
        /// Meant for access by MainWindow form.
        /// </summary>
        /// <returns>The current root directory input by the user</returns>
        public String GetRootDirectory()
        {
            return currentDirectory;
        }

        //----------------------------------------

        /// <summary>
        /// Gets the detected Homeworld version at the current root directory input by the user.
        /// Meant for access by MainWindow form.
        /// </summary>
        /// <returns>The detected Homeworld version</returns>
        public HomeworldVersion GetVersion()
        {
            return version;
        }
    }
}