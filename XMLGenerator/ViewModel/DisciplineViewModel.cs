using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using XMLGenerator.Model;

namespace XMLGenerator.ViewModel
{
    public class DisciplineViewModel : ViewModelBase
    {
        
        private ICommand m_addExportCommand;
        private StartFileViewModel m_startFileViewModel;
        private ObservableCollection<ExportViewModel> m_exportViewModels; 

        private string m_value;
        public DisciplineViewModel()
        {
           AddExportCommand = new DelegateCommand(AddFolderExecute);
           StartFileViewModel = new StartFileViewModel();
           ExportViewModels = new ObservableCollection<ExportViewModel> {new ExportViewModel()};
           
        }
        public string Value { get { return m_value; } set { m_value = value; OnPropertyChanged("Value"); } }
        public StartFileViewModel StartFileViewModel { get { return m_startFileViewModel; } set { m_startFileViewModel = value; OnPropertyChanged("StartFile"); } }
        public ObservableCollection<ExportViewModel> ExportViewModels { get { return m_exportViewModels; } set { m_exportViewModels = value; OnPropertyChanged("ExportViewModels"); } }
        
        public ICommand AddExportCommand
        {
            get { return m_addExportCommand; }
            set
            {
                m_addExportCommand = value;
                OnPropertyChanged("AddExportCommand");
            }
        }

        private void AddFolderExecute()
        {
            ExportViewModels.Add(new ExportViewModel());
        }
    }
}
