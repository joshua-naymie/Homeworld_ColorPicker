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

    enum ExtractorSate
    {
        DialogOpened,
        ExtractionInProgress,
        ExtractionFinished,
        ExtractionFailed,
        ExtractionCancelled,
        Cleanup
    }
    public partial class BigExtractorDialog : Form
    {
        private const
        string TEXT_START_DIALOG_FORMAT = "Required files need to be extracted from the {0}. This will take {1} of space on your C: drive.\r\n"
                                        + "All unnecessary files will be deleted after extraction unless \"Keep necessary files only\" is unchecked.\r\n\r\n"
                                        + "Press start to begin extracting...\r\n\r\n",

               OUTPUT_FORMAT = "{0}\r\n",
               TEXT_DONE = "Done",

               TEXT_INFO_DIALOG_TITLE = "Files not found!",
               TEXT_INFO_DIALOG_MESSAGE = "We couldn't find the required files!\n"
                                        + "They need to be extracted from the .big file to proceed.";

               

        private
        string homeworldRoot,
               toolkitRoot,
               extractPath = GC.DIR_DOCUMENTS_PATH + @"\output";

        private
        ExtractorSate currentState; 

        private
        IO.BigExtractor? extractor;
        
        public BigExtractorDialog(GameInstance instance)
        {
            this.Shown += ShowInfoDialog;
            InitializeComponent();

            this.homeworldRoot = instance.GetHomeworldRoot();
            this.toolkitRoot = instance.GetToolkitRoot();

            currentState = ExtractorSate.DialogOpened;

            AppendOutput(TEXT_START_DIALOG_FORMAT);
        }

        private void StartExtraction(object? sender, EventArgs e)
        {
            startDoneButton.Enabled = false;
            startDoneButton.Text = TEXT_DONE;

            cancelButton.Enabled = true;

            extractor = new IO.BigExtractor(homeworldRoot, toolkitRoot, extractPath, AppendOutput);


            Task t = new Task(extractor.ExtractBigFile);
            t.ContinueWith(MoveRequiredFiles);
            t.Start();
        }

        private void AppendOutput(string text)
        {
            if(outputTextBox.InvokeRequired)
            {
                Action safe = delegate { AppendOutput(text); };
                outputTextBox.Invoke(safe);
            }
            else
            {
                outputTextBox.AppendText(String.Format(OUTPUT_FORMAT, text));
            }
        }


        private void CancelPressed(object? sender, EventArgs e)
        {
            switch(currentState)
            {
                case ExtractorSate.DialogOpened:
                    this.DialogResult = DialogResult.Cancel;
                    break;

                case ExtractorSate.ExtractionInProgress:
                    extractor.SuspendExtraction();

                    if (ShowExitConfirmationDialog("Cancel?", "Confirm Exit"))
                    {
                        extractor.CancelExtraction();
                        DeleteExtractedData();
                        this.DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        extractor.ResumeExtraction();
                    }
                    break;
            }
        }

        private bool ShowExitConfirmationDialog(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private void Cleanup(object? sender, FormClosingEventArgs e)
        {
            extractor.KillProcess();
            DeleteExtractedData();
        }

        private void DeleteExtractedData()
        {
            DirectoryInfo outputDir = new DirectoryInfo(extractPath);

            
            foreach (DirectoryInfo directory in outputDir.GetDirectories())
            {
                directory.Delete(true);
            }
            foreach (FileInfo file in outputDir.GetFiles())
            {
                file.Delete();
            }
        }

        private void MoveRequiredFiles(Task ta)
        {
            string path = GC.DIR_DOCUMENTS_PATH + @"\HW2";

            foreach (string levelPath in GC.HW2_TEAMCOLOR_PATHS)
            {
                if (!Util.CheckPathExists(path + levelPath))
                {
                    System.IO.Directory.CreateDirectory(path + levelPath);
                }

                System.Diagnostics.Debug.WriteLine("\n" + extractPath + levelPath + "\n" + path + levelPath);
                System.Diagnostics.Debug.WriteLine(Util.CheckPathExists(extractPath + levelPath) + "," + !Util.CheckPathExists(path + levelPath) + "\n");

                File.Move(extractPath + levelPath + GC.FILE_TEAMCOLOUR_LUA, path + levelPath + GC.FILE_TEAMCOLOUR_LUA);
            }
        }

        private void MoveAllFiles()
        {

        }

        private void ShowInfoDialog(object? sender, EventArgs e)
        {
            MessageBox.Show(TEXT_INFO_DIALOG_MESSAGE, TEXT_INFO_DIALOG_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
