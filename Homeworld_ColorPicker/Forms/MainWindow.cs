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

    public partial class MainWindow : Form
    {
        /// <summary>
        /// The dialog used to set or change the directories for Homeworld root, Remastered Toolkit, and Profile.
        /// </summary>
        private DirectoryDialog directoryDialog = new DirectoryDialog();

        /// <summary>
        /// Constructor for MainWindow.
        /// </summary>
        public MainWindow()
        {            
            if(directoryDialog.ShowDialog() == DialogResult.Cancel)
            {
                Load += (s, e) => Close();
            }
            
            //----------

            InitializeComponent();
            System.Diagnostics.Debug.WriteLine(new HomeworldColor());
        }
    }
}