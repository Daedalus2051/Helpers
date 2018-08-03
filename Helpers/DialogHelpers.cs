using System;
using System.Windows.Forms;

namespace Helpers
{
    public class DialogHelpers
    {
        private string _dialogFilter;
        public string DialogFilter
        {
            get { return _dialogFilter; }
            set { _dialogFilter = value; }
        }

        private string _initalDirectory;
        public string InitialDirectory
        {
            get { return _initalDirectory; }
            set { _initalDirectory = value; }
        }

        public DialogHelpers()
        {
            _dialogFilter = "*.*";
            _initalDirectory = Environment.CurrentDirectory;
        }
        public DialogHelpers(string filter, string initialDir)
        {
            _dialogFilter = filter;
            _initalDirectory = initialDir;
        }

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
        /// Creates a folder selection dialog and displays it to the user.
        /// </summary>
        /// <returns>Full path to the folder the user has selected.</returns>
        public string InvokeFolderBrowseDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            //dialog.RootFolder = Environment.SpecialFolder.MyDocuments;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return "";
            }
        }
    }
}
