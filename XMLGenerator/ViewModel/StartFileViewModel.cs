using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using IfcConfigManager.Model;
using System.Windows.Forms;
using IfcConfigManager.Assets;

namespace IfcConfigManager.ViewModel
{
    public class StartFileViewModel : ViewModelBase
    {
        private StartFile m_startFile;
        private ICommand m_fileDialogCommand;
        private XmlViewModel m_xmlViewModel;
        public StartFileViewModel(XmlViewModel xmlViewModel)
        {
            m_xmlViewModel = xmlViewModel;
            StartFile = new StartFile { FromRestriction = m_xmlViewModel.BaseFolderViewModel.FromBasePath};
           FileDialogCommand = new DelegateCommand(FileDialogExecute);
        }

        private async void FileDialogExecute()
        {
            var x = await PathValidator.SelectFilePath(StartFile.FromRestriction);
            if (!string.IsNullOrEmpty(x))
            {
                StartFile.FromPath = x;
            }
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

