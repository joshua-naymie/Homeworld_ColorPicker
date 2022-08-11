namespace Homeworld_ColorPicker
{
    using Forms;
    using Objects;

    public enum RemasteredGame
    {
        HW1,
        HW2,
        NONE
    }

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
    /// The form used to enter the paths to both the Homeworld instance to be worked on and the Homeworld Toolkit, used for data extraction.
    /// Player profile is also selectable, when needed.
    /// </summary>
    public partial class DirectoryDialog : Form
    {
        // CONSTANTS
        //----------------------------------------

        private const
        int CONFIG_ROOT_DIR = 0,
            CONFIG_TOOLKIT_DIR = 1;
        
        private const
        string DIR_DEFAULT_ROOT_PATH = @"C:\Program Files (x86)\Steam\steamapps\common\Homeworld\HomeworldRM",
               DIR_DEFAULT_TOOLKIT_PATH = @"C:\Program Files (x86)\Steam\steamapps\common\Homeworld\GBXTools",

               FILE_CONFIG = @"\config",
               FILE_HW1_EXE_PATH = @"\exe\Homeworld.exe",
               FILE_HW2_EXE_PATH = @"\Bin\Release\Homeworld2.exe",
               FILE_HWR_EXE_PATH = @"\Bin\Release\HomeworldRM.exe",
               FILE_NAME_DAT = @"\name.dat",

               TEXT_HW1_FOUND = "Homeworld 1 Found - Not Supported!",
               TEXT_HW2_FOUND = "Homeworld 2 Found - Not Supported!",
               TEXT_HWR_FOUND = "Homeworld Remastered Found",
               TEXT_HW_NOT_FOUND = "Homeworld Not Found!",
               TEXT_TOOLKIT_FOUND = "Remastered Toolkit Found",
               TEXT_TOOLKIT_NOT_FOUND = "Remastered Toolkit Not Found!",
            
               TEXT_REMASTERED_HOMEWORLD_2 = "Homeworld 2",
               TEXT_REMASTERED_HOMEWORLD_1 = "Homeworld 1";


        /// <summary>
        /// Array of every remastered game represented as a text name.
        /// Used for Game ComboBox.
        /// </summary>
        private static readonly
        string[] REMASTERED_GAME_TYPES = { TEXT_REMASTERED_HOMEWORLD_2, TEXT_REMASTERED_HOMEWORLD_1 };

        /// <summary>
        /// Dictionary of remastered games (HW1, HW2) accessible through their text names.
        /// </summary>
        private static readonly
        Dictionary<string, RemasteredGame> REMASTERED_GAME_DICTIONARY = new Dictionary<string, RemasteredGame>
        {
            { TEXT_REMASTERED_HOMEWORLD_2, RemasteredGame.HW2 },
            { TEXT_REMASTERED_HOMEWORLD_1, RemasteredGame.HW1 }
        };

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

        /// <summary>
        /// Whether a valid Homeworld root directory has been entered.
        /// </summary>
        private
        bool validRootDir = false;

        /// <summary>
        /// Whether a valid Toolkit root directory has been entered. 
        /// </summary>
        private
        bool validToolkitDir = false;

        /// <summary>
        /// Whether a valid profile has been selected.
        /// </summary>
        private
        bool validProfile = false;

        // PROPERTIES
        //----------------------------------------

        /// <summary>
        /// The instance of the game to work on.
        /// Holds root directories and remastered game specifications.
        /// </summary>
        public GameInstance GameInstance { get; private set; }

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for the DirectoryDialog.
        /// </summary>
        public DirectoryDialog()
        {
            InitializeComponent();
        }

        // READ CONFIG FILE
        //----------------------------------------

        /// <summary>
        /// Sets the root directories for Homeworld and the Remastered Toolkit.
        /// </summary>
        public void SetRootDirectories(RootDirectoryData rootDirectories)
        {
            rootDirInput.Text = rootDirectories.HomeworldRoot;
            toolkitDirInput.Text = rootDirectories.ToolkitRoot;
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
            IO.ConfigManager.WriteConfig(new RootDirectoryData(currentRootDirectory, currentToolkitDirectory));

            RemasteredGame game = REMASTERED_GAME_DICTIONARY[(string)gameComboBox.SelectedItem];
            string profile = ((Profile)profileComboBox.SelectedItem).Path;

            GameInstance = new GameInstance(currentRootDirectory, currentToolkitDirectory, version, game, profile);

            this.DialogResult = DialogResult.OK;
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

            if (!Util.PathExists(rootDirInput.Text))
            {
                SetHomeworldNotFound();
                version = HomeworldVersion.NONE;
            }
            else
            {
                // check for version specific Homeworld .exe's
                if (Util.PathExists(rootDirInput.Text + FILE_HW1_EXE_PATH))
                {
                    SetHomeworldFound(TEXT_HW1_FOUND);
                    version = HomeworldVersion.HW1;

                    // HW1 not supported
                    //validRootDir = true;
                }
                else if (Util.PathExists(rootDirInput.Text + FILE_HW2_EXE_PATH))
                {
                    SetHomeworldFound(TEXT_HW2_FOUND);
                    version = HomeworldVersion.HW2;
                    validRootDir = false;
                }
                else if (Util.PathExists(rootDirInput.Text + FILE_HWR_EXE_PATH))
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
        /// Updates the profile ComboBox with all valid profiles at the current root directory.
        /// </summary>
        private void UpdateProfilesComboBox()
        {
            validProfile = false;
            profileComboBox.Items.Clear();

            switch (version)
            {
                case HomeworldVersion.HW1:
                    profileComboBox.Enabled = false;
                    gameComboBox.Enabled = false;

                    // no proiles in HW1
                    validProfile = true;
                    DisableGameComboBox();
                    break;

                case HomeworldVersion.HW2:
                    //HW2 not currently supported

                    profileComboBox.Enabled = false;
                    gameComboBox.Enabled = false;
                    //profileComboBox.Items.AddRange(GetAllProfiles());
                    DisableGameComboBox();
                    break;

                case HomeworldVersion.HWR:
                    profileComboBox.Enabled = true;
                    gameComboBox.Enabled = true;
                    profileComboBox.Items.AddRange(GetAllProfiles());
                    EnableGameComboBox();
                    break;

                case HomeworldVersion.NONE:
                    profileComboBox.Enabled = false;
                    gameComboBox.Enabled = false;
                    DisableGameComboBox();
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
        /// Gets all valid profile names and their directories within the profile directory of the current valid root directory.
        /// </summary>
        /// <returns>All valid profiles in the current valid root directory</returns>
        private Profile[] GetAllProfiles()
        {
            string profilesPath = rootDirInput.Text + CONST.DIR_PROFILES_PATH;
            
            if (Util.PathExists(profilesPath))
            {
                string[] paths = Directory.GetDirectories(profilesPath);
                Profile[] profiles = new Profile[paths.Length];

                // remove paths except directory names
                int i = 0;
                foreach (string path in paths)
                {
                    if(Util.PathExists(path + FILE_NAME_DAT)
                    && Util.PathExists(path + CONST.FILE_PLAYERCFG_LUA))
                    {
                        string profilePath = path.Replace(currentRootDirectory, "");
                        string profileName = File.ReadAllText(path + FILE_NAME_DAT);

                        profiles[i++] = new Profile(profilePath, profileName);
                    }
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

            return Array.Empty<Profile>();
        }

        //----------------------------------------

        /// <summary>
        /// Checks if the toolkit directory path input by the user points to a valid Homeworld Remastered Toolkit directory. Updates labels.
        /// </summary>
        private void ValidateToolkitDir()
        {
            currentToolkitDirectory = toolkitDirInput.Text.TrimEnd('\\');

            if(Util.PathExists(currentToolkitDirectory + CONST.FILE_ARCHIVE_EXE_PATH))
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

        //----------------------------------------

        /// <summary>
        /// Enables the Remastered game ComboBox.
        /// Selects the first value.
        /// </summary>
        private void EnableGameComboBox()
        {
            gameComboBox.Enabled = true;
            gameComboBox.Items.AddRange(REMASTERED_GAME_TYPES);
            gameComboBox.SelectedIndex = 0;
        }

        //--------------------

        /// <summary>
        /// Disables the Remastered game ComboBox and clears all items.
        /// </summary>
        private void DisableGameComboBox()
        {
            gameComboBox.Enabled = false;
            gameComboBox.Items.Clear();
        }
    }
}