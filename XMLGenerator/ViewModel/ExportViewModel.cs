using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using XMLGenerator.Model;

namespace XMLGenerator.ViewModel
{
    public class ExportViewModel : ViewModelBase
    {
        private FolderViewModel m_folderViewModel;
        private string m_value;
        private ICommand m_removeExportCommand;
        private bool m_isVisible;
        public ExportViewModel()
        {
            FolderViewModel = new FolderViewModel ();
            RemoveExportCommand = new DelegateCommand(RemoveExportExecute);
            IsVisible = true;
            
        }
        private void RemoveExportExecute()
        {
            IsVisible = false;
        }

        public bool IsVisible { get { return m_isVisible; } set { m_isVisible = value; OnPropertyChanged("IsVisible"); } }
        public ICommand RemoveExportCommand { get { return m_removeExportCommand; } set { m_removeExportCommand = value; OnPropertyChanged("RemoveExportCommand"); } }
        public FolderViewModel FolderViewModel { get { return m_folderViewModel; } set { m_folderViewModel = value; OnPropertyChanged("FolderViewModel"); } }
        
        public string Value { get { return m_value; } set { m_value = value; OnPropertyChanged("Value"); } }
    }
}
