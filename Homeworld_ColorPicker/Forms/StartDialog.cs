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

    /// <summary>
    /// The form used to enter the paths to both the Homeworld instance to be worked on and the Homeworld Toolkit, used to data extraction.
    /// Player profile is also selectable, when needed.
    /// </summary>
    public partial class StartDialog : Form
    {
        // CONSTANTS
        //----------------------------------------
        private const
        int CONFIG_ROOT_DIR = 0,
            CONFIG_TOOLKIT_DIR = 1;
        
        private const
        string DIR_DOCUMENTS_COLORPICKER = @"\Homeworld ColorPicker",
               DIR_DEFAULT_ROOT_PATH = @"C:\Program Files (x86)\Steam\steamapps\common\Homeworld\HomeworldRM",
               DIR_DEFAULT_TOOLKIT_PATH = @"C:\Program Files (x86)\Steam\steamapps\common\Homeworld\GBXTools",
               DIR_PROFILE_PATH = @"\Bin\Profiles",

               FILE_CONFIG = @"\config",
               FILE_HW1_EXE_PATH = @"\exe\Homeworld.exe",
               FILE_HW2_EXE_PATH = @"\Bin\Release\Homeworld2.exe",
               FILE_HWR_EXE_PATH = @"\Bin\Release\HomeworldRM.exe",
               FILE_ARCHIVE_EXE_PATH = @"\WorkshopTool\Archive.exe",

               TEXT_HW1_FOUND = "Homeworld 1 Found - Not Supported!",
               TEXT_HW2_FOUND = "Homeworld 2 Found",
               TEXT_HWR_FOUND = "Homeworld Remastered Found",
               TEXT_HW_NOT_FOUND = "Homeworld Not Found!",
               TEXT_TOOLKIT_FOUND = "Remastered Toolkit Found",
               TEXT_TOOLKIT_NOT_FOUND = "Remastered Toolkit Not Found!";

        private static readonly
        Color LABEL_COLOR_INVALID = System.Drawing.Color.Red,
              LABEL_COLOR_VALID = System.Drawing.Color.Black;

        // INSTANCE VARIABLES
        //----------------------------------------

        /// <summary>
        /// The path of the current root directory entered by the user.
        /// </summary>
        private
        string currentRootDirectory = DIR_DEFAULT_ROOT_PATH;

        /// <summary>
        /// The path of the current toolkit directory entered by the user.
        /// </summary>
        private
        string currentToolkitDirectory = DIR_DEFAULT_TOOLKIT_PATH;

        /// <summary>
        /// The version of Homeworld found at the current root directory.
        /// </summary>
        private
        HomeworldVersion version = HomeworldVersion.NONE;

        private
        bool validRootDir = false,
             validToolkitDir = false,
             validProfile = false;

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for the StartDialog.
        /// </summary>
        public StartDialog()
        {
            InitializeComponent();
            noProfilesLabel.Hide();
            OKButton.Enabled = false;

            string configPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DIR_DOCUMENTS_COLORPICKER + FILE_CONFIG;

            SetDirInputs(configPath);
        }

        // READ CONFIG FILE
        //----------------------------------------

        /// <summary>
        /// Attemps to read the config file and set the <c>rootDirInput</c> and <c>toolkitDirInput</c> text to the saved directories.
        /// If config file doesn't exist or cannot be read, sets both inputs to their default directories.
        /// </summary>
        /// <param name="path">The path of the config file</param>
        private void SetDirInputs(string path)
        {
            bool configRead = false;

            if(Util.CheckPathExists(path))
            {
                string[] configText = System.IO.File.ReadAllLines(path);

                if (configText.Length == 2)
                {
                    rootDirInput.Text = configText[CONFIG_ROOT_DIR];
                    toolkitDirInput.Text = configText[CONFIG_TOOLKIT_DIR];

                    configRead = true;
                }
            }
            
            if(!configRead)
            {
                rootDirInput.Text = DIR_DEFAULT_ROOT_PATH;
                toolkitDirInput.Text = DIR_DEFAULT_TOOLKIT_PATH;
            }
        }

        // EVENTS
        //----------------------------------------

        /// <summary>
        /// Called when the OK button is pressed.
        /// Writes <c>currentRootDirectory</c> and <c>currentToolkitDirectory</c> to the config file in the `My Documents` directory.
        /// Closes this form.
        /// </summary>
        /// <param name="sender">The object that calls the event</param>
        /// <param name="e">The event</param>
        private void OKButton_Click(object sender, EventArgs e)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + DIR_DOCUMENTS_COLORPICKER;

            if (!Util.CheckPathExists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += FILE_CONFIG;

            File.WriteAllTextAsync(path, currentRootDirectory + "\n" + currentToolkitDirectory);

            this.Close();
        }

        //----------------------------------------

        /// <summary>
        /// Called when rootDirInput text is changed.
        /// Validates the root directory input by the user and gets updated profiles.
        /// </summary>
        /// <param name="sender">The object that calls the event</param>
        /// <param name="e">The event</param>
        private void RootDirInputChanged(object sender, EventArgs e)
        {
            ValidateRootDir();
            UpdateProfilesComboBox();

            UpdateOKButton();
        }

        //----------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolkitDirInputChanged(object sender, EventArgs e)
        {
            ValidateToolkitDir();
            UpdateOKButton();
        }

        // PATH VALIDATION
        //----------------------------------------

        /// <summary>
        /// Checks if the root directory path input by the user points to a valid Homeworld root directory. Updates labels.
        /// </summary>
        private void ValidateRootDir()
        {
            validRootDir = false;

            noProfilesLabel.Hide();
            currentRootDirectory = rootDirInput.Text.TrimEnd('\\');

            if (!Util.CheckPathExists(rootDirInput.Text))
            {
                SetHomeworldNotFound();
                version = HomeworldVersion.NONE;
            }

            else
            {
                // check for version specific Homeworld .exe's
                if (Util.CheckPathExists(rootDirInput.Text + FILE_HW1_EXE_PATH))
                {
                    SetHomeworldFound(TEXT_HW1_FOUND);
                    version = HomeworldVersion.HW1;

                    // HW1 not supported
                    //validRootDir = true;
                }
                else if (Util.CheckPathExists(rootDirInput.Text + FILE_HW2_EXE_PATH))
                {
                    SetHomeworldFound(TEXT_HW2_FOUND);
                    version = HomeworldVersion.HW2;
                    validRootDir = true;
                }
                else if (Util.CheckPathExists(rootDirInput.Text + FILE_HWR_EXE_PATH))
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
            
            if (Util.CheckPathExists(profilesPath))
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

        //----------------------------------------

        /// <summary>
        /// Checks if the toolkit directory path input by the user points to a valid Homeworld Remastered Toolkit directory. Updates labels.
        /// </summary>
        private void ValidateToolkitDir()
        {
            currentToolkitDirectory = toolkitDirInput.Text.TrimEnd('\\');

            if(Util.CheckPathExists(currentToolkitDirectory + FILE_ARCHIVE_EXE_PATH))
            {
                validToolkitDir = true;
                SetToolkitLabelFound();
            }
            else
            {
                validToolkitDir = false;
                SetToolkitLabelFound();
            }
        }

        // UI UPDATORS
        //----------------------------------------

        /// <summary>
        /// Checks whether there is currently a valid root and toolkit directory enetered as well as a valid profile selected.
        /// Enables the OK button if all are valid, disables it otherwise.
        /// </summary>
        private void UpdateOKButton()
        {
            OKButton.Enabled = validRootDir && validToolkitDir && validProfile;
        }

        //----------------------------------------

        /// <summary>
        /// Sets the <c>homeworldFoundLabel</c> text and the font color to black.
        /// </summary>
        /// <param name="labelText">The text to set the homeworldFoundLabel to</param>
        private void SetHomeworldFound(string labelText)
        {
            homeworldFoundLabel.Text = labelText;
            homeworldFoundLabel.ForeColor = LABEL_COLOR_VALID;
        }

        //--------------------

        /// <summary>
        /// Sets the <c>homeworldFoundLabel</c> text to inform the user homeworl root directory has not been found and sets the font color to red.
        /// </summary>
        private void SetHomeworldNotFound()
        {
            homeworldFoundLabel.Text = TEXT_HW_NOT_FOUND;
            homeworldFoundLabel.ForeColor = LABEL_COLOR_INVALID;
        }

        //----------------------------------------

        /// <summary>
        /// Sets the <c>toolkitFoundLabel</c> text and color based on the state of validToolkit.
        /// </summary>
        private void SetToolkitLabelFound()
        {
            if(validToolkitDir)
            {
                toolkitFoundLabel.Text = TEXT_TOOLKIT_FOUND;
                toolkitFoundLabel.ForeColor = LABEL_COLOR_VALID;
            }
            else
            {
                toolkitFoundLabel.Text = TEXT_TOOLKIT_NOT_FOUND;
                toolkitFoundLabel.ForeColor = LABEL_COLOR_INVALID;
            }
        }

        // ACCESSORS
        //----------------------------------------

        /// <summary>
        /// Gets the current root directory input by the user.
        /// Meant for access by <c>MainWindow</c> form.
        /// </summary>
        /// <returns>The current root directory input by the user</returns>
        public String GetRootDirectory()
        {
            return currentRootDirectory;
        }

        //----------------------------------------

        /// <summary>
        /// Gets the current toolkit directory input by the user.
        /// Meant for access by <c>MainWindow</c> form.
        /// </summary>
        /// <returns>The current toolkit directory input by the user</returns>
        public String GetToolkitDirectory()
        {
            return currentToolkitDirectory;
        }

        //----------------------------------------

        /// <summary>
        /// Gets the detected Homeworld version at the current root directory input by the user.
        /// Meant for access by <c>MainWindow</c> form.
        /// </summary>
        /// <returns>The detected Homeworld version</returns>
        public HomeworldVersion GetVersion()
        {
            return version;
        }
    }
}