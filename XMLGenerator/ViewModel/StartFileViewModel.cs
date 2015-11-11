using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using XMLGenerator.Model;
using System.Windows.Forms;

namespace XMLGenerator.ViewModel
{
    public class StartFileViewModel : ViewModelBase
    {
        private StartFile m_startFile;
        private ICommand m_fileDialogCommand;
        public StartFileViewModel()
        {
           StartFile = new StartFile();
           FileDialogCommand = new DelegateCommand(FileDialogExecute);
        }

        private void FileDialogExecute()
        {
            FolderBrowserDialog p = new FolderBrowserDialog();
            p.ShowDialog();
            StartFile.FromPath = p.SelectedPath;
        }

        public ICommand FileDialogCommand { get { return m_fileDialogCommand; } set { m_fileDialogCommand = value; OnPropertyChanged("FileDialogCommand"); } }

        public StartFile StartFile
        {
            get { return m_startFile; }
            set
            {
                m_startFile = value; OnPropertyChanged("StartFile");
            }
        }
      
       
    }
}

