using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLGenerator.Model;

namespace XMLGenerator.ViewModel
{
    public class ExportViewModel : ViewModelBase
    {
        private ObservableCollection<FolderViewModel> m_folderViewModels;
        
        private string m_value;
        public ExportViewModel()
        {
            FolderViewModels = new ObservableCollection<FolderViewModel> {new FolderViewModel()};
           
        } 

        public ObservableCollection<FolderViewModel> FolderViewModels { get { return m_folderViewModels; } set { m_folderViewModels = value; OnPropertyChanged("FolderViewModels"); } }
        
        public string Value { get { return m_value; } set { m_value = value; OnPropertyChanged("Value"); } }
    }
}
