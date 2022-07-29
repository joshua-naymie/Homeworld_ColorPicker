using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Homeworld_ColorPicker.Forms
{
    using Objects;

    /// <summary>
    /// States of the .big extraction process.
    /// </summary>
    enum ExtractorSate
    {
        DialogOpened,
        ExtractionInProgress,
        ExtractionFinished,
        ExtractionFailed,
        Done
    }
    /// <summary>
    /// Form for extracting and saving the required, or all, files from a Homeworld .big file.
    /// </summary>
    public partial class BigExtractorDialog : Form
    {
        // CONSTANTS
        //----------------------------------------

        private const
        string TEXT_START_DIALOG_FORMAT = "Required files need to be extracted from the {0} file. This will take {1} of space on your {2} drive.\r\n"
                                        + "All unnecessary files will be deleted after extraction unless \"{3}\" is unchecked.\r\n\r\n"
                                        + "Press start to begin extracting...\r\n\r\n",

               TEXT_BIG_NOT_FOUND = "ERROR: .big file not found! Please verify your Homeworld Remastered game files.",

               OUTPUT_FORMAT = "{0}\r\n",
               TEXT_DONE = "Done",

               TEXT_INFO_DIALOG_TITLE = "Files not found!",
               TEXT_INFO_DIALOG_MESSAGE = "We couldn't find the required files!\n"
                                        + "They need to be extracted from the .big file to proceed.",
            
               TEXT_CANCEL_EXTRACTING_TITLE = "Cancel Extraction?",
               TEXT_CANCEL_EXTRACTING_MESSAGE = "These files are required.\n"
                                              + "Canceling will close the program.\n\n"
                                              + "Are you sure you want to cancel?",
            
               TEXT_EXTRACTION_COMPLETE = "Extraction finished!\r\n",
               TEXT_MOVE_REQUIRED_FILES = "Saving required files...",
               TEXT_MOVE_ALL_FILES = "Saving all extracted files...",
               TEXT_CLEARING_OUTPUT_DIR = "Cleaning up...",
               TEXT_VERIFY_REQUIRED_FILES = "Verifying required files...\r\n",
               TEXT_VERIFICATION_FAILED = "ERROR: File verifactaion failed for an unknown reason.\r\n",

               TEXT_ALL_FINISHED = "Process complete.",
               TEXT_PROGRAM_EXIT = "Program will now exit.\r\n",
               TEXT_DONE_TO_PROCEED = "Click 'Done' to proceed.";

        private static readonly
        Dictionary<RemasteredGame, string> DICT_REMASTERED_BIG_FILE_NAMES = new Dictionary<RemasteredGame, string>
        { 
            { RemasteredGame.HW2, "HW2Campaign.big" },
            { RemasteredGame.HW1, "HW1Campaign.big" }
        };

        private static readonly
        Dictionary<RemasteredGame, string> DICT_REMASTERED_BIG_FILE_SIZES = new Dictionary<RemasteredGame, string>
        {
            { RemasteredGame.HW2, "~110 MB" },
            { RemasteredGame.HW1, "~200 MB" }
        };

        // INSTANCE
        //----------------------------------------

        /// <summary>
        /// The current state of the extraction process.
        /// </summary>
        private
        ExtractorSate currentState;

        /// <summary>
        /// The game instance to work on.
        /// </summary>
        private
        GameInstance instance;

        /// <summary>
        /// Handles the extraction process of the .big file.
        /// </summary>
        private
        IO.BigExtractor extractor;

        // CONSTRUCTOR
        //----------------------------------------

        /// <summary>
        /// Constructor for BigExtractorDialog.
        /// </summary>
        /// <param name="instance">The game instance to work on</param>
        public BigExtractorDialog(GameInstance instance)
        {
            this.instance = instance;
            this.Shown += ShowInfoDialog;

            InitializeComponent();

            extractor = new IO.BigExtractor(instance, AppendOutput);

            if (CheckBigFileExists())
            {
                currentState = ExtractorSate.DialogOpened;
                
                PostWelcomeMessage();
            }
            else
            {
                currentState = ExtractorSate.ExtractionFailed;
                
                cancelButton.Enabled = false;
                startDoneButton.Text = TEXT_DONE;

                AppendOutput(TEXT_BIG_NOT_FOUND);
                AppendOutput(TEXT_PROGRAM_EXIT);
                AppendOutput(TEXT_DONE_TO_PROCEED);
            }
        }



        // EVENTS
        //----------------------------------------

        /// <summary>
        /// Overrides the OnFormClosing event.
        /// Confirms that the user wants to close without extracting required files.
        /// Does not confirm if Windows is shutting down.
        /// </summary>
        /// <param name="e">The FormClosing event args</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if(e.CloseReason != CloseReason.WindowsShutDown
            && currentState != ExtractorSate.Done
            && !ShowExitConfirmationDialog(TEXT_CANCEL_EXTRACTING_MESSAGE, TEXT_CANCEL_EXTRACTING_TITLE))
            {
                e.Cancel = true;
            }

            base.OnFormClosing(e);
        }

        /// <summary>
        /// Called when the startDoneButton's Click event is triggered.
        /// Starts the extraction process is not started yet. Sets the form's Dialog result if the extraction ends.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        /// <exception cref="InvalidOperationException"></exception>
        private void StartDonePressed(object? sender, EventArgs e)
        {
            switch(currentState)
            {
                case ExtractorSate.DialogOpened:
                    StartExtraction();
                    break;

                case ExtractorSate.ExtractionFailed:
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case ExtractorSate.Done:
                    this.DialogResult = DialogResult.OK;
                    break;

                default:
                    throw new InvalidOperationException("startDone Button should not be clickable but even was called.");
            }
        }

        //----------------------------------------

        /// <summary>
        /// Called when the cancelButton's Click event is triggered.
        /// Closes the form is extraction hasn't started.
        /// Gets user confirmation to cancel extraction if started.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void CancelPressed(object? sender, EventArgs e)
        {
            switch (currentState)
            {
                case ExtractorSate.DialogOpened:
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case ExtractorSate.ExtractionInProgress:
                    ConfirmCancelExtraction();
                    break;
            }
        }

        //----------------------------------------

        /// <summary>
        /// Called when the form's Shown event is triggered.
        /// Shows a dialog to the user informing them of why the form is being shown.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void ShowInfoDialog(object? sender, EventArgs e)
        {
            MessageBox.Show(TEXT_INFO_DIALOG_MESSAGE, TEXT_INFO_DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //----------------------------------------

        /// <summary>
        /// Called when this forms FormClosing event is triggered.
        /// Kills the extraction process if it has started and clears the extraction output directory.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">The event arguments</param>
        private void Cleanup(object? sender, FormClosingEventArgs e)
        {
            if (extractor.HasStarted)
            {
                extractor.KillProcess();
                IO.ExtractedDataManager.ClearOutputDir();
            }
        }

        // EXTRACTION PIPELINE
        //----------------------------------------

        /// <summary>
        /// Starts the extraction as a threaded <c>Task</c>.
        /// Sets the current state to 'extraction in progress' and updates UI controls.
        /// </summary>
        private void StartExtraction()
        {
            currentState = ExtractorSate.ExtractionInProgress;

            startDoneButton.Enabled = false;
            startDoneButton.Text = TEXT_DONE;

            cancelButton.Enabled = true;

            //----------

            Task t = new Task(extractor.ExtractBigFile);
            t.ContinueWith(ExtractionThreadEnded);
            t.Start();
        }

        //----------------------------------------

        /// <summary>
        /// Asynchonously called when extraction task is completed.
        /// Invokes non-asynchronous method notifying the form the task is completed.
        /// </summary>
        /// <param name="t">The task that completed</param>
        private void ExtractionThreadEnded(Task t)
        {
            Action safeAction = delegate { ExtractionFinished(); };
            this.Invoke(safeAction);
        }

        //--------------------

        /// <summary>
        /// Called when the extraction process completes.
        /// Moves desired files to specific data directory, clears extraction output directory, and verifies required files are present.
        /// Updates UI controls.
        /// </summary>
        private void ExtractionFinished()
        {
            currentState = ExtractorSate.ExtractionFinished;
            cancelButton.Enabled = false;

            AppendOutput(TEXT_EXTRACTION_COMPLETE);

            deleteFilesCheckBox.Enabled = false;
            if (deleteFilesCheckBox.Checked)
            {
                AppendOutput(TEXT_MOVE_REQUIRED_FILES);
                IO.ExtractedDataManager.MoveRequiredFiles(instance);
            }
            else
            {
                AppendOutput(TEXT_MOVE_ALL_FILES);
                IO.ExtractedDataManager.MoveAllFiles(instance);
            }

            AppendOutput(TEXT_CLEARING_OUTPUT_DIR);
            IO.ExtractedDataManager.ClearOutputDir();

            AppendOutput(TEXT_VERIFY_REQUIRED_FILES);
            if(IO.ExtractedDataManager.VerifyRequiredFiles(instance))
            {
                currentState = ExtractorSate.Done;
                AppendOutput(TEXT_ALL_FINISHED);
                AppendOutput(TEXT_DONE_TO_PROCEED);
            }
            else
            {
                currentState = ExtractorSate.ExtractionFailed;
                AppendOutput(TEXT_VERIFICATION_FAILED);
                AppendOutput(TEXT_PROGRAM_EXIT);
                AppendOutput(TEXT_DONE_TO_PROCEED);
            }

            startDoneButton.Enabled = true;
            startDoneButton.Text = TEXT_DONE;
        }

        // CHECK BIG FILES
        //----------------------------------------

        /// <summary>
        /// Checks whether the appropriate .big file exists in the correct location.
        /// Uses game version to determing appropriate .big file.
        /// </summary>
        /// <returns>True if the file is present in the correct location, false otherwise</returns>
        /// <exception cref="Exceptions.InvalidVersionException">Thrown if the game version is invalid or not supported</exception>
        private bool CheckBigFileExists()
        {
            switch(instance.Version)
            {
                case HomeworldVersion.HWR:
                    return CheckRemasteredBigFileExists();

                default:
                    throw new Exceptions.InvalidVersionException("Homeworld version is not implemented yet: " + instance.Version);
            }
        }

        //--------------------

        /// <summary>
        /// Checks whether the appropriate Homeworld Remastered .big file exists in the correct location.
        /// Uses remastered game to determing appropriate .big file.
        /// </summary>
        /// <returns>True if the file is present in the correct location, false otherwise</returns>
        /// <exception cref="Exceptions.InvalidRemasteredGameException">Thrown if the Homeworld Remastered game is invalid</exception>
        private bool CheckRemasteredBigFileExists()
        {
            switch(instance.RemasteredGame)
            {
                case RemasteredGame.HW2:
                    return Util.PathExists(instance.HomeworldRootDir + GC.FILE_HW2_RM_BIG);

                case RemasteredGame.HW1:
                    return Util.PathExists(instance.HomeworldRootDir + GC.FILE_HW1_RM_BIG);

                default:
                    throw new Exceptions.InvalidRemasteredGameException("Cannot find big file for remastered game: " + instance.RemasteredGame);
            }
        }

        // UI & DIALOGS
        //----------------------------------------

        /// <summary>
        /// Posts a welcom message in the output window with specific details based on the Homeworld Version and Remastered game.
        /// </summary>
        /// <exception cref="Exceptions.InvalidVersionException">Thrown if the Homeworld version is not supported</exception>
        private void PostWelcomeMessage()
        {
            string bigFileName,
                   extractedFileSize,
                   documentsDriveLetter = Path.GetPathRoot(GC.DIR_DOCUMENTS_PATH).Substring(0, 2);

            if (!DICT_REMASTERED_BIG_FILE_NAMES.TryGetValue(instance.RemasteredGame, out bigFileName))
            {
                throw new Exceptions.InvalidVersionException("Only Homeworld Remastered games are currently supported.\n Cannot extract " + instance.Version + " .big file.");
            }

            if (!DICT_REMASTERED_BIG_FILE_SIZES.TryGetValue(instance.RemasteredGame, out extractedFileSize))
            {
                throw new Exceptions.InvalidVersionException("Only Homeworld Remastered games are currently supported.\n Cannot extract " + instance.Version + " .big file.");
            }

            AppendOutput(String.Format(TEXT_START_DIALOG_FORMAT, bigFileName, extractedFileSize, documentsDriveLetter, deleteFilesCheckBox.Text));
        }

        //----------------------------------------

        /// <summary>
        /// Appends a line of text to the output window.
        /// </summary>
        /// <param name="text">The text to append to the output window</param>
        private void AppendOutput(string text)
        {
            if (outputTextBox.InvokeRequired)
            {
                Action safeAction = delegate { AppendOutput(text); };
                outputTextBox.Invoke(safeAction);
            }
            else
            {
                outputTextBox.AppendText(String.Format(OUTPUT_FORMAT, text));
            }
        }

        //----------------------------------------

        /// <summary>
        /// Shows the user a dialog confirming they want to cancel the extraction process.
        /// Suspends all extraction process threads until user input is recieved.
        /// Cancles the extraction if the user confirms, resumes otherwise.
        /// </summary>
        private void ConfirmCancelExtraction()
        {
            extractor.SuspendExtraction();

            if (ShowExitConfirmationDialog(TEXT_CANCEL_EXTRACTING_MESSAGE, TEXT_CANCEL_EXTRACTING_TITLE))
            {
                extractor.CancelExtraction();
                IO.ExtractedDataManager.ClearOutputDir();

                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                extractor.ResumeExtraction();
            }
        }

        //--------------------

        /// <summary>
        /// Shows the user a dialog confirming they want to exit the form without extracting required files.
        /// </summary>
        /// <param name="message">The message to display on the dialog window</param>
        /// <param name="title">The title to display on the dialog window</param>
        /// <returns>True if the user wants to exit, false if not</returns>
        private bool ShowExitConfirmationDialog(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
    }
}
