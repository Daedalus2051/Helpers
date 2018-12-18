using System;
using System.Windows.Forms;

namespace Helpers
{
    /// <summary>
    /// A class to encapsulate various dialogs used for opening and saving files, as well as selecting directories.
    /// </summary>
    public class DialogHelpers
    {
        #region Properties
        private string _dialogFilter;
        /// <summary>
        /// Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.
        /// </summary>
        public string DialogFilter
        {
            get { return _dialogFilter; }
            set { _dialogFilter = value; }
        }

        private string _initalDirectory;
        /// <summary>
        /// Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        public string InitialDirectory
        {
            get { return _initalDirectory; }
            set { _initalDirectory = value; }
        }

        private string _browsePath;
        /// <summary>
        /// Gets or sets the selected path of the Folder Browse dialog.
        /// </summary>
        public string BrowsePath
        {
            get { return _browsePath; }
            set { _browsePath = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the class with default values (all files, current directory).
        /// </summary>
        public DialogHelpers()
        {
            _dialogFilter = "*.*";
            _initalDirectory = Environment.CurrentDirectory;
        }

        /// <summary>
        /// Creates an instance of the class using the parameters specified.
        /// </summary>
        /// <param name="filter">Sets the filter to look for files of the specified pattern.</param>
        /// <param name="initialDir">Sets the initial directory that the dialog should start from.</param>
        public DialogHelpers(string filter, string initialDir)
        {
            _dialogFilter = filter;
            _initalDirectory = initialDir;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an OpenFile dialog and displays it to the user to choose a file.
        /// </summary>
        /// <returns>Full file path for the chosen file.</returns>
        public string InvokeOpenFileDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = _dialogFilter;
            dialog.InitialDirectory = _initalDirectory;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Creates an SaveFile dialog and displays it to the user to name a file.
        /// </summary>
        /// <returns>Full file path for the chosen file.</returns>
        public string InvokeSaveFileDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = _dialogFilter;
            dialog.InitialDirectory = _initalDirectory;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Creates a folder selection dialog and displays it to the user (ignores the InitialDirectory property).
        /// </summary>
        /// <returns>Full path to the folder the user has selected.</returns>
        public string InvokeFolderBrowseDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (System.IO.Directory.Exists(_browsePath))
            {
                dialog.SelectedPath = _browsePath;
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
