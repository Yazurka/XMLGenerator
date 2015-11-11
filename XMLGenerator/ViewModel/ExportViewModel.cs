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
        private FolderViewModel m_folderViewModel;
        
        private string m_value;
        public ExportViewModel()
        {
            FolderViewModel = new FolderViewModel ();
            IsVisible = true;
           
        }
        private bool m_isVisible;
        public bool IsVisible { get { return m_isVisible; } set { m_isVisible = value; OnPropertyChanged("IsVisible"); } }

        public FolderViewModel FolderViewModel { get { return m_folderViewModel; } set { m_folderViewModel = value; OnPropertyChanged("FolderViewModel"); } }
        
        public string Value { get { return m_value; } set { m_value = value; OnPropertyChanged("Value"); } }
    }
}
